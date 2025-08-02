using System;
using System.Diagnostics;
using System.Collections.Generic;

// Data Structures and Algorithms Statistics
namespace DSAS
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10000000;
            int iterations = 100;
            string output = $"N = {n}, Iterations = {iterations}";
            output += FormatDiagnostics("ListAllocateNoReserve", ListAllocateNoReserve(n, iterations));
            output += FormatDiagnostics("ListAllocateReserve", ListAllocateReserve(n, iterations));
            Console.WriteLine(output);
        }

        static string FormatDiagnostics(string functionName, (long memUsageAvg, long elapsedMilisecondsAvg) diagnostics)
            => $"\n\t[{functionName}] t = {diagnostics.elapsedMilisecondsAvg} ms, mem = {diagnostics.memUsageAvg} bytes";

        static (long memUsageAvg, long elapsedMillisecondsAvg) ListAllocateNoReserve(int n, int iterations)
        {
            List<(long memUsage, long elapsedMilliseconds)> diagnostics = new List<(long memUsage, long elapsedMilliseconds)>();
            for (int i = 0; i < iterations; i++)
            {
                long memUsageStart = GC.GetTotalMemory(true);
                Stopwatch sw = Stopwatch.StartNew();
                List<int> newList = new List<int>();
                for (int j = 0; j < n; j++)
                    newList.Add(1);
                sw.Stop();
                long memUsageEnd = GC.GetTotalMemory(true);
                diagnostics.Add((memUsageEnd - memUsageStart, sw.ElapsedMilliseconds));
            }
            long memUsageAvg = 0;
            long elapsedMilisecondsAvg = 0;
            foreach ((long memUsage, long elapsedMilliseconds) data in diagnostics)
            {
                memUsageAvg += data.memUsage;
                elapsedMilisecondsAvg += data.elapsedMilliseconds;
            }
            memUsageAvg /= iterations;
            elapsedMilisecondsAvg /= iterations;
            return (memUsageAvg, elapsedMilisecondsAvg);
        }

        static (long memUsageAvg, long elapsedMillisecondsAvg) ListAllocateReserve(int n, int iterations)
        {
            List<(long memUsage, long elapsedMilliseconds)> diagnostics = new List<(long memUsage, long elapsedMilliseconds)>();
            for (int i = 0; i < iterations; i++)
            {
                long memUsageStart = GC.GetTotalMemory(true);
                Stopwatch sw = Stopwatch.StartNew();
                List<int> newList = new List<int>(n);
                for (int j = 0; j < n; j++)
                    newList.Add(1);
                sw.Stop();
                long memUsageEnd = GC.GetTotalMemory(true);
                diagnostics.Add((memUsageEnd - memUsageStart, sw.ElapsedMilliseconds));
            }
            long memUsageAvg = 0;
            long elapsedMilisecondsAvg = 0;
            foreach ((long memUsage, long elapsedMilliseconds) data in diagnostics)
            {
                memUsageAvg += data.memUsage;
                elapsedMilisecondsAvg += data.elapsedMilliseconds;
            }
            memUsageAvg /= iterations;
            elapsedMilisecondsAvg /= iterations;
            return (memUsageAvg, elapsedMilisecondsAvg);
        }
    };
}