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
        /// <param name="name">The <see cref="String"/> representing the namespace's
        /// fully qualified path.</param>
        /// <returns>A new <see cref="IIntermediateNamespaceDeclaration"/>
        /// instance that results from the operation.</returns>
        /// <remarks>The <paramref name="name"/> is segmented and delimited by periods (Full Stops, U+002E)
        /// which make up the invidual sub-namespaces of the <see cref="IIntermediateNamespaceDeclaration"/>
        /// that results.</remarks>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> exists
        /// already; or <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        IIntermediateNamespaceDeclaration Add(string name);
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
    }
}
