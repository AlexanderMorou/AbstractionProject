using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent> :
        CliMemberBase<IGeneralGenericSignatureMemberUniqueIdentifier, TSignatureParent, ICliMetadataMethodDefinitionTableRow>,
        IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>,
        _ICliParameterParent
        where TSignatureParameter :
            class,
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {

        /// <summary>
        /// Data member for <see cref="Assembly"/>.
        /// </summary>
        private _ICliAssembly assembly;
        /// <summary>
        /// Data member for <see cref="LastIsParams"/>.
        /// </summary>
        /// <remarks>Nullable since calls to <see cref="LastIsParams"/>
        /// peek into metadata.</remarks>
        private bool? lastIsParams;
        /// <summary>
        /// Data member for <see cref="Parameters"/>.
        /// </summary>
        private CliParameterMemberDictionary<TSignature, TSignatureParameter> parameters;
        /// <summary>
        /// Data member for <see cref="Metadata"/>.
        /// </summary>
        private CliMetadataCollection metadata;
        /// <summary>
        /// Data member for <see cref="ReturnTypeMetadata"/>.
        /// </summary>
        private CliMetadataCollection returnTypeMetadata;
        /// <summary>
        /// Data member for <see cref="DeclarationBase{TIdentifier}.UniqueIdentifier"/>.
        /// </summary>
        private IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier;
        /// <summary>
        /// Data member for <see cref="TypeParameters"/>.
        /// </summary>
        private TypeParameterDictionary typeParameters;

        /// <summary>
        /// Creates a new <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/> instance
        /// with the <paramref name="metadataEntry"/>, <paramref name="assembly"/>,
        /// <paramref name="parent"/> and <paramref name="uniqueIdentifier"/>
        /// provided.
        /// </summary>
        /// <param name="metadataEntry">The <see cref="ICliMetadataMethodDefinitionTableRow"/> in which the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// represents.</param>
        /// <param name="assembly">The <see cref="_ICliAssembly"/> in which the
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// is defined.</param>
        /// <param name="parent">The <typeparamref name="TSignatureParent"/> which contains the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/></param>
        /// <param name="uniqueIdentifier">The <see cref="IGeneralGenericSignatureMemberUniqueIdentifier"/> which uniquely identifies the 
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// from other members.</param>
        protected CliMethodSignatureBase(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, TSignatureParent parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry)
        {
            this.assembly = assembly;
            this.uniqueIdentifier = uniqueIdentifier;
        }

        internal _ICliAssembly _Assembly { get { return (_ICliAssembly)this.Assembly; } }

        internal CliManager IdentityManager
        {
            get
            {
                return (CliManager) this.assembly.IdentityManager;
            }
        }

        /// <summary>
        /// Returns the <see cref="CliParameterMemberDictionary{TSignature, TSignatureParameter}"/>
        /// which holds the <typeparamref name="TSignatureParameter"/> instances contained within the
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        protected CliParameterMemberDictionary<TSignature, TSignatureParameter> Parameters
        {
            get
            {
                CheckParameters();
                return this.parameters;
            }
        }

        private void CheckParameters()
        {
            if (this.parameters == null)
                this.parameters = this.InitializeParameters();
        }

        #region IMethodSignatureMember<TSignatureParameter,TSignature,TSignatureParent> Members

        protected abstract bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TSignature r);

        public TSignature MakeGenericClosure(IControlledTypeCollection genericReplacements) {
            if (!this.IsGenericConstruct)
                throw new InvalidOperationException("not a generic method");
            TSignature k = default(TSignature);
            IGenericType genericParent = null;
            if (this.Parent is IGenericType && (genericParent = ((IGenericType)(this.Parent))).IsGenericConstruct &&
                genericParent.IsGenericDefinition)
                throw new InvalidOperationException("Cannot obtain a closed generic method whose containing type is an open generic definition.");
            if (this.ContainsGenericMethod(genericReplacements, ref k))
                return k;
            /* *
             * _IGenericMethodRegistrar handles cache.
             * */
            var tK = this.OnMakeGenericClosure(genericReplacements);
            //CliCommon.VerifyTypeParameters<TSignatureParameter, TSignature, TParent>(this, genericReplacements);
            return tK;

        }

        protected abstract TSignature OnMakeGenericClosure(IControlledTypeCollection genericReplacements);

        public TSignature GetGenericDefinition()
        {
            if (this.IsGenericConstruct)
                throw new InvalidOperationException("Invalid for an entity which is the generic definition.");
            else
                throw new InvalidOperationException("Not a generic construct.");
        }

        /// <summary>
        /// Returns the original generic form of the current
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// generic variant.
        /// </summary>
        /// <returns>A <typeparamref name="TSignature"/>
        /// which denotes the original generic variant of the current
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when <see cref="IsGenericConstruct"/> is false.</exception>
        public TSignature MakeGenericClosure(params IType[] typeParameters)
        {
            if (this.IsGenericConstruct)
                throw new InvalidOperationException("Not a generic construct.");
            return MakeGenericClosure(typeParameters.ToLockedCollection());
        }

        #endregion

        #region IParameterParent<TSignature,TSignatureParameter> Members

        IParameterMemberDictionary<TSignature, TSignatureParameter> IParameterParent<TSignature, TSignatureParameter>.Parameters
        {
            get
            {
                return this.Parameters;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="CliParameterMemberDictionary{TSignature, TSignatureParameter}"/>
        /// used to house the generic type parameters of the
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        /// <returns>An instance of <see cref="CliParameterMemberDictionary{TSignature, TSignatureParameter}"/>
        /// used to house the generic type parameters of the
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.</returns>
        protected abstract CliParameterMemberDictionary<TSignature, TSignatureParameter> InitializeParameters();

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary) this.Parameters; }
        }

        /// <summary>
        /// Returns whether the last parameter of the set is a parameter array.
        /// </summary>
        public bool LastIsParams
        {
            get
            {
                return this.IsLastParams(this.Assembly, this._Assembly.IdentityManager);
            }
        }

        #endregion

        #region IMethodSignatureMember Members

        /// <summary>
        /// Returns the <see cref="IType"/> that the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// yields upon return.
        /// </summary>
        public IType ReturnType
        {
            get 
            {
                return this.IdentityManager.ObtainTypeReference(this.MetadataEntry.Signature.ReturnType, this.MetadataEntry.Signature.ReturnType.ReturnType, this.ActiveType, this, activeAssembly: this.ActiveType != null ? this.ActiveType.Assembly : null); 
            }
        }

        IMethodSignatureMember IMethodSignatureMember.GetGenericDefinition()
        {
            return this.GetGenericDefinition();
        }

        public ILockedTypeCollection GenericParameters
        {
            get { return this.TypeParameters.Values.ToLockedCollection(); }
        }

        #endregion

        #region IMetadataEntity Members

        /// <summary>
        /// Returns the <see cref="IMetadataCollection"/> 
        /// associated to the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        public IMetadataCollection Metadata
        {
            get {
                if (this.metadata == null)
                    this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
                return this.metadata;
            }
        }

        /// <summary>
        /// Returns the <see cref="IMetadataCollection"/> which represents
        /// the <see cref="IMetadatum"/> elements defined upon the return type
        /// of the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        public IMetadataCollection ReturnTypeMetadata
        {
            get
            {
                if (this.returnTypeMetadata == null)
                    if (this.MetadataEntry.Signature.Parameters.Count > 0 &&
                    this.MetadataEntry.Parameters[0].Sequence == 0)
                        this.returnTypeMetadata = new CliMetadataCollection(this.MetadataEntry.Parameters[0].CustomAttributes, this, this.IdentityManager);
                    else
                        this.returnTypeMetadata = new CliMetadataCollection(null, this, this.IdentityManager);
                return this.returnTypeMetadata;
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="metadatumType"/> 
        /// is defined on the current 
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/> of
        /// the attribute to check the presence of.</param>
        /// <returns>true if an attribute of the given 
        /// <paramref name="metadatumType"/> is defined
        /// on the current <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </returns>
        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }

        #endregion

        #region IGenericParamParent<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember> Members
        /// <summary>
        /// Returns the <see cref="IGenericParameterDictionary{TGenericParameter, TParent}"/> which contains the 
        /// generic parameters of the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        public IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> TypeParameters
        {
            get {
                if (this.typeParameters == null)
                    this.typeParameters = new TypeParameterDictionary(this);
                return this.typeParameters;
            }
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        #endregion

        #region IGenericParamParent Members

        /// <summary>
        /// Returns whether the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/> is
        /// the generic instance.
        /// </summary>
        public bool IsGenericDefinition
        {
            get {
                return this.MetadataEntry.TypeParameters.Count > 0;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>
        /// is a generic construct capable of utilizing type-replacements.
        /// </summary>
        public bool IsGenericConstruct
        {
            get {
                if (this.MetadataEntry.TypeParameters == null)
                    return false;
                return this.MetadataEntry.TypeParameters.Count > 0; 
            }
        }

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary) this.TypeParameters; }
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        /// <summary>
        /// Returns whether the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/> 
        /// contains generic parameters.
        /// </summary>
        public bool ContainsGenericParameters
        {
            get
            {
                return this.ReturnType.ContainsGenericParameters() ||
                       this.Parameters.ParameterTypes.Any(parameterType => parameterType.ContainsGenericParameters());
            }
        }

        #endregion

        /// <summary>
        /// Obtains the <see cref="Name"/> for the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that contains the name of the 
        /// <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.</returns>
        protected override string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier { get { return this.uniqueIdentifier; } }

        #region _ICliParameterParent Members

        _ICliManager _ICliParameterParent.IdentityManager
        {
            get { return this.IdentityManager; }
        }

        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodSignature"/>
        /// associated to the <see cref="CliMethodSignatureBase{TSignatureParameter, TSignature, TSignatureParent}"/>.
        /// </summary>
        public ICliMetadataMethodSignature Signature
        {
            get { return this.MetadataEntry.Signature; }
        }

        #endregion

        public ICliAssembly Assembly
        {
            get { return this.assembly; }
        }

        #region ICliDeclaration Members

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }

        #endregion

        /// <summary>
        /// Returns the <see cref="IType"/> which represents the active
        /// type within the active context.
        /// </summary>
        /// <remarks>Utilized within signature type
        /// resolution for type-parameters.</remarks>
        protected abstract IType ActiveType { get; }

        /// <summary>
        /// Obtains a new <see cref="TypeParameter"/> instance for the current
        /// instance relative to the <paramref name="index"/> and <paramref name="metadataEntry"/> provided.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value denoting the index within the
        /// set of type-parameters to retrieve a new <see cref="TypeParameter"/>
        /// for.</param>
        /// <param name="metadataEntry">The <see cref="ICliMetadataGenericParameterTableRow"/>
        /// which is the metadata representation of the type parameter.</param>
        /// <returns>An instance of a <see cref="TypeParameter"/> relative to the <paramref name="index"/>
        /// and <paramref name="metadataEntry"/>.</returns>
        internal abstract TypeParameter GetTypeParameter(int index, ICliMetadataGenericParameterTableRow metadataEntry);

        public override string ToString()
        {
            return string.Format("method {0}", this.UniqueIdentifier.ToString(this.Parent.ToString()));
        }

    }
}
