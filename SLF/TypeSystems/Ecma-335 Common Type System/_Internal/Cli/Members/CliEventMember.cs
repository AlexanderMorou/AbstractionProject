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
    internal abstract partial class CliEventMember<TEvent, TEventParent> :
        CliEventSignatureMember<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventMember<TEvent, TEventParent>,
        _ICliParameterParent
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            class,
            IType,
            IEventParent<TEvent, TEventParent>
    {
        private IMethodMember onFireMethod;

        protected CliEventMember(TEventParent parent, ICliMetadataEventTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(parent, metadataEntry, uniqueIdentifier)
        {
        }

        protected override sealed IParameterMemberDictionary<TEvent, IEventParameterMember<TEvent, TEventParent>> InitializeParameters()
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
            get
            {
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

        public new IMethodMember OnAddMethod
        {
            get
            {
                return (IMethodMember)base.OnAddMethod;
            }
        }

        public new IMethodMember OnRemoveMethod
        {
            get
            {
                return (IMethodMember)base.OnRemoveMethod;
            }
        }

        public IMethodMember OnRaiseMethod
        {
            get
            {
                if (this.onFireMethod == null &&
                    this.MetadataEntry.OnFire != null)
                    this.onFireMethod = InitializeOnRaiseMethod();// new EventMethodMember(this.MetadataEntry.OnFire, (_ICliAssembly)this.Parent.Assembly, this, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnFire, this.Parent, this.Parent.IdentityManager, () => this.OnRaiseMethod), EventMethodType.Fire);
                return this.onFireMethod;
            }
        }

        protected abstract IMethodMember InitializeOnAddMethod();
        protected abstract IMethodMember InitializeOnRemoveMethod();
        protected abstract IMethodMember InitializeOnRaiseMethod();

        protected override sealed IMethodSignatureMember InitializeOnAddMethodSignature()
        {
            return this.InitializeOnAddMethod();
        }

        protected override sealed IMethodSignatureMember InitializeOnRemoveMethodSignature()
        {
            return this.InitializeOnRemoveMethod();
        }

        public bool LastIsParams
        {
            get
            {
                return this.IsLastParams(Assembly, (_ICliManager)this.Assembly.IdentityManager);
            }
        }

        public bool CanRaise
        {
            get
            {
                return this.OnRaiseMethod != null;
            }
        }

        public ExtendedMemberAttributes Attributes
        {
            get { throw new NotImplementedException(); }
        }


        public bool IsAbstract
        {
            get
            {
                return (this.Attributes & ExtendedMemberAttributes.Abstract) == ExtendedMemberAttributes.Abstract;
            }
        }

        public bool IsVirtual
        {
            get
            {
                return (this.Attributes & ExtendedMemberAttributes.Virtual) == ExtendedMemberAttributes.Virtual;
            }
        }

        public bool IsFinal
        {
            get
            {
                return (this.Attributes & ExtendedMemberAttributes.Final) == ExtendedMemberAttributes.Final;
            }
        }

        public bool IsOverride
        {
            get
            {
                return (this.Attributes & ExtendedMemberAttributes.Override) == ExtendedMemberAttributes.Override;
            }
        }

        InstanceMemberAttributes IInstanceMember.Attributes
        {
            get
            {
                return ((InstanceMemberAttributes)this.Attributes) & InstanceMemberAttributes.FlagsMask;
            }
        }

        public bool IsHideBySignature
        {
            get
            {
                return (this.Attributes & ExtendedMemberAttributes.HideBySignature) == ExtendedMemberAttributes.HideBySignature;
            }
        }

        public bool IsStatic
        {
            get
            {
                return ((this.Attributes & ExtendedMemberAttributes.Static) == ExtendedMemberAttributes.Static);
            }
        }

        public AccessLevelModifiers AccessLevel
        {
            get
            {
                return this.MetadataEntry.Methods.ObtainAccessLevelModifiers();
            }
        }

    }

}
