using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace VAR.ExpressionEvaluator.Tests
{
    [TestClass()]
    public class ParserTests
    {
        #region Plus and Minus

        [TestMethod()]
        public void PlusAndMinus__Ten_EqualsTen()
        {
            string expression = "10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void PlusAndMinus__OnePlusTwo_EqualsThree()
        {
            string expression = "1   + 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(3m, result);
        }

        [TestMethod()]
        public void PlusAndMinus__OneMinusTwo_EqualsMinusOne()
        {
            string expression = "1   - 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(-1m, result);
        }

        [TestMethod()]
        public void PlusAndMinus__OneMillionMinusHundredThousands_EqualsNineHundredThousands()
        {
            string expression = "1000000   - 100000";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(900000m, result);
        }

        #endregion Plus and minus

        #region Number signs

        [TestMethod()]
        public void Signs__MinusTen()
        {
            string expression = "-10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(-10m, result);
        }

        [TestMethod()]
        public void Signs__PlusTen()
        {
            string expression = "+10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void Signs__MinusMinusTen()
        {
            string expression = "--10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void Signs__MinusPlusChainTen()
        {
            string expression = "--++-+-10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void Signs__10Minus20Minus30()
        {
            string expression = "10 + -20 - +30";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(-40m, result);
        }

        #endregion Number signs

        #region Multiplication and division

        [TestMethod()]
        public void MultAndDiv__10MutiplyBy2()
        {
            string expression = "10 * 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(20m, result);
        }

        [TestMethod()]
        public void MultAndDiv__10DividedBy2()
        {
            string expression = "10 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(5m, result);
        }

        [TestMethod()]
        public void MultAndDiv__5DividedBy2()
        {
            string expression = "5 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(2.5m, result);
        }

        [TestMethod()]
        public void MultAndDiv__5DividedBy2Plus1()
        {
            string expression = "5 / 2 + 1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(3.5m, result);
        }

        [TestMethod()]
        public void MultAndDiv__1Plus5DividedBy2()
        {
            string expression = "1 + 5 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(3.5m, result);
        }

        [TestMethod()]
        public void MultAndDiv__5DividedByParen1Plus1()
        {
            string expression = "5 / (1 + 1)";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(2.5m, result);
        }

        [TestMethod()]
        public void MultAndDiv__Paren2Plus2DividedByParen1Plus1()
        {
            string expression = "(2 + 2) / (1 + 1)";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(2m, result);
        }

        #endregion Multiplication and division

        #region Variables

        [TestMethod()]
        public void Variables__Var1PlusVar2()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", 1m);
            evaluationContex.SetVariable("v2", 1m);
            string expression = "v1 + v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.AreEqual(2m, result);
        }

        [TestMethod()]
        public void Variables__Var1MultiplyVar2()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", 10m);
            evaluationContex.SetVariable("v2", 5m);
            string expression = "v1 * v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.AreEqual(50m, result);
        }

        #endregion Variables

        #region Funcitions

        [TestMethod()]
        public void Functions__MaxFunction()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetFunction("max", (parameters) =>
            {
                return parameters.Max(p => (decimal)p);
            });
            string expression = "max(1,2,10,5)";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.AreEqual(10m, result);
        }

        #endregion Functions

    }
}