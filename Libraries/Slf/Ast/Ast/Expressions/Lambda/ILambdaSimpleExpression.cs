using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with a simple lambda 
    /// expression which contains a simplistic expression body.
    /// </summary>
    /// <remarks>
    /// <para>Difference between simple and statement bodies are the 
    /// way returns are handled.  Expression, or simple, lambda expressions
    /// imply return on an expression, whereas in statement bodies it 
    /// is explicitly required.</para>
    /// </remarks>
    public interface ILambdaSimpleExpression :
        ILambdaExpression
    {
        /// <summary>
        /// Returns the <see cref="ILambdaBodySimple"/> that defines the 
        /// <see cref="ILambdaSimpleExpression"/>.
        /// </summary>
        new ILambdaBodySimple Block { get; }

    }
}
