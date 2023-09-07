using DATA.Repository.Implementation.Models;
using System.Linq.Expressions;

namespace DATA.Repository.Abstraction
{
    public interface ISpecification<T> where T: BaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, bool>>> AndCriteria { get; }
        List<Expression<Func<T, bool>>> OrCriteria { get; }
    }


}