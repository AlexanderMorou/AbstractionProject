using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _ClassTypeBase
    {
        private class _ConstructorsBase :
            _ConstructorMembersBase<IClassCtorMember, IClassType>
        {
            internal _ConstructorsBase(_FullMembersBase master, IConstructorMemberDictionary<IClassCtorMember, IClassType> originalSet, _ClassTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            protected override IClassCtorMember ObtainWrapper(IClassCtorMember item)
            {
                return new _Constructor(item, (_ClassTypeBase)this.Parent);
            }
            
            internal class _Constructor :
                _ConstructorBase<IClassCtorMember, IClassType>,
                IClassCtorMember
            {
                internal _Constructor(IClassCtorMember original, _ClassTypeBase parent)
                    : base(original, parent)
                {
                }
            }
        }
    }
}
