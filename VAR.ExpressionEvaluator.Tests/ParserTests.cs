using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VAR.ExpressionEvaluator.Tests
{
    [TestClass()]
    public class ParserTests
    {
        #region Plus and Minus

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

        #endregion Plus and minus

        #region Number signs

        [TestMethod()]
        public void ParseString__MinusTen()
        {
            string expression = "-10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(-10m, result);
        }

        [TestMethod()]
        public void ParseString__PlusTen()
        {
            string expression = "+10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void ParseString__MinusMinusTen()
        {
            string expression = "--10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void ParseString__MinusPlusChainTen()
        {
            string expression = "--++-+-10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void ParseString__10Minus20Minus30()
        {
            string expression = "10 + -20 - +30";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(-40m, result);
        }

        #endregion Number signs

        #region Multiplication and division

        [TestMethod()]
        public void ParseString__10MutiplyBy2()
        {
            string expression = "10 * 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(20m, result);
        }

        [TestMethod()]
        public void ParseString__10DividedBy2()
        {
            string expression = "10 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(5m, result);
        }

        [TestMethod()]
        public void ParseString__5DividedBy2()
        {
            string expression = "5 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(2.5m, result);
        }

        [TestMethod()]
        public void ParseString__5DividedBy2Plus1()
        {
            string expression = "5 / 2 + 1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(3.5m, result);
        }

        [TestMethod()]
        public void ParseString__1Plus5DividedBy2()
        {
            string expression = "1 + 5 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(3.5m, result);
        }

        [TestMethod()]
        public void ParseString__5DividedByParen1Plus1()
        {
            string expression = "5 / (1 + 1)";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(2.5m, result);
        }

        [TestMethod()]
        public void ParseString__Paren2Plus2DividedByParen1Plus1()
        {
            string expression = "(2 + 2) / (1 + 1)";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(2m, result);
        }

        #endregion Multiplication and division

    }
}