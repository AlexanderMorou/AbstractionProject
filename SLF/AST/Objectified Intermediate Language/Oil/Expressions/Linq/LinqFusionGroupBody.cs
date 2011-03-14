using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    public class LinqFusionGroupBody :
        LinqFusionSelectBody,
        ILinqFusionGroupBody
    {
        /// <summary>
        /// Creates a new <see cref="LinqFusionGroupBody"/> with
        /// the <paramref name="selection"/>, <paramref name="key"/>, 
        /// <paramref name="target"/> and <paramref name="next"/>
        /// provided.
        /// </summary>
        /// <param name="selection">The <see cref="IExpression"/> which
        /// acts as a selector on the data set to retrieve the data for the query.</param>
        /// <param name="key">The <see cref="IExpression"/> which acts
        /// as the key for grouping the <see cref="ILinqSelectBody.Selection"/>s.</param>
        /// <param name="target">The <see cref="String"/> representing the name of the
        /// range variable that the data from the grouped query is stored into.</param>
        /// <param name="next">The <see cref="ILinqBody"/> which
        /// continues the language integrated query.</param>
        public LinqFusionGroupBody(IExpression selection, IExpression key, string target, ILinqBody next)
            : base(selection, target, next)
        {
            this.Key = key;
        }

        internal LinqFusionGroupBody()
            : base()
        {
        }

        #region ILinqGroupBody Members
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which acts
        /// as the key for grouping the <see cref="ILinqSelectBody.Selection"/>s.
        /// </summary>
        public IExpression Key { get; set; }
        #endregion

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} group {4} by {1} into {2}\r\n{3}", base.BodyToString(), this.Key, base.Target, base.Next, base.Selection);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit((ILinqFusionGroupBody)this);
        }

    }
}
