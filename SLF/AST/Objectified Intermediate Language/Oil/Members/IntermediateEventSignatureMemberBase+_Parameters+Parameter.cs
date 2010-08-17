using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
    {
        partial class _Parameters
        {
            private class Parameter :
                IntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
                IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                public Parameter(TIntermediateEvent parent, string name, IType parameterType, ParameterDirection direction)
                    : base(parent, name, parameterType, direction)
                {
                }
            }
        }
    }
}
