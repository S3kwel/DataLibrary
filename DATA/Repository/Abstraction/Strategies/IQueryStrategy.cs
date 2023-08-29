using DATA.Repository.Implementation;

namespace DATA.Repository.Abstraction.Strategies
{
    public interface IQueryStrategy<T> where T : BaseEntity
    {
        HistoricFetchMode FetchMode { get; }
        IQueryable<T> Apply(IQueryable<T> query, Filter<T> filter);
    }



}
