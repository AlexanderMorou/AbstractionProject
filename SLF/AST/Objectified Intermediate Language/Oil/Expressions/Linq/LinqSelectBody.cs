﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
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

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}