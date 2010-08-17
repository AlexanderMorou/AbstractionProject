using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        private partial class MethodMember :
            CompiledMethodSignatureMemberBase<IInterfaceMethodMember, IInterfaceType>,
            //MethodSignatureMemberBase<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IInterfaceMethodMember, IInterfaceType>,
            IInterfaceMethodMember
        {
            public MethodMember(MethodInfo memberInfo, IInterfaceType parent)
                : base(memberInfo, parent)
            {
            }

            protected override IInterfaceMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
            {
                return new _InterfaceTypeBase._MethodsBase._Method(this, genericReplacements); ;
            }
        }
    }
}
