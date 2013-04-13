using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliPropertyMember<TProperty, TPropertyParent> :
        CliMemberBase<IGeneralMemberUniqueIdentifier, TPropertyParent, ICliMetadataPropertyTableRow>,
        IPropertyMember<TProperty, TPropertyParent>
        where TProperty :
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertyParent<TProperty, TPropertyParent>
    {
        private IPropertyMethodMember getMethod;
        private IPropertyMethodMember setMethod;
        private IMetadataCollection metadata;
        protected CliPropertyMember(TPropertyParent parent, ICliMetadataPropertyTableRow metadataEntry)
            : base(parent, metadataEntry)
        {
        }

        protected override string OnGetName()
        {
            return this.MetadataEntry.Name;
        }

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get { return TypeSystemIdentifiers.GetMemberIdentifier(this.Name); }
        }

        public IPropertyMethodMember GetMethod
        {
            get {
                if (this.getMethod == null)
                    return this.GetPropertyMethod(PropertyMethodType.GetMethod);
                return this.getMethod;
            }
        }

        protected abstract IPropertyMethodMember GetPropertyMethod(PropertyMethodType propertyMethodType);

        public IPropertyMethodMember SetMethod
        {
            get
            {
                if (this.setMethod == null)
                    return this.GetPropertyMethod(PropertyMethodType.SetMethod);
                return this.setMethod;
            }
        }

        public IType PropertyType
        {
            get { return this.OnGetPropertyType(); }
        }

        protected abstract IType OnGetPropertyType();

        public bool CanRead
        {
            get { return this.MetadataEntry.GetMethod != null; }
        }

        public bool CanWrite
        {
            get { return this.MetadataEntry.SetMethod != null; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.GetMethod
        {
            get { return this.GetMethod; }
        }

        IPropertySignatureMethodMember IPropertySignatureMember.SetMethod
        {
            get { return this.SetMethod; }
        }

        internal abstract _ICliManager IdentityManager { get; }

        public IMetadataCollection Metadata
        {
            get
            {
                if (this.metadata == null)
                    this.metadata = new CliMetadataCollection(this.MetadataEntry.CustomAttributes, this, this.IdentityManager);
                return this.metadata;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return Metadata.Contains(metadatumType);
        }

        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get {
                var methodSemantics = this.MetadataEntry.Methods.FirstOrDefault();
                if (methodSemantics == null)
                    return ExtendedInstanceMemberFlags.None;
                var method = methodSemantics.Method;
                if (method == null)
                    return ExtendedInstanceMemberFlags.None;
                ExtendedInstanceMemberFlags flags = ExtendedInstanceMemberFlags.None;
                if ((method.UsageDetails.VTableFlags & MethodVTableLayoutFlags.NewSlot) == MethodVTableLayoutFlags.NewSlot)
                    flags |= ExtendedInstanceMemberFlags.HideByName;
                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Abstract) == MethodUseFlags.Abstract)
                    flags |= ExtendedInstanceMemberFlags.Abstract;

                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Final) == MethodUseFlags.Final)
                    flags |= ExtendedInstanceMemberFlags.Final;
                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Static) == MethodUseFlags.Static)
                    flags |= ExtendedInstanceMemberFlags.Static;
                if ((method.UsageDetails.UsageFlags & MethodUseFlags.Virtual) == MethodUseFlags.Virtual)
                    flags |= ExtendedInstanceMemberFlags.Virtual;
                if (method.UsageDetails.VTableFlags == MethodVTableLayoutFlags.ReuseSlot)
                    flags |= ExtendedInstanceMemberFlags.Override;
                return flags;
            }
        }

        public bool IsAbstract
        {
            get { return (InstanceFlags & ExtendedInstanceMemberFlags.Abstract) == ExtendedInstanceMemberFlags.Abstract; }
        }

        public bool IsVirtual
        {
            get { return (InstanceFlags & ExtendedInstanceMemberFlags.Virtual) == ExtendedInstanceMemberFlags.Virtual; }
        }

        public bool IsFinal
        {
            get { return (InstanceFlags & ExtendedInstanceMemberFlags.Final) == ExtendedInstanceMemberFlags.Final; }
        }

        public bool IsOverride
        {
            get { return (InstanceFlags & ExtendedInstanceMemberFlags.Override) == ExtendedInstanceMemberFlags.Override; }
        }

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return (InstanceMemberFlags)this.InstanceFlags & InstanceMemberFlags.FlagsMask; }
        }

        public bool IsHideBySignature
        {
            get { return (this.InstanceFlags & ExtendedInstanceMemberFlags.HideBySignature) == ExtendedInstanceMemberFlags.HideBySignature; }
        }

        public bool IsStatic
        {
            get { return (this.InstanceFlags & ExtendedInstanceMemberFlags.Static) == ExtendedInstanceMemberFlags.Static; }
        }

        public AccessLevelModifiers AccessLevel
        {
            get
            {
                return this.MetadataEntry.Methods.ObtainAccessLevelModifiers();
            }
        }

        public override string ToString()
        {
            return string.Format("property {0}::{1}", this.Parent, this.UniqueIdentifier);
        }
    }
}
