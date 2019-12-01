namespace VAR.ExpressionEvaluator
{
    public class ExpressionMinusNode : ExpressionBinaryNode
    {
        public ExpressionMinusNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, MinusOp)
        {
        }

        private static object MinusOp(object leftValue, object rightValue)
        {
            return (decimal)leftValue - (decimal)rightValue;
        }
    }
}
