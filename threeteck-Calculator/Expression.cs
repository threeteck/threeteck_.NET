using System;
using System.Text;

namespace threeteck_Calculator
{
    public class Expression
    {
        public double FirstNumber { get; private set; }
        public double SecondNumber { get; private set; }
        public char Operator { get; private set; }

        public Expression(double firstNumber, char op, double secondNumber)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
            Operator = op;
        }

        public static Expression Parse(string stringExpression)
        {
            stringExpression = stringExpression.Replace(" ", "");
            var firstPart = GetNextExpression(stringExpression);
            if(stringExpression.Length <= firstPart.Length+1) throw new FormatException();
            var op = stringExpression[firstPart.Length];
            var secondPart = GetNextExpression(stringExpression
                .Substring(firstPart.Length + 1));

            if (!double.TryParse(firstPart, out var parsedFirstPart)
                || !double.TryParse(secondPart, out var parsedSecondPart))
                throw new FormatException();

            return new Expression(parsedFirstPart, op,parsedSecondPart);

            string GetNextExpression(string expression)
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
        }
    }
}