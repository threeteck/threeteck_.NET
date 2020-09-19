using System;
using NUnit.Framework;
using threeteck_HW1;

namespace OP_Tests
{
    [TestFixture]
    public class OperationsTest
    {
        [TestCase(1, 1, 2d)]
        [TestCase(0, 0, 0d)]
        [TestCase(-3.14, 3, -0.14d)]
        [TestCase(double.NaN, 0, double.NaN)]
        public void IsAddOperationWorking(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Add(a, b), 1e-5);
        }

        [Test]
        public void IsAddOperationWorking_Random()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                double a = rnd.NextDouble() * 10000000d;
                double b = rnd.NextDouble() * 10000000d;
                Assert.AreEqual(a + b, Operations.Add(a, b), 1e-5);
            }
        }

        [TestCase(1, 1, 1d)]
        [TestCase(0, 0, 0d)]
        [TestCase(-3.14, 2, -6.28d)]
        [TestCase(double.NaN, 1, double.NaN)]
        public void IsMultiplyOperationWorking(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Multiply(a, b), 1e-5);
        }

        [Test]
        public void IsMultiplyOperationWorking_Random()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                double a = rnd.NextDouble() * 100d;
                double b = rnd.NextDouble() * 100d;
                Assert.AreEqual(a * b, Operations.Multiply(a, b), 1e-5);
            }
        }

        [TestCase(1, 1, 0d)]
        [TestCase(0, 0, 0d)]
        [TestCase(-3.14, 3, -6.14d)]
        [TestCase(1.01, -3, 4.01d)]
        [TestCase(double.NaN, 1, double.NaN)]
        public void IsSubtractOperationWorking(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Subtract(a, b), 1e-5);
        }

        [Test]
        public void IsSubtractOperationWorking_Random()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                double a = rnd.NextDouble() * 10000000d;
                double b = rnd.NextDouble() * 10000000d;
                Assert.AreEqual(a - b, Operations.Subtract(a, b), 1e-5);
            }
        }

        [TestCase(1, 1, 1d)]
        [TestCase(0, 0, double.NaN)]
        [TestCase(-3.14, 3, -1.04666d)]
        [TestCase(double.NaN, 1, double.NaN)]
        public void IsDivideOperationWorking(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Divide(a, b), 1e-5);
        }

        [Test]
        public void IsDivideOperationWorking_Random()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                double a = rnd.NextDouble() * 10000000d;
                double b = rnd.NextDouble() * 10000000d;
                Assert.AreEqual(a / b, Operations.Divide(a, b), 1e-5);
            }
        }
    }
}
