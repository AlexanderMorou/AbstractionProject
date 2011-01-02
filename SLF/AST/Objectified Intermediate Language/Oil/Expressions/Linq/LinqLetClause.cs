using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides a base <see cref="ILinqLetClause"/> implementation 
    /// for working with a language integrated query let clause for 
    /// assigning a range variable to a given value for later use 
    /// in the query.
    /// </summary>
    public class LinqLetClause :
        LinqClauseBase,
        ILinqLetClause
    {
        /// <summary>
        /// Creates a new <see cref="LinqLetClause"/> with the
        /// <paramref name="rangeVariableName"/> and <paramref name="rangeSelector"/>
        /// provided.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable defined by the 
        /// <see cref="LinqLetClause"/>.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/> to assign to the
        /// range variable.</param>
        public LinqLetClause(string rangeVariableName, IExpression rangeSelector)
        {
            this.RangeVariableName = rangeVariableName;
            this.RangeSource = rangeSelector;
        }

        /// <summary>
        /// Creates a new <see cref="LinqLetClause"/> initialized to a default state.
        /// </summary>
        protected LinqLetClause() { }

        #region ILinqLetClause Members

        /// <summary>
        /// Returns/sets the name of the range variable defined by the
        /// <see cref="LinqLetClause"/>.
        /// </summary>
        public string RangeVariableName { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> to assign to the
        /// range variable.
        /// </summary>
        public IExpression RangeSource { get; set; }

        #endregion

        public override ClauseType Type
        {
            get { return ClauseType.LetClause; }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "let {0} = {1}", RangeVariableName, RangeSource);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
