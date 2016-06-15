using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    public class LinqFusionSelectBody :
        LinqSelectBody,
        ILinqFusionSelectBody
    {
        private ILinqRangeVariable target;
        /// <summary>
        /// Creates a new <see cref="LinqFusionSelectBody"/> with the
        /// <paramref name="selection"/>, <paramref name="target"/>,
        /// and <paramref name="next"/> provided.
        /// </summary>
        /// <param name="selection">The <see cref="IExpression"/> 
        /// which denotes what is selected as a result of the 
        /// language integrated query.</param>
        /// <param name="target">The <see cref="String"/> representing the name of the
        /// range variable that the data from the grouped query is stored into.</param>
        /// <param name="next">The <see cref="ILinqBody"/> which
        /// continues the language integrated query.</param>
        public LinqFusionSelectBody(IExpression selection, string target, ILinqBody next)
            : base(selection)
        {
            this.target = new LinqRangeVariable(this, target);
            this.Next = next;
        }
        internal LinqFusionSelectBody()
        {
            this.target = new LinqRangeVariable(this, string.Empty);
        }

        #region ILinqFusionBody Members

        /// <summary>
        /// Returns/sets the <see cref="ILinqBody"/> which continues the query.
        /// </summary>
        public ILinqBody Next { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} into {1}\r\n{2}", base.ToString(), Target, Next);
        }

        public override void Accept(ILinqBodyVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ILinqBodyVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #region ILinqFusionBody Members

        public ILinqRangeVariable Target
        {
            get { return this.target; }
        }

        #endregion

        #region ILinqClause Members

        public ClauseType Type
        {
            get { return ClauseType.ContinuationClause; }
        }

        #endregion
    }
}
