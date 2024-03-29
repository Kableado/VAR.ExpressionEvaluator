﻿namespace VAR.ExpressionEvaluator
{
    public class ExpressionBooleanOrNode : ExpressionBinaryNode
    {
        public ExpressionBooleanOrNode(IExpressionNode leftNode, IExpressionNode rightNode) :
            base(leftNode, rightNode, BooleanOrOp)
        {
        }

        private static object BooleanOrOp(object leftValue, object rightValue)
        {
            bool? bLeftValue = ExpressionBooleanNode.ConvertToBoolean(leftValue);
            bool? brightValue = ExpressionBooleanNode.ConvertToBoolean(rightValue);
            if (bLeftValue == null || bLeftValue == null) { return null; }
            return bLeftValue.Value || brightValue.Value;
        }
    }
}
