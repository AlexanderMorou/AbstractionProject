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
    internal class LockedEventSignatureMembersBase<TEventSignature, TEventSignatureParent> :
        LockedEventSignatureMembersBase<TEventSignature, IEventSignatureParameterMember<TEventSignature, TEventSignatureParent>, TEventSignatureParent>,
        IEventSignatureMemberDictionary<TEventSignature, TEventSignatureParent>
        where TEventSignature :
            class,
            IEventSignatureMember<TEventSignature, TEventSignatureParent>
        where TEventSignatureParent :
            IEventSignatureParent<TEventSignature, TEventSignatureParent>
    {
        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventSignatureParent parent)
            : base(master, parent)
        {
        }

        internal LockedEventSignatureMembersBase(LockedFullMembersBase master, TEventSignatureParent parent, EventInfo[] sourceData, Func<EventInfo, TEventSignature> fetchImpl)
            : base(master, parent, sourceData, fetchImpl)
        {
        }
    }
}
