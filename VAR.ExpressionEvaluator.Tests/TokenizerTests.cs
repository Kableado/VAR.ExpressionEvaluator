using System.IO;
using Xunit;

namespace VAR.ExpressionEvaluator.Tests
{
    public class TokenizerTests
    {
        [Fact]
        public void Tokenizer__Plus()
        {
            var testString = "10 + 20";
            var t = new Tokenizer(new StringReader(testString));

            // "10"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(10, t.Number);
            t.NextToken();

            // "+"
            Assert.Equal(Token.Plus, t.Token);
            t.NextToken();

            // "20"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(20, t.Number);
            t.NextToken();

            Assert.Equal(Token.EOF, t.Token);
        }

        [Fact]
        public void Tokenizer__PlusMinusAndDecimal()
        {
            var testString = "10 + 20 - 30.123";
            var t = new Tokenizer(new StringReader(testString));

            // "10"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(10, t.Number);
            t.NextToken();

            // "+"
            Assert.Equal(Token.Plus, t.Token);
            t.NextToken();

            // "20"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(20, t.Number);
            t.NextToken();

            // "-"
            Assert.Equal(Token.Minus, t.Token);
            t.NextToken();

            // "20"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(30.123m, t.Number);
            t.NextToken();

            Assert.Equal(Token.EOF, t.Token);
        }

        [Fact]
        public void Tokenizer__SimpleString()
        {
            var testString = "\"Hello World\"";
            var t = new Tokenizer(new StringReader(testString));

            // "Hello World"
            Assert.Equal(Token.String, t.Token);
            Assert.Equal("Hello World", t.Text);
            t.NextToken();

            Assert.Equal(Token.EOF, t.Token);
        }

        [Fact]
        public void Tokenizer__StringWithEscaping()
        {
            var testString = "\"Hello \\\"World\\\"\"";
            var t = new Tokenizer(new StringReader(testString));

            // "Hello \"World\""
            Assert.Equal(Token.String, t.Token);
            Assert.Equal("Hello \"World\"", t.Text);
            t.NextToken();

            Assert.Equal(Token.EOF, t.Token);
        }

        [Fact]
        public void Tokenizer__Identifiers()
        {
            var testString = "null true false _$variable1 $variable2 [;{}#]";
            var t = new Tokenizer(new StringReader(testString));

            // "null"
            Assert.Equal(Token.Identifier, t.Token);
            Assert.Equal("null", t.Text);
            t.NextToken();

            // "true"
            Assert.Equal(Token.Identifier, t.Token);
            Assert.Equal("true", t.Text);
            t.NextToken();

            // "false"
            Assert.Equal(Token.Identifier, t.Token);
            Assert.Equal("false", t.Text);
            t.NextToken();

            // "_$variable1"
            Assert.Equal(Token.Identifier, t.Token);
            Assert.Equal("_$variable1", t.Text);
            t.NextToken();

            // "$variable2"
            Assert.Equal(Token.Identifier, t.Token);
            Assert.Equal("$variable2", t.Text);
            t.NextToken();

            // ";{}#"
            Assert.Equal(Token.Identifier, t.Token);
            Assert.Equal(";{}#", t.Text);
            t.NextToken();

            Assert.Equal(Token.EOF, t.Token);
        }

        [Fact]
        public void Tokenizer__AllTogether()
        {
            var testString = "(10 + 20) * -30.123 + \"Hello \\\"World\\\"\" = false";
            var t = new Tokenizer(new StringReader(testString));

            // "("
            Assert.Equal(Token.ParenthesisStart, t.Token);
            t.NextToken();

            // "10"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(10, t.Number);
            t.NextToken();

            // "+"
            Assert.Equal(Token.Plus, t.Token);
            t.NextToken();

            // "20"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(20, t.Number);
            t.NextToken();

            // ")"
            Assert.Equal(Token.ParenthesisEnd, t.Token);
            t.NextToken();

            // "*"
            Assert.Equal(Token.Multiply, t.Token);
            t.NextToken();

            // "-"
            Assert.Equal(Token.Minus, t.Token);
            t.NextToken();

            // "20"
            Assert.Equal(Token.Number, t.Token);
            Assert.Equal(30.123m, t.Number);
            t.NextToken();

            // "+"
            Assert.Equal(Token.Plus, t.Token);
            t.NextToken();

            // "Hello \"World\""
            Assert.Equal(Token.String, t.Token);
            Assert.Equal("Hello \"World\"", t.Text);
            t.NextToken();

            // "="
            Assert.Equal(Token.Equals, t.Token);
            t.NextToken();

            // "false"
            Assert.Equal(Token.Identifier, t.Token);
            Assert.Equal("false", t.Text);
            t.NextToken();

            Assert.Equal(Token.EOF, t.Token);
        }

        [Fact]
        public void Tokenizer__MoreTokens()
        {
            var testString = "= < > <= >= == === ";
            var t = new Tokenizer(new StringReader(testString));

            // "="
            Assert.Equal(Token.Equals, t.Token);
            t.NextToken();

            // "<"
            Assert.Equal(Token.LessThan, t.Token);
            t.NextToken();

            // ">"
            Assert.Equal(Token.GreaterThan, t.Token);
            t.NextToken();

            // "<="
            Assert.Equal(Token.LessOrEqualThan, t.Token);
            t.NextToken();

            // ">="
            Assert.Equal(Token.GreaterOrEqualThan, t.Token);
            t.NextToken();

            // "=="
            Assert.Equal(Token.Equals, t.Token);
            t.NextToken();

            // "==="
            Assert.Equal(Token.ExclusiveEquals, t.Token);
            t.NextToken();

            Assert.Equal(Token.EOF, t.Token);
        }

    }
}