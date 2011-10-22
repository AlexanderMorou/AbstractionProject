using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// Returns the <see cref="ILinqRangeVariable"/> associated
        /// to the <see cref="ILinqJoinClause"/>.
        /// </summary>
        ILinqRangeVariable RangeVariable { get; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.
        /// </summary>
        IExpression RangeSource { get; set; }
        /// <summary>
        /// Returns the <see cref="ILinqRangeVariable"/>
        /// which denotes where the results of the join are 
        /// placed associated to the <see cref="ILinqJoinClause"/>.
        /// </summary>
        ILinqRangeVariable IntoRangeVariable { get; }
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
