﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using System.Reflection;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        private partial class IndexerMember :
            CompiledIndexerSignatureMemberBase<IInterfaceIndexerMember, IInterfaceType, IndexerMember.MethodMember, IInterfaceMethodMember, IInterfaceType>,
            IInterfaceIndexerMember
        {
            private bool lastIsParams;
            public IndexerMember(PropertyInfo memberInfo, IInterfaceType parent)
                : base(memberInfo, parent)
            {
                this.lastIsParams = memberInfo.LastParameterIsParams();
            }

            protected override IndexerMember.MethodMember OnGetMethod(PropertyMethodType methodType)
            {
                MethodInfo memberInfo = null;
                switch (methodType)
                {
                    case PropertyMethodType.GetMethod:
                        memberInfo = this.MemberInfo.GetGetMethod(true);
                        break;
                    case PropertyMethodType.SetMethod:
                        memberInfo = this.MemberInfo.GetSetMethod(true);
                        break;
                }
                if (memberInfo == null)
                    return null;
                return new MethodMember(memberInfo, this.Parent, methodType);
            }

            protected override IParameterMemberDictionary<IInterfaceIndexerMember, IIndexerSignatureParameterMember<IInterfaceIndexerMember, IInterfaceType>> InitializeParameters()
            {
                ParameterInfo[] parameters = this.MemberInfo.GetIndexParameters();
                return new ParameterMemberDictionary(this, parameters);
            }

            protected override bool LastIsParamsImpl
            {
                get { return lastIsParams; }
            }
        }
    }
}