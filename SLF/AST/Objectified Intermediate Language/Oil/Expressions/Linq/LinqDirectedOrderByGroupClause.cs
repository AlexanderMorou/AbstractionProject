using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    public class LinqDirectedOrderByGroupClause :
        LinqClauseBase,
        ILinqDirectedOrderByGroupClause
    {

        public LinqDirectedOrderByGroupClause()
            : base()
        {
            this.Orderings = new LinqOrderingPairCollection();
        }

        public override ClauseType Type
        {
            get { return ClauseType.OrderByClause; }
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region ILinqDirectedOrderByGroupClause Members

        /// <summary>
        /// Returns the <see cref="ILinqOrderingPairCollection"/>
        /// that directs the output data set.
        /// </summary>
        public ILinqOrderingPairCollection Orderings { get; private set; }

        #endregion
    }
}
