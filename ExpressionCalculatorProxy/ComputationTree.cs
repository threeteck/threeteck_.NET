using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpressionCalculatorProxy
{
    public class ComputationTree<TResult>
    {
        private Func<ComputationTree<TResult>, Task<TResult>> _taskFactoryMethod;
        private Task<TResult> _computationTask = null;
        
        public ComputationTree<TResult> Left;
        public ComputationTree<TResult> Right;
        public Dictionary<string, object> Metadata;
        
        public Task<TResult> ComputationTask
        {
            get
            {
                if (_computationTask == null)
                    _computationTask = _taskFactoryMethod?.Invoke(this);
                return _computationTask;
            }
        }

        public TResult ComputedValue => ComputationTask.Result;

        public void Visit(Action<ComputationTree<TResult>, int> visitor, int depth = 0)
        {
            visitor(this, depth);
            if (Left != null) Left.Visit(visitor, depth + 1);
            if (Right != null) Right.Visit(visitor, depth + 1);
        }

        public ComputationTree(Func<ComputationTree<TResult>, Task<TResult>> taskFactoryMethod)
        {
            _taskFactoryMethod = taskFactoryMethod;
            Metadata = new Dictionary<string, object>();
        }
    }
}