using DATA.Repository.Abstraction.Models;
using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging;

namespace DATA.Repository.Abstraction.Debugging
{
    public interface IDebuggableRepository<T> where T : IBaseEntity
    {
        Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? AfterProcessing { get; set; }
        Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? BeforeProcessing { get; set; }
        bool UseDefaultDebugging { get; set; }
    }
}