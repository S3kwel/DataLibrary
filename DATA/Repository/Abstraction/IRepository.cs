using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging.Interfaces;
using DATA.Repository.Implementation.PrimaryKey;

namespace DATA.Repository.Abstraction
{
    public interface IRepository<T, TKey> where T : BaseEntity
    {
        Action<IQueryable<T?>, Filter<T>, DebugContext>? AfterProcessing { get; set; }
        Action<IQueryable<T?>, Filter<T>, DebugContext>? BeforeProcessing { get; set; }
        bool UseDefaultDebugging { get; set; }

        Result<Filter<T>, T> GetByKey(IPrimaryKey key);
        Result<Filter<T>, T> HistoricActiveBetween(DateTime startDate, DateTime endDate, Filter<T>? filter);
        Result<Filter<T>, T> HistoricActiveThrough(DateTime startDate, DateTime endDate, Filter<T>? filter);
        Result<Filter<T>, T> HistoricActiveWithin(DateTime validFrom, DateTime validTo, Filter<T>? filter = null);
        Result<Filter<T>, T> HistoricAllTime(Filter<T>? filter = null);
        Result<Filter<T>, T> HistoricAtExactTime(DateTime exactTime, Filter<T>? filter);
    }
}