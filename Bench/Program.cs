using BenchmarkDotNet.Running;
using System;

namespace Bench
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchFor>();
        }
    }
}
