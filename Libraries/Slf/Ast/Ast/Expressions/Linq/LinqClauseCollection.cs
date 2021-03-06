﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Provides a base collection for the clauses
    /// of a <see cref="ILinqBody"/>.
    /// </summary>
    public class LinqClauseCollection :
        ControlledCollection<ILinqClause>,
        ILinqClauseCollection
    {
        #region ILinqClauseCollection Members

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
        public ILinqFromClause From(string rangeVariableName, IExpression rangeSelector)
        {
            var result = new LinqFromClause(rangeVariableName, rangeSelector);
            base.baseList.Add(result);
            return result;
        }

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
        public ILinqTypedFromClause From(TypedName rangeVariable, IExpression rangeSelector)
        {
            var result = new LinqTypedFromClause(rangeVariable, rangeSelector);
            base.baseList.Add(result);
            return result;
        }

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
        public ILinqLetClause Let(string rangeVariableName, IExpression rangeSelector)
        {
            var result = new LinqLetClause(rangeVariableName, rangeSelector);
            base.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqWhereClause"/> which
        /// defines a condition to filter the series from the declaration point onward.
        /// </summary>
        /// <param name="condition">The boolean <see cref="IExpression"/> which
        /// denotes the predicate for item selection.</param>
        /// <returns>An <see cref="ILinqWhereClause"/> instance which
        /// defines the new clause.</returns>
        public ILinqWhereClause Where(IExpression condition)
        {
            var result = new LinqWhereClause(condition);
            base.baseList.Add(result);
            return result;
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqOrderByClause"/> which
        /// defines a key selector for the expression used to compare across the series
        /// to order it.
        /// </summary>
        /// <param name="orderKey">The <see cref="IExpression"/> which determines
        /// the ordering key for comparison to order the data series.</param>
        /// <returns>An <see cref="ILinqOrderByClause"/> instance which
        /// defines the new clause.</returns>
        public ILinqOrderByClause OrderBy(IExpression orderKey)
        {
            return this.OrderBy(new LinqOrderingPair(orderKey));
        }

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
        public ILinqOrderByClause OrderBy(IExpression orderKey, LinqOrderByDirection direction)
        {
            return this.OrderBy(new LinqOrderingPair(orderKey, direction));
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqOrderByClause"/>
        /// </summary>
        /// <param name="orderKeys">The <see cref="IEnumerable{T}"/> of elements
        /// which denote the keys to order the set by.</param>
        /// <returns>An <see cref="ILinqOrderByClause"/> instance which
        /// defines the new clause.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="orderKeys"/> is null.</exception>
        public ILinqOrderByClause OrderBy(IEnumerable<IExpression> orderKeys)
        {
            if (orderKeys == null)
                throw new ArgumentNullException("orderKeys");
            var orderKeysCopy = orderKeys.ToArray();
            LinqOrderingPair[] orderings = new LinqOrderingPair[orderKeysCopy.Length];
            for (int i = 0; i < orderKeysCopy.Length; i++)
                orderings[i] = new LinqOrderingPair(orderKeysCopy[i]);
            return this.OrderBy(orderings);
        }

        /// <summary>
        /// Creates, inserts, and returns a new <see cref="ILinqOrderByClause"/>
        /// </summary>
        /// <param name="orderKeys">The <see cref="IEnumerable{T}"/> of elements
        /// which denote the keys, and directions, to order the set by.</param>
        /// <returns>An <see cref="ILinqOrderByClause"/> instance which
        /// defines the new clause.</returns>
        public ILinqOrderByClause OrderBy(params LinqOrderingPair[] orderKeys)
        {
            var result = new LinqOrderByClause();
            foreach (var orderKey in orderKeys)
            {
                if (orderKey.OrderingKey == null)
                    continue;
                result.Orderings.Add(orderKey.OrderingKey, orderKey.Direction);
            }
            base.baseList.Add(result);
            return result;
        }

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
        public ILinqJoinClause Join(string rangeVariableName, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition)
        {
            var result = new LinqJoinClause(rangeVariableName, rangeSelector, leftCondition, rightCondition);
            base.baseList.Add(result);
            return result;
        }

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
        /// <param name="leftCondition">The left half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the expression.</param>
        /// <param name="rightCondition">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariable"/> provided.</param>
        /// <returns>An <see cref="ILinqTypedJoinClause"/> instance which defines the
        /// new clause.</returns>
        public ILinqTypedJoinClause Join(TypedName rangeVariable, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition)
        {
            var result = new LinqTypedJoinClause(rangeVariable, rangeSelector, leftCondition, rightCondition);
            base.baseList.Add(result);
            return result;
        }

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
        public ILinqJoinClause Join(string rangeVariableName, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName)
        {
            var result = new LinqJoinClause(rangeVariableName, rangeSelector, leftCondition, rightCondition, intoRangeVariableName);
            base.baseList.Add(result);
            return result;
        }

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
        public ILinqTypedJoinClause Join(TypedName rangeVariable, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName)
        {
            var result = new LinqTypedJoinClause(rangeVariable, rangeSelector, leftCondition, rightCondition, intoRangeVariableName);
            base.baseList.Add(result);
            return result;
        }

        #endregion
    }
}
