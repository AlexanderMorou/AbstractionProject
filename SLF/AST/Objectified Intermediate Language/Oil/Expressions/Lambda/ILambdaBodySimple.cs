﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// Defines properties and methods for working with the expression
    /// body of a lambda expression.
    /// </summary>
    public interface ILambdaBodySimple :
        ILambdaBody
    {
        /// <summary>
        /// Returns the <see cref="ILambdaSimpleExpression"/> which
        /// parents the current <see cref="ILambdaBodySimple"/>.
        /// </summary>
        new ILambdaSimpleExpression Parent { get; }
        /// <summary>
        /// Returns/sets the <see cref="InnerExpression"/> that defines
        /// the <see cref="ILambdaBodySimple"/>.
        /// </summary>
        IExpression Expression { get; set; }
    }
}