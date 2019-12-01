using System;

namespace VAR.ExpressionEvaluator
{
    public class ExpressionUnaryNode : IExpressionNode
    {
        private IExpressionNode _node;
        private readonly Func<object, object> _operation;

        public ExpressionUnaryNode(IExpressionNode node, Func<object, object> operation)
        {
            _node = node;
            _operation = operation;
        }

        public object Eval()
        {
            object value = _node.Eval();

            object result = _operation(value);
            return result;
        }
    }
}
