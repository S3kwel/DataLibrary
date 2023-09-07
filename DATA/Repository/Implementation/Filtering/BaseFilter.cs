using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Filtering;
using DATA.Repository.Implementation.Models;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public class BaseFilter<T> : IBaseFilter<T>, IFilter<T> where T : BaseEntity
    {
        #region Class Properties
        public List<ISpecification<T>> Specifications { get; internal set; } = new();
        public List<Expression<Func<T, bool>>> Includes { get; private set; } = new();
        public List<OrderingCriteria> OrderingCriteria { get; private set; } = new();
        public bool IncludeDeletedRecords { get; private set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public IReadOnlyList<ISpecification<T>> AddSpecification(ISpecification<T> specification)
        {
            Specifications.Add(specification);
            return Specifications;
        }

        #endregion

        #region Fluent Setters 

        public BaseFilter<T> AddLogicGroup(ISpecification<T> group)
        {
            Specifications.Add(group);
            return this;
        }
        public BaseFilter<T> IncludeDeleted()
        {
            IncludeDeletedRecords = true;
            return this;
        }
        public BaseFilter<T> WithPageNumber(int pageNumber)
        {
            PageNumber = pageNumber;
            return this;
        }
        public BaseFilter<T> WithPageSize(int pageSize)
        {
            PageSize = pageSize;
            return this;
        }
        public BaseFilter<T> AddInclude(Expression<Func<T, bool>> include)
        {
            Includes.Add(include);
            return this;
        }
        public BaseFilter<T> AddOrderingCriteria(OrderingCriteria criteria)
        {
            OrderingCriteria.Add(criteria);
            return this;
        }
        #endregion

        #region Validation
        public bool IsValid()
        {
            return Specifications.Count > 0;
        }
        #endregion
    }


}
