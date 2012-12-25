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
        protected CliMethodSignatureBase(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, TSignatureParent parent)
            : base(parent, metadataEntry)
        {
            this.assembly = assembly;
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

        public abstract TSignature MakeGenericClosure(IControlledTypeCollection genericReplacements);

        public TSignature GetGenericDefinition()
        {
            throw new InvalidOperationException("Not a generic instance.");
        }

        public TSignature MakeGenericClosure(params IType[] typeParameters)
        {
            if (this.IsGenericConstruct)
                throw new InvalidOperationException("Not a generic construct.");
            throw new NotImplementedException();
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

                throw new NotSupportedException();
                //if (this.returnTypeMetadata == null)
                //    this.returnTypeMetadata = new CliMetadataCollection(this.MetadataEntry.Signature.ReturnType);
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
            get { throw new NotImplementedException(); }
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            throw new NotImplementedException();
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(params IType[] typeParameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IGenericParamParent Members

        public bool IsGenericDefinition
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsGenericConstruct
        {
            get { return this.MetadataEntry.TypeParameters.Count > 0; }
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

        public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get { return (IGeneralGenericSignatureMemberUniqueIdentifier)CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry, this.Parent as IType, this.IdentityManager, () => this); }
        }


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
    }
}
