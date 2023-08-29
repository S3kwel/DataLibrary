using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class AtExactTimeStrategy<T> : IQueryStrategy<T> where T : BaseEntity
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.AtExactTime;

        public IQueryable<T> Apply(IQueryable<T> query, Filter<T> filter)
        {
            query = query.Where(e => e.PeriodStart <= filter.ValidFrom);
            return query;
        }
    }



}
