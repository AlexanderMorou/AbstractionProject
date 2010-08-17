using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Modules
{
    /// <summary>
    /// Defines properties and methods for working with a module of an assembly.
    /// </summary>
    public interface IModule :
        IMethodParent<IModuleGlobalMethod, IModule>,
        IFieldParent<IModuleGlobalField, IModule>,
        IDeclaration
    {
        /// <summary>
        /// Returns the global methods defined on the current <see cref="IModule"/>.
        /// </summary>
        new IModuleGlobalMethods Methods { get; }
        /// <summary>
        /// Returns the global fields defined on the current <see cref="IModule"/>.
        /// </summary>
        new IModuleGlobalFields Fields { get; }
        /// <summary>
        /// Returns the <see cref="IAssembly"/> in which the <see cref="IModule"/> was defined.
        /// </summary>
        IAssembly Parent { get; }
    }
}
