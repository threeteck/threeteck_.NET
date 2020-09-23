using System;
using NUnit.Framework;
using threeteck_Calculator;

namespace Calculator_Tests
{
    [TestFixture]
    public class OperationsTests
    {
        [TestCase(1, 1, 2d)]
        [TestCase(0, 0, 0d)]
        [TestCase(-3.14, 3, -0.14d)]
        [TestCase(double.NaN, 0, double.NaN)]
        public void Add_ArgumentsAreValid_ReturnSumOfTwoNumbers(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Add(a, b), 1e-5);
        }

        [TestCase(1, 1, 1d)]
        [TestCase(0, 0, 0d)]
        [TestCase(-3.14, 2, -6.28d)]
        [TestCase(double.NaN, 1, double.NaN)]
        public void Multiply_ArgumentsAreValid_ReturnProductOfTwoNumbers(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Multiply(a, b), 1e-5);
        }

        [TestCase(1, 1, 0d)]
        [TestCase(0, 0, 0d)]
        [TestCase(-3.14, 3, -6.14d)]
        [TestCase(1.01, -3, 4.01d)]
        [TestCase(double.NaN, 1, double.NaN)]
        public void Subtract_ArgumentsAreValid_ReturnDifferenceOfTwoNumbers(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Subtract(a, b), 1e-5);
        }

        [TestCase(1, 1, 1d)]
        [TestCase(-3.14, 3, -1.04666d)]
        [TestCase(double.NaN, 1, double.NaN)]
        public void Divide_ArgumentsAreValid_ReturnQuotientOfTwoNumbers(double a, double b, double expected)
        {
            Assert.AreEqual(expected, Operations.Divide(a, b), 1e-5);
        }

        [Test]
        public void Divide_InvalidInput_ReturnNaN()
        => Assert.AreEqual(double.NaN, Operations.Divide(double.NaN, 1));
        
        [Test]
        public void Add_InvalidInput_ReturnNaN()
        =>  Assert.AreEqual(double.NaN, Operations.Add(double.NaN, 1));
        
        [Test]
        public void Multiply_InvalidInput_ReturnNaN()
        => Assert.AreEqual(double.NaN, Operations.Multiply(double.NaN, 1));
        
        [Test]
        public void Subtract_InvalidInput_ReturnNaN()
        =>Assert.AreEqual(double.NaN, Operations.Subtract(double.NaN, 1));

        [Test]
        public void Divide_DivisionByZero_ThrowException()
        {
            Assert.Catch(typeof(DivideByZeroException), () => Operations.Divide(1, 0));
        }
    }
}
