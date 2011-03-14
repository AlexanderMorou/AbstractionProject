using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for 
    /// working with a lambda expression.
    /// </summary>
    public interface ILambdaExpression :
        IDelegateReferenceExpression
    {
        /// <summary>
        /// Returns the <see cref="ILambdaBody"/> that defines 
        /// the <see cref="ILambdaExpression"/>.
        /// </summary>
        ILambdaBody Block { get; }
        /// <summary>
        /// Returns the kind of expression the
        /// <see cref="ILambdaExpression"/> is.
        /// </summary>
        LambdaExpressionKind LambdaType { get; }
    }
}