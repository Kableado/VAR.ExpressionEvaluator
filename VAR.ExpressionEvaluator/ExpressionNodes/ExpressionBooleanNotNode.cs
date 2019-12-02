namespace VAR.ExpressionEvaluator
{
    public class ExpressionBooleanNotNode : ExpressionUnaryNode
    {
        public ExpressionBooleanNotNode(IExpressionNode node) :
            base(node, BooleanNotOp)
        {
        }

        private static object BooleanNotOp(object value)
        {
            value = ExpressionBooleanNode.ConvertToBoolean(value);
            return !(bool)value;
        }
    }
}
