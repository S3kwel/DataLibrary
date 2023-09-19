using DATA.Repository.Abstraction;
using DATA.Repository.Abstraction.Models;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation.Filtering
{
    public abstract class Specification<T> : ISpecification<T> where T : IBaseEntity
    {
        public virtual Expression<Func<T, bool>> Criteria { get; internal set; } = e => true;
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

        public Specification()
        {
           
        }
    }

}
