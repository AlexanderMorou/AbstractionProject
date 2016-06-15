using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
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

        public override void Accept(ILinqClauseVisitor visitor)
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

        public override TResult Accept<TResult, TContext>(ILinqClauseVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}
