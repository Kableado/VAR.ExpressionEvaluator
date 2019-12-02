namespace VAR.ExpressionEvaluator
{
    public interface IExpressionNode
    {
        object Eval(IEvaluationContext evaluationContext);
    }
}
