using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public sealed class Symbol :
        SymbolExpression
    {
        internal Symbol(string symbol)
            : base(symbol)
        {
        }

        internal Symbol(string symbol, IMemberParentReferenceExpression source)
            : base(symbol, source)
        {
        }

        /// <summary>
        /// Converts the <paramref name="symbol"/> into a symbol expression
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static implicit operator Symbol(string symbol)
        {
            return (Symbol)(symbol.GetSymbolExpression());
        }
    }
}
