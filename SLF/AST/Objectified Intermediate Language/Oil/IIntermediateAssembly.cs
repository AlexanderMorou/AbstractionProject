using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IIntermediateFieldParent<IStaticDataField, IIntermediateStaticDataField, IAssembly, IIntermediateAssembly>,
        IAssembly,
        IConcreteNode
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
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateDeclarationVisitor"/>
        /// which should receive the <see cref="IIntermediateAssembly"/> as a visitor.</param>
        void Visit(IIntermediateDeclarationVisitor visitor);
        /// <summary>
        /// Returns the <see cref="IAssemblyReferenceCollection"/> associated
        /// to the <see cref="IIntermediateAssembly"/>.
        /// </summary>
        IAssemblyReferenceCollection References { get; }
        /// <summary>
        /// Returns the <see cref="IMalleableCompilationContext"/> associated to the 
        /// <see cref="IIntermediateAssembly"/> which denotes the
        /// output type, target file, 
        /// </summary>
        IMalleableCompilationContext CompilationContext { get; }
    }
}
