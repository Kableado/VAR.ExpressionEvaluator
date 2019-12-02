using System;

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
            if (value == null)
            {
                return null;
            }

            if (value is string)
            {
                if (decimal.TryParse((string)value, out decimal dec) == false)
                {
                    throw new Exception(string.Format("Can't convert to decimal string value \"{0}\"", (string)value));
                }
                value = dec;
            }

            if ((value is decimal) == false)
            {
                throw new Exception("Can't negate non decimal values");
            }
            return -(decimal)value;
        }
    }
}
