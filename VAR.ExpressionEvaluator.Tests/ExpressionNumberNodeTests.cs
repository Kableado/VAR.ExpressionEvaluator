using Xunit;

namespace VAR.ExpressionEvaluator.Tests
{
    public class ExpressionNumberNodeTests
    {
        [Fact]
        public void ExpressionNumberNode__One()
        {
            IExpressionNode node = new ExpressionNumberNode(1);
            Assert.Equal(1m, node.Eval(null));
        }

        [Fact]
        public void ExpressionNumberNode__Two()
        {
            IExpressionNode node = new ExpressionNumberNode(2);
            Assert.Equal(2m, node.Eval(null));
        }

        [Fact]
        public void ExpressionNumberNode__OneHundredDotFortyFive()
        {
            IExpressionNode node = new ExpressionNumberNode(100.45m);
            Assert.Equal(100.45m, node.Eval(null));
        }
    }
}