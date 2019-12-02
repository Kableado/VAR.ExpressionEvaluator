namespace VAR.ExpressionEvaluator
{
    public class ExpressionNullNode : IExpressionNode
    {
        public object Eval(IEvaluationContext evaluationContext)
        {
            return null;
        }
    }
}
