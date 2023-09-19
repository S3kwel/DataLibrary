using DATA.Repository.Abstraction.Models;
using System.Linq.Expressions;

namespace DATA.Repository.Abstraction
{
    public interface ISpecification<T> where T: IBaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, bool>>> AndCriteria { get; }
        List<Expression<Func<T, bool>>> OrCriteria { get; }
    }


}