using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;
using DATA.Repository.Implementation.Debugging.Implementations;
using DATA.Repository.Implementation.Debugging.Interfaces;
using DATA.Repository.Implementation.Pagination;
using DATA.Repository.Implementation.PrimaryKey;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly IEnumerable<IQueryStrategy<T>> _queryStrategies;
        private readonly IDebugStrategy<T> _debugStrategy;
        private readonly IPrimaryKey _primaryKey;
        public bool UseDefaultDebugging { get; set; } = true;
        public Action<IQueryable<T?>, Filter<T>, DebugContext>? BeforeProcessing { get; set; }
        public Action<IQueryable<T?>, Filter<T>, DebugContext>? AfterProcessing { get; set; }

        public Repository(DbContext context,
                          IEnumerable<IQueryStrategy<T>> strategies,
                          IDebugStrategy<T> debugStrategy,
                          IPrimaryKey primaryKey)
        {
            _dbContext = context;
            _queryStrategies = strategies;
            _debugStrategy = debugStrategy ?? new DefaultDebugStrategy<T>();
            _primaryKey = primaryKey;
        }

        private IQueryStrategy<T>? GetStrategyForFetchMode(HistoricFetchMode mode)
        {

            return _queryStrategies.FirstOrDefault(s => s.FetchMode == mode);
        }


        private Filter<T> PrepareFilter(HistoricFetchMode fetchMode, DateTime? validFrom = null, DateTime? validTo = null, Filter<T>? filter = null)
        {
            filter ??= new Filter<T>().WithFetchMode(fetchMode);

            if (validFrom.HasValue)
                filter.WithValidFrom(validFrom.Value);

            if (validTo.HasValue)
                filter.WithValidTo(validTo.Value);

            return filter;
        }

        internal RequestStatus DetermineRequestStatus(IList<T> results, Filter<T> filter)
        {
            bool hasResults = results.Count > 0;
            bool isFilterValid = filter.IsValid();

            if (hasResults && !isFilterValid)
            {
                return RequestStatus.SUCCEEDED_WITH_ERRORS;
            }

            if (hasResults && isFilterValid)
            {
                return RequestStatus.SUCCEEDED;
            }

            if (!hasResults && isFilterValid)
            {
                return RequestStatus.SUCCEEDED_BUT_EMPTY;
            }

            return RequestStatus.FAILED; // default
        }


        private Result<Filter<T>, T> Process(Filter<T> filter)
        {

            if (filter.PageNumber < 1)
            {
                filter.PageNumber = 1; // default to the first page
            }

            // Ensure PageSize is within a reasonable range
            const int maxPageSize = 100;  // set a maximum limit for page size
            if (filter.PageSize < 1)
            {
                filter.PageSize = 10; // default page size
            }
            else if (filter.PageSize > maxPageSize)
            {
                filter.PageSize = maxPageSize; // cap at maximum
            }

            IQueryable<T> query = _dbContext.Set<T>();


            // status = RequestStatus.NEW;
            var debugContext = new DebugContext();

            //Return failure with no results if we're using a fetch mode we don't have a strategy for.  
            if (GetStrategyForFetchMode(filter.FetchMode) == null)
            {
                return new Result<Filter<T>, T>(filter, RequestStatus.FAILED, null);
            }

            //Pre-processing
            //TODO?  Make these return results too.  Would allow for more detailed status reporting.  



            _debugStrategy.BeforeHook(query, filter, debugContext, "PreProcessing");
            query = QueryableExtensions<T>.DisableQueryFilters(query, filter);
            query = QueryableExtensions<T>.ApplyEagerLoading(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "PreProcessing");


            //Apply historic fetch mode strategy.  
            //Null-forgiving because we've made this check already.

            _debugStrategy.BeforeHook(query, filter, debugContext, "HistoricFetching");
            query = GetStrategyForFetchMode(filter.FetchMode)!.Apply(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "HistoricFetching");

            //Post-Processing
            _debugStrategy.BeforeHook(query, filter, debugContext, "PostProcessing");
            query = QueryableExtensions<T>.ApplyFilteringLogic(query, filter);
            query = QueryableExtensions<T>.ApplyOrderingLogic(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "PostProcessing");
            query = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);


            //At this point, all filter conditions should be fulfilled.
            //Calling ToList here should be fine.  

            int totalCount = query.Count();



            int totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);

            if (totalPages == 0)
                filter.PageNumber = 1;  // or whatever default or fallback you prefer
            if (filter.PageNumber > totalPages)
                filter.PageNumber = totalPages;
            

            var results = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();



           
            var status = DetermineRequestStatus(results, filter);

            bool hasResults = results.Count > 0;
            //Results with an invalid filter leads to succeded with errors.  

            var convertResults = query.Select(r => new SingleResult<Filter<T>, T>(r, filter, status)).ToList();




            //If there are no objects to return, return a null on results.  
            if (hasResults)
            {
                return new Result<Filter<T>, T>(filter, status, convertResults)
                {
                    Pagination = new PaginationMetadata
                    {
                        CurrentPage = filter.PageNumber,
                        PageSize = filter.PageSize,
                        TotalCount = totalCount
                    }
                };
            }
            else
            {
                return new Result<Filter<T>, T>(filter, status, null)
                {
                    Pagination = new PaginationMetadata
                    {
                        CurrentPage = 1,  // Always set to the first page if no results
                        PageSize = filter.PageSize,
                        TotalCount = 0
                    }
                };
            }

        }
        public Result<Filter<T>, T> HistoricAllTime(Filter<T>? filter = null)
        {
            filter = PrepareFilter(HistoricFetchMode.AllTime, null, null, filter);
            return Process(filter);
        }

        public Result<Filter<T>, T> HistoricActiveWithin(DateTime validFrom, DateTime validTo, Filter<T>? filter = null)
        {
            filter = PrepareFilter(HistoricFetchMode.ActiveWithin, validFrom, validTo, filter);
            return Process(filter);
        }
        public Result<Filter<T>, T> HistoricAtExactTime(DateTime exactTime, Filter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.AtExactTime, exactTime, null, filter);
            return Process(filter);
        }
        public Result<Filter<T>, T> HistoricActiveBetween(DateTime startDate, DateTime endDate, Filter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.ActiveBetween, startDate, endDate, filter);
            return Process(filter);
        }
        public Result<Filter<T>, T> HistoricActiveThrough(DateTime startDate, DateTime endDate, Filter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.ActiveThrough, startDate, endDate, filter);
            return Process(filter);
        }


        public Result<Filter<T>, T> GetById(TKey primaryKey)
        {
            // You can adjust the predicate here based on TKey.
            var entity = _dbContext.Set<T>().Find(primaryKey);

            // Convert to Result object and return.
            // This is a simplified example. Adjust as needed.
            return new Result<Filter<T>, T>(new Filter<T>(), RequestStatus.SUCCEEDED, new List<SingleResult<Filter<T>, T>> { new SingleResult<Filter<T>, T>(entity) });
        }
        public Result<Filter<T>, T> GetByKey(IPrimaryKey key)
        {
            var values = key.GetKeyValues();
            var keyProperties = key.GetKeyProperties();
            var filterGroup = new List<FilterGroup<T>>();

            if (keyProperties.Length != values.Length)
                throw new ArgumentException("Number of key properties doesn't match the number of values.");

            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];
                var property = keyProperties[i];
                var parameter = Expression.Parameter(typeof(T), "e");
                var propertyExp = Expression.Property(parameter, property);
                var valueExp = Expression.Constant(value);
                var equalsExp = Expression.Equal(propertyExp, valueExp);
                var lambda = Expression.Lambda<Func<T, bool>>(equalsExp, parameter);

                filterGroup.Add(new FilterGroup<T>
                {
                    Conditions = new List<FilterCondition<T>>
            {
                new FilterCondition<T>
                {
                    Condition = lambda
                }
            }
                });
            }

            var filter = new Filter<T>
            {
                LogicGroups = filterGroup
            };

            return Process(filter);
        }


    }
}