namespace VAR.ExpressionEvaluator
{
    public enum Token
    {
        EOF,
        Plus,
        Minus,
        Division,
        Multiply,
        ParenthesisStart,
        ParenthesisEnd,
        Not,
        NotEquals,
        Equals,
        ExclusiveEquals,
        GreaterThan,
        GreaterOrEqualThan,
        LessThan,
        LessOrEqualThan,
        Comma,
        Identifier,
        String,
        Number,
    }

}
