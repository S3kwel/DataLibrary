using DATA.Repository.Abstraction;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public interface IFilter<T, TKey> where T : BaseEntity<TKey>
    {
        bool IncludeDeletedRecords { get; }
        List<Expression<Func<T, bool>>> Includes { get; }
        List<FilterGroup<T, TKey>> LogicGroups { get; set; }
        List<OrderingCriteria> OrderingCriteria { get; }
        int PageNumber { get; }
        int PageSize { get; }

        Filter<T, TKey> AddInclude(Expression<Func<T, bool>> include);
        Filter<T, TKey> AddLogicGroup(FilterGroup<T, TKey> group);
        Filter<T, TKey> AddOrderingCriteria(OrderingCriteria criteria);
        Filter<T, TKey> IncludeDeleted();
        bool IsValid();
        Filter<T, TKey> WithPageNumber(int pageNumber);
        Filter<T, TKey> WithPageSize(int pageSize);

        HistoricFetchMode FetchMode { get; }
    }
}