using System;
using System.Linq;
using Xunit;

namespace VAR.ExpressionEvaluator.Tests
{
    public class ParserTests
    {
        #region Plus and Minus

        [Fact]
        public void PlusAndMinus__Ten_EqualsTen()
        {
            string expression = "10";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(10m, result);
        }

        [Fact]
        public void PlusAndMinus__OnePlusTwo_EqualsThree()
        {
            string expression = "1   + 2";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(3m, result);
        }

        [Fact]
        public void PlusAndMinus__OneMinusTwo_EqualsMinusOne()
        {
            string expression = "1   - 2";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(-1m, result);
        }

        [Fact]
        public void PlusAndMinus__OneMillionMinusHundredThousands_EqualsNineHundredThousands()
        {
            string expression = "1000000   - 100000";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(900000m, result);
        }

        #endregion Plus and minus

        #region Number signs

        [Fact]
        public void Signs__MinusTen()
        {
            string expression = "-10";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(-10m, result);
        }

        [Fact]
        public void Signs__PlusTen()
        {
            string expression = "+10";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(10m, result);
        }

        [Fact]
        public void Signs__MinusMinusTen()
        {
            string expression = "--10";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(10m, result);
        }

        [Fact]
        public void Signs__MinusPlusChainTen()
        {
            string expression = "--++-+-10";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(10m, result);
        }

        [Fact]
        public void Signs__10Minus20Minus30()
        {
            string expression = "10 + -20 - +30";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(-40m, result);
        }

        #endregion Number signs

        #region Decimal numbers

        [Fact]
        public void Decimals__OnePointZero()
        {
            string expression = "1.0";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(1.0m, result);
        }

        [Fact]
        public void Decimals__OnePointOne()
        {
            string expression = "1.1";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(1.1m, result);
        }

        #endregion Decimal numbers

        #region Multiplication and division

        [Fact]
        public void MultAndDiv__10MutiplyBy2()
        {
            string expression = "10 * 2";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(20m, result);
        }

        [Fact]
        public void MultAndDiv__10DividedBy2()
        {
            string expression = "10 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(5m, result);
        }

        [Fact]
        public void MultAndDiv__5DividedBy2()
        {
            string expression = "5 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(2.5m, result);
        }

        [Fact]
        public void MultAndDiv__5DividedBy2Plus1()
        {
            string expression = "5 / 2 + 1";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(3.5m, result);
        }

        [Fact]
        public void MultAndDiv__1Plus5DividedBy2()
        {
            string expression = "1 + 5 / 2";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(3.5m, result);
        }

        [Fact]
        public void MultAndDiv__5DividedByParen1Plus1()
        {
            string expression = "5 / (1 + 1)";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(2.5m, result);
        }

        [Fact]
        public void MultAndDiv__Paren2Plus2DividedByParen1Plus1()
        {
            string expression = "(2 + 2) / (1 + 1)";
            object result = Parser.EvaluateString(expression);
            Assert.Equal(2m, result);
        }

        #endregion Multiplication and division

        #region Variables

        [Fact]
        public void Variables__Var1PlusVar2()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", 1);
            evaluationContex.SetVariable("v2", 1);
            string expression = "v1 + v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.Equal(2m, result);
        }

