﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    public interface ILinqOrderedBodyBuilder :
        ILinqBodyBuilder
    {
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/>
        /// which furthers the language integrated query build with an
        /// order by clause injected into the query expression.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/> on which
        /// to order the resulted series.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqOrderedBodyBuilder ThenBy(IExpression orderingKey);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which furthers
        /// the language integrated query build with an orderby clause injected
        /// into the query expression.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/> on which
        /// to order the resulted series.</param>
        /// <param name="direction">The <see cref="LinqOrderByDirection"/>
        /// which denotes the specific direction the ordering coerces
        /// the output data set.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqOrderedBodyBuilder ThenBy(IExpression orderingKey, LinqOrderByDirection direction);
    }
}
