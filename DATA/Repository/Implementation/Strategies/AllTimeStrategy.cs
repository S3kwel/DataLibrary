using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Helpers;
using DATA.Repository.Abstraction.Strategies;

namespace DATA.Repository.Implementation.Strategies
{
    public class AllTimeStrategy<T> : IQueryStrategy<T> where T : IHistoricEntity
    {
        public HistoricFetchMode FetchMode => HistoricFetchMode.AllTime; 

        public IQueryable<T> Apply(IQueryable<T> query, HistoricFilter<T> filter)
        {
            return query;
        }
    }

   

}
