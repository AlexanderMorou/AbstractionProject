using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    /// <summary>
    /// Defines properties and methods for working with an indexer relative to an array.
    /// </summary>
    public interface IArrayIndexerExpression :
        IMemberParentExpression<CodeArrayIndexerExpression>,
        IAssignStatementTarget
    {
        /// <summary>
        /// The series of indices needed to access the specific element of the array.
        /// </summary>
        IExpressionCollection Indices { get; }
        /// <summary>
        /// The expression that represents an array to be accessed.
        /// </summary>
        IExpression Reference { get; }
    }
}
