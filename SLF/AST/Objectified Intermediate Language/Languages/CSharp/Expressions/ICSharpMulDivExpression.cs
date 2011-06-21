using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// The type of operation to be performed by the <see cref="ICSharpMulDivExpression"/>.
    /// </summary>
    public enum CSharpMulDivOperation
    {
        /// <summary>
        /// The <see cref="ICSharpMulDivExpression"/> represents a multiplication operation.
        /// </summary>
        Multiplication,
        /// <summary>
        /// The <see cref="ICSharpMulDivExpression"/> represents a strict division operation.
        /// </summary>
        Division,
        /// <summary>
        /// The <see cref="ICSharpMulDivExpression"/> represents a division operation in which the remainder is
        /// returned.
        /// </summary>
        /// <remarks>Also known as 'modulus'.</remarks>
        Remainder,
        /// <summary>
        /// The <see cref="ICSharpMulDivExpression"/> represents a non-operational term as a pointer to a 
        /// <see cref="IUnaryOperationExpression"/>
        /// </summary>
        Term,
    }
    /// <summary>
    /// Defines properties and methods for working with a multiplication, division or division remainder
    /// operation expression.
    /// </summary>
    public interface ICSharpMulDivExpression :
        IBinaryOperationExpression<ICSharpMulDivExpression, IUnaryOperationExpression>,
        ICSharpExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="CSharpMulDivOperation"/> represented by the <see cref="ICSharpMulDivExpression"/>
        /// </summary>
        CSharpMulDivOperation Operation { get; set; }
    }
}
