using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a series of <see cref="INamespaceDeclaration"/> 
    /// instances contained within a 
    /// <see cref="INamespaceParent"/>.
    /// </summary>
    public interface INamespaceDictionary :
        IDeclarationDictionary<IGeneralDeclarationUniqueIdentifier, INamespaceDeclaration>
    {
        /// <summary>
        /// Returns the <see cref="INamespaceParent"/>
        /// which contains the 
        /// <see cref="INamespaceDictionary"/>.
        /// </summary>
        INamespaceParent Parent { get; }
        /// <summary>
        /// Returns whether the <paramref name="path"/> provided
        /// exists in the <see cref="INamespaceDictionary"/> and 
        /// its elements and their children.
        /// </summary>
        /// <param name="path">The <see cref="String"/> that 
        /// represents the path of the namespace to check 
        /// the existance of.</param>
        /// <returns>true if a <see cref="INamespaceDeclaration"/>
        /// exists under the path provided; false, otherwise.</returns>
        /// <remarks>The path must be a series of 
        /// quantifiable namespace names delimited by a
        /// period.</remarks>
        bool PathExists(string path);
        /// <summary>
        /// Returns the <see cref="INamespaceDeclaration"/>
        /// based off of the <paramref name="path"/> provided.
        /// </summary>
        /// <param name="path">The <see cref="String"/> value denoting the
        /// potentially dotted trail of declaration identifiers that represent
        /// the path of the <see cref="INamespaceDeclaration"/> to retrieve.</param>
        /// <returns></returns>
        INamespaceDeclaration this[string path] { get; }
    }
}
