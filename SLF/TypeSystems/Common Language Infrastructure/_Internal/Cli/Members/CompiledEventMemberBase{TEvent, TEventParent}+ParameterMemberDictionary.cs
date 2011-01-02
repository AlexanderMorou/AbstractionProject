using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledEventMemberBase<TMethod, TEvent, TEventParent>
        where TMethod :
            class,
            IMethodMember<TMethod, TEventParent>,
            IExtendedInstanceMember
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IMethodParent<TMethod, TEventParent>,
            IEventParent<TEvent, TEventParent>
    {
        private class ParameterMemberDictionary :
            LockedParameterMembersBase<TEvent, IEventParameterMember<TEvent, TEventParent>>
        {
            internal ParameterMemberDictionary(CompiledEventMemberBase<TMethod, TEvent, TEventParent> parent, IEnumerable<IEventParameterMember<TEvent, TEventParent>> parameters)
                : base(((TEvent)(object)(parent)), parameters)
            {
            }
        }
    }
}
