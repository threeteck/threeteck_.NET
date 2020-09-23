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
        public void MakeCalculation_DecimalInputValues_MakeValidCalculation()
        {
            Assert.AreEqual(3.5d, Calculator.MakeCalculation(2.5,'+',1), 1e-5);
            Assert.AreEqual(1, Calculator.MakeCalculation(2.5,'-',1.5), 1e-5);
            Assert.AreEqual(2.75d, Calculator.MakeCalculation(1.1,'*',2.5), 1e-5);
            Assert.AreEqual(0.44d, Calculator.MakeCalculation(1.1,'/',2.5), 1e-5);
        }

        [Test]
        public void MakeCalculation_NegativeInputValues_MakeValidCalculation()
        {
            Assert.AreEqual(-1, Calculator.MakeCalculation(-3,'+',2), 1e-5);
            Assert.AreEqual(-1, Calculator.MakeCalculation(2,'+',-3), 1e-5);
            Assert.AreEqual(-5, Calculator.MakeCalculation(-2,'+',-3), 1e-5);
            Assert.AreEqual(5.5d, Calculator.MakeCalculation(2,'-',-3.5), 1e-5);
            Assert.AreEqual(-3, Calculator.MakeCalculation(2,'*',-1.5), 1e-5);
            Assert.AreEqual(4d, Calculator.MakeCalculation(-2,'*',-2), 1e-5);
            Assert.AreEqual(-0.5d, Calculator.MakeCalculation(2,'/',-4), 1e-5);
            Assert.AreEqual(-1, Calculator.MakeCalculation(-2,'/',2), 1e-5);
        }

        [Test]
        public void MakeCalculation_DivisionByZero_ThrowException()
        {
            Assert.Catch(typeof(DivideByZeroException), () => Calculator.MakeCalculation(1, '/', 0));
        }
        
        [Test]
        public void MakeCalculation_OperationNotSupported_ThrowException()
        {
            Assert.Catch(() => Calculator.MakeCalculation(3, '^', 2));
        }
    }
}