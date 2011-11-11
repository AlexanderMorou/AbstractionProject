using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a dictionary of intermediate namespace
    /// instances.
    /// </summary>
    public interface IIntermediateNamespaceDictionary :
        IIntermediateDeclarationDictionary<IGeneralDeclarationUniqueIdentifier, INamespaceDeclaration, IIntermediateNamespaceDeclaration>,
        INamespaceDictionary
    {
        /// <summary>
        /// Adds a new <see cref="IIntermediateNamespaceDeclaration"/> to the
        /// <see cref="IIntermediateNamespaceDictionary"/>.
        /// </summary>
        /// <param name="path">The <see cref="String"/> representing the namespace's
        /// fully qualified path.</param>
        /// <returns>A new <see cref="IIntermediateNamespaceDeclaration"/>
        /// instance that results from the operation.</returns>
        /// <remarks>The <paramref name="path"/> is segmented and delimited by periods (Full Stops, U+002E)
        /// which make up the invidual sub-namespaces of the <see cref="IIntermediateNamespaceDeclaration"/>
        /// that results.</remarks>
        /// <exception cref="System.ArgumentException"><paramref name="path"/> exists
        /// already; or <paramref name="path"/> is <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="path"/>
        /// is null.</exception>
        IIntermediateNamespaceDeclaration Add(string path);
        /// <summary>
        /// Adds an existing <see cref="IIntermediateNamespaceDeclaration"/>
        /// instance to the <see cref="IIntermediateNamespaceDictionary"/>.
        /// </summary>
        /// <param name="ns">the <see cref="IIntermediateNamespaceDeclaration"/> to add.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="ns"/> is null.</exception>
        void Add(IIntermediateNamespaceDeclaration ns);
        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceParent"/>
        /// which contains the 
        /// <see cref="IIntermediateNamespaceDictionary"/>.
        /// </summary>
        new IIntermediateNamespaceParent Parent { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceDeclaration"/>
        /// based off of the <paramref name="path"/> provided.
        /// </summary>
        /// <param name="path">The <see cref="String"/> value denoting the
        /// potentially dotted trail of declaration identifiers that represent
        /// the path of the <see cref="IIntermediateNamespaceDeclaration"/> to retrieve.</param>
        /// <returns></returns>
        new IIntermediateNamespaceDeclaration this[string path] { get; }
    }
}
