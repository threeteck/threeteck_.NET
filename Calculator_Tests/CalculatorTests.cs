using NUnit.Framework;
using threeteck_Calculator;

namespace Calculator_Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void MakeCalculation_StandartCalculator_DoubleValues_Tests()
        {
            var calculator = Calculator.GetStandartCalculator();
            Assert.AreEqual(3.5d, calculator.MakeCalculation("2.5 + 1"), 1e-5);
            Assert.AreEqual(1, calculator.MakeCalculation(" 2,5 - 1.5 "), 1e-5);
            Assert.AreEqual(2.75d, calculator.MakeCalculation("1.1 * 2,5"), 1e-5);
            Assert.AreEqual(0.44d, calculator.MakeCalculation("1,1 / 2.5"), 1e-5);
        }

        [Test]
        public void MakeCalculation_IsReturnNaN_WhenInvalidInput()
        {
            var calculator = Calculator.GetStandartCalculator();
            Assert.AreEqual(double.NaN, calculator.MakeCalculation("1.0"));
            Assert.AreEqual(double.NaN, calculator.MakeCalculation("1.0+"));
            Assert.AreEqual(double.NaN, calculator.MakeCalculation("1..0+2.0"));
            Assert.AreEqual(double.NaN, calculator.MakeCalculation("1.0 + a"));
        }
    }
}