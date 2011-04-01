using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides language integrated query building aids.
    /// </summary>
    public static class LinqHelper
    {
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> with a 
        /// from clause to root the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        public static ILinqBodyBuilder From(TypedName rangeVariable, IExpression rangeSource)
        {
            return new LinqTypedFromBodyBuilder(rangeVariable, rangeSource);
        }
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> with a 
        /// from clause to root the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSourceName">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        public static ILinqBodyBuilder From(TypedName rangeVariable, string rangeSourceName)
        {
            return new LinqTypedFromBodyBuilder(rangeVariable, (Symbol)rangeSourceName);
        }

        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> with a 
        /// from clause to root the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        public static ILinqBodyBuilder From(string rangeVariableName, IExpression rangeSource)
        {
            return new LinqFromBodyBuilder(rangeVariableName, rangeSource);
        }

        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> with a 
        /// from clause to root the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// referenced in the expression.</param>
        /// <param name="rangeSourceName">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        public static ILinqBodyBuilder From(string rangeVariableName, string rangeSourceName)
        {
            return new LinqFromBodyBuilder(rangeVariableName,(Symbol) rangeSourceName);
        }
    }
}
