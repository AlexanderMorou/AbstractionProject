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
    internal class LinqJoinBodyBuilder :
        LinqBodyBuilderBase
    {
        internal LinqJoinBodyBuilder(ILinqBodyBuilderParent root, string rangeVariableName, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight)
            : base(root)
        {
            this.RangeVariableName = rangeVariableName;
            this.RangeSource = rangeSource;
            this.LeftConditionSelector = conditionLeft;
            this.RightConditionSelector = conditionRight;
        }

        internal LinqJoinBodyBuilder(ILinqBodyBuilderParent root, string rangeVariableName, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight, string intoRangeVariableName)
            : this(root, rangeVariableName, rangeSource, conditionLeft, conditionRight)
        {
            this.IntoTarget = intoRangeVariableName;
        }

        public string RangeVariableName { get; private set; }
        public IExpression RangeSource { get; private set; }
        public IExpression LeftConditionSelector { get; private set; }
        public IExpression RightConditionSelector { get; private set; }
        public string IntoTarget { get; private set; }
    }
}
