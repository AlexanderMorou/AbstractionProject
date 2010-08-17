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
        partial class IndexerMember
        {
            protected internal class MethodMember :
                CompiledInterfaceType.MethodMember,
                IPropertySignatureMethodMember
            {
                internal MethodMember(MethodInfo memberInfo, IInterfaceType parent, PropertyMethodType methodType)
                    : base(memberInfo, parent)
                {
                    this.MethodType = methodType;
                }
                #region IPropertySignatureMethodMember Members

                public PropertyMethodType MethodType { get; private set; }

                #endregion

            }
        }
    }
}
