namespace VAR.ExpressionEvaluator
{
    public class ExpressionStringNode : IExpressionNode
    {
        private readonly string _string;

        public ExpressionStringNode(string str)
        {
            _string = str;
        }

        public object Eval()
        {
            return _string;
        }
    }
}
