using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides a linq order by clause.
    /// </summary>
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
