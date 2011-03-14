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
    [DebuggerDisplay("Interfaces: {Count}")]
    public class IntermediateInterfaceTypeDictionary :
        IntermediateGenericTypeDictionary<IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateInterfaceTypeDictionary
    {
        public IntermediateInterfaceTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }
        public IntermediateInterfaceTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateInterfaceTypeDictionary root)
            : base(parent, master, root)
        {
        }
        #region IInterfaceTypeDictionary Members

        ITypeParent IInterfaceTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IDeclarationDictionary<IInterfaceType> Members

        public new int IndexOf(IInterfaceType decl)
        {
            if (this.valuesInstance == null)
                return -1;
            int index = 0;
            foreach (var item in this.Values)
                if (item == decl)
                    return index;
                else
                    index++;
            return -1;
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="IIntermediateInterfaceType"/> 
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <see cref="IIntermediateStructType"/>.</param>
        /// <returns>A new <see cref="IntermediateInterfaceType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/>
        /// equals <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        protected override IIntermediateInterfaceType GetNewType(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentException("name");
            return new IntermediateInterfaceType(name, this.Parent);
        }
    }
}
