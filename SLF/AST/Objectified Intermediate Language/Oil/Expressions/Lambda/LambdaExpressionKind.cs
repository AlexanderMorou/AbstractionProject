using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Lambda
{
    /// <summary>
    /// The kind of expression the lambda expression is, as far as its
    /// body and parameter type inference goes.
    /// </summary>
    [FlagsAttribute]
    public enum LambdaExpressionKind
    {
        /* 010001 */
        /// <summary>
        /// The types of the lambda expressions parameters are inferred.
        /// </summary>
        /// <remarks>Cannot be mixed with <see cref="Typed"/>.</remarks>
        TypeInferred = 0x11,
        /* 010010 */
        /// <summary>
        /// The types of the lambda expressions parameters are explicitly
        /// stated.
        /// </summary>
        /// <remarks>Cannot be mixed with <see cref="TypeInferred"/>.</remarks>
        Typed = 0x12,
        /* 100100 */
        /// <summary>
        /// The lambda expressions contains a body that a simple 
        /// expression.
        /// </summary>
        /// <remarks>Cannot be mixed with <see cref="StatementBlock"/>.</remarks>
        Simple = 0x24,
        /* 101000 */
        /// <summary>
        /// The lambda expressions contains a body that is a statement 
        /// block.
        /// </summary>
        /// <remarks>Cannot be mixed with <see cref="Simple"/>.</remarks>
        StatementBlock = 0x28,
    }
}
