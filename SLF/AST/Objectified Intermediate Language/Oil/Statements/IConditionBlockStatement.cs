using System;
using System.Collections.Generic;
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
    /// Defines properties and methods for working with a 
    /// condition statement which alters the flow of the 
    /// execution based upon a boolean condition.
    /// </summary>
    public interface IConditionBlockStatement :
        IConditionContinuationStatement
    {
        /// <summary>
        /// Returns/sets the condition for the <see cref="IConditionBlockStatement"/> to execute.
        /// </summary>
        IExpression Condition { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IConditionContinuationStatement"/> which continues the
        /// code flow control conditioning.
        /// </summary>
        IConditionContinuationStatement Next { get; set; }
        void CreateNext();
        void CreateNext(IExpression condition);
    }
}
