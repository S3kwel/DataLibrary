using DATA.Repository.Abstraction;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation
{
    public class FilterCondition<T, TKey> where T : BaseEntity<TKey>
    {
        public Expression<Func<T, bool>>? Condition { get; set; }
    }


}