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
    internal class LinqTailGroupBodyBuilder :
        LinqTailSelectBodyBuilder
    {
        public LinqTailGroupBodyBuilder(IExpression selection, IExpression key, ILinqBodyBuilderParent root)
            : base(selection, root)
        {
            this.Key = key;
        }

        public IExpression Key { get; private set; }
    }
}
