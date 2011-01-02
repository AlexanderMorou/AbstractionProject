using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression which can 
    /// function as a statement.
    /// </summary>
    public interface IStatementExpression :
        IExpression
    {
        /// <summary>
        /// Returns whether the <see cref="IStatementExpression"/> is valid as a statement in its 
        /// current form.
        /// </summary>
        bool ValidAsStatement { get; }
    }
}
