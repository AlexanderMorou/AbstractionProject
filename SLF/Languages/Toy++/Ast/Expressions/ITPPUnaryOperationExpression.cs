using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The type of unary operation.
    /// </summary>
    /// <remarks><see cref="TPPUnaryOperationFlags.PreAction"/> and
    /// <see cref="TPPUnaryOperationFlags.PostAction"/> cannot be applied together;
    /// <see cref="TPPUnaryOperationFlags.Increment"/> and <see cref="TPPUnaryOperationFlags.Decrement"/>
    /// cannot be applied together; and
    /// <see cref="TPPUnaryOperationFlags.Negate"/> and <see cref="TPPUnaryOperationFlags.Invert"/> cannot
    /// be applied together.</remarks>
    [FlagsAttribute]
    public enum TPPUnaryOperationFlags :
        byte
    {
        /// <summary>
        /// No unary operation is performed, the <see cref="ICStarUnaryOperationExpression"/> is merely
        /// a forward for the <see cref="TPPUnaryOperationFlags.Term"/>.
        /// </summary>
        None = 0,
        /// <summary>
        /// The unary operation performed is handled before the value is retrieved; thus,
        /// the result of the increment or decrement will be part of the result.
        /// </summary>
        PreAction = 1,
        /// <summary>
        /// The unary operation performed is handled after the value is retrieved; thus
        /// the result will be the value prior to the increment or decrement.
        /// </summary>
        PostAction = 3,
        /// <summary>
        /// The unary operation performed is an increment operation.
        /// </summary>
        /// <remarks><see cref="Increment"/> and <see cref="Decrement"/>
        /// cannot be applied in tandem.</remarks>
        Increment = 4,
        /// <summary>
        /// The unary operation performed is a decrement operation.
        /// </summary>
        /// <remarks><see cref="Decrement"/> and <see cref="Increment"/> 
        /// cannot be applied in tandem.</remarks>
        Decrement = 12,
        /// <summary>
        /// The unary operation performed negates the value of the operand.
        /// </summary>
        /// <remarks><see cref="Negate"/> and <see cref="Invert"/> cannot
        /// be applied in tandem.</remarks>
        Negate = 16,
        /// <summary>
        /// The unary operation perfromed inverts the bits of the operand.
        /// </summary>
        /// <remarks><see cref="Invert"/> and <see cref="Negate"/> cannot 
        /// be applied in tandem.</remarks>
        Invert = 48,
    }
    /// <summary>
    /// Defines properties and methods for working with a unary operation in the 
    /// T*y++ language.
    /// </summary>
    public interface ITPPUnaryOperationExpression :
        ITPPExpression,
        IUnaryOperationExpression
    {
        /// <summary>
        /// Returns/sets the unary operation to be performed on the <see cref="Term"/>.
        /// </summary>
        TPPUnaryOperationFlags Operation { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IUnaryOperationPrimaryTerm"/> that the <see cref="ITPPUnaryOperationExpression"/>
        /// operates on.
        /// </summary>
        IUnaryOperationPrimaryTerm Term { get; set; }
    }
}
