﻿using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;
using DATA.Repository.Implementation.Debugging.Implementations;
using DATA.Repository.Implementation.Debugging.Interfaces;
using DATA.Repository.Implementation.Pagination;
using DATA.Repository.Implementation.PrimaryKey;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly IDebugStrategy<T, TKey> _debugStrategy;
        public bool UseDefaultDebugging { get; set; } = true;
        public int Count { get; set; } = -1;
        public Action<IQueryable<T?>, Filter<T, TKey>, DebugContext>? BeforeProcessing { get; set; }
        public Action<IQueryable<T?>, Filter<T, TKey>, DebugContext>? AfterProcessing { get; set; }


        public Repository(DbContext context, IDebugStrategy<T, TKey> debugStrategy)
        {
            _dbContext = context;
            _debugStrategy = debugStrategy ?? new DefaultDebugStrategy<T, TKey>();
            Count = context.Set<T>().Count();
        }
        internal RequestStatus DetermineRequestStatus(IList<T> results, Filter<T, TKey> filter)
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


        private Result<Filter<T, TKey>, T, TKey> Process(Filter<T, TKey> filter)
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

            
            //Pre-processing
            _debugStrategy.BeforeHook(query, filter, debugContext, "PreProcessing");
            query = QueryableExtensions<T, TKey>.DisableQueryFilters(query, filter);
            query = QueryableExtensions<T, TKey>.ApplyEagerLoading(query, filter);
            _debugStrategy.AfterHook(query, filter, debugContext, "PreProcessing");

            //Post-Processing
            _debugStrategy.BeforeHook(query, filter, debugContext, "PostProcessing");
            query = QueryableExtensions<T, TKey>.ApplyFilteringLogic(query, filter);
            query = QueryableExtensions<T, TKey>.ApplyOrderingLogic(query, filter);
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

            var convertResults = query.Select(r => new SingleResult<Filter<T, TKey>, T, TKey>(r, filter, status)).ToList();




            //If there are no objects to return, return a null on results.  
            if (hasResults)
            {
                return new Result<Filter<T, TKey>, T, TKey>(filter, status, convertResults)
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
                return new Result<Filter<T, TKey>, T, TKey>(filter, status, null)
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


        public Result<Filter<T, TKey>, T, TKey> GetById(TKey primaryKey)
        {
            // You can adjust the predicate here based on TKey.
            var entity = _dbContext.Set<T>().Find(primaryKey);

            // Convert to Result object and return.
            // This is a simplified example. Adjust as needed.
            return new Result<Filter<T, TKey>, T, TKey>(new Filter<T, TKey>(), RequestStatus.SUCCEEDED, new List<SingleResult<Filter<T, TKey>, T, TKey>> { new SingleResult<Filter<T, TKey>, T, TKey>(entity) });
        }
        public Result<Filter<T, TKey>, T, TKey> GetByKey(IPrimaryKey key)
        {
            var values = key.GetKeyValues();
            var keyProperties = key.GetKeyProperties();
            var filterGroup = new List<FilterGroup<T, TKey>>();

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

                filterGroup.Add(new FilterGroup<T, TKey>
                {
                    Conditions = new List<FilterCondition<T, TKey>>
            {
                new FilterCondition< T, TKey >
                {
                    Condition = lambda
                }
            }
                });
            }

            var filter = new Filter<T, TKey>
            {
                LogicGroups = filterGroup
            };

            return Process(filter);
        }

        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);   
        }
    }
}