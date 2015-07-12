using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// condition statement which alters the flow of the 
    /// execution based upon a boolean condition within a
    /// breakable section of code.
    /// </summary>
    public interface IBreakableConditionBlockStatement :
        IBreakableConditionContinuationStatement,
        IConditionBlockStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IBreakableConditionContinuationStatement"/> which continues the
        /// code flow control conditioning.
        /// </summary>
        new IBreakableConditionContinuationStatement Next { get; set; }
    }
}
