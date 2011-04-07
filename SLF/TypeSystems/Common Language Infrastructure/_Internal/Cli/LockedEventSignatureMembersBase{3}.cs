using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal partial class LockedEventSignatureMembersBase<TEventSignature, TEventSignatureParameter, TEventSignatureParent> :
        LockedSignatureMembersBase<TEventSignature, TEventSignatureParameter, TEventSignatureParent, EventInfo>,
        IEventSignatureMemberDictionary<TEventSignature, TEventSignatureParameter, TEventSignatureParent>,
        IEventSignatureMemberDictionary
        where TEventSignature :
            class,
            IEventSignatureMember<TEventSignature, TEventSignatureParameter, TEventSignatureParent>
        where TEventSignatureParameter :
            IEventSignatureParameterMember<TEventSignature, TEventSignatureParameter, TEventSignatureParent>
        where TEventSignatureParent :
            IEventSignatureParent<TEventSignature, TEventSignatureParameter, TEventSignatureParent>
    {
        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventSignatureParent parent, EventInfo[] sourceData, Func<EventInfo, TEventSignature> fetchImpl)
            : base(master, parent, sourceData, fetchImpl, GetName)
        {
        }
        private static string GetName(EventInfo @event)
        {
            return @event.Name;
        }
        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventSignatureParent parent)
            : base(master, parent)
        {
        }

        protected override string FetchKey(EventInfo item)
        {
            return item.Name;
        }
    }
}
