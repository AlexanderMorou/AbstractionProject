﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _InterfaceTypesBase :
        _Types<IInterfaceType, IInterfaceTypeDictionary>,
        IInterfaceTypeDictionary
    {
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
            throw new NotImplementedException();
        }
    }
}