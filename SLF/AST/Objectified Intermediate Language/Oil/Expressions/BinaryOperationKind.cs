using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public enum BinaryOperationKind
    {
        Term,
        Assign,
        AssignMultiply,
        AssignModulus,
        AssignDivide,
        AssignAdd,
        AssignSubtract,
        AssignLeftShift,
        AssignRightShift,
        AssignBitwiseAnd,
        AssignBitwiseOr,
        AssignBitwiseExclusiveOr,
        LogicalOr,
        LogicalAnd,
        BitwiseOr,
        BitwiseExclusiveOr,
        BitwiseAnd,
        Inequality,
        Equality,
        LessThan,
        LessThanOrEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        TypeCheck,
        TypeCastOrNull,
        LeftShift,
        RightShift,
        Add,
        Subtract,
        Multiply,
        Modulus,
        StrictDivision,
        IntegerDivision,
        FlexibleDivision,
    }
}
