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
        And,
        Or,
        Comma,
        Identifier,
        String,
        Number,
    }

}
