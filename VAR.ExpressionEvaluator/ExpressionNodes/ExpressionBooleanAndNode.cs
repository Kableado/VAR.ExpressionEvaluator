namespace VAR.ExpressionEvaluator
{
    public class ExpressionBooleanAndNode : ExpressionBinaryNode
    {
        public ExpressionBooleanAndNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, BooleanAndOp)
        {
        }

        private static object BooleanAndOp(object leftValue, object rightValue)
        {
            bool? bLeftValue = ExpressionBooleanNode.ConvertToBoolean(leftValue);
            bool? brightValue = ExpressionBooleanNode.ConvertToBoolean(rightValue);
            if (bLeftValue == null || bLeftValue == null) { return null; }
            return bLeftValue.Value && brightValue.Value;
        }
    }
}
