using System;
using NUnit.Framework;
using threeteck_Calculator;

namespace Calculator_Tests
{
    [TestFixture]
    public class StandartCalculatorTests
    {
        private static Calculator Calculator = Calculator.GetStandartCalculator();
        
        [Test]
        public void Should_ParseAndMakeValidCalculation_When_DoubleValuesInString()
        {
            Assert.AreEqual(3.5d, Calculator.MakeCalculation("2.5 + 1"), 1e-5);
            Assert.AreEqual(1, Calculator.MakeCalculation(" 2,5 - 1.5 "), 1e-5);
            Assert.AreEqual(2.75d, Calculator.MakeCalculation("1.1 * 2,5"), 1e-5);
            Assert.AreEqual(0.44d, Calculator.MakeCalculation("1,1 / 2.5"), 1e-5);
        }

        [Test]
        public void Should_ParseAndMakeValidCalculation_When_NegativeValuesInString()
        {
            Assert.AreEqual(-1, Calculator.MakeCalculation("-3 + 2"), 1e-5);
            Assert.AreEqual(-1, Calculator.MakeCalculation("2 + -3"), 1e-5);
            Assert.AreEqual(-5, Calculator.MakeCalculation("-2 + -3"), 1e-5);
            Assert.AreEqual(5.5d, Calculator.MakeCalculation("2 - -3.5"), 1e-5);
            Assert.AreEqual(-3, Calculator.MakeCalculation("2 * -1.5"), 1e-5);
            Assert.AreEqual(4d, Calculator.MakeCalculation("-2 * -2"), 1e-5);
            Assert.AreEqual(-0.5d, Calculator.MakeCalculation("2 / -4"), 1e-5);
            Assert.AreEqual(-1, Calculator.MakeCalculation("-2 / 2"), 1e-5);
        }

        [Test]
        public void Should_ReturnPositiveInfinity_When_DivisionByZero()
        {
            Assert.AreEqual(Double.PositiveInfinity, Calculator.MakeCalculation("1/0"));
        }
            
        [Test]
        public void Should_ReturnNaN_When_InvalidInput()
        {
            Assert.AreEqual(double.NaN, Calculator.MakeCalculation("1 - --1"));
            Assert.AreEqual(double.NaN, Calculator.MakeCalculation("1.0"));
            Assert.AreEqual(double.NaN, Calculator.MakeCalculation("1.0+"));
            Assert.AreEqual(double.NaN, Calculator.MakeCalculation("1..0+2.0"));
            Assert.AreEqual(double.NaN, Calculator.MakeCalculation("1.0 + a"));
        }
    }
}