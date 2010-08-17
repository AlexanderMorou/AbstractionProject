using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public interface IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IEventParameterMember<TEvent, TEventParent>
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
    {
    }
}
