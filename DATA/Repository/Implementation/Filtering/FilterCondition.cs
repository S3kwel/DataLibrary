using System.Linq.Expressions;
using DATA.Repository.Implementation.Models;

namespace DATA.Repository.Implementation
{
    //Doesn't need to be refactored
    public class FilterCondition<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Condition { get; set; }
    }


}