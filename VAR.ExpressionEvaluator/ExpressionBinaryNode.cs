﻿using System;

namespace VAR.ExpressionEvaluator
{
    public class ExpressionBinaryNode : IExpressionNode
    {
        private IExpressionNode _leftNode;
        private IExpressionNode _rightNode;
        private readonly Func<object, object, object> _operation;

        public ExpressionBinaryNode(IExpressionNode leftNode, IExpressionNode rightNode, Func<object, object, object> operation)
        {
            _leftNode = leftNode;
            _rightNode = rightNode;
            _operation = operation;
        }

        public object Eval()
        {
            object leftValue = _leftNode.Eval();
            object rightValue = _rightNode.Eval();

            object result = _operation(leftValue, rightValue);
            return result;
        }
    }
}
