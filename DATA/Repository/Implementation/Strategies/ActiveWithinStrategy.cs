using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class ActiveWithinStrategy<T> : IQueryStrategy<T> where T : IHistoricEntity
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.ActiveWithin;

        public IQueryable<T> Apply(IQueryable<T> query, HistoricFilter<T> filter)
        {
                query = query.Where(e => e.PeriodStart >= filter.ValidFrom && e.PeriodEnd <= filter.ValidTo);
                return query;
        }
    }

   

}
