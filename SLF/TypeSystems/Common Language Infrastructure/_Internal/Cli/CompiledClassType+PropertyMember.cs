using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledClassType
    {
        private sealed class PropertyMember :
            CompiledPropertyMemberBase<IClassPropertyMember, IClassType, IClassPropertyMethodMember, IClassMethodMember, IClassType>,
            IClassPropertyMember,
            ICompiledPropertyMember
        {
            public PropertyMember(PropertyInfo memberInfo, CompiledClassType @class)
                : base(memberInfo, @class)
            {

            }

            protected override IClassPropertyMethodMember OnGetMethod(PropertyMethodType methodType, MethodInfo memberInfo)
            {
                return new MethodMember(methodType, memberInfo, ((CompiledClassType)(this.Parent)));
            }

            protected override AccessLevelModifiers AccessLevelImpl
            {
                get { return this.MemberInfo.GetAccessModifiers(); }
            }
            private class MethodMember :
                CompiledClassType.MethodMember,
                IClassPropertyMethodMember,
                IPropertyMethodMember
            {
                
                internal MethodMember(PropertyMethodType methodType, MethodInfo memberInfo, CompiledClassType parent)
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
