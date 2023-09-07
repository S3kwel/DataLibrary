using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class AtExactTimeStrategy<T> : IQueryStrategy<T> where T : HistoricEntity
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.AtExactTime;

        public IQueryable<T> Apply(IQueryable<T> query, HistoricFilter<T> filter)
        {
            query = query.Where(e => e.PeriodStart <= filter.ValidFrom);
            return query;
        }
    }



}
