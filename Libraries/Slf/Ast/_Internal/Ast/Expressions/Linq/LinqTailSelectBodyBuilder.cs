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

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal class LinqTailSelectBodyBuilder :
        LinqTailBodyBuilder
    {
        public LinqTailSelectBodyBuilder(IExpression selection, ILinqBodyBuilderParent root)
            : base(root)
        {
            this.Selection = selection;
        }
        public IExpression Selection { get; private set; }
    }
}
