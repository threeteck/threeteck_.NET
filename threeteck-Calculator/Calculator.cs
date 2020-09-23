using System;
using System.Collections.Generic;
using System.Text;

namespace threeteck_Calculator
{
    public class Calculator
    {
        private Dictionary<char, Func<double, double, double>> _operators;

        public Calculator()
        {
            _operators = new Dictionary<char, Func<double, double, double>>();
        }

        public void AddOperator(char symbol, Func<double, double, double> @operator)
            => _operators.Add(symbol, @operator);

        public double MakeCalculation(double firstNumber, char operation, double secondNumber)
        {
            if(!_operators.ContainsKey(operation))
                throw new ArgumentException("Expression operator is not supported by this instance.", nameof(operation));

            return _operators[operation](firstNumber, secondNumber);
        }

        public double MakeCalculation(Expression expression)
            => MakeCalculation(expression.FirstNumber, expression.Operator, expression.SecondNumber);
        public double MakeCalculation(string expression) => MakeCalculation(Expression.Parse(expression));
        
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
