﻿using System;

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
            if (leftValue == null || rightValue == null)
            {
                return null;
            }

            if (leftValue is string)
            {
                if (decimal.TryParse((string)leftValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal dec) == false)
                {
                    throw new Exception(string.Format("Can't convert to decimal string value \"{0}\"", (string)leftValue));
                }
                leftValue = dec;
            }
            if (rightValue is string)
            {
                if (decimal.TryParse((string)rightValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal dec) == false)
                {
                    throw new Exception(string.Format("Can't convert to decimal string value \"{0}\"", (string)rightValue));
                }
                rightValue = dec;
            }

            if ((leftValue is decimal) == false || (rightValue is decimal) == false)
            {
                throw new Exception("Can't divive non decimal values");
            }
            return (decimal)leftValue / (decimal)rightValue;
        }
    }
}
