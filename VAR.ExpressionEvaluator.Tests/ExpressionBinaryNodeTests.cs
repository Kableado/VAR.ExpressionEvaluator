using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VAR.ExpressionEvaluator.Tests
{
    [TestClass()]
    public class ExpressionBinaryNodeTests
    {
        [TestMethod()]
        public void ExpressionBinaryNode__Plus()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(20),
                operation: (a, b) => (decimal)a + (decimal)b
            );

            var result = expr.Eval();

            Assert.AreEqual(30m, result);
        }

        [TestMethod()]
        public void ExpressionBinaryNode__Minus()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(20),
                operation: (a, b) => (decimal)a - (decimal)b
            );

            var result = expr.Eval();

            Assert.AreEqual(-10m, result);
        }

        [TestMethod()]
        public void ExpressionBinaryNode__Multiply()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(20),
                operation: (a, b) => (decimal)a * (decimal)b
            );

            var result = expr.Eval();

            Assert.AreEqual(200m, result);
        }

        [TestMethod()]
        public void ExpressionBinaryNode__Division()
        {
            var expr = new ExpressionBinaryNode(
                leftNode: new ExpressionNumberNode(10),
                rightNode: new ExpressionNumberNode(5),
                operation: (a, b) => (decimal)a / (decimal)b
            );

            var result = expr.Eval();

            Assert.AreEqual(2m, result);
        }
    }
}