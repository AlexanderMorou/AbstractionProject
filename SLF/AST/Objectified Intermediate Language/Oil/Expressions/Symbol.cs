﻿using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public sealed class Symbol :
        SymbolExpression
    {
        public Symbol(string symbol)
            : base(symbol)
        {
        }

        public Symbol(string symbol, IMemberParentReferenceExpression source)
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
            return new Symbol(symbol);
        }
    }
}