namespace VAR.ExpressionEvaluator
{
    public class ExpressionDivisionNode : ExpressionBinaryNode
    {
        public ExpressionDivisionNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, DivisionOp)
        {
        }

        private static object DivisionOp(object leftValue, object rightValue)
        {
            return (decimal)leftValue / (decimal)rightValue;
        }
    }
}
