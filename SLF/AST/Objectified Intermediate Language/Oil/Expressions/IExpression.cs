using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Defines properties and methods for working with an expression.
    /// </summary>
    public interface IExpression :
        ISourceElement
    {
        /// <summary>
        /// Returns the type of expression the <see cref="IExpression"/> is.
        /// </summary>
        ExpressionKinds Type { get; }
        /// <summary>
        /// Visits the elements of the <see cref="IExpression"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/>
        /// to which the <see cref="IExpression"/> needs to repay the visit
        /// to.</param>
        void Visit(IExpressionVisitor visitor);
    }
}
