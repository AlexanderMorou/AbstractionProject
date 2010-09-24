using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledStructType
    {
        private class PropertyMember :
            CompiledPropertyMemberBase<IStructPropertyMember, IStructType, IStructPropertyMethodMember, IStructMethodMember, IStructType>,
            IStructPropertyMember,
            ICompiledPropertyMember
        {
            public PropertyMember(PropertyInfo propInfo, CompiledStructType @struct)
                : base(propInfo, @struct)
            {
            }

            protected override IStructPropertyMethodMember OnGetMethod(PropertyMethodType methodType, MethodInfo memberInfo)
            {
                return new MethodMember(methodType, memberInfo, (CompiledStructType)this.Parent);
            }

            protected override AccessLevelModifiers AccessLevelImpl
            {
                get { return this.MemberInfo.GetAccessModifiers(); }
            }

            private sealed class MethodMember :
                CompiledStructType.MethodMember,
                IStructPropertyMethodMember,
                IPropertyMethodMember
            {

                internal MethodMember(PropertyMethodType methodType, MethodInfo memberInfo, CompiledStructType parent)
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
