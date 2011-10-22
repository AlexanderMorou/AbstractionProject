using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The kind of assignment the <see cref="IAssignmentExpression"/> is.
    /// </summary>
    public enum AssignmentOperation
    {
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a simple assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: '='
        ///     VB: '='
        /// </remarks>
        SimpleAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a multiplicitive assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "*="
        ///     VB: Term '=' Term * 
        /// </remarks>
        MultiplicationAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a division assignment.
        /// </summary>
        DivisionAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a modulus assignment.
        /// </summary>
        ModulusAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents an addition assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "+=" 
        ///     VB: Term '=' Term '+'
        /// </remarks>
        AddAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a subtraction assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "-="
        ///     VB: Term '=' Term '-'
        /// </remarks>
        SubtractionAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a left-shift assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "&lt;&lt;="
        /// </remarks>
        LeftShiftAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a right-shift assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "&gt;&gt;="
        ///     VB: Term '=' Term "&gt;&gt;" 
        /// </remarks>
        RightShiftAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a bitwise and assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "&amp;="
        ///     VB: Term '=' Term "And" 
        /// </remarks>
        BitwiseAndAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a bitwise or assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "|="
        ///     VB: Term '=' Term "Or" 
        /// </remarks>
        BitwiseOrAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/>'s operation represents a bitwise exclusive or assignment.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "^="
        ///     VB: Term '=' Term "XOr" 
        /// </remarks>
        BitwiseExclusiveOrAssign,
        /// <summary>
        /// The <see cref="IAssignmentExpression"/> is a non-operational
        /// pass-through affixed to the current precedence for 
        /// pointing purposes.
        /// </summary>
        Term,
    }
    /// <summary>
    /// Defines properties and methods for working with an assignment operation expression.
    /// </summary>
    public interface IAssignmentExpression :
        IBinaryOperationExpression,
        IStatementExpression
    {
        /// <summary>
        /// Returns/sets the kind of assignment associated to the 
        /// <see cref="IAssignmentExpression"/>.
        /// </summary>
        AssignmentOperation Operation { get; set; }
    }
}
