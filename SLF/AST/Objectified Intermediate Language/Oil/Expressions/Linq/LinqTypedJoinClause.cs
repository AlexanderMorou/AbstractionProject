using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private IType rangeType;

        public LinqTypedJoinClause(TypedName rangeVariable, IExpression rangeSource, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName)
            : base(rangeVariable.Name, rangeSource, leftCondition, rightCondition, intoRangeVariableName)
        {
            SetRangeVariableType(rangeVariable);
        }
        public LinqTypedJoinClause(TypedName rangeVariable, IExpression rangeSource, IExpression leftCondition, IExpression rightCondition)
            : base(rangeVariable.Name, rangeSource, leftCondition, rightCondition)
        {
            SetRangeVariableType(rangeVariable);
        }

        private void SetRangeVariableType(TypedName rangeVariable)
        {
            switch (rangeVariable.Source)
            {
                case TypedNameSource.TypeReference:
                    this.rangeType = rangeVariable.Reference;
                    break;
                case TypedNameSource.SymbolReference:
                    this.rangeType = rangeVariable.SymbolReference.GetSymbolType();
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

        /// <summary>
        /// Creates the <see cref="RangeVariable"/> as defined on the parent.
        /// </summary>
        /// <param name="rangeVariableName">The <see cref="String"/> value representing
        /// the name of the range variable.</param>
        /// <returns>A new <see cref="LinqTypedRangeVariable"/> with the
        /// <paramref name="rangeVariableName"/> provided.</returns>
        protected override ILinqRangeVariable CreateRangeVariable(string rangeVariableName)
        {
            return new LinqTypedRangeVariable(this, rangeVariableName, this.rangeType);
        }

        public override string ToString()
        {
            if (this.IntoRangeVariable != null)
                return string.Format(CultureInfo.CurrentCulture, "join {0} {1} in {2} on {3} equals {4} into {5}", this.RangeVariable.RangeType, this.RangeVariable.Name, RangeSource, LeftSelector, RightSelector, this.IntoRangeVariable.Name);
            return string.Format(CultureInfo.CurrentCulture, "join {0} {1} in {2} on {3} equals {4}", this.RangeVariable.RangeType, this.RangeVariable.Name, RangeSource, LeftSelector, RightSelector);
        }
        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region ILinqTypedJoinClause Members

        public new ILinqTypedRangeVariable RangeVariable
        {
            get { return ((ILinqTypedRangeVariable)base.RangeVariable); }
        }

        #endregion
    }
}
