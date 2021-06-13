using Xunit;

namespace VAR.ExpressionEvaluator.Tests
{
    public class ExpressionBinaryNodeTests
    {
        [Fact]
        public void ExpressionBinaryNode__Plus()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(20),
                operation: (a, b) => (decimal)a + (decimal)b
            );

            var result = expr.Eval(null);

            Assert.Equal(30m, result);
        }

        [Fact]
        public void ExpressionBinaryNode__Minus()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(20),
                operation: (a, b) => (decimal)a - (decimal)b
            );

            var result = expr.Eval(null);

            Assert.Equal(-10m, result);
        }

        [Fact]
        public void ExpressionBinaryNode__Multiply()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(20),
                operation: (a, b) => (decimal)a * (decimal)b
            );

            var result = expr.Eval(null);

            Assert.Equal(200m, result);
        }

        [Fact]
        public void ExpressionBinaryNode__Division()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(5),
                operation: (a, b) => (decimal)a / (decimal)b
            );

            var result = expr.Eval(null);

            Assert.Equal(2m, result);
        }
    }
}