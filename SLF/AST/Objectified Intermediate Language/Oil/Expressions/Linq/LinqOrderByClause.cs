using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    public class LinqOrderByClause :
        LinqClauseBase,
        ILinqOrderByClause
    {
        protected LinqOrderByClause()
        {
        }

        /// <summary>
        /// Creates a new <see cref="LinqOrderByClause"/> with the
        /// <paramref name="orderKey"/> provided.
        /// </summary>
        /// <param name="orderKey">The <see cref="IExpression"/> which determines
        /// the ordering key for comparison to order the data series.</param>
        public LinqOrderByClause(IExpression orderKey)
        {
            this.OrderKey = orderKey;
        }

        public override ClauseType Type
        {
            get { return ClauseType.OrderByClause; }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "orderby {0}");
        }

        #region ILinqOrderByClause Members

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which determines
        /// the ordering key for comparison to order the data series.
        /// </summary>
        public IExpression OrderKey { get; set; }

        #endregion

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
