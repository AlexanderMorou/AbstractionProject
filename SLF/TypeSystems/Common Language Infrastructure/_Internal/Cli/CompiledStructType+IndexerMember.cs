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
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledStructType
    {
        private partial class IndexerMember :
            CompiledIndexerMemberBase<IStructIndexerMember, IStructType, IndexerMember.MethodMember, IStructMethodMember, IStructType>,
            IStructIndexerMember
        {
            internal IndexerMember(PropertyInfo memberInfo, CompiledStructType parent)
                : base(memberInfo, parent)
            {
            }
            private CompiledStructType _Parent { get { return (CompiledStructType)base.Parent; } }
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
