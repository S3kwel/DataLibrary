namespace DATA.Repository.Implementation
{
    public class FilterGroup<T, TKey> where T : BaseEntity<TKey>
    {
        public List<FilterCondition<T, TKey>> Conditions { get; internal set; } = new();

        // Fluent API methods

        public FilterGroup<T, TKey> AddCondition(FilterCondition<T, TKey> condition)
        {
            Conditions.Add(condition);
            return this;
        }
    }



}