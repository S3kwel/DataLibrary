using DATA.Repository.Abstraction;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public interface Filter<T, TKey> where T : BaseEntity
    {
        bool IncludeDeletedRecords { get; }
        List<Expression<Func<T, bool>>> Includes { get; }
        List<FilterGroup<T>> LogicGroups { get; set; }
        List<OrderingCriteria> OrderingCriteria { get; }
        int PageNumber { get; }
        int PageSize { get; }

        Filter<T> AddInclude(Expression<Func<T, bool>> include);
        Filter<T> AddLogicGroup(FilterGroup<T> group);
        Filter<T> AddOrderingCriteria(OrderingCriteria criteria);
        Filter<T> IncludeDeleted();
        bool IsValid();
        Filter<T> WithPageNumber(int pageNumber);
        Filter<T> WithPageSize(int pageSize);
    }
}