using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Paralell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using Mutex m = new Mutex(false, "773D85AB-79C7-4613-B9D6-A1756A8A581D");

            object obj = new object();
            ConcurrentBag<int> ints = new();
            System.Threading.Tasks.Parallel.For(0, 10_000, i =>
            {
                //m.WaitOne();
                ints.Add(i);
                //m.ReleaseMutex();
                //lock (obj)
                //{
                //    ints.Add(i);
                //}
                //párhuzamosan fog futni
            });

            string s = string.Join(',', ints);
            Console.WriteLine(s);

            string[] tmb = ["egy", "kettő", "négy", "három"];

            System.Threading.Tasks.Parallel.ForEach(tmb, elem =>
            {
                Console.WriteLine(elem);
            });

            Debug.Assert(ints.Count == 10_000);

        }
    }
}
