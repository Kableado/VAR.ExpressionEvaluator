using System;
using System.Collections.Generic;
using System.IO;

namespace VAR.ExpressionEvaluator
{
    public class Parser
    {
        #region Custom exceptions

        public class UnexpectedCharactersAtEndException : Exception
        {
            public UnexpectedCharactersAtEndException() : base("Unexpected characters at end of expression") { }
        }

        public class MissingCloseParenthesisException : Exception
        {
            public MissingCloseParenthesisException() : base("Missing close parenthesis") { }
        }

        public class UnexpectedEOFException : Exception
        {
            public UnexpectedEOFException() : base("Unexpected EOF") { }
        }

        public class UnexpectedTokenException : Exception
        {
            public UnexpectedTokenException(string token) : base(string.Format("Unexpected token: {0}", token)) { }
        }

        #endregion Custom exceptions

        #region Creator

        private ITokenizer _tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        #endregion Creator

        #region Parsing methods

        private IExpressionNode ParseBooleanOp()
        {
            if (_tokenizer.Token == Token.Not)
            {
                _tokenizer.NextToken();
                var node = ParseBooleanOp();
                return new ExpressionBooleanNotNode(node);
            }
            IExpressionNode leftNode = ParseRelations();
            while (true)
            {
                if (_tokenizer.Token == Token.And)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParseRelations();
                    leftNode = new ExpressionBooleanAndNode(leftNode, rightNode);
                }
                if (_tokenizer.Token == Token.Or)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParseRelations();
                    leftNode = new ExpressionBooleanOrNode(leftNode, rightNode);
                }
                else
                {
                    return leftNode;
                }
            }
        }

        private IExpressionNode ParseRelations()
        {
            IExpressionNode leftNode = ParsePlusAndMinus();
            while (true)
            {
                if (_tokenizer.Token == Token.Equals || _tokenizer.Token == Token.ExclusiveEquals)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParsePlusAndMinus();
                    leftNode = new ExpressionEqualsNode(leftNode, rightNode);
                }
                if (_tokenizer.Token == Token.ExclusiveEquals)
                {
                    // TODO: Implement ExpressionExclusiveEqualsNode
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParsePlusAndMinus();
                    leftNode = new ExpressionEqualsNode(leftNode, rightNode);
                }
                else if (_tokenizer.Token == Token.NotEquals)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParsePlusAndMinus();
                    leftNode = new ExpressionNotEqualsNode(leftNode, rightNode);
                }
                else if (_tokenizer.Token == Token.LessThan)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParsePlusAndMinus();
                    leftNode = new ExpressionLessThanNode(leftNode, rightNode);
                }
                else if (_tokenizer.Token == Token.LessOrEqualThan)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParsePlusAndMinus();
                    leftNode = new ExpressionLessOrEqualThanNode(leftNode, rightNode);
                }
                else if (_tokenizer.Token == Token.GreaterThan)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParsePlusAndMinus();
                    leftNode = new ExpressionGreaterThanNode(leftNode, rightNode);
                }
                else if (_tokenizer.Token == Token.GreaterOrEqualThan)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParsePlusAndMinus();
                    leftNode = new ExpressionGreaterOrEqualThanNode(leftNode, rightNode);
                }
                else
                {
                    return leftNode;
                }
            }
        }

        private IExpressionNode ParsePlusAndMinus()
        {
            IExpressionNode leftNode = ParseMultiplyDivision();
            while (true)
            {
                if (_tokenizer.Token == Token.Plus)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParseMultiplyDivision();
                    leftNode = new ExpressionPlusNode(leftNode, rightNode);
                }
                else if (_tokenizer.Token == Token.Minus)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rightNode = ParseMultiplyDivision();
                    leftNode = new ExpressionMinusNode(leftNode, rightNode);
                }
                else
                {
                    return leftNode;
                }
            }
        }

        private IExpressionNode ParseMultiplyDivision()
        {
            IExpressionNode lhs = ParseNumericSign();
            while (true)
            {
                if (_tokenizer.Token == Token.Multiply)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rhs = ParseNumericSign();
                    lhs = new ExpressionMultiplyNode(lhs, rhs);

                }
                else if (_tokenizer.Token == Token.Division)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rhs = ParseNumericSign();
                    lhs = new ExpressionDivisionNode(lhs, rhs);
                }
                else
                {
                    return lhs;
                }
            }
        }

        private IExpressionNode ParseNumericSign()
        {
            if (_tokenizer.Token == Token.Plus)
            {
                _tokenizer.NextToken();
                return ParseNumericSign();
            }
            if (_tokenizer.Token == Token.Minus)
            {
                _tokenizer.NextToken();
                var node = ParseNumericSign();
                return new ExpressionNumberNegateNode(node);
            }
            return ParseTerminus();
        }

        private IExpressionNode ParseTerminus()
        {
            if (_tokenizer.Token == Token.Number)
            {
                var node = new ExpressionNumberNode(_tokenizer.Number ?? 0m);
                _tokenizer.NextToken();
                return node;
            }

            if (_tokenizer.Token == Token.String)
            {
                IExpressionNode node = new ExpressionStringNode(_tokenizer.Text);
                _tokenizer.NextToken();
                return node;
            }

            if (_tokenizer.Token == Token.Identifier)
            {
                string identifierToLower = _tokenizer.Text.ToLower();
                if (identifierToLower == "true")
                {
                    _tokenizer.NextToken();
                    return new ExpressionBooleanNode(true);
                }
                if (identifierToLower == "false")
                {
                    _tokenizer.NextToken();
                    return new ExpressionBooleanNode(false);
                }
                if (identifierToLower == "null")
                {
                    _tokenizer.NextToken();
                    return new ExpressionNullNode();
                }

                string identifier = _tokenizer.Text;
                _tokenizer.NextToken();
                if (_tokenizer.Token != Token.ParenthesisStart)
                {
                    IExpressionNode node = new ExpressionVariableNode(identifier);
                    return node;
                }
                else
                {
                    _tokenizer.NextToken();
                    var parameters = new List<IExpressionNode>();
                    while (true)
                    {
                        parameters.Add(ParseBooleanOp());
                        if (_tokenizer.Token == Token.Comma)
                        {
                            _tokenizer.NextToken();
                            continue;
                        }
                        break;
                    }
                    if (_tokenizer.Token != Token.ParenthesisEnd)
                    {
                        throw new MissingCloseParenthesisException();
                    }
                    _tokenizer.NextToken();

                    IExpressionNode node = new ExpressionFunctionNode(identifier, parameters.ToArray());
                    return node;
                }
            }

            if (_tokenizer.Token == Token.ParenthesisStart)
            {
                _tokenizer.NextToken();
                IExpressionNode node = ParseBooleanOp();
                if (_tokenizer.Token != Token.ParenthesisEnd)
                {
                    throw new MissingCloseParenthesisException();
                }
                _tokenizer.NextToken();
                return node;
            }

            if (_tokenizer.Token == Token.EOF)
            {
                throw new UnexpectedEOFException();
            }

            throw new UnexpectedTokenException(_tokenizer.Token.ToString());
        }

        #endregion Parsing methods

        #region Public API

        public IExpressionNode ParseExpression()
        {
            var expr = ParseBooleanOp();

            if (_tokenizer.Token != Token.EOF)
            {
                throw new UnexpectedCharactersAtEndException();
            }

            return expr;
        }

        public static IExpressionNode ParseString(string str)
        {
            TextReader textReader = new StringReader(str);
            ITokenizer tokenizer = new Tokenizer(textReader);
            Parser parser = new Parser(tokenizer);
            return parser.ParseExpression();
        }

        public static object EvaluateString(string str, IEvaluationContext evaluationContext = null)
        {
            IExpressionNode node = ParseString(str);
            return node.Eval(evaluationContext);
        }

        #endregion Public API
    }
}
