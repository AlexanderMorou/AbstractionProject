using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with the clauses
    /// of a <see cref="ILinqBody"/>.
    /// </summary>
    public interface ILinqClauseCollection :
        IControlledStateCollection<ILinqClause>
    {
        /// <summary>
        /// Creates, inserts,  and returns a new <see cref="ILinqFromClause"/> defines
        /// the from clause by its range variable name and range source selector.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// referenced in the expression.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>An <see cref="ILinqFromClause"/> instance which 
        /// defines the new clause.</returns>
        ILinqFromClause From(string rangeVariableName, IExpression rangeSelector);
        /// <summary>
        /// Creates, inserts,  and returns a new <see cref="ILinqTypedFromClause"/> which
        /// defines the from clause by its range variable name and range source selector.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>An <see cref="ILinqTypedFromClause"/> instance which 
        /// defines the new clause.</returns>
        ILinqTypedFromClause From(TypedName rangeVariable, IExpression rangeSelector);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqLetClause"/> which
        /// defines the range variable to assign and its range source selector.
        /// </summary>
        /// <param name="rangeVariableName">The <see cref="String"/> representing the name
        /// of the range variable declared by the clause.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/>
        /// which denotes where to source the data.</param>
        /// <returns>An <see cref="ILinqLetClause"/> instance which defines
        /// the new clause.</returns>
        ILinqLetClause Let(string rangeVariableName, IExpression rangeSelector);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqWhereClause"/> which
        /// defines a condition to filter the series from the declaration point onward.
        /// </summary>
        /// <param name="condition">The boolean <see cref="IExpression"/> which
        /// denotes the predicate for item selection.</param>
        /// <returns>An <see cref="ILinqWhereClause"/> instance which
        /// defines the new clause.</returns>
        ILinqWhereClause Where(IExpression condition);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqOrderByClause"/> which
        /// defines a key selector for the expression used to compare across the series
        /// to order it.
        /// </summary>
        /// <param name="orderKey">The <see cref="IExpression"/> which determines
        /// the ordering key for comparison to order the data series.</param>
        /// <returns>An <see cref="ILinqOrderByClause"/> instance which
        /// defines the new clause.</returns>
        ILinqOrderByClause OrderBy(IExpression orderKey);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqOrderByClause"/> which
        /// defines a key selector for the expression used to compare across the series
        /// to order it.
        /// </summary>
        /// <param name="orderKey">The <see cref="IExpression"/> which determines
        /// the ordering key for comparison to order the data series.</param>
        /// <param name="direction">The <see cref="LinqOrderByDirection"/>
        /// for the <see cref="ILinqOrderByClause"/>.</param>
        /// <returns>An <see cref="ILinqOrderByClause"/> instance which
        /// defines the new clause.</returns>
        ILinqOrderByClause OrderBy(IExpression orderKey, LinqOrderByDirection direction);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqOrderByGroupClause"/>
        /// </summary>
        /// <param name="orderKeys">The <see cref="IEnumerable{T}"/> of elements
        /// which denote the keys to order the set by.</param>
        /// <returns>An <see cref="ILinqOrderByGroupClause"/> instance which
        /// defines the new clause.</returns>
        ILinqOrderByClause OrderBy(IEnumerable<IExpression> orderKeys);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqDirectedOrderByGroupClause"/>
        /// </summary>
        /// <param name="orderKeys">The <see cref="IEnumerable{T}"/> of elements
        /// which denote the keys, and directions, to order the set by.</param>
        /// <returns>An <see cref="ILinqDirectedOrderByGroupClause"/> instance which
        /// defines the new clause.</returns>
        ILinqOrderByClause OrderBy(params LinqOrderingPair[] orderKeys);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqJoinClause"/> which 
        /// defines a join operation on a sequence given the <paramref name="leftCondition"/>
        /// and <paramref name="rightCondition"/> to determine an item-wise relationship
        /// between the two series.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable defined by the
        /// join clause.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="leftCondition">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="rightCondition">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <returns>An <see cref="ILinqJoinClause"/> instance which defines the
        /// new clause.</returns>
        ILinqJoinClause Join(string rangeVariableName, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqTypedJoinClause"/> which 
        /// defines a join operation on a typed sequence given the <paramref name="leftCondition"/>
        /// and <paramref name="rightCondition"/> to determine an item-wise relationship
        /// between the two series.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="leftCondition">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="rightCondition">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariable"/> provided.</param>
        /// <returns>An <see cref="ILinqTypedJoinClause"/> instance which defines the
        /// new clause.</returns>
        ILinqTypedJoinClause Join(TypedName rangeVariable, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqJoinClause"/> which 
        /// defines a join operation on a sequence given the <paramref name="leftCondition"/>
        /// and <paramref name="rightCondition"/> to determine an item-wise relationship
        /// between the two series with the results placed into the 
        /// <paramref name="intoRangeVariableName"/> provided.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable defined by the
        /// join clause.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="leftCondition">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="rightCondition">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <param name="intoRangeVariableName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the join clause's results into.</param>
        /// <returns>An <see cref="ILinqJoinClause"/> instance which defines the
        /// new clause.</returns>
        ILinqJoinClause Join(string rangeVariableName, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName);
        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqTypedJoinClause"/> which 
        /// defines a join operation on a typed sequence given the <paramref name="leftCondition"/>
        /// and <paramref name="rightCondition"/> to determine an item-wise relationship
        /// between the two series with the results placed into the 
        /// <paramref name="intoRangeVariableName"/> provided.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSelector">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="leftCondition">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="rightCondition">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariable"/> provided.</param>
        /// <param name="intoRangeVariableName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the join clause's results into.</param>
        /// <returns>An <see cref="ILinqTypedJoinClause"/> instance which defines the
        /// new clause.</returns>
        ILinqTypedJoinClause Join(TypedName rangeVariable, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName);

    }
}
