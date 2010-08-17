using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a block statement
    /// that represents an iteration.
    /// </summary>
    public interface IIterationBlockStatement :
        IBreakableBlockStatement
    {
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> which executes once at the initialization
        /// of the iteration process.
        /// </summary>
        IMalleableExpressionCollection DeclarePart { get; }
        /// <summary>
        /// Returns the <see cref="IExpression"/> which evaluates as a boolean to determine
        /// whether to continue the iteration process.
        /// </summary>
        IExpression ConditionPart { get; set; }
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> of expressions that should 
        /// execute at the end of each iteration.
        /// </summary>
        IMalleableExpressionCollection IteratePart { get; }
    }
}
