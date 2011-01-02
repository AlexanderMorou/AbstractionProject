using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides a base implementation of a linq directed by clause.
    /// </summary>
    public class LinqDirectedOrderByClause :
        LinqOrderByClause,
        ILinqDirectedOrderByClause
    {
        /// <summary>
        /// Creates a new <see cref="LinqDirectedOrderByClause"/> with the
        /// <paramref name="orderKey"/> and <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="orderKey">The <see cref="IExpression"/> which determines
        /// the ordering key for comparison to order the data series.</param>
        /// <param name="direction">The <see cref="LinqOrderByDirection"/>
        /// for the <see cref="LinqDirectedOrderByClause"/>.</param>
        public LinqDirectedOrderByClause(IExpression orderKey, LinqOrderByDirection direction)
            : base(orderKey)
        {
            this.Direction = direction;
        }

        #region ILinqDirectedOrderByClause Members

        /// <summary>
        /// Returns/sets the <see cref="LinqOrderByDirection"/>
        /// for the <see cref="ILinqDirectedOrderByClause"/>.
        /// </summary>
        public LinqOrderByDirection Direction { get; set; }

        #endregion

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
