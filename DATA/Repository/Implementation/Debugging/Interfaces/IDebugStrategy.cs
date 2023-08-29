namespace DATA.Repository.Implementation.Debugging.Interfaces
{
    public interface IDebugStrategy<T> where T : BaseEntity
    {
        void BeforeHook(IQueryable<T> query, Filter<T> filter, DebugContext context, string sectionName);
        void AfterHook(IQueryable<T> query, Filter<T> filter, DebugContext context, string sectionName);
    }

}
