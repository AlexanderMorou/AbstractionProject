using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
    internal class LinqTypedJoinBodyBuilder :
        LinqJoinBodyBuilder
    {
        public LinqTypedJoinBodyBuilder(ILinqBodyBuilderParent root, TypedName rangeVariable, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight)
            : base(root, rangeVariable.Name, rangeSource, conditionLeft, conditionRight)
        {
            RangeVariableInfo = rangeVariable;
        }
        public LinqTypedJoinBodyBuilder(ILinqBodyBuilderParent root, TypedName rangeVariable, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight, string intoRangeVariableName)
            : base(root, rangeVariable.Name, rangeSource, conditionLeft, conditionRight, intoRangeVariableName)
        {
            RangeVariableInfo = rangeVariable;
        }

        public TypedName RangeVariableInfo { get; private set; }

    }
}
