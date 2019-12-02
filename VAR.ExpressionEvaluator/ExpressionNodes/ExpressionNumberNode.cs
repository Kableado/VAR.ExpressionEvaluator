namespace VAR.ExpressionEvaluator
{
    public class ExpressionNumberNode : IExpressionNode
    {
        private readonly decimal _number;

        public ExpressionNumberNode(decimal number)
        {
            _number = number;
        }

        public object Eval(IEvaluationContext evaluationContext)
        {
            return _number;
        }
    }
}
