﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledClassType
    {
        partial class IndexerMember :
            CompiledIndexerMemberBase<IClassIndexerMember, IClassType, IndexerMember.MethodMember,IClassMethodMember,IClassType>,
            IClassIndexerMember
        {
            internal IndexerMember(PropertyInfo memberInfo, CompiledClassType parent)
                : base(memberInfo, parent)
            {
            }
            private CompiledClassType _Parent { get { return (CompiledClassType)base.Parent; } }
            protected override IndexerMember.MethodMember OnGetMethod(PropertyMethodType methodType)
            {
                MethodInfo memberInfo;
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        memberInfo = this.MemberInfo.GetSetMethod(true);
                        break;
                    case PropertyMethodType.GetMethod:
                    default:
                        memberInfo = this.MemberInfo.GetGetMethod(true);
                        break;
                }
                return new MethodMember(memberInfo, this._Parent, methodType);
            }
        }
    }
}