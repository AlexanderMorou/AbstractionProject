
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
    /// The type of inequality operation.
    /// </summary>
    public enum CSharpInequalityOperation
    {
        /// <summary>
        /// The <see cref="ICSharpInequalityExpression"/> is 
        /// merely a single sided term with no operation.
        /// </summary>
        Term,
        /// <summary>
        /// The <see cref="ICSharpInequalityExpression"/> 
        /// represents a comparison of equality.
        /// </summary>
        Equality,
        /// <summary>
        /// The <see cref="ICSharpInequalityExpression"/> 
        /// represents a comparison of inequality.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: "!="
        ///     VB: "&lt;&gt;"
        /// </remarks>
        Inequality
    }
    /// <summary>
    /// Defines properties and methods for working 
    /// with an inequality expression.
    /// </summary>
    /// <remarks>The <see cref="ICSharpInequalityExpression"/> uses/has
    /// <see cref="BinaryOperationAssociativity.Left"/> recursion.</remarks>
    public interface ICSharpInequalityExpression :
        IBinaryOperationExpression<ICSharpInequalityExpression, ICSharpRelationalExpression>,
        ICSharpExpression
    {
        /// <summary>
        /// Returns the <see cref="CSharpInequalityOperation"/> 
        /// defined by the <see cref="ICSharpInequalityExpression"/>.
        /// </summary>
        CSharpInequalityOperation Operation { get; set;  }
    }
}
