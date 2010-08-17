using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledGenericParameterMethodMember<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        protected class GenericParameterMember :
            CompiledGenericParameterMemberBase<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>,
            IMethodSignatureGenericTypeParameterMember
        {
            public GenericParameterMember(CompiledGenericParameterMethodMember<TGenericParameter> parent, Type underlyingSystemType)
                : base(parent, underlyingSystemType)
            {
            }

            protected override TypeKind TypeImpl
            {
                get { throw new NotImplementedException(); }
            }
        }
    }
}
