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
    internal class _StructTypesBase :
        _Types<IStructType, IStructTypeDictionary>,
        IStructTypeDictionary
    {
        internal _StructTypesBase(_FullTypesBase master, IStructTypeDictionary originalSet, IGenericType parent)
            : base(master, originalSet, parent)
        {
        }

        #region IStructTypeDictionary Members

        ITypeParent IStructTypeDictionary.Parent
        {
            get { return ((ITypeParent)(base.Parent)); }
        }

        #endregion

        protected override IStructType ObtainWrapper(IStructType item)
        {
            if (this.Parent.GenericParameters.Count != item.GenericParameters.Count)
                return item;
            else
                return new _StructTypeBase(item, Parent.GenericParameters);
        }
    }
}