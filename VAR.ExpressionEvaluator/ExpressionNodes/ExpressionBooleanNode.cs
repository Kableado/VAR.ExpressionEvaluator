namespace VAR.ExpressionEvaluator
{
    public class ExpressionBooleanNode : IExpressionNode
    {
        private bool _value;

        public ExpressionBooleanNode(bool value)
        {
            _value = value;
        }

        public object Eval(IEvaluationContext evaluationContext)
        {
            return _value;
        }

        public static bool ConvertToBoolean(object value)
        {
            if (value is bool)
            {
                return (bool)value;
            }
            if (value is decimal)
            {
                return (decimal)value == 0;
            }
            if (value is string)
            {
                string str = (string)value;
                if (string.IsNullOrEmpty(str) || str == "0" || str.ToLower() == "false")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
