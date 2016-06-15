using System;
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
    partial class _InterfaceTypeBase
    {
        private class _Events :
            _EventSignatureMembersBase<IInterfaceEventMember, IInterfaceType>
        {
            private _InterfaceTypeBase _Parent { get { return (_InterfaceTypeBase)base.Parent; } }
            internal _Events(_FullMembersBase master, IEventSignatureMemberDictionary<IInterfaceEventMember, IInterfaceType> originalSet, _InterfaceTypeBase parent)
                : base(master, originalSet, parent)
            {
            }
            protected override IInterfaceEventMember ObtainWrapper(IInterfaceEventMember item)
            {
                return new _Event(item, this._Parent);
            }
            private class _Event :
                _EventSignatureMemberBase<IInterfaceEventMember, IInterfaceType>,
                IInterfaceEventMember
            {
                internal _Event(IInterfaceEventMember original, _InterfaceTypeBase parent)
                    : base(original, parent)
                {
                }

                protected override IMethodSignatureMember OnGetMethod(IMethodSignatureMember original)
                {
                    return new _MethodsBase._Method(this.Parent, (IInterfaceMethodMember)original);
                }
            }
        }
    }
}
