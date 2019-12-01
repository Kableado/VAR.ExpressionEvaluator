using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VAR.ExpressionEvaluator.Tests
{
    [TestClass()]
    public class ExpressionNumberNodeTests
    {
        [TestMethod()]
        public void ExpressionNumberNode__One()
        {
            IExpressionNode node = new ExpressionNumberNode(1);
            Assert.AreEqual(1m, node.Eval(null));
        }

        [TestMethod()]
        public void ExpressionNumberNode__Two()
        {
            IExpressionNode node = new ExpressionNumberNode(2);
            Assert.AreEqual(2m, node.Eval(null));
        }

        [TestMethod()]
        public void ExpressionNumberNode__OneHundredDotFortyFive()
        {
            IExpressionNode node = new ExpressionNumberNode(100.45m);
            Assert.AreEqual(100.45m, node.Eval(null));
        }
    }
}