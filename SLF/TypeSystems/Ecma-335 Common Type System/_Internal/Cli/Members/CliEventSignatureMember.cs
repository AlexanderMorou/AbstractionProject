using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
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
        IEventSignatureMember<TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        protected CliEventSignatureMember(TEventParent parent, ICliMetadataEventTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry, uniqueIdentifier)
        {
        }

        protected override sealed IParameterMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>> InitializeParameters()
        {
            throw new NotImplementedException();
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
    }
}
