namespace DATA.Repository.Implementation.Debugging.Interfaces
{
    public interface IDebugStrategy<T, TKey> where T : BaseEntity<TKey>
    {
        void BeforeHook(IQueryable<T> query, Filter<T, TKey> filter, DebugContext context, string sectionName);
        void AfterHook(IQueryable<T> query, Filter<T, TKey> filter, DebugContext context, string sectionName);
    }

}
