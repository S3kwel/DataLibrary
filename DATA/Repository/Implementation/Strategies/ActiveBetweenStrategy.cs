using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class ActiveBetweenStrategy<T> : IQueryStrategy<T> where T : HistoricEntity
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.ActiveBetween; 

        public IQueryable<T> Apply(IQueryable<T> query, HistoricFilter<T> filter)
        {
            query = query.Where(e => e.PeriodStart <= filter.ValidFrom && e.PeriodEnd > filter.ValidTo);
            return query;
        }
    }

   

}
