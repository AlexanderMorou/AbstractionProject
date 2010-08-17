using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledClassType
    {
        partial class IndexerMember
        {
            internal class MethodMember :
                CompiledClassType.MethodMember,
                IPropertyMethodMember
            {
                private PropertyMethodType methodType;
                public MethodMember(MethodInfo memberInfo, CompiledClassType parent, PropertyMethodType methodType)
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
