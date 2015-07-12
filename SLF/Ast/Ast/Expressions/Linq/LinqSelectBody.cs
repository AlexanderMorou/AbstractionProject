using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Provides a base implementation of a language integrated query body which
    /// ends with a select clause.
    /// </summary>
    public class LinqSelectBody :
        LinqBody,
        ILinqSelectBody
    {
        /// <summary>
        /// Creates a new <see cref="LinqSelectBody"/> with the
        /// <paramref name="selection"/> provided.
        /// </summary>
        /// <param name="selection">The <see cref="IExpression"/> 
        /// which denotes what is selected as a result of the 
        /// language integrated query.</param>
        public LinqSelectBody(IExpression selection)
        {
            this.Selection = selection;
        }

        /// <summary>
        /// Creates a new <see cref="LinqSelectBody"/> 
        /// initialized to a default state.
        /// </summary>
        public LinqSelectBody()
        {

        }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which denotes what is selected
        /// as a result of the language integrated query.
        /// </summary>
        public IExpression Selection { get; set; }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}select {1}", base.ToString(), Selection);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
