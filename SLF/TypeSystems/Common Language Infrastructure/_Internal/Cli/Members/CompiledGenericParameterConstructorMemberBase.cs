using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal partial class CompiledGenericParameterConstructorMemberBase<TGenericParameter> :
        GenericParameterConstructorMemberBase<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        bool lastIsParams;
        private ConstructorInfo memberInfo;
        internal CompiledGenericParameterConstructorMemberBase(TGenericParameter parent, ConstructorInfo ctorInfo)
            : base(parent)
        {
            this.memberInfo = ctorInfo;
            this.lastIsParams = ctorInfo.LastParameterIsParams();
        }

        protected ConstructorInfo MemberInfo { get { return this.memberInfo; } }

        protected override sealed bool LastIsParamsImpl
        {
            get { return this.lastIsParams; }
        }

        protected override AccessLevelModifiers AccessLevelImpl
        {
            get { return this.MemberInfo.GetAccessModifiers(); }
        }

        protected override IParameterMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, IConstructorParameterMember<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>> InitializeParameters()
        {
            return new Parameters(this.MemberInfo.GetParameters(), this);
        }
    }
}
