using System;

namespace VAR.ExpressionEvaluator
{
    public class ExpressionEqualsNode : ExpressionBinaryNode
    {
        public ExpressionEqualsNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, EqualsOp)
        {
        }

        private static object EqualsOp(object leftValue, object rightValue)
        {
            if (leftValue == null && rightValue == null)
            {
                return true;
            }
            if (leftValue == null || rightValue == null)
            {
                return false;
            }

            if (leftValue is string && rightValue is string)
            {
                return string.Compare((string)leftValue, (string)rightValue) == 0;
            }

            if (leftValue is string)
            {
                if (decimal.TryParse((string)leftValue, out decimal dec) == false)
                {
                    throw new Exception(string.Format("Can't convert to decimal string value \"{0}\"", (string)leftValue));
                }
                leftValue = dec;
            }
            if (rightValue is string)
            {
                if (decimal.TryParse((string)rightValue, out decimal dec) == false)
                {
                    throw new Exception(string.Format("Can't convert to decimal string value \"{0}\"", (string)rightValue));
                }
                rightValue = dec;
            }

            if ((leftValue is decimal) == false || (rightValue is decimal) == false)
            {
                throw new Exception("Can't compare non decimal values");
            }
            return (decimal)leftValue == (decimal)rightValue;
        }
    }
}
