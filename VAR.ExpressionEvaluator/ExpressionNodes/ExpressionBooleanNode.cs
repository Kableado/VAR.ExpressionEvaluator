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
    }
}
