using System;
using System.Collections.Generic;
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

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _EventSignatureMembersBase<TEvent, TEventParent> :
        _EventSignatureMembersBase<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent, IEventSignatureMemberDictionary<TEvent, TEventParent>>,
        IEventSignatureMemberDictionary<TEvent, TEventParent>
        where TEvent :
            class,
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            class,
            IEventSignatureParent<TEvent, TEventParent>
    {
        internal _EventSignatureMembersBase(_FullMembersBase master, IEventSignatureMemberDictionary<TEvent, TEventParent> originalSet, TEventParent parent)
            : base(master, originalSet, parent)
        {
        }

    }
}
