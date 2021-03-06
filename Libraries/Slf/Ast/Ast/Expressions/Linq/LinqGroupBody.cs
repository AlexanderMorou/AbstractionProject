﻿using System;
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

        public override void Accept(ILinqBodyVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override TResult Accept<TResult, TContext>(ILinqBodyVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}
