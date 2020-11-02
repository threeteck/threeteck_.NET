using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpressionCalculatorProxy
{
    class Program
    {
        public static IServiceProvider Services;
        
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();
            
            var executor = Services.GetRequiredService<ParallelExpressionCalculatorProxyExecutor>();
            var expression = Console.ReadLine();
            Console.WriteLine("Stating computation...");
            var result = await executor.ExecuteAsync(expression);
            Console.WriteLine($"\nResult: {result:0.##}");
            Console.ReadKey();
        }

        static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var calculatorProxy = new CalculatorProxy("http://localhost:5000");
            serviceCollection.AddSingleton<ICalculatorProxy>(calculatorProxy);
            
            serviceCollection.AddTransient<ParallelExpressionCalculatorProxyExecutor>();
        }
    }
}