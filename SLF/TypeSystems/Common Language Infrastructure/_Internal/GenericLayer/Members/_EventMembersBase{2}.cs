using System;
using System.Collections.Generic;
using System.Text;
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

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _EventMembersBase<TEvent, TEventParent> :
        _EventSignatureMembersBase<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent, IEventMemberDictionary<TEvent, TEventParent>>,
        IEventMemberDictionary<TEvent, TEventParent>
        where TEvent :
            class,
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            class,
            IEventParent<TEvent, TEventParent>
    {
        internal _EventMembersBase(_FullMembersBase master, IEventMemberDictionary<TEvent, TEventParent> originalSet, TEventParent parent)
            : base(master, originalSet, parent)
        {
        }
    }
}
