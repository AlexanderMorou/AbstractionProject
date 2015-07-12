using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _MethodSignatureMemberBase<TSignatureParameter, TSignature, TSignatureParent> :
        MethodSignatureMemberBase<TSignatureParameter, TSignature, TSignatureParent>,
        IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>,
        IGeneralDeclarationsParent<TSignature, TSignatureParameter>
        where TSignatureParameter :
            class,
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        /// <summary>
        /// Data member which links to the original signature.
        /// </summary>
        private TSignature original;
        /// <summary>
        /// For when the signature is a generic itself...
        /// </summary>
        internal ILockedTypeCollection genericReplacements;
        private ILockedTypeCollection typeParameters;

        /// <summary>
        /// Creates a new <see cref="_MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// with the <paramref name="parent"/> and <paramref name="original"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> the 
        /// the <see cref="_MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// is contained within.</param>
        /// <param name="original">The <typeparamref name="TSignature"/> from which
        /// the current instance is based.</param>
        public _MethodSignatureMemberBase(TSignatureParent parent, TSignature original)
            : base(parent)
        {
            if (original is _IGenericMethodSignatureRegistrar && parent is IMethodSignatureParent)
                ((_IGenericMethodSignatureRegistrar)(original)).RegisterGenericChild((IMethodSignatureParent)parent, (IMethodSignatureMember)(this));
            else if (original is _IGenericMethodRegistrar && parent is IMethodParent)
                ((_IGenericMethodRegistrar)(original)).RegisterGenericChild((IMethodParent)parent, (IMethodMember)(this));
            this.original = original;
        }

        public _MethodSignatureMemberBase(TSignature original, IControlledTypeCollection genericReplacements)
            : base(original.Parent)
        {
            /* *
             * Lock the series if it's not already.
             * */
            if (!(genericReplacements is ILockedTypeCollection))
                genericReplacements = new LockedTypeCollection(genericReplacements);
            if (original is _IGenericMethodSignatureRegistrar)
                ((_IGenericMethodSignatureRegistrar) (original)).RegisterGenericMethod((IMethodSignatureMember) (this), genericReplacements);
            else if (original is _IGenericMethodRegistrar)
                ((_IGenericMethodRegistrar) (original)).RegisterGenericMethod((IMethodMember) (this), genericReplacements);
            this.GenericReplacementsImpl = (ILockedTypeCollection) genericReplacements;
            this.original = original;
        }

        protected override IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> InitializeTypeParameters()
        {
            if (this.GenericReplacementsImpl != null)
                /* *
                 * Generic method instances
                 * don't have malleable type-parameters
                 * */
                throw new InvalidOperationException();
            /* *
             * ToDo: create wrapper to encapsulate and alter parent target to
             *       current instance instead of the original.
             * */
            return original.TypeParameters;
        }

        internal ILockedTypeCollection GenericReplacementsImpl
        {
            get
            {
                return this.genericReplacements;
            }
            set
            {
                this.typeParameters = null;
                this.genericParameters = null;
                this.genericReplacements = value;
            }
        }

        protected override ILockedTypeCollection OnGetGenericParameters()
        {
            if (this.typeParameters == null)
            {
                if (this.GenericReplacementsImpl == null)
                    this.typeParameters = this.TypeParameters.Values.ToLockedCollection();
                else
                    this.typeParameters = this.GenericReplacementsImpl;
            }
            return this.typeParameters;
        }

        public override bool IsGenericConstruct
        {
            get
            {
                return this.Original.IsGenericConstruct;
            }
        }

        protected override bool CanCacheReturn
        {
            get
            {
                return false;
            }
        }

        protected override bool CanCacheGenericParameters
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Obtains the <see cref="IType"/> that the 
        /// <see cref="_MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// yields upon return.
        /// </summary>
        protected override IType OnGetReturnType()
        {
            return ResolveType(this.original.ReturnType);
        }

        protected IType ResolveType(IType targetType)
        {
            if (Parent is IGenericType)
            {
                IGenericType parent = ((IGenericType)(this.Parent));
                if (parent.IsGenericConstruct)
                {
                    if (!parent.IsGenericDefinition)
                        if (this.IsGenericConstruct && this.GenericReplacementsImpl != null)
                            return targetType.Disambiguify(parent.GenericParameters, this.GenericReplacementsImpl, TypeParameterSources.Both);
                        else
                            return targetType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                else if (this.IsGenericConstruct && this.GenericReplacementsImpl != null)
                    return targetType.Disambiguify(null, this.GenericReplacementsImpl, TypeParameterSources.Method);
            }
            else if (this.IsGenericConstruct && this.GenericReplacementsImpl != null)
                return targetType.Disambiguify(null, this.GenericReplacementsImpl, TypeParameterSources.Method);
            return targetType;
        }

        protected override bool LastIsParamsImpl
        {
            get
            {
                return this.original.LastIsParams;
            }
        }

        protected override string OnGetName()
        {
            return this.original.Name;
        }

        public override void Dispose()
        {
            if (this.typeParameters != null)
            {
                this.typeParameters.Dispose();
                this.typeParameters = null;
            }
            if (this.GenericReplacementsImpl != null)
            {
                this.GenericReplacementsImpl.Dispose();
                this.GenericReplacementsImpl = null;
            }
            base.Dispose();
        }
        TSignature IGeneralDeclarationsParent<TSignature, TSignatureParameter>.Original
        {
            get
            {
                return this.original;
            }
        }

        protected TSignature Original
        {
            get
            {
                return this.original;
            }
        }

        protected sealed override TSignature OnGetGenericDefinition()
        {
            if (this.GenericReplacementsImpl != null)
                return this.Original;
            else
                return default(TSignature);
        }

        public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.IsGenericConstruct)
                    return TypeSystemIdentifiers.GetGenericSignatureIdentifier(this.Name, this.Original.TypeParameters.Count, this.Parameters.ParameterTypes);
                else
                    return TypeSystemIdentifiers.GetGenericSignatureIdentifier(this.Name, 0, this.Parameters.ParameterTypes);
            }
        }

        protected override bool CanCacheReturnMetadata
        {
            get
            {
                return false;
            }
        }

        protected override IMetadataCollection OnGetReturnMetadata()
        {
            return this.Original.ReturnTypeMetadata;
        }


        protected override bool CanCacheCustomAttributes
        {
            get
            {
                return false;
            }
        }

        protected override IMetadataCollection OnGetCustomAttributes()
        {
            return this.Original.Metadata;
        }

    }
}