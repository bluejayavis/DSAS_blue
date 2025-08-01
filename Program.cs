using System;
using System.Diagnostics;
using System.Collections;

// Data Structures and Algorithms Statistics
namespace DSAS
{
    class Program
    {
        static void Main(string[] args)
        {
            long memUsageStart = GC.GetTotalMemory(true);

            Stopwatch sw = Stopwatch.StartNew();
            List<int> newList = new List<int>();
            for (int i = 0; i < 1000000; i++)
            {
                newList.Add(1);
            }
            long memUsageEnd = GC.GetTotalMemory(true);

            Console.WriteLine($"Allocated 1,000,000 integers to List<int>\n\tElapsed Time: {sw.ElapsedMilliseconds} ms\n\tMemory Usage: {memUsageEnd - memUsageStart}");
            sw.Reset();
        }
    }
}