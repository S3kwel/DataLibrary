using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging;

namespace DATA.Repository.Abstraction
{
    public interface IDebugStrategy<T> where T : IBaseEntity
    {
        void BeforeHook(IQueryable<T> query, BaseFilter<T> filter, DebugContext context, string sectionName);
        void AfterHook(IQueryable<T> query, BaseFilter<T> filter, DebugContext context, string sectionName);
    }

}
