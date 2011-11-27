using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides a base implementation of a language
    /// integrated query where clause which further refines a 
    /// data series by providing conditional logic to element
    /// selection.
    /// </summary>
    public class LinqWhereClause :
        LinqClauseBase,
        ILinqWhereClause
    {
        /// <summary>
        /// Creates a new <see cref="LinqWhereClause"/> with the
        /// <paramref name="condition"/> provided.
        /// </summary>
        /// <param name="condition">The boolean <see cref="IExpression"/>
        /// which helps filter the series.</param>
        public LinqWhereClause(IExpression condition)
        {
            this.Condition = condition;
        }

        /// <summary>
        /// Returns the kind of clause the <see cref="ILinqClause"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ClauseType.WhereClause"/>.</remarks>
        public override ClauseType Type
        {
            get { return ClauseType.WhereClause; }
        }

        #region ILinqWhereClause Members

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/>
        /// which helps filter the series.
        /// </summary>
        public IExpression Condition { get; set; }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representing the current
        /// <see cref="LinqWhereClause"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the current
        /// <see cref="LinqWhereClause"/>.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "where {0}", Condition);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
