namespace DATA.Repository.Implementation
{
    public class FilterGroup<T> where T : BaseEntity
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