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
            if (value == null)
            {
                throw new Exception(string.Format("Variable {0} not found", _name));
            }
            return value;
        }
    }
}
