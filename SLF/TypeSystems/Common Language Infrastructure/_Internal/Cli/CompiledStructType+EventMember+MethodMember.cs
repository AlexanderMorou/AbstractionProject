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
    partial class CompiledStructType
    {
        internal sealed class EventMember :
            CompiledEventMemberBase<IStructMethodMember, IStructEventMember, IStructType>,
            IStructEventMember
        {
            public EventMember(EventInfo memberInfo, CompiledStructType parent)
                : base(memberInfo, parent)
            {

            }

            protected override IStructMethodMember OnGetMethod(MethodInfo memberInfo)
            {
                return new MethodMember(memberInfo, this._Parent);
            }

            private CompiledStructType _Parent
            {
                get
                {
                    return (CompiledStructType)base.Parent;
                }
            }

            private class MethodMember :
                CompiledStructType.MethodMember
            {
                public MethodMember(MethodInfo memberInfo, CompiledStructType parent)
                    : base(memberInfo, parent)
                {
                    
                }
            }
        }
    }
}
