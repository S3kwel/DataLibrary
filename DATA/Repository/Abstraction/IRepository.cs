using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging.Interfaces;
using DATA.Repository.Implementation.PrimaryKey;

namespace DATA.Repository.Abstraction
{
    public interface IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Action<IQueryable<T?>, Filter<T, TKey>, DebugContext>? AfterProcessing { get; set; }
        Action<IQueryable<T?>, Filter<T, TKey>, DebugContext>? BeforeProcessing { get; set; }
        int Count { get; set; }
        bool UseDefaultDebugging { get; set; }
        Result<Filter<T, TKey>, T, TKey> GetById(TKey primaryKey);
        Result<Filter<T, TKey>, T, TKey> GetByKey(IPrimaryKey key);
        void Insert(T entity);
    }
}