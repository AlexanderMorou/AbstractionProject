using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliEventSignatureMember<TEvent, TEventParent> :
        CliEventSignatureMember<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventSignatureMember<TEvent, TEventParent>,
        _ICliParameterParent
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            class,
            IType,
            IEventSignatureParent<TEvent, TEventParent>
    {
        protected CliEventSignatureMember(TEventParent parent, ICliMetadataEventTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry, uniqueIdentifier)
        {
        }

        protected override sealed IParameterMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>> InitializeParameters()
        {
            if (this.SignatureType is ICliDelegateType)
                return new Parameters((_ICliManager)this.Parent.IdentityManager, this.MetadataEntry, this);
            else
                return new DependentParameterDictionary((TEvent)(object)this, this.SignatureType);
        }


        public _ICliManager IdentityManager
        {
            get { return (_ICliManager)this.Parent.IdentityManager; }
        }

        public ICliMetadataMethodSignature Signature
        {
            get {
                var signatureDelegate = ((ICliDelegateType)this.SignatureType);
                return signatureDelegate.MetadataEntry.MetadataRoot.TableStream.MethodDefinitionTable[(int)signatureDelegate.InvokeMethodIndex].Signature;
            }
        }

        public ICliAssembly Assembly
        {
            get { return (ICliAssembly)this.Parent.Assembly; }
        }

        ICliMetadataTableRow ICliDeclaration.MetadataEntry
        {
            get { return this.MetadataEntry; }
        }
    }
    internal abstract partial class CliEventSignatureMember<TEvent, TEventParameter, TEventParent> :
        CliMemberBase<IGeneralSignatureMemberUniqueIdentifier, TEventParent, ICliMetadataEventTableRow>,
        IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            class,
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private IParameterMemberDictionary<TEvent, TEventParameter> parameters;
        private IMethodSignatureMember onAddMethod;
        private IMethodSignatureMember onRemoveMethod;
        protected CliEventSignatureMember(TEventParent parent, ICliMetadataEventTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry)
        {
            this.uniqueIdentifier = uniqueIdentifier;
        }

        protected override string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get { return this.uniqueIdentifier; }
        }

        public IParameterMemberDictionary<TEvent, TEventParameter> Parameters
        {
            get
            {
                this.CheckParameters();
                return this.parameters;
            }
        }

        private void CheckParameters()
        {
            if (this.parameters == null)
                this.parameters = this.InitializeParameters();
        }

        protected abstract IParameterMemberDictionary<TEvent, TEventParameter> InitializeParameters();

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary)this.Parameters; }
        }

        private _ICliManager IdentityManager
        {
            get
            {
                return (_ICliManager)this.Parent.IdentityManager;
            }
        }

        public bool LastIsParams
        {
            get
            {
                var lastParam = this.Parameters.Values.LastOrDefault();
                if (lastParam != null)
                    return lastParam.IsDefined(this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ParamArrayMetadatum), this.Parent.Assembly));
                return false;
            }
        }

        public EventSignatureSource SignatureSource
        {
            get { return EventSignatureSource.Delegate; }
        }

        public IDelegateType SignatureType
        {
            get { return (IDelegateType)this.IdentityManager.ObtainTypeReference(this.MetadataEntry.SignatureType); }
        }

        public IType ReturnType
        {
            get { return this.SignatureType.ReturnType; }
        }

        public IMethodSignatureMember OnAddMethod
        {
            get {
                if (this.onAddMethod == null)
                    this.onAddMethod = this.InitializeOnAddMethodSignature();
                return this.onAddMethod;
            }
        }

        public IMethodSignatureMember OnRemoveMethod
        {
            get
            {
                if (this.onRemoveMethod == null)
                    this.onRemoveMethod = this.InitializeOnRemoveMethodSignature();
                return this.onAddMethod;
            }
        }

        protected abstract IMethodSignatureMember InitializeOnAddMethodSignature();

        protected abstract IMethodSignatureMember InitializeOnRemoveMethodSignature();
    }
}
