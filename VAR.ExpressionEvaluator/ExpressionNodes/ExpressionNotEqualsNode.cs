using System;

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
            if (leftValue is string && rightValue is string)
            {
                return string.Compare((string)leftValue, (string)rightValue) != 0;
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
            return (decimal)leftValue != (decimal)rightValue;
        }
    }
}
