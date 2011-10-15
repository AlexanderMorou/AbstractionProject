using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParentIdentifier, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        private class ParameterMemberDictionary :
            LockedParameterMembersBase<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>>
        {
            internal ParameterMemberDictionary(CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParentIdentifier, TEventParent> parent, IEnumerable<IEventSignatureParameterMember<TEvent, TEventParent>> parameters)
                : base(((TEvent)(object)(parent)), parameters)
            {
            }
        }
    }
}
