using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    /* *
     * Provides a LINQ from-clause body builder which stores the range-variable and
     * range-source information for the final query build relative to a given
     * body's clauses.
     * */
    internal class LinqTypedFromBodyBuilder :
        LinqFromBodyBuilder
    {
        public LinqTypedFromBodyBuilder(TypedName rangeVariable, IExpression rangeSource)
            : base(rangeVariable.Name, rangeSource)
        {
            RangeVariableInfo = rangeVariable;
        }

        public LinqTypedFromBodyBuilder(ILinqBodyBuilderParent root, TypedName rangeVariable, IExpression rangeSource)
            : base(root, rangeVariable.Name, rangeSource)
        {
            RangeVariableInfo = rangeVariable;
        }

        public TypedName RangeVariableInfo { get; private set; }
    }
}
