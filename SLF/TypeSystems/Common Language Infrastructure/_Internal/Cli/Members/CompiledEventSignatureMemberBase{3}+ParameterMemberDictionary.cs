using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        private class ParameterMemberDictionary :
            LockedParameterMembersBase<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>>
        {
            internal ParameterMemberDictionary(CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParent> parent, IEnumerable<IEventSignatureParameterMember<TEvent, TEventParent>> parameters)
                : base(((TEvent)(object)(parent)), parameters)
            {
            }
        }
    }
}
