using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Provides a base collection for the clauses
    /// of a <see cref="ILinqBody"/>.
    /// </summary>
    public class LinqClauseCollection :
        ControlledStateCollection<ILinqClause>,
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
            base.baseCollection.Add(result);
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
            base.baseCollection.Add(result);
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
            base.baseCollection.Add(result);
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
            base.baseCollection.Add(result);
            return result;
        }

        public ILinqOrderByClause OrderBy(IExpression orderKey)
        {
            var result = new LinqOrderByClause(orderKey);
            base.baseCollection.Add(result);
            return result;
        }

        public ILinqDirectedOrderByClause OrderBy(IExpression orderKey, LinqOrderByDirection direction)
        {
            var result = new LinqDirectedOrderByClause(orderKey, direction);
            base.baseCollection.Add(result);
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
            base.baseCollection.Add(result);
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
        /// <param name="leftCondition">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="rightCondition">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <returns>An <see cref="ILinqTypedJoinClause"/> instance which defines the
        /// new clause.</returns>
        public ILinqTypedJoinClause Join(TypedName rangeVariable, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition)
        {
            var result = new LinqTypedJoinClause(rangeVariable, rangeSelector, leftCondition, rightCondition);
            base.baseCollection.Add(result);
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
            base.baseCollection.Add(result);
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
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <param name="intoRangeVariableName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the join clause's results into.</param>
        /// <returns>An <see cref="ILinqTypedJoinClause"/> instance which defines the
        /// new clause.</returns>
        public ILinqTypedJoinClause Join(TypedName rangeVariable, IExpression rangeSelector, IExpression leftCondition, IExpression rightCondition, string intoRangeVariableName)
        {
            var result = new LinqTypedJoinClause(rangeVariable, rangeSelector, leftCondition, rightCondition, intoRangeVariableName);
            base.baseCollection.Add(result);
            return result;
        }

        #endregion
    }
}
