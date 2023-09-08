using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Debugging;
using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Strategies;
using DATA.Repository.Implementation.Debugging;
using DATA.Repository.Implementation.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DATA.Repository.Implementation
{
    public class HistoricRepository<T> : DebuggableRepository<T>, IHistoricRepository<T> where T : HistoricEntity
    {
        private readonly DbContext _dbContext;
        private readonly IEnumerable<IQueryStrategy<T>> _queryStrategies;
        private readonly IDebugStrategy<T> _debugStrategy;

        public HistoricRepository(DbContext context,
                          IEnumerable<IQueryStrategy<T>> strategies,
                          IDebugStrategy<T> debugStrategy)
        {
            _dbContext = context;
            _queryStrategies = strategies;
            _debugStrategy = debugStrategy ?? new DefaultDebugStrategy<T>();
        }


        private IQueryStrategy<T>? GetStrategyForFetchMode(HistoricFetchMode mode)
        {

            return _queryStrategies.FirstOrDefault(s => s.FetchMode == mode);
        }


        private HistoricFilter<T> PrepareFilter(HistoricFetchMode fetchMode, DateTime? validFrom = null, DateTime? validTo = null, HistoricFilter<T>? filter = null)
        {
            filter ??= new HistoricFilter<T>().WithFetchMode(fetchMode);

            if (validFrom.HasValue)
                filter.WithValidFrom(validFrom.Value);

            if (validTo.HasValue)
                filter.WithValidTo(validTo.Value);

            return filter;
        }

        internal RequestStatus DetermineRequestStatus(IList<T> results, BaseFilter<T> filter)
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

        //TODO:  Consider whether duplication is more of a problem than maintaining flexibility in the approaches these methods use.  
        public Result<HistoricFilter<T>, T> Process(HistoricFilter<T> filter)
        {
            //For debug hooks, etc. 
            var debugContext = new DebugContext();

            //Set the IQueryable.  
            IQueryable<T> query = _dbContext.Set<T>();

            // Pagination defaults. 
            const int maxPageSize = 100;
            filter.PageNumber = Math.Max(1, filter.PageNumber);
            filter.PageSize = Math.Clamp(filter.PageSize, 1, maxPageSize);

            // Pre-processing
            _debugStrategy.BeforeHook(query, filter, debugContext, "PreProcessing");
            query = QueryableExtensions<T>.DisableQueryFilters(query, filter);
            query = QueryableExtensions<T>.ApplyEagerLoading(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "PreProcessing");

            // Apply historic fetch mode strategy.  
            _debugStrategy.BeforeHook(query, filter, debugContext, "HistoricFetching");
            query = GetStrategyForFetchMode(filter.FetchMode)!.Apply(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "HistoricFetching");

            // Post-Processing
            _debugStrategy.BeforeHook(query, filter, debugContext, "PostProcessing");
            query = QueryableExtensions<T>.ApplyFilteringLogic(query, filter);
            query = QueryableExtensions<T>.ApplyOrderingLogic(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "PostProcessing");

            // Get the total count before pagination
            int totalCount = query.Count();

            // Paginate. 
            query = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

            // Finalize
            var final = query.ToList();

            // Pagination checks. 
            int totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
            filter.PageNumber = (totalPages == 0) ? 1 : Math.Min(filter.PageNumber, totalPages);
            var convertResults = final.Select(r => new SingleResult<T>(r, filter, RequestStatus.SUCCEEDED)).ToList();
            var status = DetermineRequestStatus(final, filter);

            var pagination = new PaginationMetadata
            {
                CurrentPage = (final.Count > 0) ? filter.PageNumber : 1,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };

            return new Result<HistoricFilter<T>, T>(filter, status, (final.Count > 0) ? null : convertResults)
            {
                Pagination = pagination,
                Status = status,
                Values = convertResults
            };
        }
        public async Task<Result<HistoricFilter<T>, T>> ProcessAsync(HistoricFilter<T> filter)
        {
            //For debug hooks, etc. 
            var debugContext = new DebugContext();

            //Set the IQueryable.  
            IQueryable<T> query = _dbContext.Set<T>();

            // Pagination defaults. 
            const int maxPageSize = 100;
            filter.PageNumber = Math.Max(1, filter.PageNumber);
            filter.PageSize = Math.Clamp(filter.PageSize, 1, maxPageSize);

            // Pre-processing
            _debugStrategy.BeforeHook(query, filter, debugContext, "PreProcessing");
            query = QueryableExtensions<T>.DisableQueryFilters(query, filter);
            query = QueryableExtensions<T>.ApplyEagerLoading(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "PreProcessing");

            // Apply historic fetch mode strategy.  
            _debugStrategy.BeforeHook(query, filter, debugContext, "HistoricFetching");
            query = GetStrategyForFetchMode(filter.FetchMode)!.Apply(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "HistoricFetching");

            // Post-Processing
            _debugStrategy.BeforeHook(query, filter, debugContext, "PostProcessing");
            query = QueryableExtensions<T>.ApplyFilteringLogic(query, filter);
            query = QueryableExtensions<T>.ApplyOrderingLogic(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "PostProcessing");

            // Get the total count before pagination
            int totalCount = query.Count();

            // Paginate. 
            query = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

            // Finalize
            var final = await query.ToListAsync();

            // Pagination checks. 
            int totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
            filter.PageNumber = (totalPages == 0) ? 1 : Math.Min(filter.PageNumber, totalPages);
            var convertResults = final.Select(r => new SingleResult<T>(r, filter, RequestStatus.SUCCEEDED)).ToList();
            var status = DetermineRequestStatus(final, filter);

            var pagination = new PaginationMetadata
            {
                CurrentPage = (final.Count > 0) ? filter.PageNumber : 1,
                PageSize = filter.PageSize,
                TotalCount = totalCount
            };

            return new Result<HistoricFilter<T>, T>(filter, status, (final.Count > 0) ? null : convertResults)
            {
                Pagination = pagination,
                Status = status,
                Values = convertResults
            };
        }


        public Result<HistoricFilter<T>, T> HistoricAllTime(HistoricFilter<T>? filter = null)
        {
            filter = PrepareFilter(HistoricFetchMode.AllTime, null, null, filter);
            return Process(filter);
        }
        public async Task<Result<HistoricFilter<T>, T>> HistoricAllTimeAsync(HistoricFilter<T>? filter = null)
        {
            filter = PrepareFilter(HistoricFetchMode.AllTime, null, null, filter);
            return await ProcessAsync(filter);
        }

        public Result<HistoricFilter<T>, T> HistoricActiveWithin(DateTime validFrom, DateTime validTo, HistoricFilter<T>? filter = null)
        {
            filter = PrepareFilter(HistoricFetchMode.ActiveWithin, validFrom, validTo, filter);
            return Process(filter);
        }
        public async Task<Result<HistoricFilter<T>, T>> HistoricActiveWithinAsync(DateTime validFrom, DateTime validTo, HistoricFilter<T>? filter = null)
        {
            filter = PrepareFilter(HistoricFetchMode.ActiveWithin, validFrom, validTo, filter);
            return await ProcessAsync(filter);
        }

      
        public Result<HistoricFilter<T>, T> HistoricAtExactTime(DateTime exactTime, HistoricFilter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.AtExactTime, exactTime, null, filter);
            return Process(filter);
        }
        public async Task<Result<HistoricFilter<T>, T>> HistoricAtExactTimeAsync(DateTime exactTime, HistoricFilter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.AtExactTime, exactTime, null, filter);
            return await ProcessAsync(filter);
        }

        public Result<HistoricFilter<T>, T> HistoricActiveBetween(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.ActiveBetween, startDate, endDate, filter);
            return Process(filter);
        }
        public async Task<Result<HistoricFilter<T>, T>> HistoricActiveBetweenAsync(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.ActiveBetween, startDate, endDate, filter);
            return await ProcessAsync(filter);
        }

        public Result<HistoricFilter<T>, T> HistoricActiveThrough(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.AllTime, startDate, endDate, filter);
            return Process(filter);
        }
        public async Task<Result<HistoricFilter<T>, T>> HistoricActiveThroughAsync(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter)
        {
            filter = PrepareFilter(HistoricFetchMode.AllTime, startDate, endDate, filter);
            return await ProcessAsync(filter);
        }


        public Result<HistoricFilter<T>, T> ApplyFilter(HistoricFilter<T> filter)
        {
            filter = PrepareFilter(filter.FetchMode, filter.ValidFrom, filter.ValidTo);
            return Process(filter);
        }
        public async Task<Result<HistoricFilter<T>, T>> ApplyFilterAsync(HistoricFilter<T> filter)
        {
            filter = PrepareFilter(filter.FetchMode, filter.ValidFrom, filter.ValidTo);
            return await ProcessAsync(filter);
        }

    }
}