        [Fact]
        public void Variables__Var1MultiplyVar2()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", 10);
            evaluationContex.SetVariable("v2", 5);
            string expression = "v1 * v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.Equal(50m, result);
        }

        [Fact]
        public void Variables__Var1DivideVar2()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("_v1", 100);
            evaluationContex.SetVariable("$v2", 20);
            string expression = "_v1 / $v2";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.Equal(5m, result);
        }

        #endregion Variables

        #region Functions

        [Fact]
        public void Functions__MaxFunction()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetFunction("max", (parameters) =>
            {
                return parameters.Max(p => (decimal)p);
            });
            string expression = "max(1,2,10,5)";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.Equal(10m, result);
        }

        [Fact]
        public void Functions__NestedTest()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("linea1", 1);
            evaluationContex.SetVariable("linea2", 1);
            evaluationContex.SetVariable("linea4", 4);
            evaluationContex.SetFunction("iif", (parameters) =>
            {
                if (((bool)parameters[0]) == true)
                {
                    return parameters[1];
                }
                else
                {
                    return parameters[2];
                }
            });
            string expression = "iif(linea1>linea2,iif(linea1>linea4, linea1, iif(linea4>linea2,linea4,linea2)),iif(linea2>linea4,linea2,linea4))";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.Equal(4m, result);
        }

        #endregion Functions

        #region Strings

        [Fact]
        public void Strings__Contatenate_Hello_World()
        {
            string expression = "\"Hello\" + ' ' +\"World\"";
            object result = Parser.EvaluateString(expression);
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void Strings__Contatenate_Hello_World_WithVariables()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", "Hello");
            evaluationContex.SetVariable("v2", " ");
            evaluationContex.SetVariable("v3", "World");
            string expression = "v1 + v2 + v3";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public void Strings__Fail_Minus()
        {
            string expression = "'Hello' - 'World'";
            try
            {
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception)
            {
            }
        }

        [Fact]
        public void Strings__Fail_Multiply()
        {
            string expression = "'Hello' * 'World'";
            try
            {
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception)
            {
            }
        }

        [Fact]
        public void Strings__Fail_Division()
        {
            string expression = "'Hello' / 'World'";
            try
            {
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception)
            {
            }
        }

        #endregion Strings

        #region Relations

        [Fact]
        public void Relations_1GreatherThan2_EqualsFalse()
        {
            string expression = "1>2";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_StringEmptyGreatherThan1_EqualsFalse()
        {
            string expression = "\"\">1";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1GreatherThanStringEmpty_EqualsFalse()
        {
            string expression = "1>\"\"";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1Equals1_EqualsTrue()
        {
            string expression = "1=1";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_StringEmptyEquals1_EqualsFalse()
        {
            string expression = "\"\"=1";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1EqualsStringEmpty_EqualsFalse()
        {
            string expression = "1=\"\"";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_10NotEquals1_EqualsTrue()
        {
            string expression = "10!=1";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_StringEmptyNotEquals1_EqualsTrue()
        {
            string expression = "\"\"!=1";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_1NotEqualsStringEmpty_EqualsTrue()
        {
            string expression = "1!=\"\"";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_10Different1_EqualsTrue()
        {
            string expression = "10<>1";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_StringEmptyDifferent1_EqualsTrue()
        {
            string expression = "\"\"<>1";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_1DifferentStringEmpty_EqualsTrue()
        {
            string expression = "1<>\"\"";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_10LessThan1_EqualsFalse()
        {
            string expression = "10<1";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_StringEmptyLessThan1_EqualsFalse()
        {
            string expression = "\"\"<1";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1LessThanStringEmpty_EqualsFalse()
        {
            string expression = "1<\"\"";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1GreaterOrEqualThan1_EqualsTrue()
        {
            string expression = "1>=1";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_StringEmptyGreaterOrEqualThan1_EqualsFalse()
        {
            string expression = "\"\">=1";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1GreaterOrEqualThanStringEmpty_EqualsFalse()
        {
            string expression = "1>=\"\"";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1LessOrEqualThan1_EqualsTrue()
        {
            string expression = "1<=1";
            object result = Parser.EvaluateString(expression);
            Assert.True((bool?)result);
        }

        [Fact]
        public void Relations_1GreaterOrEqualThan10_EqualsFalse()
        {
            string expression = "1>=10";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_10LessOrEqualThan1_EqualsFalse()
        {
            string expression = "10<=1";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_StringEmptyLessOrEqualThan1_EqualsFalse()
        {
            string expression = "\"\"<=1";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact]
        public void Relations_1LessOrEqualThanStringEmpty_EqualsFalse()
        {
            string expression = "1<=\"\"";
            object result = Parser.EvaluateString(expression);
            Assert.False((bool?)result);
        }

        [Fact()]
        public void Relations__Var1NullEquals666_EqualsFalse()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", null);
            string expression = "v1 = 666";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.False((bool?)result);
        }

        [Fact()]
        public void Relations__Var1NullEqualsStringHello_EqualsFalse()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", null);
            string expression = "v1 = \"Hello\"";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.False((bool?)result);
        }

        [Fact()]
        public void Relations__Var1666Equals666_EqualsFalse()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", 666);
            string expression = "v1 = 666";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.True((bool?)result);
        }

        [Fact()]
        public void Relations__Var1HelloEqualsStringHello_EqualsFalse()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", "Hello");
            string expression = "v1 = \"Hello\"";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.True((bool?)result);
        }

        #endregion Relations

        #region BooleanOps

        [Fact]
        public void BooleanOps_And()
        {
            Assert.False((bool?)Parser.EvaluateString("false and false"));
            Assert.False((bool?)Parser.EvaluateString("false and true"));
            Assert.False((bool?)Parser.EvaluateString("true and false"));
            Assert.True((bool?)Parser.EvaluateString("true and true"));

            Assert.False((bool?)Parser.EvaluateString("False And False"));
            Assert.False((bool?)Parser.EvaluateString("False And True"));
            Assert.False((bool?)Parser.EvaluateString("True And False"));
            Assert.True((bool?)Parser.EvaluateString("True And True"));

            Assert.False((bool?)Parser.EvaluateString("false && false"));
            Assert.False((bool?)Parser.EvaluateString("false && true"));
            Assert.False((bool?)Parser.EvaluateString("true && false"));
            Assert.True((bool?)Parser.EvaluateString("true && true"));
        }

        [Fact]
        public void BooleanOps_Or()
        {
            Assert.False((bool?)Parser.EvaluateString("false or false"));
            Assert.True((bool?)Parser.EvaluateString("false or true"));
            Assert.True((bool?)Parser.EvaluateString("true or false"));
            Assert.True((bool?)Parser.EvaluateString("true or true"));

            Assert.False((bool?)Parser.EvaluateString("False Or False"));
            Assert.True((bool?)Parser.EvaluateString("False Or True"));
            Assert.True((bool?)Parser.EvaluateString("True Or False"));
            Assert.True((bool?)Parser.EvaluateString("True Or True"));

            Assert.False((bool?)Parser.EvaluateString("false || false"));
            Assert.True((bool?)Parser.EvaluateString("false || true"));
            Assert.True((bool?)Parser.EvaluateString("true || false"));
            Assert.True((bool?)Parser.EvaluateString("true || true"));
        }

        [Fact]
        public void BooleanOps_Not()
        {
            Assert.True((bool?)Parser.EvaluateString("!false"));
            Assert.False((bool?)Parser.EvaluateString("!true"));

            Assert.True((bool?)Parser.EvaluateString("not false"));
            Assert.False((bool?)Parser.EvaluateString("not true"));
        }

        #endregion BooleanOps

        #region Null value


        [Fact]
        public void NullValue_NullPlusAnything_EqualsNull()
        {
            Assert.Null((bool?)Parser.EvaluateString("null + 1"));
            Assert.Null((bool?)Parser.EvaluateString("null + 100"));
            Assert.Null((bool?)Parser.EvaluateString("1 + null"));
            Assert.Null((bool?)Parser.EvaluateString("100 + null"));
            Assert.Null((bool?)Parser.EvaluateString("null + null"));
        }

        [Fact]
        public void NullValue_NullMinusAnything_EqualsNull()
        {
            Assert.Null((bool?)Parser.EvaluateString("null - 1"));
            Assert.Null((bool?)Parser.EvaluateString("null - 100"));
            Assert.Null((bool?)Parser.EvaluateString("1 - null"));
            Assert.Null((bool?)Parser.EvaluateString("100 - null"));
            Assert.Null((bool?)Parser.EvaluateString("null - null"));
        }

        [Fact]
        public void NullValue_NullByAnything_EqualsNull()
        {
            Assert.Null((bool?)Parser.EvaluateString("null * 1"));
            Assert.Null((bool?)Parser.EvaluateString("null * 100"));
            Assert.Null((bool?)Parser.EvaluateString("1 * null"));
            Assert.Null((bool?)Parser.EvaluateString("100 * null"));
            Assert.Null((bool?)Parser.EvaluateString("null * null"));

            Assert.Null((bool?)Parser.EvaluateString("null / 1"));
            Assert.Null((bool?)Parser.EvaluateString("null / 100"));
            Assert.Null((bool?)Parser.EvaluateString("1 / null"));
            Assert.Null((bool?)Parser.EvaluateString("100 / null"));
            Assert.Null((bool?)Parser.EvaluateString("null / null"));
        }

        #endregion

        #region String coercions

        [Fact()]
        public void StringCoercions__Var1NullEqualsStringEmpty_EqualsTrue()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", null);
            string expression = "v1 = \"\"";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.True((bool?)result);
        }

        [Fact()]
        public void StringCoercions__Var1TrueStringEqualsTrue_EqualsTrue()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", "True");
            string expression = "v1 = true";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.True((bool?)result);
        }

        [Fact()]
        public void StringCoercions__Var1FalseStringEqualsFalse_EqualsTrue()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("v1", "False");
            string expression = "v1 = false";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.True((bool?)result);
        }

        #endregion String coercions

        #region Exceptions

        [Fact]
        public void Exceptions__HelloAtEnd__UnexpectedCharactersAtEndException()
        {
            try
            {
                string expression = "1 + 1 \"Hello\"";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedCharactersAtEndException>(ex);
            }
        }

        [Fact]
        public void Exceptions__TrueAtEndInsideParens__MissingCloseParenthesisException()
        {
            try
            {
                string expression = "(1+1 true)";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.MissingCloseParenthesisException>(ex);
            }
        }

        [Fact]
        public void Exceptions__TrueAtEndInsideFunctionCall__MissingCloseParenthesisException()
        {
            try
            {
                string expression = "Func(1 true)";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.MissingCloseParenthesisException>(ex);
            }
        }


        [Fact]
        public void Exceptions__EOF__UnexpectedEOFException()
        {
            try
            {
                string expression = "";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedEOFException>(ex);
            }
        }

        [Fact]
        public void Exceptions__Plus__UnexpectedEOFException()
        {
            try
            {
                string expression = "+";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedEOFException>(ex);
            }
        }

        [Fact]
        public void Exceptions__Minus__UnexpectedEOFException()
        {
            try
            {
                string expression = "-";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedEOFException>(ex);
            }
        }

        [Fact]
        public void Exceptions__OpenParens__UnexpectedEOFException()
        {
            try
            {
                string expression = "(";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedEOFException>(ex);
            }
        }

        [Fact]
        public void Exceptions__Comma__UnexpectedTokenException()
        {
            try
            {
                string expression = ",";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedTokenException>(ex);
            }
        }

        [Fact]
        public void Exceptions__Division__UnexpectedTokenException()
        {
            try
            {
                string expression = "/";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedTokenException>(ex);
            }
        }

        [Fact]
        public void Exceptions__Multiply__UnexpectedTokenException()
        {
            try
            {
                string expression = "*";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedTokenException>(ex);
            }
        }

        [Fact]
        public void Exceptions__CloseParens__UnexpectedTokenException()
        {
            try
            {
                string expression = ")";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedTokenException>(ex);
            }
        }

        [Fact]
        public void Exceptions__Parens__UnexpectedTokenException()
        {
            try
            {
                string expression = "()";
                object result = Parser.EvaluateString(expression);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.IsType<Parser.UnexpectedTokenException>(ex);
            }
        }


        #endregion Exceptions

        #region Misc

        [Fact()]
        public void Misc__MixedExpression_EqualsFalse()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("QI_86", null);
            evaluationContex.SetVariable("QI_87", null);
            evaluationContex.SetVariable("QI_104", null);
            string expression = "( QI_86 = 0 Or QI_86 = null ) And ( QI_87 = 0 Or QI_87 = null ) And QI_104 > 0";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.False((bool?)result);
        }

        [Fact()]
        public void Misc__ProductWithStringDecimals_EqualsFalse()
        {
            EvaluationContext evaluationContex = new EvaluationContext();
            evaluationContex.SetVariable("$1109", "1933");
            evaluationContex.SetVariable("$1150", "02.00000");
            string expression = "$1109 * $1150";
            object result = Parser.EvaluateString(expression, evaluationContex);
            Assert.Equal(3866.00000M, result);
        }

        #endregion Misc
    }
}