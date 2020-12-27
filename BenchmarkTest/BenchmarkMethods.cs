using System;

namespace BenchmarkTest
{
    public class BenchmarkMethods
    {
        public static Func<int> GetVirtualMethod()
            => () =>
            {
                int a = 1;
                a += a + 1;
                return a;
            };

        public static int StaticMethod()
        {
            int a = 1;
            a += a + 1;
            return a;
        }

        public int InstanceMethod()
        {
            int a = 1;
            a += a + 1;
            return a;
        }

        public int GenericMethod<T>(T a) where T : class
        {
            int b = 1;
            if (a != null)
                b++;
            return b;
        }
    }
}