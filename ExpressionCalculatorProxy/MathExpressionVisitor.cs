using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpressionCalculatorProxy
{
    public class MathExpressionVisitor : DynamicExpressionVisitor
    {
        private Func<double, double, ComputationTree<double>, Task<double>> _computeBinary;
        private Func<double, ComputationTree<double>, Task<double>> _computeConstant;
        private ComputationTree<double> _root;

        public MathExpressionVisitor(
            Func<double, double, ComputationTree<double>, Task<double>> computeBinary, 
            Func<double, ComputationTree<double>, Task<double>> computeConstant)
        {
            _computeBinary = computeBinary;
            _computeConstant = computeConstant;
        }

        public async Task<double> ComputeAsync(Expression expression, Action<ComputationTree<double>, int> visitor = null)
        {
            VisitExpression(expression);
            var result = await _root.ComputationTask;
            if (visitor != null)
                _root.Visit(visitor);
            return result;
        }

        protected override Expression Visit(BinaryExpression node)
        {
            var temp = new ComputationTree<double>(async (tree) =>
            {
                await Task.WhenAll(tree.Left.ComputationTask, tree.Right.ComputationTask);
                var leftResult = tree.Left.ComputationTask.Result;
                var rightResult = tree.Right.ComputationTask.Result;

                var computed = await _computeBinary(leftResult, rightResult, tree);
                return computed;
            });

            temp.Metadata["expression"] = node;
            
            VisitExpression(node.Left);
            temp.Left = _root;
            VisitExpression(node.Right);
            temp.Right = _root;

            _root = temp;
            
            return node;
        }

        protected override Expression Visit(ConstantExpression node)
        {
            var temp = new ComputationTree<double>( async (tree) 
                => await _computeConstant((double)node.Value, tree));

            temp.Metadata["expression"] = node;

            _root = temp;
            
            return node;
        }
    }
}