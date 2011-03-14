using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides a base class for a typed from clause.
    /// </summary>
    public class LinqTypedFromClause :
        LinqFromClause,
        ILinqTypedFromClause
    {
        private IType rangeType;

        /// <summary>
        /// Creates a new <see cref="LinqTypedFromClause"/> which has
        /// an explicit type referenced to its <paramref name="rangeVariable"/>.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> denoting the name
        /// and type of the range variable for the from clause.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which
        /// denotes where to obtain </param>
        public LinqTypedFromClause(TypedName rangeVariable, IExpression rangeSource)
            : base(rangeVariable.Name, rangeSource)
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
        /// <summary>
        /// Obtains a <see cref="String"/> representing the current
        /// <see cref="LinqTypedFromClause"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the current
        /// <see cref="LinqTypedFromClause"/>.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "from {0} {1} in {2}", this.RangeVariable.RangeType.FullName, this.RangeVariable.Name, this.RangeSource);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Creates the <see cref="RangeVariable"/> as defined on the parent.
        /// </summary>
        /// <param name="rangeVariableName">The <see cref="String"/> value representing
        /// the name of the range variable.</param>
        /// <returns>A new <see cref="LinqTypedRangeVariable"/> with the
        /// <paramref name="rangeVariableName"/> provided.</returns>
        protected override sealed ILinqRangeVariable CreateRangeVariable(string rangeVariableName)
        {
            return new LinqTypedRangeVariable(this, rangeVariableName, this.rangeType);
        }

        #region ILinqTypedFromClause Members

        public new ILinqTypedRangeVariable RangeVariable
        {
            get { return (ILinqTypedRangeVariable)base.RangeVariable; }
        }

        #endregion
    }
}
