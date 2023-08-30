using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class ActiveWithinStrategy<T, TKey> : IQueryStrategy<T, TKey> where T : BaseEntity<TKey>
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.ActiveWithin;

        public IQueryable<T> Apply(IQueryable<T> query, Filter<T, TKey> filter)
        {
                query = query.Where(e => e.PeriodStart >= filter.ValidFrom && e.PeriodEnd <= filter.ValidTo);
                return query;
        }
    }

   

}
