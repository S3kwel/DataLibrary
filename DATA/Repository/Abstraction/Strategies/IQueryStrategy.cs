using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Implementation;

namespace DATA.Repository.Abstraction.Strategies
{
    public interface IQueryStrategy<T> where T : HistoricEntity
    {
        HistoricFetchMode FetchMode { get; }
        IQueryable<T> Apply(IQueryable<T> query, HistoricFilter<T> filter);
    }



}
