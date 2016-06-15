using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Denotes the kind of binary operation involved.
    /// </summary>
    public enum BinaryOperationKind
    {
        /// <summary>
        /// The operation is a term, the target binary operator
        /// should forward to its default associativity-based term.
        /// </summary>
        Term,
        /// <summary>
        /// The operation is an assignment operation.
        /// </summary>
        Assign,
        /// <summary>
        /// The operation is a multiplication and assignment operation.
        /// </summary>
        AssignMultiply,
        /// <summary>
        /// The operation is a modulus and assignment operation.
        /// </summary>
        AssignModulus,
        /// <summary>
        /// The operation is a division and assignment operation.
        /// </summary>
        AssignDivide,
        /// <summary>
        /// The operation is an addition and assignment operation.
        /// </summary>
        AssignAdd,
        /// <summary>
        /// The operation is a subtraction and assignment operation.
        /// </summary>
        AssignSubtract,
        /// <summary>
        /// The operation is a left-shift and assignment operation.
        /// </summary>
        AssignLeftShift,
        /// <summary>
        /// The operation is a right-shift and assignment operation.
        /// </summary>
        AssignRightShift,
        /// <summary>
        /// The operation is a bitwise-and and assignment operation.
        /// </summary>
        AssignBitwiseAnd,
        /// <summary>
        /// The operation is a bitwise-or and assignment operation.
        /// </summary>
        AssignBitwiseOr,
        /// <summary>
        /// The operation is a bitwise exclusive-or and assignment
        /// operation.
        /// </summary>
        AssignBitwiseExclusiveOr,
        /// <summary>
        /// The operation is a logical or operation.
        /// </summary>
        LogicalOr,
        /// <summary>
        /// The operation is a logical and operation.
        /// </summary>
        LogicalAnd,
        /// <summary>
        /// The operation is a bitwise or operation.
        /// </summary>
        BitwiseOr,
        /// <summary>
        /// The operation is a bitwise exclusive or operation.
        /// </summary>
        BitwiseExclusiveOr,
        /// <summary>
        /// The operation is a bitwise and operation.
        /// </summary>
        BitwiseAnd,
        /// <summary>
        /// The operation is an inequality comparison operation.
        /// </summary>
        Inequality,
        /// <summary>
        /// The operation is an equality comparison operation.
        /// </summary>
        Equality,
        /// <summary>
        /// The operation is a less-than comparison operation.
        /// </summary>
        LessThan,
        /// <summary>
        /// The operation is a less than or equal to comparison operation.
        /// </summary>
        LessThanOrEqualTo,
        /// <summary>
        /// The operation is a greater than comparison operation.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// The operation is a greater than or equal to comparison operation.
        /// </summary>
        GreaterThanOrEqualTo,
        /// <summary>
        /// The operation is a type check comparison operation.
        /// </summary>
        TypeCheck,
        /// <summary>
        /// The operation is a type-cast or yield null comparison operation.
        /// </summary>
        TypeCastOrNull,
        /// <summary>
        /// The operation is a bitwise left-shift operation.
        /// </summary>
        LeftShift,
        /// <summary>
        /// The operation is a bitwise right-shift operation.
        /// </summary>
        RightShift,
        /// <summary>
        /// The operation is an add operation.
        /// </summary>
        Add,
        /// <summary>
        /// The operation is a subtraction operation.
        /// </summary>
        Subtract,
        /// <summary>
        /// The operation is amultiplicitive operation.
        /// </summary>
        Multiply,
        /// <summary>
        /// The operation is a remainder operation.
        /// </summary>
        Modulus,
        /// <summary>
        /// The operation is a strict division operation.
        /// </summary>
        StrictDivision,
        /// <summary>
        /// The operation is an integer division operation.
        /// </summary>
        IntegerDivision,
        /// <summary>
        /// The operation is a flexible division operation.
        /// </summary>
        FlexibleDivision,
    }
}
