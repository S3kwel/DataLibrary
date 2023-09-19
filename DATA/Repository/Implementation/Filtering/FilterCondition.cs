using System.Linq.Expressions;
using DATA.Repository.Abstraction.Models;

namespace DATA.Repository.Implementation
{
    //Doesn't need to be refactored
    public class FilterCondition<T> where T : IBaseEntity
    {
        public Expression<Func<T, bool>>? Condition { get; set; }
    }


}