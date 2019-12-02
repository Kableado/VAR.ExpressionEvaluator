using System;

namespace VAR.ExpressionEvaluator
{
    public class ExpressionVariableNode : IExpressionNode
    {
        private readonly string _name;

        public ExpressionVariableNode(string name)
        {
            _name = name;
        }

        public object Eval(IEvaluationContext evaluationContext)
        {
            object value = evaluationContext.GetVariable(_name);
            return value;
        }
    }
}
