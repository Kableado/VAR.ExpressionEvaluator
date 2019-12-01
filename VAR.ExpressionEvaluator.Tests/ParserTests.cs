using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VAR.ExpressionEvaluator.Tests
{
    [TestClass()]
    public class ParserTests
    {
        [TestMethod()]
        public void ParseString__Ten_EqualsTen()
        {
            string expression = "10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void ParseString__OnePlusTwo_EqualsThree()
        {
            string expression = "1   + 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(3m, result);
        }

        [TestMethod()]
        public void ParseString__OneMinusTwo_EqualsMinusOne()
        {
            string expression = "1   - 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(-1m, result);
        }

        [TestMethod()]
        public void ParseString__OneMillionMinusHundredThousands_EqualsNineHundredThousands()
        {
            string expression = "1000000   - 100000";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(900000m, result);
        }
    }
}