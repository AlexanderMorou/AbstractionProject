using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// The binary operation associativity.
    /// </summary>
    public enum BinaryOperationAssociativity
    {
        /// <summary>
        /// The binary operation's left side can resolve to the type of the right side and thus
        /// the left side can be null.
        /// </summary>
        Left,
        /// <summary>
        /// The binary operation's right side can resolve to the type of the left side and thus
        /// the right side can be null.
        /// </summary>
        Right
    }

    /// <summary>
    /// Defines generic properties and methods for working
    /// with an expression that contains two leafs delimited 
    /// by an operation (operation not expressly defined 
    /// here).
    /// </summary>
    /// <typeparam name="TLeft">The type of left side of the 
    /// <see cref="IBinaryOperationExpression{TLeft, TRight}"/>.</typeparam>
    /// <typeparam name="TRight">The type of right side of the 
    /// <see cref="IBinaryOperationExpression{TLeft, TRight}"/>.</typeparam>
    public interface IBinaryOperationExpression<TLeft, TRight> :
        IBinaryOperationExpression,
        IExpression
        where TLeft :
            INaryOperandExpression
        where TRight :
            INaryOperandExpression
    {
        /// <summary>
        /// Returns the left side of the <see cref="IBinaryOperationExpression{TLeft, TRight}"/>.
        /// </summary>
        /// <remarks>Can be null if precedence is Left</remarks>
        new TLeft LeftSide { get; set; }

        /// <summary>
        /// Returns the right side of the <see cref="IBinaryOperationExpression{TLeft, TRight}"/>.
        /// </summary>
        /// <remarks>Can be null if precedence is Right</remarks>
        new TRight RightSide { get; set; }
    }

    /// <summary>
    /// Defines properties and methods for working with a binary operation expression.
    /// </summary>
    public interface IBinaryOperationExpression :
        INaryOperandExpression
    {
        /// <summary>
        /// Returns the left side of the <see cref="IBinaryOperationExpression{TLeft, TRight}"/>.
        /// </summary>
        /// <remarks>Can be null if precedence is Left</remarks>
        INaryOperandExpression LeftSide { get; set; }

        /// <summary>
        /// Returns the right side of the <see cref="IBinaryOperationExpression{TLeft, TRight}"/>.
        /// </summary>
        /// <remarks>Can be null if precedence is Right</remarks>
        INaryOperandExpression RightSide { get; set; }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="IBinaryOperationExpression"/>.
        /// </summary>
        BinaryOperationAssociativity Associativity { get; }

        /// <summary>
        /// Returns the <see cref="BinaryOperationKind"/> associated
        /// to the <see cref="IBinaryOperationExpression"/>.
        /// </summary>
        BinaryOperationKind OperationKind { get; }
    }
}
