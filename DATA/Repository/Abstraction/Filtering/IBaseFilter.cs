using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation;
using System.Linq.Expressions;

namespace DATA.Repository.Abstraction.Filtering
{
    public interface IBaseFilter<T> where T : IBaseEntity
    {
        bool IncludeDeletedRecords { get; }
        List<Expression<Func<T, bool>>> Includes { get; }
        List<OrderingCriteria> OrderingCriteria { get; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        List<ISpecification<T>> Specifications { get; }

        BaseFilter<T> AddInclude(Expression<Func<T, bool>> include);
        BaseFilter<T> AddLogicGroup(ISpecification<T> group);
        BaseFilter<T> AddOrderingCriteria(OrderingCriteria criteria);
        IReadOnlyList<ISpecification<T>> AddSpecification(ISpecification<T> specification);
        BaseFilter<T> IncludeDeleted();
        bool IsValid();
        BaseFilter<T> WithPageNumber(int pageNumber);
        BaseFilter<T> WithPageSize(int pageSize);
    }
}