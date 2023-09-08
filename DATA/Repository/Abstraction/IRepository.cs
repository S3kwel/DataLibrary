using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Models;

namespace DATA.Repository.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        SimpleResult<T> Add(T entity);
        SimpleResult<T> Add(List<T> entities);
        Task<SimpleResult<T>> AddAsync(T entity);
        Task<SimpleResult<T>> AddAsync(List<T> entities);
        Result<BaseFilter<T>, T> ApplyFilter(BaseFilter<T> filter);
        Task<Result<BaseFilter<T>, T>> ApplyFilterAsync(BaseFilter<T> filter);
        SimpleResult<T> Delete(T entity);
        SimpleResult<T> Delete(List<T> entities);
        Result<BaseFilter<T>, T> Process(BaseFilter<T> filter);
        Task<Result<BaseFilter<T>, T>> ProcessAsync(BaseFilter<T> filter);
        SimpleResult<T> Update(T entity);
        SimpleResult<T> Update(List<T> entities);
    }
}