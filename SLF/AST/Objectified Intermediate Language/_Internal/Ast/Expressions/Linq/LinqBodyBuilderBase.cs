using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal abstract class LinqBodyBuilderBase :
        ILinqBodyBuilder,
        ILinqBodyBuilderParent
    {
        private ILinqBodyBuilderParent root;

        internal LinqBodyBuilderBase(ILinqBodyBuilderParent root)
        {
            this.root = root;
        }

        internal LinqBodyBuilderBase()
        {

        }
        #region ILinqBodyBuilder Members

        public ILinqBodyBuilder From(TypedName rangeVariable, IExpression rangeSource)
        {
            return new LinqTypedFromBodyBuilder(this, rangeVariable, rangeSource);
        }

        public ILinqBodyBuilder From(string rangeVariableName, IExpression rangeSource)
        {
            return new LinqFromBodyBuilder(this, rangeVariableName, rangeSource);
        }

        public ILinqBodyBuilder Let(string rangeVariableName, IExpression rangeSource)
        {
            return new LinqLetBodyBuilder(this, rangeVariableName, rangeSource);
        }

        public ILinqBodyBuilder Where(IExpression booleanCondition)
        {
            return new LinqWhereBodyBuilder(this, booleanCondition);
        }

        public ILinqBodyBuilder Join(TypedName rangeVariable, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight)
        {
            return new LinqTypedJoinBodyBuilder(this, rangeVariable, rangeSource, conditionLeft, conditionRight);
        }

        public ILinqBodyBuilder Join(string rangeVariableName, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight)
        {
            return new LinqJoinBodyBuilder(this, rangeVariableName, rangeSource, conditionLeft, conditionRight);
        }

        public ILinqBodyBuilder Join(TypedName rangeVariable, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight, string intoRangeName)
        {
            return new LinqTypedJoinBodyBuilder(this, rangeVariable, rangeSource, conditionLeft, conditionRight, intoRangeName);
        }

        public ILinqBodyBuilder Join(string rangeVariableName, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight, string intoRangeName)
        {
            return new LinqJoinBodyBuilder(this, rangeVariableName, rangeSource, conditionLeft, conditionRight, intoRangeName);
        }

        public ILinqOrderedBodyBuilder OrderBy(IExpression orderingKey)
        {
            return new LinqOrderByBodyBuilder(this, orderingKey);
        }

        public ILinqOrderedBodyBuilder OrderBy(IExpression orderingKey, LinqOrderByDirection direction)
        {
            return new LinqDirectedOrderByBodyBuilder(this, orderingKey, direction);
        }

        public ILinqTailBodyBuilder Select(IExpression selection)
        {
            return new LinqTailSelectBodyBuilder(selection, this);
        }

        public ILinqTailBodyBuilder GroupBy(IExpression selection, IExpression key)
        {
            return new LinqTailGroupBodyBuilder(selection, key, this);
        }

        public ILinqBodyBuilder From(TypedName rangeVariable, string rangeSource)
        {
            return this.From(rangeVariable, rangeSource.GetSymbolExpression());
        }

        public ILinqBodyBuilder From(string rangeVariableName, string rangeSource)
        {
            return this.From(rangeVariableName, rangeSource.GetSymbolExpression());
        }

        public ILinqBodyBuilder Join(TypedName rangeVariable, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight)
        {
            return this.Join(rangeVariable, rangeSourceSymbol.GetSymbolExpression(), conditionLeft, conditionRight);
        }

        public ILinqBodyBuilder Join(string rangeVariableName, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight)
        {
            return this.Join(rangeVariableName, rangeSourceSymbol.GetSymbolExpression(), conditionLeft, conditionRight);
        }

        public ILinqBodyBuilder Join(TypedName rangeVariable, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight, string intoRangeName)
        {
            return this.Join(rangeVariable, rangeSourceSymbol.GetSymbolExpression(), conditionLeft, conditionRight, intoRangeName);
        }

        public ILinqBodyBuilder Join(string rangeVariableName, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight, string intoRangeName)
        {
            return this.Join(rangeVariableName, rangeSourceSymbol.GetSymbolExpression(), conditionLeft, conditionRight, intoRangeName);
        }

        public ILinqTailBodyBuilder Select(string symbolSelection)
        {
            return this.Select(symbolSelection.GetSymbolExpression());
        }

        #endregion

        #region ILinqBodyBuilderParent Members

        public ILinqBodyBuilderParent Parent
        {
            get { return this.root; }
        }

        #endregion
    }
}
