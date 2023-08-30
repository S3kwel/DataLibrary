using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class AllTimeStrategy<T, TKey> : IQueryStrategy<T, TKey> where T : BaseEntity<TKey>
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.AllTime; 

        public IQueryable<T> Apply(IQueryable<T> query, Filter<T, TKey> filter)
        {
            return query;
        }
    }

   

}
