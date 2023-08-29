using DATA.Repository.Abstraction;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public class FilterCondition<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Condition { get; set; }
    }


}