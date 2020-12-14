using System;
using System.Linq.Expressions;

namespace ExpressionCalculatorProxy
{
    public class DynamicExpressionVisitor
    {
        public Expression VisitExpression(Expression expression)
        {
            return Visit(expression as dynamic);
        }

        protected virtual Expression Visit(BinaryExpression binaryExpression)
        {
            return binaryExpression;
        }
        
        protected virtual Expression Visit(ConstantExpression constantExpression)
        {
            return constantExpression;
        }

        private Expression Visit(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}