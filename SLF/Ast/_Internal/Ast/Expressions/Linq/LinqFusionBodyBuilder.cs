using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal class LinqFusionBodyBuilder :
        LinqBodyBuilderBase,
        ILinqFusionBodyBuilder
    {
        public LinqFusionBodyBuilder(ILinqBodyBuilderParent root, string intoRangeName)
             : base(root)
        {
            this.IntoRangeName = intoRangeName;
        }

        public String IntoRangeName { get; private set; }
    }
}
