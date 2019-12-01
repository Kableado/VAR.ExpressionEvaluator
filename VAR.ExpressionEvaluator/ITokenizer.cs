namespace VAR.ExpressionEvaluator
{
    public interface ITokenizer
    {
        Token Token { get; }

        string Text { get; }

        decimal? Number { get; }

        void NextToken();
    }
}