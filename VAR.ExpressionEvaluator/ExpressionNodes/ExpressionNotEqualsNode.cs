namespace VAR.ExpressionEvaluator
{
    public class ExpressionNotEqualsNode : ExpressionBinaryNode
    {
        public ExpressionNotEqualsNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, NotEqualsOp)
        {
        }

        private static object NotEqualsOp(object leftValue, object rightValue)
        {
            bool result = (bool)ExpressionEqualsNode.EqualsOp(leftValue, rightValue);
            return !result;
        }
    }
}
