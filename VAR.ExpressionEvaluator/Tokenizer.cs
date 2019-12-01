using System.Globalization;
using System.IO;
using System.Text;

namespace VAR.ExpressionEvaluator
{
    public enum Token
    {
        EOF,
        Plus,
        Minus,
        Division,
        Multiply,
        Equals,
        ParentesisStart,
        ParentesisEnd,
        Keyword,
        String,
        Number,
    }

    public class Tokenizer
    {
        private TextReader _reader;
        private int _currentPosition = 0;
        private char _currentChar;
        private Token _currentToken;
        private string _text;
        private decimal? _number;


        public Tokenizer(TextReader reader)
        {
            _reader = reader;
            _currentPosition = -1;
            NextChar();
            NextToken();
        }

        public Token Token
        {
            get { return _currentToken; }
        }

        public string Text
        {
            get { return _text; }
        }

        public decimal? Number
        {
            get { return _number; }
        }

        private void NextChar()
        {
            int ch = _reader.Read();
            if (ch < 0)
            {
                _currentChar = '\0';
                return;
            }
            _currentChar = (char)ch;
            _currentPosition++;
        }

        private void SkipWhite()
        {
            while (char.IsWhiteSpace(_currentChar))
            {
                NextChar();
            }
        }

        public void NextToken()
        {
            _currentToken = Token.EOF;
            _text = null;
            _number = null;

            SkipWhite();

            // Special characters
            switch (_currentChar)
            {
                case '\0':
                    return;

                case '+':
                    NextChar();
                    _currentToken = Token.Plus;
                    return;

                case '-':
                    NextChar();
                    _currentToken = Token.Minus;
                    return;

                case '/':
                    NextChar();
                    _currentToken = Token.Division;
                    return;

                case '*':
                    NextChar();
                    _currentToken = Token.Multiply;
                    return;

                case '(':
                    NextChar();
                    _currentToken = Token.ParentesisStart;
                    return;

                case ')':
                    NextChar();
                    _currentToken = Token.ParentesisEnd;
                    return;

                case '=':
                    NextChar();
                    _currentToken = Token.Equals;
                    return;
            }

            // Keywords
            if (char.IsLetter(_currentChar))
            {
                var sb = new StringBuilder();
                while (char.IsLetterOrDigit(_currentChar) || _currentChar == '_')
                {
                    sb.Append(_currentChar);
                    NextChar();
                    if (_currentChar == '\0') { break; }
                }
                _text = sb.ToString();
                _currentToken = Token.Keyword;
                return;
            }

            // String
            if (_currentChar == '"' || _currentChar == '\'')
            {
                char stringEndsWith = _currentChar;
                NextChar();
                StringBuilder sbString = new StringBuilder();
                while (_currentChar != stringEndsWith && _currentChar != '\0')
                {
                    if (_currentChar != '\\')
                    {
                        sbString.Append(_currentChar);
                    }
                    else
                    {
                        NextChar();
                        if (_currentChar == '\\')
                        {
                            sbString.Append('\\');
                        }
                        else if (_currentChar == 't')
                        {
                            sbString.Append('\t');
                        }
                        else if (_currentChar == 'n')
                        {
                            sbString.Append('\n');
                        }
                        else
                        {
                            // FIXME: Other escaped characters
                            sbString.Append(_currentChar);
                        }
                    }
                    NextChar();
                }
                NextChar();
                _text = sbString.ToString();
                _currentToken = Token.String;
                return;
            }

            // Numbers
            if (char.IsDigit(_currentChar) || _currentChar == '.')
            {
                var sbNumber = new StringBuilder();
                bool haveDecimalPoint = false;
                while (char.IsDigit(_currentChar) || (!haveDecimalPoint && _currentChar == '.'))
                {
                    sbNumber.Append(_currentChar);
                    haveDecimalPoint = _currentChar == '.';
                    NextChar();
                }
                _number = decimal.Parse(sbNumber.ToString(), CultureInfo.InvariantCulture);
                _currentToken = Token.Number;
                return;
            }

            throw new InvalidDataException(string.Format("Unexpected character: {0} at {1}", _currentChar, _currentPosition));
        }
    }
}
