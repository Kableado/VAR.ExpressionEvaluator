namespace VAR.ExpressionEvaluator
{
    public class ExpressionMultiplyNode : ExpressionBinaryNode
    {
        public ExpressionMultiplyNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, MultiplyOp)
        {
        }

        private static object MultiplyOp(object leftValue, object rightValue)
        {
            return (decimal)leftValue * (decimal)rightValue;
        }
    }
}
