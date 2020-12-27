using System;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace BenchmarkTest
{
    [MemoryDiagnoser]
    public class BenchmarkExecutor
    {
        private static BenchmarkMethods _methods = new BenchmarkMethods();
        private static dynamic _dynamicMethods = _methods;
        private static MethodInfo _reflectionMethod = typeof(BenchmarkMethods).GetMethod("InstanceMethod");
        private static Func<int> _virtualMethod = BenchmarkMethods.GetVirtualMethod();
        
        [Benchmark]
        public int StaticCall() => BenchmarkMethods.StaticMethod();

        [Benchmark]
        public int InstanceCall() => _methods.InstanceMethod();

        [Benchmark]
        public int VirtualCall() => _virtualMethod();

        [Benchmark]
        public int GenericCall() => _methods.GenericMethod<string>(null);
        
        [Benchmark]
        public int DynamicCall() => _dynamicMethods.GenericMethod<string>(null);

        [Benchmark]
        public int ReflectionCall()
        {
            return (int) _reflectionMethod.Invoke(_methods, new object[] {});
        }
    }
}