using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a local variable
    /// declaration statement.
    /// </summary>
    public interface IExplicitlyTypedLocalVariableDeclarationStatement :
        ILocalVariableDeclarationStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> representing the 
        /// kind of values that are defined by the 
        /// <see cref="ILocalVariableDeclarationStatement"/>.
        /// </summary>
        IType LocalType { get; set; }
    }
}