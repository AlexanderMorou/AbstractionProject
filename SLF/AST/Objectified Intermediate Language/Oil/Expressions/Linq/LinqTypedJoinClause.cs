using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    public class LinqTypedJoinClause :
        LinqJoinClause,
        ILinqTypedJoinClause
    {
        public LinqTypedJoinClause(TypedName rangeVariable, IExpression rangeSource, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName)
            : this(rangeVariable, rangeSource, leftCondition, rightCondition)
        {
            this.IntoRangeVariableName = intoRangeVariableName;
        }
        public LinqTypedJoinClause(TypedName rangeVariable, IExpression rangeSource, IExpression leftCondition, IExpression rightCondition)
            : base(rangeVariable.Name, rangeSource, leftCondition, rightCondition)
        {
            switch (rangeVariable.Source)
            {
                case TypedNameSource.TypeReference:
                    this.RangeType = rangeVariable.Reference;
                    break;
                case TypedNameSource.SymbolReference:
                    this.RangeType = rangeVariable.SymbolReference.GetSymbolType();
                    break;
                case TypedNameSource.InvalidReference:
                    throw new ArgumentOutOfRangeException("rangeVariable");
                default:
                    break;
            }
        }

        protected LinqTypedJoinClause()
        {
        }

        #region ILinqTypedJoinClause Members

        /// <summary>
        /// Gets/sets the type of element used in 
        /// the range selection.
        /// </summary>
        public IType RangeType { get; set; }

        #endregion


        public override string ToString()
        {
            if (this.RangeType == null)
                return string.Format(CultureInfo.CurrentCulture, "join {1} in {2} on {3} equals {4} into {5}", RangeType, RangeVariableName, RangeSource, LeftSelector, RightSelector, IntoRangeVariableName);
            return string.Format(CultureInfo.CurrentCulture, "join {0} {1} in {2} on {3} equals {4}", RangeType, RangeVariableName, RangeSource, LeftSelector, RightSelector);
        }
        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
