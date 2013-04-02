using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _ClassTypesBase :
        _Types<IGeneralGenericTypeUniqueIdentifier, IClassType, IClassTypeDictionary>,
        IClassTypeDictionary
    {
        private IGenericType _Parent
        {
            get
            {
                return base.Parent as IGenericType;
            }
        }
        internal _ClassTypesBase(_FullTypesBase master, IClassTypeDictionary originalSet, IGenericType parent)
            : base(master, originalSet, parent)
        {
        }

        #region IClassTypeDictionary Members

        ITypeParent IClassTypeDictionary.Parent
        {
            get { return ((ITypeParent)(base.Parent)); }
        }

        #endregion

        protected override IClassType ObtainWrapper(IClassType item)
        {
            if (this._Parent.GenericParameters.Count != item.GenericParameters.Count)
                return item;
            else
                return new _ClassTypeBase(item, _Parent.GenericParameters);
        }
    }
}
