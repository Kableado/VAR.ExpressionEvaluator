using System;
using System.Linq;

namespace VAR.ExpressionEvaluator
{
    public class ExpressionFunctionNode : IExpressionNode
    {
        private readonly string _name;
        private IExpressionNode[] _paramNodes;

        public ExpressionFunctionNode(string name, IExpressionNode[] paramNodes)
        {
            _name = name;
            _paramNodes = paramNodes;
        }

        public object Eval(IEvaluationContext evaluationContext)
        {
            object[] paramValues = _paramNodes.Select(p => p.Eval(evaluationContext)).ToArray();
            Func<object[], object> func = evaluationContext.GetFunction(_name);
            if (func == null)
            {
                throw new Exception(string.Format("Function {0} not found", _name));
            }
            object result = func(paramValues);
            return result;
        }
    }
}
