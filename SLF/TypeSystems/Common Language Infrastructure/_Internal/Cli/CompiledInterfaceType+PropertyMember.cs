using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType 
    {
        private sealed partial class PropertyMember :
            CompiledPropertySignatureMemberBase<IInterfacePropertyMember, IInterfaceType, PropertyMember.PropertyMethod, IInterfaceMethodMember, IInterfaceType>,
            IInterfacePropertyMember
        {

            public PropertyMember(PropertyInfo memberInfo, IInterfaceType parent)
                : base(parent, memberInfo)
            {
            }

            protected override PropertyMember.PropertyMethod OnGetMethod(PropertyMethodType methodType, MethodInfo memberInfo)
            {
                return new PropertyMethod(memberInfo, this.Parent, methodType);
            }
        }
    }
}
