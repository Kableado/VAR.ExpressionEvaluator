using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace VAR.ExpressionEvaluator.Tests
{
    [TestClass()]
    public class TokenizerTests
    {
        [TestMethod()]
        public void Tokenizer__Plus()
        {
            var testString = "10 + 20";
            var t = new Tokenizer(new StringReader(testString));

            // "10"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 10);
            t.NextToken();

            // "+"
            Assert.AreEqual(t.Token, Token.Plus);
            t.NextToken();

            // "20"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 20);
            t.NextToken();

            Assert.AreEqual(t.Token, Token.EOF);
        }

        [TestMethod()]
        public void Tokenizer__PlusMinusAndDecimal()
        {
            var testString = "10 + 20 - 30.123";
            var t = new Tokenizer(new StringReader(testString));

            // "10"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 10);
            t.NextToken();

            // "+"
            Assert.AreEqual(t.Token, Token.Plus);
            t.NextToken();

            // "20"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 20);
            t.NextToken();

            // "-"
            Assert.AreEqual(t.Token, Token.Minus);
            t.NextToken();

            // "20"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 30.123m);
            t.NextToken();

            Assert.AreEqual(t.Token, Token.EOF);
        }

        [TestMethod()]
        public void Tokenizer__SimpleString()
        {
            var testString = "\"Hello World\"";
            var t = new Tokenizer(new StringReader(testString));

            // "Hello World"
            Assert.AreEqual(t.Token, Token.String);
            Assert.AreEqual(t.Text, "Hello World");
            t.NextToken();

            Assert.AreEqual(t.Token, Token.EOF);
        }

        [TestMethod()]
        public void Tokenizer__StringWithEscaping()
        {
            var testString = "\"Hello \\\"World\\\"\"";
            var t = new Tokenizer(new StringReader(testString));

            // "Hello \"World\""
            Assert.AreEqual(t.Token, Token.String);
            Assert.AreEqual(t.Text, "Hello \"World\"");
            t.NextToken();

            Assert.AreEqual(t.Token, Token.EOF);
        }

        [TestMethod()]
        public void Tokenizer__Keywords()
        {
            var testString = "null true false";
            var t = new Tokenizer(new StringReader(testString));

            // "null"
            Assert.AreEqual(t.Token, Token.Keyword);
            Assert.AreEqual(t.Text, "null");
            t.NextToken();

            // "true"
            Assert.AreEqual(t.Token, Token.Keyword);
            Assert.AreEqual(t.Text, "true");
            t.NextToken();

            // "false"
            Assert.AreEqual(t.Token, Token.Keyword);
            Assert.AreEqual(t.Text, "false");
            t.NextToken();

            Assert.AreEqual(t.Token, Token.EOF);
        }

        [TestMethod()]
        public void Tokenizer__AllTogether()
        {
            var testString = "(10 + 20) * -30.123 + \"Hello \\\"World\\\"\" = false";
            var t = new Tokenizer(new StringReader(testString));

            // "("
            Assert.AreEqual(t.Token, Token.ParentesisStart);
            t.NextToken();

            // "10"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 10);
            t.NextToken();

            // "+"
            Assert.AreEqual(t.Token, Token.Plus);
            t.NextToken();

            // "20"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 20);
            t.NextToken();

            // ")"
            Assert.AreEqual(t.Token, Token.ParentesisEnd);
            t.NextToken();

            // "*"
            Assert.AreEqual(t.Token, Token.Multiply);
            t.NextToken();

            // "-"
            Assert.AreEqual(t.Token, Token.Minus);
            t.NextToken();

            // "20"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 30.123m);
            t.NextToken();

            // "+"
            Assert.AreEqual(t.Token, Token.Plus);
            t.NextToken();

            // "Hello \"World\""
            Assert.AreEqual(t.Token, Token.String);
            Assert.AreEqual(t.Text, "Hello \"World\"");
            t.NextToken();

            // "="
            Assert.AreEqual(t.Token, Token.Equals);
            t.NextToken();

            // "false"
            Assert.AreEqual(t.Token, Token.Keyword);
            Assert.AreEqual(t.Text, "false");
            t.NextToken();

            Assert.AreEqual(t.Token, Token.EOF);
        }

        [TestMethod()]
        public void Tokenizer__MoreTokens()
        {
            var testString = "= < > <= >= == === ";
            var t = new Tokenizer(new StringReader(testString));

            // "="
            Assert.AreEqual(t.Token, Token.Equals);
            t.NextToken();

            // "<"
            Assert.AreEqual(t.Token, Token.LessThan);
            t.NextToken();

            // ">"
            Assert.AreEqual(t.Token, Token.GreaterThan);
            t.NextToken();

            // "<="
            Assert.AreEqual(t.Token, Token.LessOrEqualThan);
            t.NextToken();

            // ">="
            Assert.AreEqual(t.Token, Token.GreaterOrEqualThan);
            t.NextToken();

            // "=="
            Assert.AreEqual(t.Token, Token.Equals);
            t.NextToken();

            // "==="
            Assert.AreEqual(t.Token, Token.ExclusiveEquals);
            t.NextToken();

            Assert.AreEqual(t.Token, Token.EOF);
        }

    }
}