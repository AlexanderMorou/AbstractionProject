using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        IDeclarationDictionary<INamespaceDeclaration>
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
    }
}
