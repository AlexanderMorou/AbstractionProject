using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The type of unary operation.
    /// </summary>
    [FlagsAttribute]
    public enum UnaryOperation
    {
        /// <summary>
        /// No unary operation is used, the unary operation
        /// falls through to the <see cref="IUnaryOperationExpression.Term"/>.
        /// </summary>
        None = 0x0,
        /* 000000011110000001 */
        /// <summary>
        /// The results of the operation are passed to the lower-order
        /// expressions.
        /// </summary>
        PreAction = 0x781,
        /* 000011100010000010 */
        /// <summary>
        /// The target of the operation is passed to the lower-order 
        /// expressions and then the operation is applied to the 
        /// target.
        /// </summary>
        PostAction = 0x3882,
        /* 000100000000000100 */
        /// <summary>
        /// The target of the operation is decremented by one.
        /// </summary>
        Increment = 0x4004,
        /* 001000000000001000 */
        /// <summary>
        /// The target of the operation is incremented by one.
        /// </summary>
        Decrement = 0x8008,
        /* 010000100100010000 */
        /// <summary>
        /// The sign of the target is inverted.
        /// </summary>
        /// <remarks>Typically for signed integers of varied sizes, or
        /// types with the unary '-' operator overloaded.</remarks>
        SignInversion = 0x10910,
        /* 111101001000100000 */
        /// <summary>
        /// The target of the operation is assumed a
        /// boolean value and inverted; from 
        /// false-&gt;true, and true-&gt;false, respectively.
        /// </summary>
        BooleanInversion = 0x3d220,
        /* 100010010001000000 */
        /// <summary>
        /// The target of the operation is bitwise inverted.
        /// </summary>
        BitwiseInversion = 0x22440,
    }
    /// <summary>
    /// Defines properties and methods for working with a unary operation.
    /// </summary>
    public interface IUnaryOperationExpression :
        INaryOperandExpression,
        IStatementExpression
    {
        /// <summary>
        /// Returns/sets the unary operation to be performed on the <see cref="Term"/>.
        /// </summary>
        UnaryOperation Operation { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IUnaryOperationPrimaryTerm"/>
        /// to be used as either a forward or as the target
        /// of the operation.
        /// </summary>
        IUnaryOperationPrimaryTerm Term { get; set; }
    }

}
