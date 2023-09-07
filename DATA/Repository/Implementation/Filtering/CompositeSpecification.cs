using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Models;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation.Filtering
{
    //Determine if we need this...
    //
    public abstract class CompositeSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public virtual Expression<Func<T, bool>> Criteria { get; } = e => true;
        public List<Expression<Func<T, bool>>> AndCriteria { get; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, bool>>> OrCriteria { get; } = new List<Expression<Func<T, bool>>>();

        protected ISpecification<T> And(ISpecification<T> specification)
        {
            AndCriteria.Add(specification.Criteria);
            return this;
        }

        protected ISpecification<T> Or(ISpecification<T> specification)
        {
            OrCriteria.Add(specification.Criteria);
            return this;
        }
    }

}
