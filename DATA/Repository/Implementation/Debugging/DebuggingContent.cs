using System.Diagnostics;

namespace DATA.Repository.Implementation.Debugging
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
