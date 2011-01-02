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
    internal class LockedEventMembersBase<TEvent, TEventParent> :
        LockedEventSignatureMembersBase<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventMemberDictionary<TEvent, TEventParent>,
        IEventMemberDictionary
        where TEvent :
            class,
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
        internal LockedEventMembersBase(LockedFullMembersBase master, TEventParent parent)
            : base(master, parent)
        {
        }

        internal LockedEventMembersBase(LockedFullMembersBase master, TEventParent parent, EventInfo[] sourceData, Func<EventInfo, TEvent> fetchImpl)
            : base(master, parent, sourceData, fetchImpl)
        {
        }

    }
}
