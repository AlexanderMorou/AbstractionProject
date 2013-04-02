using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _StructTypeBase
    {
        private class _EventsBase :
            _EventMembersBase<IStructEventMember, IStructType>
        {
            private new _StructTypeBase Parent
            {
                get
                {
                    return (_StructTypeBase)(base.Parent);
                }
            }

            public _EventsBase(_FullMembersBase master, IEventMemberDictionary<IStructEventMember, IStructType> originalSet, _StructTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            private class _EventMember :
                _EventMemberBase<IStructEventMember, IStructType>,
                IStructEventMember
            {
                private new _StructTypeBase Parent
                {
                    get
                    {
                        return (_StructTypeBase)(base.Parent);
                    }
                }

                public _EventMember(IStructEventMember original, _StructTypeBase parent)
                    : base(original, parent)
                {
                }

                protected override IMethodMember OnGetMethod(IMethodMember original)
                {
                    return new _MethodMember((IStructMethodMember)original, this.Parent);
                }

                private class _MethodMember :
                    _StructTypeBase._MethodsBase._Method
                {
                    public _MethodMember(IStructMethodMember original, _StructTypeBase parent)
                        : base(parent, original)
                    {

                    }
                }

            }

            protected override IStructEventMember ObtainWrapper(IStructEventMember item)
            {
                return new _EventMember(original: item, parent: this.Parent);
            }
        }
    }
}
