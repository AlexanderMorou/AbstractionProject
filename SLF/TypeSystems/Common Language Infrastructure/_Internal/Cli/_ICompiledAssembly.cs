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
    internal interface _ICompiledAssembly :
        ICompiledAssembly,
        _ICompiledNamespaceParent
    {
        /// <summary>
        /// Returns the <see cref="ITypeCollection"/>
        /// relative to the types contained within the <see cref="_ICompiledAssembly"/>.
        /// </summary>
        Type[] AssemblyTypes { get; }
        /// <summary>
        /// Returns a <see cref="IList{T}"/> of the 
        /// namespace names.
        /// </summary>
        IList<string> FullNamespaceNames { get; }
    }
}
