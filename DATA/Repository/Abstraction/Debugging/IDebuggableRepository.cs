using DATA.Repository.Implementation;
using DATA.Repository.Implementation.Debugging;
using DATA.Repository.Implementation.Models;

namespace DATA.Repository.Abstraction.Debugging
{
    public interface IDebuggableRepository<T> where T : BaseEntity
    {
        Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? AfterProcessing { get; set; }
        Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? BeforeProcessing { get; set; }
        bool UseDefaultDebugging { get; set; }
    }
}