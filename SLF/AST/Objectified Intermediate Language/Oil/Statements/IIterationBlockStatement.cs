using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IIterationBlockBaseStatement
    {
        /// <summary>
        /// Returns the <see cref="IMalleableStatementExpressionCollection"/> which executes once at the initialization
        /// of the iteration process.
        /// </summary>
        IMalleableStatementExpressionCollection Initializers { get; }
    }

    public interface IIterationBlockBaseStatement :
        IBreakableBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which evaluates as a boolean to determine
        /// whether to continue the iteration process.
        /// </summary>
        IExpression Condition { get; set; }
        /// <summary>
        /// Returns the <see cref="IMalleableStatementExpressionCollection"/> of expressions that should 
        /// execute at the end of each iteration.
        /// </summary>
        IMalleableStatementExpressionCollection Iterations { get; }
    }
}
