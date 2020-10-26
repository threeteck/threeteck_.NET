using System;
using System.Threading.Tasks;

namespace ExpressionCalculatorProxy
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var calculatorProxy = new CalculatorProxy("http://localhost:5000");
            var executor = new ParallelExpressionCalculatorProxyExecutor(calculatorProxy);
            var expression = Console.ReadLine();
            Console.WriteLine("Stating computation...");
            var result = await executor.ExecuteAsync(expression);
            Console.WriteLine($"\nResult: {result:0.##}");
            Console.ReadKey();
        }
    }
}