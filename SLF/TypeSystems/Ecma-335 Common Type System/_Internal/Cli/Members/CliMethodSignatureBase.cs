using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent> :
        IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            class,
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        private TSignatureParent parent;
        private ICliMetadataMethodDefinitionTableRow metadataEntry;
        private _ICliAssembly assembly;
        private bool? lastIsParams;
        private CliParameterMemberDictionary<TSignature, TSignatureParameter> parameters;

        protected CliMethodSignatureBase(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, TSignatureParent parent)
        {
            this.metadataEntry = metadataEntry;
            this.assembly = assembly;
            this.parent = parent;
        }

        protected ICliMetadataMethodDefinitionTableRow MetadataEntry
        {
            get
            {
                return this.metadataEntry;
            }
        }

        internal _ICliManager IdentityManager
        {
            get
            {
                return (_ICliManager) this.assembly.IdentityManager;
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

        public abstract TSignature MakeGenericClosure(ITypeCollectionBase genericReplacements);

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
                    var paramArrayAttrType = this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum, this.assembly), IdentityManager.GetRelativeAssembly(metadataEntry.MetadataRoot));
                    var lParam = metadataEntry.Parameters.LastOrDefault();
                    if (lParam != null && lParam.CustomAttributes.Count > 0)
                        lastIsParams = CliCommon.GetMetadatum(this.IdentityManager, paramArrayAttrType, lParam.CustomAttributes) != null;
                }
                return (bool) lastIsParams;
            }
        }

        #endregion

        #region IMember<IGeneralGenericSignatureMemberUniqueIdentifier,TSignatureParent> Members

        public TSignatureParent Parent
        {
            get { return this.parent; }
        }

        #endregion

        #region IDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier> Members

        public IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDeclaration Members

        public string Name
        {
            get { return this.metadataEntry.Name; }
        }

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }

        public event EventHandler Disposed;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.metadataEntry = null;
        }

        #endregion

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IMethodSignatureMember Members

        public IType ReturnType
        {
            get { throw new NotImplementedException(); }
        }

        IMethodSignatureMember IMethodSignatureMember.GetGenericDefinition()
        {
            throw new NotImplementedException();
        }

        public ILockedTypeCollection GenericParameters
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection Metadata
        {
            get {
                throw new NotImplementedException();
            }
        }

        public IMetadataCollection ReturnTypeMetadata
        {
            get
            {
                throw new NotImplementedException();
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

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(ITypeCollectionBase typeParameters)
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
            get { return this.metadataEntry.TypeParameters.Count > 0; }
        }

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary) this.TypeParameters; }
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(ITypeCollectionBase typeParameters)
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

    }
}
