using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging;

namespace DATA.Repository.Abstraction
{
    public interface IHistoricRepository<T> where T : HistoricEntity
    {
        Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? AfterProcessing { get; set; }
        Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? BeforeProcessing { get; set; }
        bool UseDefaultDebugging { get; set; }

        Result<HistoricFilter<T>, T> ApplyFilter(HistoricFilter<T> filter);
        Result<HistoricFilter<T>, T> HistoricActiveBetween(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter);
        Result<HistoricFilter<T>, T> HistoricActiveThrough(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter);
        Result<HistoricFilter<T>, T> HistoricActiveWithin(DateTime validFrom, DateTime validTo, HistoricFilter<T>? filter = null);
        Result<HistoricFilter<T>, T> HistoricAllTime(HistoricFilter<T>? filter = null);
        Result<HistoricFilter<T>, T> HistoricAtExactTime(DateTime exactTime, HistoricFilter<T>? filter);
    }
}