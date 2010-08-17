using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

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
        private partial class _Parameters :
            IntermediateParameterMemberDictionary<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>>
        {
            public _Parameters(TIntermediateEvent parent)
                : base(parent)
            {
            }

            protected override IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                return new Parameter(this.Parent, name, parameterType, direction);
            }
        }
    }
}
