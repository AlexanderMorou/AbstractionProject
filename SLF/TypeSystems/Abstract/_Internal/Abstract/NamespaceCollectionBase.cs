using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    /// <summary>
    /// Provides a base rough implementation of 
    /// <see cref="INamespaceDictionary"/>.
    /// </summary>
    internal class NamespaceCollectionBase :
        DeclarationDictionaryBase<INamespaceDeclaration>,
        INamespaceDictionary
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private INamespaceParent parent;
        /// <summary>
        /// Creates a new <see cref="NamespaceCollectionBase"/>
        /// instance with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <see cref="INamespaceParent"/>
        /// which contains the <see cref="NamespaceCollectionBase"/>.</param>
        protected NamespaceCollectionBase(INamespaceParent parent)
        {
            this.parent = parent;
        }

        #region INamespaceDictionary Members

        /// <summary>
        /// Returns the <see cref="INamespaceParent"/>
        /// which contains the 
        /// <see cref="NamespaceCollectionBase"/>.
        /// </summary>
        public INamespaceParent Parent
        {
            get { return this.parent; }
        }

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
        public bool PathExists(string path)
        {
            return _CoreHelperMethods.PathExists(this, path);
        }

        #endregion

    }
}
