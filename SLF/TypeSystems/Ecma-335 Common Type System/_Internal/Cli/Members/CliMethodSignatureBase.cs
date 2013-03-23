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
        private _ICliAssembly assembly;
        private bool? lastIsParams;
        private CliParameterMemberDictionary<TSignature, TSignatureParameter> parameters;
        private CliMetadataCollection metadata;
        private CliMetadataCollection returnTypeMetadata;
        private  IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier;
        private TypeParameterDictionary typeParameters;
        /// <summary>
        /// Data member for maintaining a single-ton view of the generic
        /// closures of the generic series.
        /// </summary>
        private IDictionary<IControlledTypeCollection, TSignature> genericCache;

        protected CliMethodSignatureBase(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, TSignatureParent parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry)
        {
            this.assembly = assembly;
            this.uniqueIdentifier = uniqueIdentifier;
        }

        internal CliManager IdentityManager
        {
            get
            {
                return (CliManager) this.assembly.IdentityManager;
            }
        }

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

        private bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TSignature r)
        {
            if (this.genericCache == null)
                return false;
            var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
            if (fd == null)
                return false;
            r = this.genericCache[fd];
            return true;
        }

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
            throw new InvalidOperationException("Not a generic instance.");
        }

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

        protected abstract CliParameterMemberDictionary<TSignature, TSignatureParameter> InitializeParameters();

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary) this.Parameters; }
        }

        public bool LastIsParams
        {
            get
            {
                if (lastIsParams == null)
                {
                    var paramArrayAttrType = this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum, this.assembly), IdentityManager.GetRelativeAssembly(MetadataEntry.MetadataRoot));
                    var lParam = this.MetadataEntry.Parameters.LastOrDefault();
                    if (lParam != null && lParam.CustomAttributes.Count > 0)
                        lastIsParams = CliCommon.GetMetadatum(this.IdentityManager, paramArrayAttrType, lParam.CustomAttributes) != null;
                }
                return (bool) lastIsParams;
            }
        }

        #endregion

        #region IMethodSignatureMember Members

        public IType ReturnType
        {
            get { return this.IdentityManager.ObtainTypeReference(this.MetadataEntry.Signature.ReturnType, this.MetadataEntry.Signature.ReturnType.ReturnType, this.ActiveType, this); }
        }

        IMethodSignatureMember IMethodSignatureMember.GetGenericDefinition()
        {
            throw new InvalidOperationException();
        }

        public ILockedTypeCollection GenericParameters
        {
            get { return this.TypeParameters.Values.ToLockedCollection(); }
        }

        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection Metadata
        {
            get {
                if (this.metadata == null)
                    this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
                return this.metadata;
            }
        }

        public IMetadataCollection ReturnTypeMetadata
        {
            get
            {
                if (this.returnTypeMetadata == null && 
                    this.MetadataEntry.Signature.Parameters.Count > 0 &&
                    this.MetadataEntry.Parameters[0].Sequence == 0)
                    this.returnTypeMetadata = new CliMetadataCollection(this.MetadataEntry.Parameters[0].CustomAttributes, this, this.IdentityManager);
                return this.returnTypeMetadata;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IGenericParamParent<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember> Members

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

        public bool IsGenericDefinition
        {
            get { throw new NotImplementedException(); }
        }

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

        public bool ContainsGenericParameters
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


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

        protected abstract IType ActiveType { get; }

        internal abstract TypeParameter GetTypeParameter(int index, ICliMetadataGenericParameterTableRow metadataEntry);
    }
}
