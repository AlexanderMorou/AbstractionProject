using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a language integrated query
    /// expression's join clause.
    /// </summary>
    public interface ILinqJoinClause :
        ILinqClause
    {
        /// <summary>
        /// Returns/sets the name of the range variable
        /// defined by the <see cref="ILinqFromClause"/>.
        /// </summary>
        string RangeVariableName { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.
        /// </summary>
        IExpression RangeSource { get; set; }
        /// <summary>
        /// Returns/sets the name of the range variable which 
        /// the data from the join operation is placed into.
        /// </summary>
        string IntoRangeVariableName { get; set; }
        /// <summary>
        /// Returns/sets the left <see cref="IExpression"/> which
        /// determines the join condition.
        /// </summary>
        IExpression LeftSelector { get; set; }
        /// <summary>
        /// Returns/sets the right <see cref="IExpression"/> which
        /// determines the join condition.
        /// </summary>
        IExpression RightSelector { get; set; }
    }
}
