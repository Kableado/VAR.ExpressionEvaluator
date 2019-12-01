namespace VAR.ExpressionEvaluator
{
    public class ExpressionPlusNode : ExpressionBinaryNode
    {
        public ExpressionPlusNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, PlusOp)
        {
        }

        private static object PlusOp(object leftValue, object rightValue)
        {
            return (decimal)leftValue + (decimal)rightValue;
        }
    }
}
