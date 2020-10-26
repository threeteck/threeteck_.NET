using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
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
            var depths = new Dictionary<int, int>();
            var result = await visitor.ComputeAsync(expressionTree, (tree, depth) =>
            {
                var spacing = new StringBuilder();
                for (int i = 0; i < depth - 1; i++)
                {
                    if (!depths.ContainsKey(i + 1) || depths[i + 1] < 2)
                        spacing.Append('│');
                    else spacing.Append(' ');
                    spacing.Append("    ");
                }

                if (depth != 0)
                {
                    if (!depths.ContainsKey(depth))
                    {
                        spacing.Append('├');
                        depths.Add(depth, 1);
                    }
                    else
                    {
                        spacing.Append('└');
                        depths[depth] = 2;
                    }

                    spacing.Append("─── ");
                    
                }

                if (tree.Metadata["expression"] is BinaryExpression binaryExpression)
                {
                    if (depths.ContainsKey(depth + 1))
                        depths.Remove(depth + 1);
                    var message = tree.Metadata["message"];
                    if (!double.IsNaN(tree.ComputedValue))
                        message = $"Result: {tree.ComputedValue:0.##}";
                    Console.WriteLine(
                        $"{spacing}{MapExpressionTypeToChar(binaryExpression.NodeType)} ({message})");
                }
                else Console.WriteLine($"{spacing}{tree.ComputedValue:0.##}");
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