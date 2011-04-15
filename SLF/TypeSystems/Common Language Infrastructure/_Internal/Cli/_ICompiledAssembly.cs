using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// Returns the <see cref="Type"/> array relative to the types
        /// contained within the <see cref="_ICompiledAssembly"/>.
        /// </summary>
        Type[] AssemblyTypes { get; }
        /// <summary>
        /// Returns a <see cref="IList{T}"/> of the 
        /// namespace names.
        /// </summary>
        IList<string> FullNamespaceNames { get; }
        Tuple<FieldInfo[], MethodInfo[]> GetNamespaceMembers(string @namespace);

        /// <summary>
        /// Obtains the <see cref="ICompiledModule"/> associated to 
        /// the given <paramref name="module"/> provided.
        /// </summary>
        /// <param name="module">The <see cref="Module"/>
        /// to wrap.</param>
        /// <returns>A <see cref="ICompiledModule"/>
        /// relative to the <paramref name="module"/>
        /// proivded.</returns>
        ICompiledModule GetModule(Module module);
    }
}
