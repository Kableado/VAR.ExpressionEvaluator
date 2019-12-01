using System;
using System.IO;

namespace VAR.ExpressionEvaluator
{
    public class Parser
    {
        private ITokenizer _tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public IExpressionNode ParseExpression()
        {
            var expr = ParsePlusAndMinus();

            if (_tokenizer.Token != Token.EOF)
            {
                throw new Exception("Unexpected characters at end of expression");
            }

            return expr;
        }

        public IExpressionNode ParsePlusAndMinus()
        {
            IExpressionNode leftNode = ParseTerminus();
            while (true)
            {
                if (_tokenizer.Token == Token.Plus)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParseTerminus();
                    leftNode = new ExpressionPlusNode(leftNode, rightNode);
                }
                else if (_tokenizer.Token == Token.Minus)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParseTerminus();
                    leftNode = new ExpressionMinusNode(leftNode, rightNode);
                }
                else
                {
                    return leftNode;
                }
            }
        }

        private IExpressionNode ParseTerminus()
        {
            if (_tokenizer.Token == Token.Number)
            {
                var node = new ExpressionNumberNode(_tokenizer.Number ?? 0m);
                _tokenizer.NextToken();
                return node;
            }

            throw new Exception(string.Format("Unexpected token: {0}", _tokenizer.Token.ToString()));
        }

        public static IExpressionNode ParseString(string str)
        {
            TextReader textReader = new StringReader(str);
            ITokenizer tokenizer = new Tokenizer(textReader);
            Parser parser = new Parser(tokenizer);
            return parser.ParseExpression();
        }

        public static object EvaluateString(string str)
        {
            IExpressionNode node = ParseString(str);
            return node.Eval();
        }
    }
}
