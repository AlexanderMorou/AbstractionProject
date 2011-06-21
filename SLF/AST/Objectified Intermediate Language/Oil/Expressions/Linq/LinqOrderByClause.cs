using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    public class LinqOrderByClause :
        LinqClauseBase,
        ILinqOrderByClause
    {

        public LinqOrderByClause()
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


        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "orderby {0}", string.Join(", ", this.Orderings));
        }
    }
}
