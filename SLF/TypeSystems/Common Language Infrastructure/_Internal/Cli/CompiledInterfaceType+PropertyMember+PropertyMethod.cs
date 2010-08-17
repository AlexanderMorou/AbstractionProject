using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        partial class PropertyMember
        {
            internal sealed class PropertyMethod :
                MethodMember,
                IPropertySignatureMethodMember
            {
                private PropertyMethodType methodType;

                public PropertyMethod(MethodInfo memberInfo, IInterfaceType parent, PropertyMethodType methodType)
                    : base(memberInfo, parent)
                {
                    this.methodType = methodType;
                }

                #region IPropertySignatureMethodMember Members

                public PropertyMethodType MethodType
                {
                    get { return this.methodType; }
                }

                #endregion
            }
        }
    }
}
