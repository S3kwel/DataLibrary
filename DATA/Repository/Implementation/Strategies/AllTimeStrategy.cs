using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class AllTimeStrategy<T> : IQueryStrategy<T> where T : BaseEntity
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.AllTime; 

        public IQueryable<T> Apply(IQueryable<T> query, Filter<T> filter)
        {
            return query;
        }
    }

   

}
