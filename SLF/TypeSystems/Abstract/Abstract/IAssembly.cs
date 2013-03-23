using System;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using System.Collections.Generic;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /* *
     * If you wonder why I did all of this on types, assemblies, modules and so on
     * it's because I didn't want to derive from the System bases.  Primarily
     * due to a few structural differences between how I preferred to work
     * and how the bases work.  Additionally, an 'IntermediateAssembly'
     * precedes an actual assembly, therefore deriving from the system's 
     * Assembly would confuse this fact.  Even though I used similar names,
     * I chose interfaces as a common ground to ensure that it's clear
     * that the only similarity between intermediate and compiled
     * assembly is in name and underlying functionality, not that
     * they are identical.
     * */
    /// <summary>
    /// Defines properties and methods for working with an assembly.
    /// </summary>
    public interface IAssembly :
        IDeclaration<IAssemblyUniqueIdentifier>,
        INamespaceParent,
        IMetadataEntity,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IAssemblyInformation"/> about the current <see cref="IAssembly"/>
        /// instance.
        /// </summary>
        IAssemblyInformation AssemblyInformation { get; }
        /// <summary>
        /// Returns the <see cref="IModule"/> which exposes
        /// the manifest data for the current <see cref="IAssembly"/>.
        /// </summary>
        IModule ManifestModule { get; }
        /// <summary>
        /// Returns the <see cref="IModuleDictionary"/> which denotes
        /// the individual modules the <see cref="IAssembly"/>
        /// consists of.
        /// </summary>
        IModuleDictionary Modules { get; }
        /// <summary>
        /// Returns whether the <see cref="IAssembly"/> has been
        /// disposed.
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// Returns the <see cref="IStrongNamePublicKeyInfo"/>
        /// associated to the <see cref="IAssembly"/>.
        /// </summary>
        IStrongNamePublicKeyInfo PublicKeyInfo { get; }
        /// <summary>
        /// Returns the <see cref="IControlledDictionary{TKey, TValue}">IControlledDictionary<IAssemblyUniqueIdentifier, IAssembly></see> which 
        /// </summary>
        IControlledDictionary<IAssemblyUniqueIdentifier, IAssembly> References { get; }

        /// <summary>
        /// Returns the <see cref="IType"/> relative to the <paramref name="typeIdentifier"/>
        /// provided.
        /// </summary>
        /// <param name="typeIdentifier">The <see cref="IGeneralTypeUniqueIdentifier"/> to obtain
        /// the <see cref="IType"/> of.</param>
        /// <returns>A <see cref="IType"/> relative to the <paramref name="typeIdentifier"/>
        /// provided.</returns>
        IType GetType(IGeneralTypeUniqueIdentifier typeIdentifier);

        /// <summary>
        /// Returns an <see cref="IEnumerable{IType}"/> which steps through the types
        /// within the assembly.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{IType}"/> which steps through the types
        /// within the assembly.</returns>
        IEnumerable<IType> GetTypes();
    }
}
