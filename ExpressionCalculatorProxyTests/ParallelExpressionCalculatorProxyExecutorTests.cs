using System;
using ExpressionCalculatorProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using Xunit;

namespace ExpressionCalculatorProxyTests
{
    public class TestsServiceProvider : IDisposable
    {
        public ServiceProvider ServiceProvider;
        public TestsServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            Configure(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void Configure(IServiceCollection serviceCollection)
        {
            var stubProxy = new StubCalculatorProxy();
            serviceCollection.AddSingleton<ICalculatorProxy>(stubProxy);
            
            serviceCollection.AddTransient<ParallelExpressionCalculatorProxyExecutor>();
        }

        public T GetRequiredService<T>() => ServiceProvider.GetRequiredService<T>();

        public void Dispose()
        {
        }
    }
    
    public class ParallelExpressionCalculatorProxyExecutorTests : IClassFixture<TestsServiceProvider>
    {
        public TestsServiceProvider ServiceProvider;

        public ParallelExpressionCalculatorProxyExecutorTests(TestsServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        
        [Theory]
        [InlineData("(2+3) / 12 * 7,1 + 8 * 9")]
        [InlineData("1 + 2")]
        [InlineData("2+2+2+2")]
        [InlineData("  2+2*2-2  ")]
        [InlineData("2-2+2-2+2")]
        [InlineData("(2 + 3) / 2 * 2,5 / 3 + 5")]
        [InlineData("2 * 3 / 5,0")]
        [InlineData("3 / 5 * 2")]
        [InlineData("6 / 2 * (1 + 2)")]
        [InlineData("(((2*2,5)/2,5)-1,1)/9,5")]
        public void ExecuteAsync_VariousExpressions_MakeValidCalculation(string expression)
        {
            var executor = ServiceProvider.GetRequiredService<ParallelExpressionCalculatorProxyExecutor>();
            var expressionTree = ExpressionExtensions.CreateMathExpression(expression);
            var compiledExpressionTree = Expression.Lambda<Func<double>>(expressionTree).Compile();
            
            Assert.Equal(executor.ExecuteAsync(expression).Result, compiledExpressionTree());
        }
    }
}