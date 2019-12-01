namespace VAR.ExpressionEvaluator
{
    public class ExpressionNumberNegateNode : ExpressionUnaryNode
    {
        public ExpressionNumberNegateNode(IExpressionNode node) :
            base(node, NumberNegateOp)
        {
        }

        private static object NumberNegateOp(object value)
        {
            return - (decimal)value;
        }
    }
}
