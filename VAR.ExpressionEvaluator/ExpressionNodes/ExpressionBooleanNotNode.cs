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
            if (value == null) { return null; }
            return !(bool)value;
        }
    }
}
