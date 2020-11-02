using System.Threading.Tasks;

namespace ExpressionCalculatorProxy
{
    public interface ICalculatorProxy
    {
        Task<CalculationProxyResult> GetCalculationResultAsync(double firstNumber, char op,
            double secondNumber);
    }
}