using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpressionCalculatorProxy
{
    public class ParallelExpressionCalculatorProxyExecutor
    {
        private CalculatorProxy _calculatorProxy;

        public ParallelExpressionCalculatorProxyExecutor(CalculatorProxy calculatorProxy)
        {
            _calculatorProxy = calculatorProxy;
        }

        public async Task<double> ExecuteAsync(string expression)
        {
            var expressionTree = ExpressionExtensions.CreateMathExpression(expression);
            var visitor = new MathExpressionVisitor(ComputeBinary, ComputeConstant);
            var result = await visitor.ComputeAsync(expressionTree, (tree, depth) =>
            {
                if (tree.Metadata["expression"] is BinaryExpression binaryExpression)
                {
                    var message = tree.Metadata["message"];
                    if (!double.IsNaN(tree.ComputedValue))
                        message = $"Result: {tree.ComputedValue:0.##}";
                    Console.WriteLine(
                        $"{new string('-', depth * 4)} {MapExpressionTypeToChar(binaryExpression.NodeType)} ({message})");
                }
                else Console.WriteLine($"{new string('-', depth * 4)} {tree.ComputedValue:0.##}");
            });
            return result;
        }

        private char MapExpressionTypeToChar(ExpressionType type)
        {
            return type switch
            {
                ExpressionType.Add => '+',
                ExpressionType.Subtract => '-',
                ExpressionType.Multiply => '*',
                ExpressionType.Divide => '/',
            };
        }

        private async Task<double> ComputeBinary(double firstNumber, double secondNumber
            , ComputationTree<double> tree)
        {
            var binaryExpression = tree.Metadata["expression"] as BinaryExpression;
            
            var result = await _calculatorProxy.GetCalculationResultAsync(
                firstNumber
                , MapExpressionTypeToChar(binaryExpression.NodeType),
                secondNumber);

            tree.Metadata["message"] = result.ToString();
            
            if(result.HasResult)
                return result.Result;
            return double.NaN;
        }

        private Task<double> ComputeConstant(double constant, ComputationTree<double> tree)
        {
            return Task.FromResult(constant);
        }
    }
}