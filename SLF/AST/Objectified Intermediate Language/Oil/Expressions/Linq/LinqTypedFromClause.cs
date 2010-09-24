using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
        /// <summary>
        /// Gets/sets the type of element used in 
        /// the range selection.
        /// </summary>
        public IType RangeType { get; set; }
        /// <summary>
        /// Obtains a <see cref="String"/> representing the current
        /// <see cref="LinqTypedFromClause"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the current
        /// <see cref="LinqTypedFromClause"/>.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "from {0} {1} in {2}", this.RangeType.FullName, this.RangeVariableName, this.RangeSource);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
