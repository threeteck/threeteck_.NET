using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threeteck_HW1
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
            return makeCalculation(expression);
        }

        private double makeCalculation(string expression)
        {
            string firstPart = GetNextExpression(expression);
            char op = expression[firstPart.Length];
            string secondPart = GetNextExpression(expression
                .Substring(firstPart.Length + 1));

            double parsedFirstPart = 0;
            double parsedSecondPart = 1;
            if (!double.TryParse(firstPart, out parsedFirstPart)
                || !double.TryParse(secondPart, out parsedSecondPart)
                || !operators.ContainsKey(op))
                return double.NaN;

            return operators[op](parsedFirstPart, parsedSecondPart);
        }

        private string GetNextExpression(string expression)
        {
            StringBuilder nextExpression = new StringBuilder();
            int index = 0;
            while (index < expression.Length && (
                char.IsDigit(expression[index]) || expression[index] == '.'
                    || expression[index] == ','))
                nextExpression.Append(expression[index++]);

            return nextExpression.ToString();
        }

        public static Calculator GetStandartCalculator()
        {
            Calculator calculator = new Calculator();
            calculator.AddOperator('+', Operations.Add);
            calculator.AddOperator('*', Operations.Multiply);
            calculator.AddOperator('-', Operations.Subtract);
            calculator.AddOperator('/', Operations.Divide);
            return calculator;
        }
    }
}
