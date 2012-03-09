using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private ILockedTypeCollection genericReplacements;
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
            this.original = original;
        }

        public _MethodSignatureMemberBase(TSignature original, ITypeCollectionBase genericReplacements)
            : base(original.Parent)
        {
            /* *
             * Lock the series if it's not already.
             * */
            if (!(genericReplacements is ILockedTypeCollection))
                genericReplacements = new LockedTypeCollection(genericReplacements);
            if (original is _IGenericMethodSignatureRegistrar)
                ((_IGenericMethodSignatureRegistrar)(original)).RegisterGenericMethodSignature((IMethodSignatureMember)(this), genericReplacements);
            if (original is _IGenericMethodRegistrar)
                ((_IGenericMethodRegistrar)(original)).RegisterGenericMethod((IMethodMember)(this), genericReplacements);
            this.genericReplacements = (ILockedTypeCollection)genericReplacements;
            this.original = original;
        }

        protected override IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> InitializeTypeParameters()
        {
            if (this.genericReplacements != null)
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


        protected override ILockedTypeCollection OnGetGenericParameters()
        {
            if (this.typeParameters == null)
            {
                if (this.genericReplacements == null)
                    this.typeParameters = this.TypeParameters.Values.ToLockedCollection();
                else
                    this.typeParameters = this.genericReplacements;
            }
            return this.typeParameters;
        }

        public override bool IsGenericConstruct
        {
            get
            {
                return this.original.IsGenericConstruct;
            }
        }

        protected override bool CanCacheReturn
        {
            get { return false; }
        }

        protected override bool CanCacheGenericParameters
        {
            get { return true; }
        }

        /// <summary>
        /// Obtains the <see cref="IType"/> that the 
        /// <see cref="_MethodSignatureMemberBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// yields upon return.
        /// </summary>
        protected override IType OnGetReturnType()
        {
            if (Parent is IGenericType)
            {
                IGenericType parent = ((IGenericType)(this.Parent));
                if (parent.IsGenericConstruct)
                {
                    if (!parent.IsGenericDefinition)
                        if (this.IsGenericConstruct && this.genericReplacements != null)
                            return this.original.ReturnType.Disambiguify(parent.GenericParameters, this.genericReplacements, TypeParameterSources.Both);
                        else
                            return this.original.ReturnType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                else if (this.IsGenericConstruct && this.genericReplacements != null)
                    return this.original.ReturnType.Disambiguify(null, this.genericReplacements, TypeParameterSources.Method);
            }
            else if (this.IsGenericConstruct && this.genericReplacements != null)
                return this.original.ReturnType.Disambiguify(null, this.genericReplacements, TypeParameterSources.Method);
            return this.original.ReturnType;
        }

        protected override bool LastIsParamsImpl
        {
            get { return this.original.LastIsParams; }
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
            if (this.genericReplacements != null)
            {
                this.genericReplacements.Dispose();
                this.genericReplacements = null;
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
            if (this.genericReplacements != null)
                return this.Original;
            else
                return default(TSignature);
        }

        public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.IsGenericConstruct)
                    return AstIdentifier.GenericSignature(this.Name, this.Original.TypeParameters.Count, this.Parameters.ParameterTypes);
                else
                    return AstIdentifier.GenericSignature(this.Name, 0, this.Parameters.ParameterTypes);
            }
        }

         protected override bool CanCacheReturnMetadata
        {
            get { return false; }
        }

        protected override IModifiersAndAttributesMetadata OnGetReturnMetadata()
        {
            return this.Original.ReturnTypeMetadata;
        }


        protected override bool CanCacheCustomAttributes
        {
            get { return false; }
        }

        protected override ICustomAttributeCollection OnGetCustomAttributes()
        {
            return this.Original.CustomAttributes;
        }

    }
}