﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    public abstract class CSharpExpressionBase :
        ExpressionBase
    {
        /// <summary>
        /// Creates a new <see cref="CSharpExpressionBase"/> instance
        /// initialized to a default state.
        /// </summary>
        protected CSharpExpressionBase()
            : base()
        {

        }
    }
}
