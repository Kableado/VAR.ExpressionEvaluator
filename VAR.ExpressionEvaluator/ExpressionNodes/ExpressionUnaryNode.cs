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

        public object Eval(IEvaluationContext evaluationContext)
        {
            object value = _node.Eval(evaluationContext);

            object result = _operation(value);
            return result;
        }
    }
}
