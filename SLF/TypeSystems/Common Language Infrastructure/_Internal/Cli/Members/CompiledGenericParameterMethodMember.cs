using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal partial class CompiledGenericParameterMethodMember<TGenericParameter> :
        MethodSignatureMemberBase<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterMethodMember<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        private Dictionary<ITypeCollectionBase, _GenericParameterMethodMemberBase<TGenericParameter>> genericCache = null;
        private MethodInfo memberInfo;
        bool lastIsParams;

        internal CompiledGenericParameterMethodMember(TGenericParameter parent, MethodInfo memberInfo)
            : base(parent)
        {
            this.memberInfo = memberInfo;
            this.lastIsParams = memberInfo.LastParameterIsParams();
        }

        protected MethodInfo MemberInfo
        {
            get
            {
                return this.memberInfo;
            }
        }

        protected override IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> InitializeTypeParameters()
        {
            return new LockedGenericParameters<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>(this, this.MemberInfo.GetGenericArguments().OnAll(gParamType =>
                    (IMethodSignatureGenericTypeParameterMember)new GenericParameterMember(this, gParamType)));
        }

        protected override ILockedTypeCollection OnGetGenericParameters()
        {
            return this.TypeParameters.Values.ToLockedCollection();
        }

        public override bool IsGenericConstruct
        {
            get { return this.MemberInfo.IsGenericMethod; }
        }

        protected override bool CanCacheReturn
        {
            get { return true; }
        }

        protected override bool CanCacheGenericParameters
        {
            get { return true; }
        }

        protected override IType OnGetReturnType()
        {
            return this.MemberInfo.ReturnType.GetTypeReference();
        }

        private bool ContainsGenericMethodSignature(ITypeCollectionBase typeParameters, ref _GenericParameterMethodMemberBase<TGenericParameter> r)
        {
            if (this.genericCache == null)
                return false;
            var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
            if (fd == null)
                return false;
            r = this.genericCache[fd];
            return true;
        }
        
        public override IGenericParameterMethodMember<TGenericParameter> MakeGenericClosure(ITypeCollectionBase genericReplacements)
        {
            //*//
            _GenericParameterMethodMemberBase<TGenericParameter> k = null;
            if (this.ContainsGenericMethodSignature(genericReplacements, ref k))
                return k;
            /* *
             * _IGenericMethodSignatureRegistrar handles cache.
             * */
            var v = new _GenericParameterMethodMemberBase<TGenericParameter>(this, genericReplacements);
            CLICommon.VerifyTypeParameters<IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>(this, genericReplacements);
            return v;
            //*/
        }

        protected override IGenericParameterMethodMember<TGenericParameter> OnGetGenericDefinition()
        {
            return null;
        }

        protected override IParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>> InitializeParameters()
        {
            return new LockedParameterMembersBase<IGenericParameterMethodMember<TGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>>(this, this.MemberInfo.GetParameters().OnAll<ParameterInfo, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>>(paramInfo => new ParameterMember(paramInfo, this)));
        }

        protected override bool LastIsParamsImpl
        {
            get { return this.lastIsParams; }
        }

        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }
    }
}
