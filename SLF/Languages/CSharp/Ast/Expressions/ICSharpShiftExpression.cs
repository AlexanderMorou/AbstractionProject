using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public enum CSharpShiftOperation
    {
        /// <summary>
        /// The <see cref="ICSharpShiftExpression"/> is not a binary operation but rather a non-operational
        /// term which points to a <see cref="ICSharpAddSubtExpression"/>.
        /// </summary>
        Term,
        /// <summary>
        /// The <see cref="ICSharpShiftExpression"/> represents a left-shift operation.
        /// </summary>
        LeftShift,
        /// <summary>
        /// The <see cref="ICSharpShiftExpression"/> represents a right-shift operation.
        /// </summary>
        RightShift
    }
    /// <summary>
    /// Defines properties and methods for working with a shift expression.
    /// </summary>
    public interface ICSharpShiftExpression :
        IBinaryOperationExpression<ICSharpShiftExpression, ICSharpAddSubtExpression>,
        ICSharpExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="CSharpShiftOperation"/> associated to the <see cref="ICSharpShiftExpression"/>.
        /// </summary>
        CSharpShiftOperation Operation { get; set; }
    }
}
