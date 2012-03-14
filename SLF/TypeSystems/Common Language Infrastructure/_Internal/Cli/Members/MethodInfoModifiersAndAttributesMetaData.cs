using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class MethodInfoModifiersAndAttributesMetadata :
        ModifiersAndAttributesMetadata
    {
        private MethodInfo memberInfo;
        private CompiledCustomAttributeCollection attributes;

        public MethodInfoModifiersAndAttributesMetadata(MethodInfo memberInfo, ICliManager manager)
            : base(manager)
        {
            this.memberInfo = memberInfo;
        }


        protected override Type[] GetRequiredModifiers()
        {
            return this.memberInfo.ReturnParameter.GetRequiredCustomModifiers();
        }

        protected override Type[] GetOptionalModifiers()
        {
            return this.memberInfo.ReturnParameter.GetOptionalCustomModifiers();
        }

        protected override object[] GetCustomAttributes(bool inherit)
        {
            return this.memberInfo.ReturnParameter.GetCustomAttributes(inherit);
        }
    }
}
