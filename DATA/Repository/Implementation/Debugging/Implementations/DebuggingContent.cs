using System.Diagnostics;

namespace DATA.Repository.Implementation.Debugging.Interfaces
{
    public class DebugContext
    {
        public Stopwatch Stopwatch { get; }

        public DebugContext()
        {
            Stopwatch = new Stopwatch();
        }
    }


}
