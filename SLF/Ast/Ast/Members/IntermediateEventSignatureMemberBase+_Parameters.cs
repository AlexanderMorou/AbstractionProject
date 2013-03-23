using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodSignature>
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
        where TMethodSignature :
            class,
            IIntermediateMethodSignatureMember
    {
        protected partial class _Parameters :
            IntermediateParameterMemberDictionary<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>>
        {
            public _Parameters(TIntermediateEvent parent)
                : base(parent)
            {
            }

            protected override IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> GetNewParameter(string name, IType parameterType, ParameterCoercionDirection direction)
            {
                return new Parameter(this.Parent, name, parameterType, direction);
            }
        }
    }
}
