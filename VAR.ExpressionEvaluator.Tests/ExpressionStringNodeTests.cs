using Xunit;

namespace VAR.ExpressionEvaluator.Tests
{
    public class ExpressionStringNodeTests
    {
        [Fact]
        public void ExpressionNumberNode__Hello()
        {
            IExpressionNode node = new ExpressionStringNode("Hello");
            Assert.Equal("Hello", node.Eval(null));
        }

        [Fact]
        public void ExpressionNumberNode__World()
        {
            IExpressionNode node = new ExpressionStringNode("World");
            Assert.Equal("World", node.Eval(null));
        }

        [Fact]
        public void ExpressionNumberNode__Hello_World()
        {
            IExpressionNode node = new ExpressionStringNode("Hello World");
            Assert.Equal("Hello World", node.Eval(null));
        }
    }
}