using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier;
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
            ICliManager manager = null;
            if (this.Parent is ICompiledType)
            {
                var parent = this.Parent as ICompiledType;
                manager = parent.Manager;
            }
            return new LockedGenericParameters<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>(this, this.MemberInfo.GetGenericArguments().OnAll(gParamType =>
                    (IMethodSignatureGenericTypeParameterMember) new GenericParameterMember(this, gParamType, manager)));
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
            return ((ICompiledType) this.Parent).Manager.ObtainTypeReference(this.MemberInfo.ReturnType);
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
            return new _GenericParameterMethodMemberBase<TGenericParameter>(this, genericReplacements);
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
        private ICliManager manager;
        private ICliManager Manager
        {
            get
            {
                if (this.manager == null)
                {
                    var parent = this.Parent as ICompiledType;
                    if (parent == null)
                    {
                        var aParent = this.Parent as ICompiledAssembly;
                        if (aParent == null)
                        {
                            var nParent = this.Parent as INamespaceDeclaration;
                            if (nParent == null ||
                                !(nParent.Assembly is ICompiledAssembly))
                                throw new InvalidOperationException();
                            else
                                manager = (nParent.Assembly as ICompiledAssembly).Manager;
                        }
                        else
                            manager = aParent.Manager;
                    }
                    else
                        manager = parent.Manager;
                }
                return manager;
            }
        }

        public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = this.MemberInfo.GetUniqueIdentifier(this.Manager);
                return this.uniqueIdentifier;
            }
        }

        protected override bool CanCacheReturnMetadata
        {
            get { return true; }
        }

        protected override IModifiersAndAttributesMetadata OnGetReturnMetadata()
        {
            return new MethodInfoModifiersAndAttributesMetadata(this.MemberInfo, this.Manager);
        }

        protected override bool CanCacheCustomAttributes
        {
            get { return true; }
        }

        protected override IMetadataCollection OnGetCustomAttributes()
        {
            return new CompiledCustomAttributeCollection(this.MemberInfo.GetCustomAttributes, this.Manager);
        }
    }
}
