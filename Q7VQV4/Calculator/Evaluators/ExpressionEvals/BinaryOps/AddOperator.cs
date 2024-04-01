using Calculator.Syntax.Tokens;

namespace Calculator.Evaluators.ExpressionEvals.BinaryOps;

[BinaryOp(typeof(AddToken))]
public class AddOperator : IBinaryOperator
{
    public object Evaluate(object? leftValue, object? rightValue)
    {
        if (leftValue is not double left || rightValue is not double right)
        {
            throw new TypeException("Add operator requires both operands to be numbers.");
        }

        return left + right;
    }
}
