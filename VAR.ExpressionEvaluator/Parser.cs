﻿using System;
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
            IExpressionNode lhs = ParseUnary();
            while (true)
            {
                if (_tokenizer.Token == Token.Multiply)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rhs = ParseUnary();
                    lhs = new ExpressionMultiplyNode(lhs, rhs);

                }
                else if (_tokenizer.Token == Token.Division)
                {
                    _tokenizer.NextToken();
                    IExpressionNode rhs = ParseUnary();
                    lhs = new ExpressionDivisionNode(lhs, rhs);
                }
                else
                {
                    return lhs;
                }
            }
        }

        private IExpressionNode ParseUnary()
        {
            if (_tokenizer.Token == Token.Plus)
            {
                _tokenizer.NextToken();
                return ParseUnary();
            }
            if (_tokenizer.Token == Token.Minus)
            {
                _tokenizer.NextToken();
                var node = ParseUnary();
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

            if (_tokenizer.Token == Token.ParenthesisStart)
            {
                _tokenizer.NextToken();
                var node = ParsePlusAndMinus();
                if (_tokenizer.Token != Token.ParenthesisEnd)
                {
                    throw new Exception("Missing close parenthesis");
                }
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
