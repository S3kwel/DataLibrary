using DATA.Repository.Implementation;

namespace DATA.Repository.Abstraction.Strategies
{
    public interface IQueryStrategy<T, TKey> where T : BaseEntity<TKey>
    {
        HistoricFetchMode FetchMode { get; }
        IQueryable<T> Apply(IQueryable<T> query, Filter<T, TKey> filter);
    }



}
