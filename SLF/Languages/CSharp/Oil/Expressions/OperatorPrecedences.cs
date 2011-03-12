using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The mathematical (or evaluational) precedence 
    /// yielded by a given <see cref="IExpression"/>.
    /// </summary>
    public enum CSharpOperatorPrecedences
    {
        /// <summary>
        /// The <see cref="IExpression"/> cannot be
        /// used in a mathematical expression.
        /// </summary>
        NoPrecedence = -1,
        /// <summary>
        /// The <see cref="IExpression"/> is an assignment
        /// expression.
        /// </summary>
        AssignmentOperation = 0,
        /// <summary>
        /// The <see cref="IExpression"/> is a ternary conditional
        /// expression.
        /// </summary>
        ConditionalOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a logical or
        /// expression.
        /// </summary>
        LogicalOrOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a logical
        /// and expression.
        /// </summary>
        LogicalAndOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a bitwise
        /// or expression.
        /// </summary>
        BitwiseOrOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a bitwise
        /// exclusive expression.
        /// </summary>
        BitwiseExclusiveOrOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a bitwise
        /// and expression.
        /// </summary>
        BitwiseAndOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is an [in]equality
        /// expression.
        /// </summary>
        InequalityOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a relational
        /// check expression.
        /// </summary>
        RelationalOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a bit-shift
        /// expression.
        /// </summary>
        ShiftOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is an 
        /// addition or subtraction expression.
        /// </summary>
        AddSubtOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a multiplication,
        /// division, or modulus (remainder) expression.
        /// </summary>
        MulDivOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a unary expression.
        /// </summary>
        UnaryOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is the term of a
        /// <see cref="UnaryOperation"/>.
        /// </summary>
        UnaryTerm,
    }
}
