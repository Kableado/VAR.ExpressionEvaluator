using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            evaluationContex.SetVariable("v1", 1);
            evaluationContex.SetVariable("v2", 1);
            string expression = "v1 + v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.AreEqual(2m, result);
        }

        [TestMethod()]
        public void Variables__Var1MultiplyVar2()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", 10);
            evaluationContex.SetVariable("v2", 5);
            string expression = "v1 * v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.AreEqual(50m, result);
        }

        [TestMethod()]
        public void Variables__Var1DivideVar2()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("_v1", 100);
            evaluationContex.SetVariable("$v2", 20);
            string expression = "_v1 / $v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.AreEqual(5m, result);
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

        #region Strings

        [TestMethod()]
        public void Strings__Contatenate_Hello_World()
        {
            string expression = "\"Hello\" + ' ' +\"World\"";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual("Hello World", result);
        }

        [TestMethod()]
        public void Strings__Contatenate_Hello_World_WithVariables()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", "Hello");
            evaluationContex.SetVariable("v2", " ");
            evaluationContex.SetVariable("v3", "World");
            string expression = "v1 + v2 + v3";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.AreEqual("Hello World", result);
        }

        [TestMethod()]
        public void Strings__Fail_Minus()
        {
            string expression = "'Hello' - 'World'";
            try
            {
                object result = Parser.EvaluateString(expression);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }

        [TestMethod()]
        public void Strings__Fail_Multiply()
        {
            string expression = "'Hello' * 'World'";
            try
            {
                object result = Parser.EvaluateString(expression);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }

        [TestMethod()]
        public void Strings__Fail_Division()
        {
            string expression = "'Hello' / 'World'";
            try
            {
                object result = Parser.EvaluateString(expression);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }

        #endregion Strings

        #region Relations

        [TestMethod()]
        public void Relations_1GreatherThan2_EqualsFalse()
        {
            string expression = "1>2";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void Relations_1Equals1_EqualsTrue()
        {
            string expression = "1=1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void Relations_10NotEquals1_EqualsTrue()
        {
            string expression = "10!=1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void Relations_10LessThan1_EqualsFalse()
        {
            string expression = "10<1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void Relations_1GreaterOrEqualThan1_EqualsTrue()
        {
            string expression = "1>=1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void Relations_1LessOrEqualThan1_EqualsTrue()
        {
            string expression = "1<=1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void Relations_1GreaterOrEqualThan10_EqualsFalse()
        {
            string expression = "1>=10";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void Relations_10LessOrEqualThan1_EqualsFalse()
        {
            string expression = "10<=1";
            object result = Parser.EvaluateString(expression);
            Assert.AreEqual(false, result);
        }

        #endregion Relations

        #region BooleanOps

        [TestMethod()]
        public void BooleanOps_And()
        {
            Assert.AreEqual(false, Parser.EvaluateString("false and false"));
            Assert.AreEqual(false, Parser.EvaluateString("false and true"));
            Assert.AreEqual(false, Parser.EvaluateString("true and false"));
            Assert.AreEqual(true, Parser.EvaluateString("true and true"));

            Assert.AreEqual(false, Parser.EvaluateString("false && false"));
            Assert.AreEqual(false, Parser.EvaluateString("false && true"));
            Assert.AreEqual(false, Parser.EvaluateString("true && false"));
            Assert.AreEqual(true, Parser.EvaluateString("true && true"));
        }

        [TestMethod()]
        public void BooleanOps_Or()
        {
            Assert.AreEqual(false, Parser.EvaluateString("false or false"));
            Assert.AreEqual(true, Parser.EvaluateString("false or true"));
            Assert.AreEqual(true, Parser.EvaluateString("true or false"));
            Assert.AreEqual(true, Parser.EvaluateString("true or true"));

            Assert.AreEqual(false, Parser.EvaluateString("false || false"));
            Assert.AreEqual(true, Parser.EvaluateString("false || true"));
            Assert.AreEqual(true, Parser.EvaluateString("true || false"));
            Assert.AreEqual(true, Parser.EvaluateString("true || true"));
        }

        [TestMethod()]
        public void BooleanOps_Not()
        {
            Assert.AreEqual(true, Parser.EvaluateString("!false"));
            Assert.AreEqual(false, Parser.EvaluateString("!true"));

            Assert.AreEqual(true, Parser.EvaluateString("not false"));
            Assert.AreEqual(false, Parser.EvaluateString("not true"));
        }


        #endregion BooleanOps

    }
}