using System;

namespace VAR.ExpressionEvaluator
{
    public interface IEvaluationContext
    {
        object GetVariable(string name);
        Func<object[], object> GetFunction(string name);
    }
}
