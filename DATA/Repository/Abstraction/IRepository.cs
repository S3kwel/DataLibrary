using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Models;

namespace DATA.Repository.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        SimpleResult<T> Add(T entity);
        Result<BaseFilter<T>, T> ApplyFilter(BaseFilter<T> filter);
        SimpleResult<T> Delete(T entity);
        SimpleResult<T> Update(T entity);
    }
}