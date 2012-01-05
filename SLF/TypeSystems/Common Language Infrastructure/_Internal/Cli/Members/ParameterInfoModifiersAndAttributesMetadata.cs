using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class ParameterInfoModifiersAndAttributesMetadata :
        ModifiersAndAttributesMetadata
    {
        private ParameterInfo parameterInfo;
        public ParameterInfoModifiersAndAttributesMetadata(ParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo;
        }
        protected override Type[] GetRequiredModifiers()
        {
            return parameterInfo.GetRequiredCustomModifiers();
        }

        protected override Type[] GetOptionalModifiers()
        {
            return parameterInfo.GetOptionalCustomModifiers();
        }

        protected override object[] GetCustomAttributes(bool inherit)
        {
            return parameterInfo.GetCustomAttributes(inherit);
        }
    }
}
