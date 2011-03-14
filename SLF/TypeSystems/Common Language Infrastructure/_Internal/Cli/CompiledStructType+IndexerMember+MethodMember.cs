using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledStructType
    {
        partial class IndexerMember
        {
            internal class MethodMember :
                CompiledStructType.MethodMember,
                IPropertyMethodMember
            {
                private PropertyMethodType methodType;
                public MethodMember(MethodInfo memberInfo, CompiledStructType parent, PropertyMethodType methodType)
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
