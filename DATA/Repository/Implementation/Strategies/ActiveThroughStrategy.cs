using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class ActiveThroughStrategy<T, TKey> : IQueryStrategy<T,TKey> where T : HistoricBaseEntity<TKey>
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.ActiveThrough;

        public IQueryable<T> Apply(IQueryable<T> query, Filter<T, TKey> filter)
        {
            query = query.Where(e => e.PeriodStart <= filter.ValidFrom && e.PeriodEnd >= filter.ValidTo);
            return query;
        }
    }



}
