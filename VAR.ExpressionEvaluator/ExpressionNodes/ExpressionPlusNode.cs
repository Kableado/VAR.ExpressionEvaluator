using System;

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
            if (leftValue == null || rightValue == null)
            {
                return null;
            }

            if (leftValue is string || rightValue is string)
            {
                return string.Concat(Convert.ToString(leftValue), Convert.ToString(rightValue));
            }

            if ((leftValue is decimal) == false || (rightValue is decimal) == false)
            {
                throw new Exception("Can't sum non decimal values");
            }
            return (decimal)leftValue + (decimal)rightValue;
        }
    }
}
