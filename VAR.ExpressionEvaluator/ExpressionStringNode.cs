namespace VAR.ExpressionEvaluator
{
    public class ExpressionStringNode : IExpressionNode
    {
        private readonly string _string;

        public ExpressionStringNode(string str)
        {
            _string = str;
        }

        public object Eval(IEvaluationContext evaluationContext)
        {
            return _string;
        }
    }
}
