using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation.Debugging;
using DATA.Repository.Implementation.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DATA.Repository.Implementation
{
    public class Repository<T> : DebuggableRepository<T>, IRepository<T> where T : class, IBaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly IDebugStrategy<T> _debugStrategy;


        public Repository(DbContext context,
                          IDebugStrategy<T> debugStrategy)
        {
            _dbContext = context;
            _debugStrategy = debugStrategy ?? new DefaultDebugStrategy<T>();
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
        public Result<BaseFilter<T>, T> Process(BaseFilter<T> filter)
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

            return new Result<BaseFilter<T>, T>(filter, status, (final.Count > 0) ? null : convertResults)
            {
                Pagination = pagination,
                Status = status,
                Values = convertResults
            };
        }
        public async Task<Result<BaseFilter<T>, T>> ProcessAsync(BaseFilter<T> filter)
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

            return new Result<BaseFilter<T>, T>(filter, status, (final.Count > 0) ? null : convertResults)
            {
                Pagination = pagination,
                Status = status,
                Values = convertResults
            };
        }

        public Result<BaseFilter<T>, T> ApplyFilter(BaseFilter<T> filter)
        {
            return Process(filter);
        }
        public async Task<Result<BaseFilter<T>, T>> ApplyFilterAsync(BaseFilter<T> filter)
        {
            return await ProcessAsync(filter);
        }

        public SimpleResult<T> Update(T entity)
        {
            if (entity == null)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entity to be updated is null."
                };
            }

            try
            {
                _dbContext.Update(entity);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entity successfully updated."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to update entity. Error: {ex.Message}"
                };
            }
        }
        public SimpleResult<T> Update(List<T> entities)
        {
            if (entities == null || entities.Count <= 0)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entities to be updated are null."
                };
            }

            try
            {
                _dbContext.UpdateRange(entities);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entity successfully updated."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to add entity. Error: {ex.Message}"
                };
            }
        }
        public SimpleResult<T> Add(T entity)
        {
            if (entity == null)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entity to be added is null."
                };
            }

            try
            {
                _dbContext.Add(entity);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entity successfully added."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to add entity. Error: {ex.Message}"
                };
            }
        }
        public SimpleResult<T> Add(List<T> entities)
        {
            if (entities == null || entities.Count <= 0)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entities to be added are null."
                };
            }

            try
            {
                _dbContext.AddRange(entities);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entity successfully added."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to add entity. Error: {ex.Message}"
                };
            }
        }
        public async Task<SimpleResult<T>> AddAsync(T entity)
        {
            if (entity == null)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entity to be added is null."
                };
            }

            try
            {
                await _dbContext.AddAsync(entity);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entity successfully added."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to add entity. Error: {ex.Message}"
                };
            }
        }
        public async Task<SimpleResult<T>> AddAsync(List<T> entities)
        {
            if (entities == null || entities.Count <= 0)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entities to be added are null."
                };
            }

            try
            {
                await _dbContext.AddRangeAsync(entities);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entity successfully added."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to add entity. Error: {ex.Message}"
                };
            }
        }
        public SimpleResult<T> Delete(T entity)
        {
            if (entity == null)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entity to be deleted is null."
                };
            }

            try
            {
                _dbContext.Remove(entity);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entity successfully deleted."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to delete entity. Error: {ex.Message}"
                };
            }
        }
        public SimpleResult<T> Delete(List<T> entities)
        {
            if (entities == null || entities.Count <= 0)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = "The entities to be deleted are null."
                };
            }

            try
            {
                _dbContext.RemoveRange(entities);
                return new SimpleResult<T>
                {
                    Status = RequestStatus.SUCCEEDED,
                    Message = "Entities successfully deleted."
                };
            }
            catch (Exception ex)
            {
                return new SimpleResult<T>
                {
                    Status = RequestStatus.FAILED,
                    Message = $"Failed to delete entities. Error: {ex.Message}"
                };
            }
        }
    }
}