using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// The type of operation the <see cref="ICSharpAddSubtExpression"/> is.
    /// </summary>
    public enum CSharpAddSubtOperation
    {
        /// <summary>
        /// The <see cref="ICSharpAddSubtExpression"/> is a non-operational term that references
        /// the <see cref="ICSharpMulDivExpression"/>.
        /// </summary>
        Term,
        /// <summary>
        /// The <see cref="ICSharpAddSubtExpression"/> is an addition operation.
        /// </summary>
        Addition,
        /// <summary>
        /// The <see cref="ICSharpAddSubtExpression"/> is an subtraction operation.
        /// </summary>
        Subtraction
    }
    /// <summary>
    /// Defines properties and methods for working with an addition or subtraction operation expression.
    /// </summary>
    public interface ICSharpAddSubtExpression :
        IBinaryOperationExpression<ICSharpAddSubtExpression, ICSharpMulDivExpression>,
        ICSharpExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="CSharpAddSubtOperation"/> the <see cref="ICSharpAddSubtExpression"/>
        /// represents.
        /// </summary>
        CSharpAddSubtOperation Operation { get; set; }
    }
}
