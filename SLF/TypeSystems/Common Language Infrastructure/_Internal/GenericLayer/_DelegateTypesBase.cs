using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _DelegateTypesBase :
        _Types<IDelegateType, IDelegateTypeDictionary>,
        IDelegateTypeDictionary
    {
        internal _DelegateTypesBase(_FullTypesBase master, IDelegateTypeDictionary originalSet, IGenericType parent)
            : base(master, originalSet, parent)
        {
        }

        #region IDelegateTypeDictionary Members

        ITypeParent IDelegateTypeDictionary.Parent
        {
            get { return ((ITypeParent)(base.Parent)); }
        }

        #endregion


        protected override IDelegateType ObtainWrapper(IDelegateType item)
        {
            if (this.Parent.GenericParameters.Count != item.GenericParameters.Count)
                return item;
            else
                return new _DelegateTypeBase(item, Parent.GenericParameters);
        }
    }
}
