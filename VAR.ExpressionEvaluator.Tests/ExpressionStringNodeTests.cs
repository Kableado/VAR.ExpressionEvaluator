using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VAR.ExpressionEvaluator.Tests
{
    [TestClass()]
    public class ExpressionStringNodeTests
    {
        [TestMethod()]
        public void ExpressionNumberNode__Hello()
        {
            IExpressionNode node = new ExpressionStringNode("Hello");
            Assert.AreEqual("Hello", node.Eval());
        }

        [TestMethod()]
        public void ExpressionNumberNode__World()
        {
            IExpressionNode node = new ExpressionStringNode("World");
            Assert.AreEqual("World", node.Eval());
        }

        [TestMethod()]
        public void ExpressionNumberNode__Hello_World()
        {
            IExpressionNode node = new ExpressionStringNode("Hello World");
            Assert.AreEqual("Hello World", node.Eval());
        }

    }
}