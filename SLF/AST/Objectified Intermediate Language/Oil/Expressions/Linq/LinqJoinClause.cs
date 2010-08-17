using System;
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
    /// Provides a base class for a language integrated query
    /// expression's join clause.
    /// </summary>
    public class LinqJoinClause :
        LinqClauseBase,
        ILinqJoinClause
    {
        /// <summary>
        /// Creates a new <see cref="LinqJoinClause"/> with the 
        /// <paramref name="rangeVariableName"/>, <paramref name="rangeSource"/>,
        /// <paramref name="leftCondition"/>, <paramref name="rightCondition"/> and
        /// <paramref name="intoRangeVariableName"/> provided.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// defined by the <see cref="ILinqFromClause"/>.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.</param>
        /// <param name="leftCondition">The left <see cref="IExpression"/> which
        /// determines the join condition.</param>
        /// <param name="rightCondition">The right <see cref="IExpression"/> which
        /// determines the join condition.</param>
        /// <param name="intoRangeVariableName">The name of the range variable which 
        /// the data from the join operation is placed into.</param>
        public LinqJoinClause(string rangeVariableName, IExpression rangeSource, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName)
            : this(rangeVariableName, rangeSource, leftCondition, rightCondition)
        {
            this.IntoRangeVariableName = intoRangeVariableName;
        }
        /// <summary>
        /// Creates a new <see cref="LinqJoinClause"/> with the 
        /// <paramref name="rangeVariableName"/>, <paramref name="rangeSource"/>,
        /// <paramref name="leftCondition"/> and <paramref name="rightCondition"/> provided.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// defined by the <see cref="ILinqFromClause"/>.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.</param>
        /// <param name="leftCondition">The left <see cref="IExpression"/> which
        /// determines the join condition.</param>
        /// <param name="rightCondition">The right <see cref="IExpression"/> which
        /// determines the join condition.</param>
        public LinqJoinClause(string rangeVariableName, IExpression rangeSource, IExpression leftCondition, IExpression rightCondition)
        {
            this.RangeVariableName = rangeVariableName;
            this.RangeSource = rangeSource;
            this.LeftSelector = leftCondition;
            this.RightSelector = rightCondition;
        }

        /// <summary>
        /// Creates a new <see cref="LinqJoinClause"/> initialized to 
        /// a default state.
        /// </summary>
        protected LinqJoinClause()
        {

        }
        #region ILinqJoinClause Members

        /// <summary>
        /// Returns/sets the name of the range variable
        /// defined by the <see cref="ILinqFromClause"/>.
        /// </summary>
        public string RangeVariableName { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.
        /// </summary>
        public IExpression RangeSource { get; set; }

        /// <summary>
        /// Returns/sets the name of the range variable which 
        /// the data from the join operation is placed into.
        /// </summary>
        public string IntoRangeVariableName { get; set; }

        /// <summary>
        /// Returns/sets the left <see cref="IExpression"/> which
        /// determines the join condition.
        /// </summary>
        public IExpression LeftSelector { get; set; }

        /// <summary>
        /// Returns/sets the right <see cref="IExpression"/> which
        /// determines the join condition.
        /// </summary>
        public IExpression RightSelector { get; set; }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> value representing the current 
        /// <see cref="LinqJoinClause"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value representing the current 
        /// <see cref="LinqJoinClause"/>.</returns>
        public override string ToString()
        {
            if (!(string.IsNullOrEmpty(this.IntoRangeVariableName)))
                return string.Format(CultureInfo.CurrentCulture, "join {0} in {1} on {2} equals {3} into {4}", RangeVariableName, RangeSource, LeftSelector, RightSelector, IntoRangeVariableName);
            return string.Format(CultureInfo.CurrentCulture, "join {0} in {1} on {2} equals {3}", RangeVariableName, RangeSource, LeftSelector, RightSelector);
        }

        /// <summary>
        /// Returns the kind of clause the <see cref="LinqJoinClause"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ClauseType.JoinClause"/>.</remarks>
        public override ClauseType Type
        {
            get { return ClauseType.JoinClause; }
        }

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
