using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _ClassTypeBase
    {
        private class _EventsBase :
            _EventMembersBase<IClassEventMember, IClassType>
        {
            private _ClassTypeBase Parent
            {
                get
                {
                    return (_ClassTypeBase)(base.Parent);
                }
            }

            public _EventsBase(_FullMembersBase master, IEventMemberDictionary<IClassEventMember, IClassType> originalSet, _ClassTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            private class _EventMember : 
                _EventMemberBase<IClassEventMember, IClassType>,
                IClassEventMember
            {
                private _ClassTypeBase Parent
                {
                    get
                    {
                        return (_ClassTypeBase)(base.Parent);
                    }
                }

                public _EventMember(IClassEventMember original, _ClassTypeBase parent)
                    : base(original, parent)
                {
                }

                protected override IMethodMember OnGetMethod(IMethodMember original)
                {
                    return new _MethodMember((IClassMethodMember)original, this.Parent);
                }

                private class _MethodMember :
                    _ClassTypeBase._MethodsBase._Method
                {
                    public _MethodMember(IClassMethodMember original, _ClassTypeBase parent)
                        : base(parent, original)
                    {

                    }
                }

            }

            protected override IClassEventMember ObtainWrapper(IClassEventMember item)
            {
                return new _EventMember(original: item, parent: this.Parent);
            }
        }
    }
}
