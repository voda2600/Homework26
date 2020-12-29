using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bench
{
    internal class HabrExampleConfig : ManualConfig
    {
        public HabrExampleConfig()
        {
            Add(StatisticColumn.Median);
            Add(StatisticColumn.StdDev);
            Add(StatisticColumn.Mean);
        }
    }

    [Config(typeof(HabrExampleConfig))]
    class BenchFor
    {
        public static void StaticFor()
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "a";
            }
        }
        public static void StaticDynamicFor()
        {
            dynamic s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "a";
            }
        }
        [Benchmark(Description = "SimpleMethod")]
        public void SIimpleFor()
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "a";
            }
        }
        [Benchmark(Description = "DynamicMethod")]
        public void DynamicFor()
        {
            dynamic s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "a";
            }
        }
        [Benchmark(Description = "StaticSimpleMethod")]
        public void func1()
        {
            StaticFor();
        }
        [Benchmark(Description = "StaticDynamicMethod")]
        public void func2()
        {
            StaticDynamicFor();
        }
        [Benchmark(Description = "VirtualMethod")]
        public virtual void VirtualFor()
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "a";
            }
        }
        [Benchmark(Description = "DynamicVirtualMethod")]
        public virtual void DynamicVirtuaFor()
        {
            dynamic s = "";
            for (int i = 0; i < 10; i++)
            {
                s += "a";
            }
        }
        [Benchmark(Description = "GenericMethod")]
        public void func3()
        {
            GenericFor("a");
        }
        [Benchmark(Description = "DynamicGenericMethod")]
        public void func4()
        {
            DynamicGenericFor("a");
        }
        private void GenericFor<T>(T x)
        {
            string s = "";
            for (int i = 0; i < 10; i++)
            {
                s += x;
            }
        }
        private void DynamicGenericFor<T>(T x)
        {
            dynamic s = "";
            for (int i = 0; i < 10; i++)
            {
                s += x;
            }
        }
        [Benchmark(Description = "ReflectionMethod")]
        public void ReflectionFor()
        {
            Type type = this.GetType();
            var methodInfo = type.GetMethod("SIimpleFor").Invoke(this, null);
        }
       
    }
}
