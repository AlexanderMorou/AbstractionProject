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

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedEventSignatureMembersBase<TEventSignature, TEventParent> :
        LockedEventSignatureMembersBase<TEventSignature, IEventSignatureParameterMember<TEventSignature, TEventParent>, TEventParent>,
        IEventSignatureMemberDictionary<TEventSignature, TEventParent>
        where TEventSignature :
            class,
            IEventSignatureMember<TEventSignature, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEventSignature, TEventParent>
    {
        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventParent parent)
            : base(master, parent)
        {
        }

        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventParent parent, EventInfo[] sourceData, Func<EventInfo, TEventSignature> fetchImpl)
            : base(master, parent, sourceData, fetchImpl)
        {
        }
    }
}
