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
    public class LinqFusionSelectBody :
        LinqSelectBody,
        ILinqFusionSelectBody
    {
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
            this.Target = target;
            this.Next = next;
        }
        public LinqFusionSelectBody()
        {

        }

        #region ILinqFusionBody Members

        /// <summary>
        /// Returns/sets the <see cref="String"/> representing the
        /// local the current query data is stored into for use in the
        /// <see cref="Next"/> <see cref="ILinqBody"/>.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="ILinqBody"/> which continues the query.
        /// </summary>
        public ILinqBody Next { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0} into {1}\r\n{2}", base.ToString(), Target, Next);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
