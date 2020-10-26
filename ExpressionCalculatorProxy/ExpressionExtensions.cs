using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionCalculatorProxy
{
    public static class ExpressionExtensions
    {
        public static Expression CreateMathExpression(string expression
            , Dictionary<string, (Func<Expression, Expression, Expression> expr, int precedence)> operationMapping = null)
        {
            if (operationMapping == null)
                operationMapping =
                    new Dictionary<string, (Func<Expression, Expression, Expression> expr, int precedence)>()
                    {
                        ["+"] = (Expression.Add, 0),
                        ["-"] = (Expression.Subtract, 0),
                        ["*"] = (Expression.Multiply, 1),
                        ["/"] = (Expression.Divide, 1),
                    };

            expression = expression.Replace(" ", "");
            string token = null;
            int index = 0;
            var tree = new Stack<Expression>();
            var operators = new Stack<string>();

            void PushOperatorOntoTree()
            {
                if (operationMapping.ContainsKey(operators.Peek()))
                {
                    var op = operationMapping[operators.Pop()];
                    var second = tree.Pop();
                    var first = tree.Pop();
                    tree.Push(op.expr(first, second));
                }
                else throw new FormatException();
            }

            while ((token = GetNextToken(expression, index)) != null)
            {
                index += token.Length;
                
                if (Double.TryParse(token, out var result))
                    tree.Push(Expression.Constant(result));
                
                else if (operationMapping.ContainsKey(token))
                {
                    var operation = operationMapping[token];
                    while (operators.Count > 0 && operators.Peek() != "("
                                               && operationMapping[operators.Peek()].precedence >=
                                               operation.precedence)
                        PushOperatorOntoTree();
                    operators.Push(token);
                }
                
                else if (token == "(")
                    operators.Push(token);
                
                else if (token == ")")
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                        PushOperatorOntoTree();

                    if (operators.Count == 0)
                        throw new FormatException(); //mismatched parenthesis

                    if (operators.Peek() == "(")
                        operators.Pop(); //discard
                }
            }

            while (operators.Count > 0)
                PushOperatorOntoTree();

            return tree.Pop();
        }

        public static string GetNextToken(string str, int i)
        {
            if (i >= str.Length)
                return null;
            
            var stringBuilder = new StringBuilder();
            while (i < str.Length)
            {
                if (char.IsDigit(str[i]) || str[i] == '.' || str[i] == ',') //number
                    stringBuilder.Append(str[i++]);
                else if (stringBuilder.Length > 0)
                    break;
                else return str[i].ToString(); //operator or parenthesis
            }
            
            return stringBuilder.ToString();
        }
    }
}