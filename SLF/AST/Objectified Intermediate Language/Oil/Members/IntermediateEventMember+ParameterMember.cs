using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember>
        where TMethodMember :
            class,
            IIntermediateEventMethodMember
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
        partial class ParameterDictionary
        {
            protected sealed class ParameterMember :
                IntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
                IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                /// <summary>
                /// Creates a new <see cref="ParameterMember"/> with the <paramref name="parent"/>,
                /// <paramref name="name"/>, <paramref name="parameterType"/> and
                /// <paramref name="direction"/> provided.
                /// </summary>
                /// <param name="parent">The <typeparamref name="TIntermediateEvent"/> which
                /// contains the <see cref="ParameterMember"/>.</param>
                /// <param name="name">The <see cref="String"/>
                /// name of the parameter.</param>
                /// <param name="parameterType">The <see cref="IType"/> of the parameter.</param>
                /// <param name="direction">The <see cref="ParameterDirection"/> which determines how the informaiton about the parameter
                /// is managed (in, out, or by reference).</param>
                public ParameterMember(IntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember> parent, string name, IType parameterType, ParameterDirection direction)
                    : base(((TIntermediateEvent)((object)(parent))), name, parameterType, direction)
                {
                }
            }
        }
    }
}
