using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledClassType
    {
        internal sealed class EventMember :
            CompiledEventMemberBase<IClassMethodMember, IClassEventMember, IClassType>,
            IClassEventMember
        {
            public EventMember(EventInfo memberInfo, CompiledClassType parent)
                : base(memberInfo, parent)
            {

            }

            protected override IClassMethodMember OnGetMethod(MethodInfo memberInfo)
            {
                return new MethodMember(memberInfo, this._Parent);
            }

            private CompiledClassType _Parent
            {
                get
                {
                    return (CompiledClassType)base.Parent;
                }
            }

            private class MethodMember :
                CompiledClassType.MethodMember
            {
                public MethodMember(MethodInfo memberInfo, CompiledClassType parent)
                    : base(memberInfo, parent)
                {
                    
                }
            }
        }
    }
}
