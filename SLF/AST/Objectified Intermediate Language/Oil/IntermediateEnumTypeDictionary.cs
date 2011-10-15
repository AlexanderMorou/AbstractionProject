using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [DebuggerDisplay("Enumerations: {Count}")]
    public class IntermediateEnumTypeDictionary :
        IntermediateTypeDictionary<IGeneralTypeUniqueIdentifier, IEnumType, IIntermediateEnumType>,
        IIntermediateEnumTypeDictionary
    {
        public IntermediateEnumTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }
        public IntermediateEnumTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateEnumTypeDictionary root)
            : base(parent, master, root)
        {
        }
        #region IEnumTypeDictionary Members

        ITypeParent IEnumTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion


        /// <summary>
        /// Creates a new <see cref="IIntermediateEnumType"/> 
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <see cref="IIntermediateStructType"/>.</param>
        /// <returns>A new <see cref="IntermediateEnumType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/>
        /// equals <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        protected override IIntermediateEnumType GetNewType(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentException("name");
            return new IntermediateEnumType(name, this.Parent);
        }

    }
}
