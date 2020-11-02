using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using threeteck_Calculator;

namespace ExpressionCalculatorProxy
{
    public class StubCalculatorProxy : ICalculatorProxy
    {
        public Task<CalculationProxyResult> GetCalculationResultAsync(double firstNumber, char op, double secondNumber)
        {
            var result = Calculator.GetStandartCalculator().MakeCalculation(firstNumber, op, secondNumber);

            return Task.FromResult(new CalculationProxyResult(200, result.ToString()));
        }
    }
}