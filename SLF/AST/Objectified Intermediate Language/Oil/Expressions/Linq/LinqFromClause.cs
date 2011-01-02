using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides a base class for a language integrated query from clause.
    /// </summary>
    public class LinqFromClause :
        LinqClauseBase,
        ILinqFromClause
    {
        /// <summary>
        /// Creates a new <see cref="LinqFromClause"/> with the
        /// <paramref name="rangeVariableName"/>, and 
        /// <paramref name="rangeSource"/> provided.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// defined by the <see cref="LinqFromClause"/>. </param>
        /// <param name="rangeSource">The <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.</param>
        public LinqFromClause(string rangeVariableName, IExpression rangeSource)
        {
            this.RangeVariableName = rangeVariableName;
            this.RangeSource = rangeSource;
        }

        /// <summary>
        /// Returns the kind of clause the <see cref="ILinqClause"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ClauseType.FromClause"/>.</remarks>
        public override ClauseType Type
        {
            get { return ClauseType.FromClause; }
        }

        #region ILinqFromClause Members

        /// <summary>
        /// Returns/sets the name of the range variable
        /// defined by the <see cref="LinqFromClause"/>.
        /// </summary>
        public string RangeVariableName { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.
        /// </summary>
        public IExpression RangeSource { get; set; }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representing the current <see cref="LinqFromClause"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the current <see cref="LinqFromClause"/>.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "from {0} in {1}", this.RangeVariableName, this.RangeSource);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
