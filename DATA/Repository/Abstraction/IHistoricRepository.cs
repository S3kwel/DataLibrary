using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging;

namespace DATA.Repository.Abstraction
{
    public interface IHistoricRepository<T> where T : HistoricEntity
    {
        Result<HistoricFilter<T>, T> ApplyFilter(HistoricFilter<T> filter);
        Task<Result<HistoricFilter<T>, T>> ApplyFilterAsync(HistoricFilter<T> filter);
        Result<HistoricFilter<T>, T> HistoricActiveBetween(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter);
        Result<HistoricFilter<T>, T> HistoricActiveThrough(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter);
        Task<Result<HistoricFilter<T>, T>> HistoricActiveThroughAsync(DateTime startDate, DateTime endDate, HistoricFilter<T>? filter);
        Result<HistoricFilter<T>, T> HistoricActiveWithin(DateTime validFrom, DateTime validTo, HistoricFilter<T>? filter = null);
        Task<Result<HistoricFilter<T>, T>> HistoricActiveWithinAsync(DateTime validFrom, DateTime validTo, HistoricFilter<T>? filter = null);
        Result<HistoricFilter<T>, T> HistoricAllTime(HistoricFilter<T>? filter = null);
        Task<Result<HistoricFilter<T>, T>> HistoricAllTimeAsync(HistoricFilter<T>? filter = null);
        Result<HistoricFilter<T>, T> HistoricAtExactTime(DateTime exactTime, HistoricFilter<T>? filter);
        Task<Result<HistoricFilter<T>, T>> HistoricAtExactTimeAsync(DateTime exactTime, HistoricFilter<T>? filter);
        Result<HistoricFilter<T>, T> Process(HistoricFilter<T> filter);
        Task<Result<HistoricFilter<T>, T>> ProcessAsync(HistoricFilter<T> filter);
    }
}