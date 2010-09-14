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
    /* *
     * A switch statement's 'cases' are nothing more than a label that's 
     * jumped to when a condition is met.
     * */
    /// <summary>
    /// Defines properties and methods for working with a switch case block statement.
    /// </summary>
    public interface ISwitchCaseBlockStatement :
        IBreakableBlockStatement,
        IJumpTarget
    {
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> that relates to the current 
        /// switch case.
        /// </summary>
        IMalleableExpressionCollection Cases { get; }
        /// <summary>
        /// Returns the <see cref="ISwitchStatement"/> in which the current
        /// <see cref="ISwitchCaseBlockStatement"/> exists within.
        /// </summary>
        new ISwitchStatement Parent { get; }
        /// <summary>
        /// Returns whether the current <see cref="ISwitchCaseBlockStatement"/>
        /// represents the default case
        /// </summary>
        bool IsDefault { get; set; }
    }
}
