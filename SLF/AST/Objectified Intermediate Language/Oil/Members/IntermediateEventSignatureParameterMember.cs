using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public class IntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> :
        IntermediateSignatureParameterMemberBase<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParameter :
            TEventParameter,
            IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateEventSignatureParameterMember{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>
        /// with the <paramref name="parent"/>,
        /// <paramref name="name"/>, <paramref name="parameterType"/> and
        /// <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateEvent"/> which
        /// contains the <see cref="IntermediateEventSignatureParameterMember{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>.</param>
        /// <param name="name">The <see cref="String"/>
        /// name of the parameter.</param>
        /// <param name="parameterType">The <see cref="IType"/> of the parameter.</param>
        /// <param name="direction">The <see cref="ParameterDirection"/> which determines how the informaiton about the parameter
        /// is managed (in, out, or by reference).</param>
        public IntermediateEventSignatureParameterMember(TIntermediateEvent parent, string name, IType parameterType, ParameterDirection direction)
            : base(parent)
        {
            this.Name = name;
            this.ParameterType = parameterType;
            this.Direction = direction;
        }
    }
}
