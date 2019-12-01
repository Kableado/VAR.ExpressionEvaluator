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
        ExclusiveEquals,
        GreaterThan,
        GreaterOrEqualThan,
        LessThan,
        LessOrEqualThan,
        ParenthesisStart,
        ParenthesisEnd,
        Keyword,
        String,
        Number,
    }

}
