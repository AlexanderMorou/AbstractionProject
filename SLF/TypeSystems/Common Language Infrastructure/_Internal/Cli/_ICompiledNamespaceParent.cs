using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides an interface for relating back to the 
    /// compiled assembly to provide enumerative access
    /// to the types contained within the assembly.
    /// </summary>
    internal interface _ICompiledNamespaceParent :
        INamespaceParent
    {
        new _ICompiledAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="_ICompiledNamespaceDeclarations"/>
        /// of <see cref="INamespaceDeclaration"/> instances
        /// contained within the <see cref="_ICompiledNamespaceParent"/>.
        /// </summary>
        new _ICompiledNamespaceDeclarations Namespaces { get; }
        /// <summary>
        /// Returns a <see cref="IList{T}"/> of the 
        /// namespace names.
        /// </summary>
        IList<string> NamespaceNames { get; }
    }
}
