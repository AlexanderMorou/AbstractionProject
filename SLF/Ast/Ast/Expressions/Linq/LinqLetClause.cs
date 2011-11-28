using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
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
        private string rangeVariableName;
        private ILinqRangeVariable rangeVariable;
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
            if (string.IsNullOrEmpty(rangeVariableName))
                throw new ArgumentNullException("rangeVariableName");
            this.rangeVariableName = rangeVariableName;
            this.RangeSource = rangeSelector;
        }

        /// <summary>
        /// Creates a new <see cref="LinqLetClause"/> initialized to a default state.
        /// </summary>
        protected LinqLetClause() { }

        #region ILinqLetClause Members

        public ILinqRangeVariable RangeVariable
        {
            get
            {
                if (rangeVariable == null)
                {
                    rangeVariable = new LinqRangeVariable(this, rangeVariableName);;
                    this.rangeVariableName = null;
                }
                return this.rangeVariable;
            }
        }
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
            return string.Format(CultureInfo.CurrentCulture, "let {0} = {1}", RangeVariable.Name, RangeSource);
        }

        public override void Visit(ILinqVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
