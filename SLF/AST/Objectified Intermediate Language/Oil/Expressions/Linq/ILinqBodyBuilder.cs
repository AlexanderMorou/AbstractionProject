using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods building language integrated
    /// query expressions.
    /// </summary>
    public interface ILinqBodyBuilder
    {
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// from clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder From(TypedName rangeVariable, IExpression rangeSource);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// from clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder From(TypedName rangeVariable, string rangeSource);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// from clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder From(string rangeVariableName, IExpression rangeSource);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// from clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable
        /// referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder From(string rangeVariableName, string rangeSource);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// let clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable assigned by the
        /// let clause.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes the data source
        /// for the range variable's assignment.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Let(string rangeVariableName, IExpression rangeSource);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// new where-condition clause inserted into the query expression.
        /// </summary>
        /// <param name="booleanCondition">The boolean <see cref="IExpression"/>
        /// which is used to filter the query.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Where(IExpression booleanCondition);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariable"/> provided.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(TypedName rangeVariable, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSourceSymbol">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariable"/> provided.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(TypedName rangeVariable, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable defined by the
        /// join clause.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(string rangeVariableName, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable defined by the
        /// join clause.</param>
        /// <param name="rangeSourceSymbol">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(string rangeVariableName, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariable"/> provided.</param>
        /// <param name="intoRangeName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the join clause's results into.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(TypedName rangeVariable, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight, string intoRangeName);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariable">The <see cref="TypedName"/> which denotes
        /// the type and name of the range variable referenced in the expression.</param>
        /// <param name="rangeSourceSymbol">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariable"/> provided.</param>
        /// <param name="intoRangeName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the join clause's results into.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(TypedName rangeVariable, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight, string intoRangeName);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable defined by the
        /// join clause.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <param name="intoRangeName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the join clause's results into.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(string rangeVariableName, IExpression rangeSource, IExpression conditionLeft, IExpression conditionRight, string intoRangeName);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqBodyBuilder"/> which
        /// furthers the language integrated query build with a 
        /// join clause injected into the query expression.
        /// </summary>
        /// <param name="rangeVariableName">The name of the range variable defined by the
        /// join clause.</param>
        /// <param name="rangeSourceSymbol">The <see cref="String"/> <see cref="Symbol"/> which denotes where to 
        /// find the range source.</param>
        /// <param name="conditionLeft">The left half of the condition for the join expression, valid range variables
        /// at this point include those already a part of the expression.</param>
        /// <param name="conditionRight">The right half of the condition for the join expression,
        /// valid range variables at this point include those already a part of the
        /// expression and the <paramref name="rangeVariableName"/> provided.</param>
        /// <param name="intoRangeName">The <see cref="String"/>
        /// which represents the name of the range variable to place
        /// the join clause's results into.</param>
        /// <returns>A new <see cref="ILinqBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqBodyBuilder Join(string rangeVariableName, string rangeSourceSymbol, IExpression conditionLeft, IExpression conditionRight, string intoRangeName);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqOrderedBodyBuilder"/>
        /// which furthers the language integrated query build with an
        /// order by clause injected into the query expression.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/> on which
        /// to order the resulted series.</param>
        /// <returns>A new <see cref="ILinqOrderedBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqOrderedBodyBuilder OrderBy(IExpression orderingKey);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqOrderedBodyBuilder"/> which furthers
        /// the language integrated query build with an orderby clause injected
        /// into the query expression.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/> on which
        /// to order the resulted series.</param>
        /// <param name="direction">The <see cref="LinqOrderByDirection"/>
        /// which denotes the specific direction the ordering coerces
        /// the output data set.</param>
        /// <returns>A new <see cref="ILinqOrderedBodyBuilder"/> which furthers the 
        /// language integrated query expression build.</returns>
        ILinqOrderedBodyBuilder OrderBy(IExpression orderingKey, LinqOrderByDirection direction);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqTailBodyBuilder"/> which
        /// finalizes the language integrated query build with a
        /// select clause injected at the tail of the query expression with the 
        /// option of continuation through an into sub clause on the select clause.
        /// </summary>
        /// <param name="selection">The <see cref="IExpression"/> which determines
        /// what the query expression ultimately returns.</param>
        /// <returns>A new <see cref="ILinqTailBodyBuilder"/> which finalizes
        /// or furthers the language integrated query build.</returns>
        ILinqTailBodyBuilder Select(IExpression selection);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqTailBodyBuilder"/> which
        /// finalizes the language integrated query build with a
        /// select clause injected at the tail of the query expression with the 
        /// option of continuation through an into sub clause on the select clause.
        /// </summary>
        /// <param name="symbolSelection">The <see cref="String"/> <see cref="Symbol"/> which determines
        /// what the query expression ultimately returns.</param>
        /// <returns>A new <see cref="ILinqTailBodyBuilder"/> which finalizes
        /// or furthers the language integrated query build.</returns>
        ILinqTailBodyBuilder Select(string symbolSelection);
        /// <summary>
        /// Creates and returns a new <see cref="ILinqTailBodyBuilder"/> which
        /// finalizes the language integrated query build with a
        /// group by clause injected at the tail of the query expression with the 
        /// option of continuation through an into sub clause on the group by clause.
        /// </summary>
        /// <param name="selection">The <see cref="IExpression"/> which determines
        /// what the query expression ultimately returns.</param>
        /// <param name="key">The <see cref="IExpression"/> which denotes what the
        /// <paramref name="selection"/> values are grouped by.</param>
        /// <returns>A new <see cref="ILinqTailBodyBuilder"/> which finalizes
        /// or furthers the language integrated query build.</returns>
        ILinqTailBodyBuilder GroupBy(IExpression selection, IExpression key);
    }
}
