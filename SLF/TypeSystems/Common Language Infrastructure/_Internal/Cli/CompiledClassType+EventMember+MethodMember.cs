using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using System.Reflection;
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
