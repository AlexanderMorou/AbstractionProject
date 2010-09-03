using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [DebuggerDisplay("Delegates: {Count}")]
    public class IntermediateDelegateTypeDictionary :
        IntermediateGenericTypeDictionary<IDelegateType, IIntermediateDelegateType>,
        IIntermediateDelegateTypeDictionary
    {
        public IntermediateDelegateTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }
        public IntermediateDelegateTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateDelegateTypeDictionary root)
            : base(parent, master, root)
        {
        }
        #region IDelegateTypeDictionary Members

        ITypeParent IDelegateTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        /// <summary>
        /// Creates and adds a new <see cref="IIntermediateDelegateType"/>
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <see cref="IIntermediateDelegateType"/>.</param>
        /// <returns>A new <see cref="IIntermediateDelegateType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when a type by the <paramref name="name"/>
        /// provided already exists in the containing type parent, or <paramref name="name"/> equals
        /// <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        public override IIntermediateDelegateType Add(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentException("name");

            var result = new IntermediateDelegateType(name, this.Parent);
            base.Add(result.UniqueIdentifier, result);
            return result;
        }
    }
}
