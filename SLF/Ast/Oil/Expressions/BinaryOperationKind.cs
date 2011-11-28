using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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
