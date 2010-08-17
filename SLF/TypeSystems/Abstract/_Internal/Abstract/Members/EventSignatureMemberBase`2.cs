using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract class EventSignatureMemberBase<TEvent, TEventParent> :
        EventSignatureMemberBase<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventSignatureMember<TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Creates a new <see cref="EventSignatureMemberBase{TEvent, TEventParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The parent of the 
        /// <see cref="EventSignatureMemberBase{TEvent, TEventParent}"/>.</param>
        public EventSignatureMemberBase(TEventParent parent)
            : base(parent)
        {
        }
    }
}
