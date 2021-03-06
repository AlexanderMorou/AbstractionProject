﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _StructTypeBase
    {
        private class _ConstructorsBase :
            _ConstructorMembersBase<IStructCtorMember, IStructType>
        {
            internal _ConstructorsBase(_FullMembersBase master, IConstructorMemberDictionary<IStructCtorMember, IStructType> originalSet, _StructTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            protected override IStructCtorMember ObtainWrapper(IStructCtorMember item)
            {
                if (item == ((_StructTypeBase)this.Parent).Original.TypeInitializer)
                    return ((_StructTypeBase)this.Parent).TypeInitializer;
                return new _Constructor(item, (_StructTypeBase)this.Parent);
            }
            
            internal class _Constructor :
                _ConstructorBase<IStructCtorMember, IStructType>,
                IStructCtorMember
            {
                internal _Constructor(IStructCtorMember original, _StructTypeBase parent)
                    : base(original, parent)
                {
                }
            }
        }
    }
}
