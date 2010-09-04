using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a default implementation of a dictionary 
    /// of a series of intermediate structure types.
    /// </summary>
    [DebuggerDisplay("Data Structures: {Count}")]
    public class IntermediateStructTypeDictionary :
        IntermediateGenericTypeDictionary<IStructType, IIntermediateStructType>,
        IIntermediateStructTypeDictionary
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateStructTypeDictionary"/> with the
        /// <paramref name="parent"/> and <paramref name="master"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the
        /// <see cref="IntermediateStructTypeDictionary"/></param>
        /// <param name="master">The <see cref="IntermediateFullTypeDictionary"/>
        /// which maintains a verbatim-order set of kind-inspecific types.</param>
        /// <exception cref="System.ArgumentNullException">thrown when one or more of 
        /// <paramref name="parent"/> or <paramref name="master"/> is null.</exception>
        public IntermediateStructTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateStructTypeDictionary"/> with the
        /// <paramref name="parent"/>, <paramref name="master"/>, and <paramref name="root"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the
        /// <see cref="IntermediateStructTypeDictionary"/></param>
        /// <param name="master">The <see cref="IntermediateFullTypeDictionary"/>
        /// which maintains a verbatim-order set of kind-inspecific types.</param>
        /// <param name="root">The <see cref="IntermediateStructTypeDictionary"/> 
        /// which is synchronized with the current instance to enable varied parents
        /// across a single set of structure declarations; used for partial types.</param>
        /// <exception cref="System.ArgumentNullException">thrown when one or more of <paramref name="parent"/>,
        /// <paramref name="master"/> or <paramref name="root"/> is null.</exception>
        public IntermediateStructTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateStructTypeDictionary root)
            : base(parent, master, root)
        {
        }
        #region IStructTypeDictionary Members

        ITypeParent IStructTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="IIntermediateStructType"/> 
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <see cref="IIntermediateStructType"/>.</param>
        /// <returns>A new <see cref="IntermediateStructType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/>
        /// equals <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        protected override IIntermediateStructType GetNewType(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentException("name");
            return new IntermediateStructType(name, this.Parent);
        }

    }
}
