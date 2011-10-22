using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal class LinqLetBodyBuilder :
        LinqBodyBuilderBase
    {
        public LinqLetBodyBuilder(ILinqBodyBuilderParent root, string rangeVariableName, IExpression rangeSource)
            : base(root)
        {
            this.RangeVariableName = rangeVariableName;
            this.RangeSource = rangeSource;
        }

        public string RangeVariableName { get; private set; }

        public IExpression RangeSource { get; private set; }
    }
}
