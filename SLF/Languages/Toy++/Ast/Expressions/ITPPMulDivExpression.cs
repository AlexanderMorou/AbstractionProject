using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The type of operation to be performed by the <see cref="ITPPMulDivExpression"/>.
    /// </summary>
    public enum TPPMulDivOperation
    {
        /// <summary>
        /// The <see cref="ITPPMulDivExpression"/> represents a multiplication operation.
        /// </summary>
        Multiplication,
        /// <summary>
        /// The <see cref="ITPPMulDivExpression"/> represents a strict division operation.
        /// </summary>
        /// <remarks>This division doesn't assist the process by autocasting the operands.</remarks>
        StrictDivision,
        /// <summary>
        /// The <see cref="ITPPMulDivExpression"/> represents a flexible division operation.
        /// </summary>
        /// <remarks>This division yields a <see cref="Double"/> and casts the operands to the appropriate datatype.</remarks>
        FlexibleDivision,
        /// <summary>
        /// The <see cref="ITPPMulDivExpression"/> represents an integer division operation.
        /// </summary>
        /// <remarks>This division performs an integer division regardless of the operand data types.</remarks>
        IntegerDivision,
        /// <summary>
        /// The <see cref="ITPPMulDivExpression"/> represents a division operation in which the remainder is
        /// returned.
        /// </summary>
        /// <remarks>Also known as 'modulus'.</remarks>
        Remainder,
        /// <summary>
        /// The <see cref="ITPPMulDivExpression"/> represents a non-operational term as a pointer to a 
        /// <see cref="ITPPUnaryOperationExpression"/>
        /// </summary>
        Term,
    }
    public interface ITPPMulDivExpression :
        IBinaryOperationExpression<ITPPMulDivExpression, ITPPUnaryOperationExpression>,
        ITPPExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="TPPMulDivOperation"/> represented by the <see cref="ITPPMulDivExpression"/>
        /// </summary>
        TPPMulDivOperation Operation { get; set; }
    }
}
