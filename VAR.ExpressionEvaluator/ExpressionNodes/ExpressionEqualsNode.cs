namespace VAR.ExpressionEvaluator
{
    public class ExpressionEqualsNode : ExpressionBinaryNode
    {
        public ExpressionEqualsNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, EqualsOp)
        {
        }

        public static object EqualsOp(object leftValue, object rightValue)
        {
            // Null
            if (leftValue is string && string.IsNullOrEmpty(leftValue as string))
            {
                leftValue = null;
            }
            if (rightValue is string && string.IsNullOrEmpty(rightValue as string))
            {
                rightValue = null;
            }
            if (leftValue == null && rightValue == null)
            {
                return true;
            }
            if (leftValue == null || rightValue == null)
            {
                return false;
            }

            // String
            if (leftValue is string && rightValue is string)
            {
                return string.Compare((string)leftValue, (string)rightValue) == 0;
            }

            // Bool
            if (leftValue is bool || rightValue is bool)
            {
                bool? leftBool = ConvertToNullableBool(leftValue);
                bool? rightBool = ConvertToNullableBool(rightValue);
                return leftBool == rightBool;
            }

            // Decimal
            if (leftValue is string)
            {
                if (decimal.TryParse((string)leftValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal dec) == false)
                {
                    leftValue = null;
                }
                else
                {
                    leftValue = dec;
                }
            }
            if (rightValue is string)
            {
                if (decimal.TryParse((string)rightValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal dec) == false)
                {
                    rightValue = null;
                }
                else
                {
                    rightValue = dec;
                }
            }
            if ((leftValue is decimal) == false || (rightValue is decimal) == false)
            {
                return false;
            }
            return (decimal)leftValue == (decimal)rightValue;
        }

        private static bool? ConvertToNullableBool(object value)
        {
            if (value is bool)
            {
                return (bool)value;
            }
            if (value is string)
            {
                string text = value as string;
                if (string.IsNullOrEmpty(text))
                {
                    return null;
                }
                decimal decValue;
                if (decimal.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decValue))
                {
                    return decValue != 0;
                }
                string textLower = text.ToLower();
                if (textLower == "false" || textLower == "no" || textLower == "ez" || textLower == "non" || textLower == "nein")
                {
                    return false;
                }
                if (textLower == "true" || textLower == "si" || textLower == "sí" || textLower == "yes" || textLower == "bai" || textLower == "oui" || textLower == "ja")
                {
                    return true;
                }

                return null;
            }
            if (value is decimal)
            {
                return ((decimal)value) != 0;
            }
            return false;
        }
    }
}
