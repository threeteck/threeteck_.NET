using System;
using System.Collections.Generic;
using System.Text;

namespace threeteck_Calculator
{
    public class Calculator
    {
        private Dictionary<char, Func<double, double, double>> operators;

        public Calculator()
        {
            operators = new Dictionary<char, Func<double, double, double>>();
        }

        public void AddOperator(char symbol, Func<double, double, double> @operator)
            => operators.Add(symbol, @operator);

        public double MakeCalculation(string expression)
        {
            expression = expression.Replace(" ", "");
            var firstPart = GetNextExpression(expression);
            if(expression.Length <= firstPart.Length+1) return double.NaN;
            var op = expression[firstPart.Length];
            var secondPart = GetNextExpression(expression
                .Substring(firstPart.Length + 1));

            if (!double.TryParse(firstPart, out var parsedFirstPart)
                || !double.TryParse(secondPart, out var parsedSecondPart)
                || !operators.ContainsKey(op))
                return double.NaN;

            return operators[op](parsedFirstPart, parsedSecondPart);
        }

        private static string GetNextExpression(string expression)
        {
            var nextExpression = new StringBuilder();
            int index = 0;
            if (expression[index] == '-')
            {
                nextExpression.Append('-');
                index++;
            }
            
            while (index < expression.Length && (
                char.IsDigit(expression[index]) || expression[index] == '.'
                                                || expression[index] == ','))
            {
                nextExpression.Append(expression[index] == '.' ? ',' : expression[index]);
                index++;
            }

            return nextExpression.ToString();
        }

        public static Calculator GetStandartCalculator()
        {
            var calculator = new Calculator();
            calculator.AddOperator('+', Operations.Add);
            calculator.AddOperator('*', Operations.Multiply);
            calculator.AddOperator('-', Operations.Subtract);
            calculator.AddOperator('/', Operations.Divide);
            return calculator;
        }
    }
}
