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
    /// <summary>
    /// Provides a base language integrated query body implementation which
    /// ends with a group clause.
    /// </summary>
    public class LinqGroupBody :
        LinqSelectBody,
        ILinqGroupBody
    {
        public LinqGroupBody(IExpression selection, IExpression key)
            : base(selection)
        {
            this.Key = key;
        }
        public LinqGroupBody()
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
            return string.Format(CultureInfo.CurrentCulture, "{0} by {1}", base.ToString(), Key);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
