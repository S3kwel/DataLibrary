using DATA.Repository.Abstraction.Debugging;
using DATA.Repository.Implementation.Models;
using System.Diagnostics;

namespace DATA.Repository.Implementation.Debugging
{
    public abstract class DebuggableRepository<T>: IDebuggableRepository<T> where T : BaseEntity 
    {
        public Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? BeforeProcessing { get; set; }
        public Action<IQueryable<T?>, BaseFilter<T>, DebugContext>? AfterProcessing { get; set; }
        public bool UseDefaultDebugging { get; set; } = true;
    }


}
