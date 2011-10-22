using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _InterfaceTypesBase :
        _Types<IGeneralGenericTypeUniqueIdentifier, IInterfaceType, IInterfaceTypeDictionary>,
        IInterfaceTypeDictionary
    {
        private IGenericType _Parent
        {
            get
            {
                return base.Parent as IGenericType;
            }
        }

        internal _InterfaceTypesBase(_FullTypesBase master, IInterfaceTypeDictionary originalSet, IGenericType parent)
            : base(master, originalSet, parent)
        {
        }

        #region IInterfaceTypeDictionary Members

        ITypeParent IInterfaceTypeDictionary.Parent
        {
            get { return ((ITypeParent)(base.Parent)); }
        }

        #endregion

        protected override IInterfaceType ObtainWrapper(IInterfaceType item)
        {
            if (this._Parent.GenericParameters.Count != item.GenericParameters.Count)
                return item;
            else
                return new _InterfaceTypeBase(item, _Parent.GenericParameters);
        }
    }
}
