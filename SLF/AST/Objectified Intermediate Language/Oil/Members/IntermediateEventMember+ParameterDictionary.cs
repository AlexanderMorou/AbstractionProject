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
        protected sealed partial class ParameterDictionary :
            IntermediateParameterMemberDictionary<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>>
        {
            private new IntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember> Parent
            {
                get
                {
                    return ((IntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember>)((object)(base.Parent)));
                }
            }

            internal ParameterDictionary(IntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember> parent) 
                : base(((TIntermediateEvent)((object)(parent))))
            {
            }

            /// <summary>
            /// Obtains a <see cref="ParameterMember"/> 
            /// for insertion into the <see cref="ParameterDictionary"/>.
            /// </summary>
            /// <param name="name">The name of the parameter to create.</param>
            /// <param name="parameterType">The type of the parameter to create.</param>
            /// <param name="direction">The direction in which the <see cref="ParameterMember"/>
            /// is coerced.</param>
            /// <returns>A new <see cref="ParameterMember"/> instance, if successful.</returns>
            protected override IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                return new ParameterMember(this.Parent, name, parameterType, direction);
            }
        }
    }
}
