using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging;
using DATA.Repository.Implementation.Models;

namespace DATA.Repository.Abstraction
{
    public interface IDebugStrategy<T> where T : BaseEntity
    {
        void BeforeHook(IQueryable<T> query, BaseFilter<T> filter, DebugContext context, string sectionName);
        void AfterHook(IQueryable<T> query, BaseFilter<T> filter, DebugContext context, string sectionName);
    }

}
