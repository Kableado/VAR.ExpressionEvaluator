using System;
using System.Collections.Generic;

namespace VAR.ExpressionEvaluator
{
    public class EvaluationContext : IEvaluationContext
    {
        private Dictionary<string, Func<object[], object>> _functions = new Dictionary<string, Func<object[], object>>();
        private Dictionary<string, object> _variables = new Dictionary<string, object>();

        public void CleanFunctions()
        {
            _functions.Clear();
        }

        public void SetFunction(string name, Func<object[], object> function)
        {
            _functions.Add(name, function);
        }

        public Func<object[], object> GetFunction(string name)
        {
            return _functions[name];
        }

        public void CleanVariables()
        {
            _variables.Clear();
        }

        public void SetVariable(string name, object value)
        {
            _variables.Add(name, value);
        }

        public object GetVariable(string name)
        {
            return _variables[name];
        }

        public void Clean()
        {
            CleanFunctions();
            CleanVariables();
        }
    }
}
