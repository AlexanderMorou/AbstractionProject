using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Linkers;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// assembly.
    /// </summary>
    public interface IIntermediateAssembly :
        IIntermediateNamespaceParent,
        IIntermediateSegmentableDeclaration<IAssemblyUniqueIdentifier, IIntermediateAssembly>,
        IIntermediateMetadataEntity,
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
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateDeclarationVisitor{TResult, TContext}"/>
        /// which should receive the <see cref="IIntermediateAssembly"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TResult, TContext>(IIntermediateDeclarationVisitor<TResult, TContext> visitor, TContext context);
        /// <summary>
        /// Returns the <see cref="IAssemblyReferenceCollection"/> associated
        /// to the <see cref="IIntermediateAssembly"/>.
        /// </summary>
        new IAssemblyReferenceCollection References { get; }
        /// <summary>
        /// Returns the <see cref="IMalleableCompilationContext"/> associated to the 
        /// <see cref="IIntermediateAssembly"/> which denotes the
        /// output type, target file, and other properties which describe the 
        /// resulted assembly.
        /// </summary>
        IMalleableCompilationContext CompilationContext { get; }
        /// <summary>
        /// Returns the <see cref="ILanguageProvider"/>
        /// which made the assembly and denotes information about the
        /// source language.
        /// </summary>
        /// <remarks>May be null.</remarks>
        ILanguageProvider Provider { get; }
        /// <summary>
        /// Returns the <see cref="ILanguage"/> which denotes information
        /// about the source language.
        /// </summary>
        /// <remarks>May be null.</remarks>
        ILanguage Language { get; }
        /// <summary>
        /// Returns the <see cref="IStrongNamePrivateKeyInfo"/> which denotes
        /// the strong-name key pair information for the <see cref="IIntermediateAssembly"/>.
        /// </summary>
        IStrongNamePrivateKeyInfo PrivateKeyInfo { get; set; }
        /// <summary>
        /// Occurs after the <see cref="IIntermediateAssembly"/> has changed in a way
        /// which invalidates the previous unique identifier.
        /// </summary>
        new event EventHandler<DeclarationIdentifierChangeEventArgs<IAssemblyUniqueIdentifier>> IdentifierChanged;
        /// <summary>
        /// Returns an <see cref="IEnumerable{IType}"/> which steps through the types
        /// within the assembly.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{IType}"/> which steps through the types
        /// within the assembly.</returns>
        new IEnumerable<IType> GetTypes();
    }
}
