using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
/*---------------------------------------------------------------------\
| Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a local variable
    /// declaration statement.
    /// </summary>
    public interface IExplicitlyTypedLocalVariableDeclarationStatement :
        ILocalDeclarationStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> representing the 
        /// kind of values that are defined by the 
        /// <see cref="ILocalDeclarationStatement"/>.
        /// </summary>
        IType LocalType { get; set; }
    }
}
