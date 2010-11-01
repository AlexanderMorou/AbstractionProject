using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// assembly.
    /// </summary>
    public interface IIntermediateAssembly :
        IIntermediateNamespaceParent,
        IIntermediateSegmentableDeclaration<IIntermediateAssembly>,
        IIntermediateCustomAttributedDeclaration,
        IAssembly
    {
        /// <summary>
        /// Returns itself.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        new IIntermediateAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssemblyInformation"/> about 
        /// the current <see cref="IIntermediateAssembly"/> instance.
        /// </summary>
        new IIntermediateAssemblyInformation AssemblyInformation { get; }
        /// <summary>
        /// Returns/sets the <see cref="IIntermediateModule"/> which 
        /// exposes the manifest data for the current 
        /// <see cref="IIntermediateAssembly"/>.
        /// </summary>
        new IIntermediateModule ManifestModule { get; set; }
        /// <summary>
        /// Returns the <see cref="IAssemblyWorkspace"/> which maintains 
        /// a seamless integration of all the <see cref="IIntermediateAssembly"/>'s
        /// references as a unified namespace model.
        /// </summary>
        IAssemblyWorkspace Workspace { get; }
        /// <summary>
        /// Returns/sets the default namespace for the <see cref="IIntermediateAssembly"/>.
        /// </summary>
        IIntermediateNamespaceDeclaration DefaultNamespace { get; set; }
        /// <summary>
        /// Returns the <see cref="IPrivateImplementationDetails"/> part of the assembly
        /// which denotes specific features, such as defined anonymous types, 
        /// and so on.
        /// </summary>
        IPrivateImplementationDetails PrivateImplementationDetails { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateModuleDictionary"/>
        /// which contains the modules associated to the
        /// <see cref="IIntermediateAssembly"/>.
        /// </summary>
        new IIntermediateModuleDictionary Modules { get; }
        /// <summary>
        /// Returns/sets the filename associated to the present 
        /// assembly partial instance.
        /// </summary>
        /// <remarks>Used to specify the filename associated to a specific
        /// instance of an assembly.</remarks>
        string FileName { get; set; }
    }
}
