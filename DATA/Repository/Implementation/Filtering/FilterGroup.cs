using DATA.Repository.Abstraction.Models;

namespace DATA.Repository.Implementation.Filtering
{
    public class FilterGroup<T> where T : IBaseEntity
    {
        public List<FilterCondition<T>> Conditions { get; internal set; } = new();

        // Fluent API methods

        public FilterGroup<T> AddCondition(FilterCondition<T> condition)
        {
            Conditions.Add(condition);
            return this;
        }
    }



}