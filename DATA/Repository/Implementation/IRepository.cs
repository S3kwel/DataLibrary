using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Debugging.Interfaces;
using DATA.Repository.Implementation.PrimaryKey;

namespace DATA.Repository.Implementation
{
    public interface IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        Action<IQueryable<T?>, Filter<T, TKey>, DebugContext>? AfterProcessing { get; set; }
        Action<IQueryable<T?>, Filter<T, TKey>, DebugContext>? BeforeProcessing { get; set; }
        int Count { get; set; }
        bool UseDefaultDebugging { get; set; }

        Result<Filter<T, TKey>, T, TKey> GetById(TKey primaryKey);
        Result<Filter<T, TKey>, T, TKey> GetByKey(IPrimaryKey key);
        Result<Filter<T, TKey>, T, TKey> HistoricActiveBetween(DateTime startDate, DateTime endDate, Filter<T, TKey>? filter);
        Result<Filter<T, TKey>, T, TKey> HistoricActiveThrough(DateTime startDate, DateTime endDate, Filter<T, TKey>? filter);
        Result<Filter<T, TKey>, T, TKey> HistoricActiveWithin(DateTime validFrom, DateTime validTo, Filter<T, TKey>? filter = null);
        Result<Filter<T, TKey>, T, TKey> HistoricAllTime(Filter<T, TKey>? filter = null);
        Result<Filter<T, TKey>, T, TKey> HistoricAtExactTime(DateTime exactTime, Filter<T, TKey>? filter);
        void Insert(T entity);
    }
}