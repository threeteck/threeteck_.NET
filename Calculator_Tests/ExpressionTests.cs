using System;
using threeteck_Calculator;
using NUnit.Framework;

namespace Calculator_Tests
{
    [TestFixture]
    public class ExpressionTests
    {
        public void CheckEquality(Expression a, Expression b)
        {
            Assert.AreEqual(a.FirstNumber, b.FirstNumber);
            Assert.AreEqual(a.SecondNumber, b.SecondNumber);
            Assert.AreEqual(a.Operator, b.Operator);
        }
        [Test]
        public void Parse_InvalidFormat_ReturnFormatException()
        {
            Assert.Catch(typeof(FormatException), () => Expression.Parse("1 - --1"));
            Assert.Catch(typeof(FormatException), () => Expression.Parse("1.0"));
            Assert.Catch(typeof(FormatException), () => Expression.Parse("1.0+"));
            Assert.Catch(typeof(FormatException), () => Expression.Parse("1..0+2.0"));
            Assert.Catch(typeof(FormatException), () => Expression.Parse("1.0 + a"));
        }

        [Test]
        public void Parse_DecimalValuesInString_CreateValidInstance()
        {
            CheckEquality(new Expression(2.5,'+',1), Expression.Parse("2.5 + 1"));
            CheckEquality(new Expression(2.5,'-',1.5), Expression.Parse(" 2,5 - 1.5 "));
            CheckEquality(new Expression(1.1,'*',2.5), Expression.Parse("1.1 * 2,5"));
            CheckEquality(new Expression(1.1,'/',2.5), Expression.Parse("1,1 / 2.5"));
        }

        [Test]
        public void Parse_NegativeValuesInString_CreateValidInstance()
        {
            CheckEquality(new Expression(-3,'+',2), Expression.Parse("-3 + 2"));
            CheckEquality(new Expression(2,'+',-3), Expression.Parse("2 + -3"));
            CheckEquality(new Expression(-2,'+',-3), Expression.Parse("-2 + -3"));
            CheckEquality(new Expression(2,'-',-3.5), Expression.Parse("2 - -3.5"));
            CheckEquality(new Expression(2,'*',-1.5), Expression.Parse("2 * -1.5"));
            CheckEquality(new Expression(-2,'*',-2), Expression.Parse("-2 * -2"));
            CheckEquality(new Expression(2,'/',-4), Expression.Parse("2 / -4"));
            CheckEquality(new Expression(-2,'/',2), Expression.Parse("-2 / 2"));
        }
    }
}