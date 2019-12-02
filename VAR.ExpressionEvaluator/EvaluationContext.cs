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
            if (_functions.ContainsKey(name))
            {
                _functions[name] = function;
                return;
            }
            _functions.Add(name, function);
        }

        public Func<object[], object> GetFunction(string name)
        {
            if (_functions.ContainsKey(name) == false)
            {
                return null;
            }
            return _functions[name];
        }

        public void CleanVariables()
        {
            _variables.Clear();
        }

        public void SetVariable(string name, object value)
        {
            if (value is DateTime)
            {
                value = ((DateTime)value).ToString("s");
            }
            if ((value is string) == false && (value is bool) == false)
            {
                value = Convert.ToDecimal(value);
            }

            if (_variables.ContainsKey(name))
            {
                _variables[name] = value;
                return;
            }
            _variables.Add(name, value);
        }

        public object GetVariable(string name)
        {
            if (_variables.ContainsKey(name) == false)
            {
                return null;
            }
            return _variables[name];
        }

        public void Clean()
        {
            CleanFunctions();
            CleanVariables();
        }
    }
}
