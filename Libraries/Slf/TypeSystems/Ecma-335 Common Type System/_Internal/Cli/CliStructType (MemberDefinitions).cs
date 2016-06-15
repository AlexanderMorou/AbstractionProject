using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    //docsis - DocsDiag
    internal partial class CliStructType :
        CliGenericInstantiableTypeBase<IStructCtorMember, IStructEventMember, IStructFieldMember,
             IStructIndexerMember, IStructMethodMember, IStructPropertyMember, IStructType>,
        IStructType
    {
        private class CtorMember :
            CliConstructorMember<IStructCtorMember, IStructType>,
            IStructCtorMember
        {
            internal CtorMember(IStructType parent, ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliManager identityManager, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(parent, metadataEntry, identityManager, uniqueIdentifier)
            {
            }
        }

        private class MethodMember :
            CliMethodMemberBase<IStructMethodMember, IStructType>,
            IStructMethodMember
        {
            private IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier;

            internal MethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, IStructType parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(metadataEntry, assembly, parent, uniqueIdentifier)
            {
                this.uniqueIdentifier = uniqueIdentifier;
            }

            public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
            {
                get
                {
                    return this.uniqueIdentifier;
                }
            }

            protected override IStructMethodMember OnMakeGenericClosure(IControlledTypeCollection genericReplacements)
            {
                return new _StructTypeBase._MethodsBase._Method(this, genericReplacements);
            }

            internal override IMethodParameterMember<IStructMethodMember, IStructType> CreateParameter(int index, ICliMetadataParameterTableRow metadata)
            {
                return new ParameterMember(metadata, this, index);
            }

            private new class ParameterMember :
                CliMethodMemberBase<IStructMethodMember, IStructType>.ParameterMember
            {
                public ParameterMember(ICliMetadataParameterTableRow metadataEntry, MethodMember parent, int index)
                    : base(metadataEntry, parent, index)
                {
                }

                protected override IType ActiveType
                {
                    get
                    {
                        return this.Parent.Parent;
                    }
                }
            }

            public IClassMethodMember BaseDefinition
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsAsynchronous
            {
                get
                {
                    return (this.Attributes & ExtendedMethodAttributes.Async) == ExtendedMethodAttributes.Async;
                }
            }

            public ExtendedMethodAttributes Attributes
            {
                get {
                    ExtendedMethodAttributes flags = ExtendedMethodAttributes.None;
                    if ((MetadataEntry.UsageDetails.VTableFlags & MethodVTableLayoutFlags.NewSlot) == MethodVTableLayoutFlags.NewSlot)
                        flags |= ExtendedMethodAttributes.HideByName;
                    if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Abstract) == MethodUseFlags.Abstract)
                        flags |= ExtendedMethodAttributes.Abstract;

                    if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Final) == MethodUseFlags.Final)
                        flags |= ExtendedMethodAttributes.Final;
                    if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Static) == MethodUseFlags.Static)
                        flags |= ExtendedMethodAttributes.Static;
                    if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Virtual) == MethodUseFlags.Virtual)
                        flags |= ExtendedMethodAttributes.Virtual;
                    if (MetadataEntry.UsageDetails.VTableFlags == MethodVTableLayoutFlags.ReuseSlot)
                        flags |= ExtendedMethodAttributes.Override;

                    return flags;
                }
            }

            ExtendedMemberAttributes IExtendedInstanceMember.Attributes
            {
                get
                {
                    return ((ExtendedMemberAttributes)this.Attributes) & ExtendedMemberAttributes.FlagsMask;
                }
            }

            public bool IsAbstract
            {
                get
                {
                    return (this.Attributes & ExtendedMethodAttributes.Abstract) == ExtendedMethodAttributes.Abstract;
                }
            }

            public bool IsVirtual
            {
                get
                {
                    return (this.Attributes & ExtendedMethodAttributes.Virtual) == ExtendedMethodAttributes.Virtual;
                }
            }

            public bool IsFinal
            {
                get
                {
                    return (this.Attributes & ExtendedMethodAttributes.Final) == ExtendedMethodAttributes.Final;
                }
            }

            public bool IsOverride
            {
                get
                {
                    return (this.Attributes & ExtendedMethodAttributes.Override) == ExtendedMethodAttributes.Override;
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
                    return (this.Attributes & ExtendedMethodAttributes.HideBySignature) == ExtendedMethodAttributes.HideBySignature;
                }
            }

            public bool IsStatic
            {
                get
                {
                    return (this.Attributes & ExtendedMethodAttributes.Static) == ExtendedMethodAttributes.Static;
                }
            }

            protected override IType ActiveType
            {
                get { return this.Parent; }
            }

            internal override CliMethodSignatureBase<IMethodParameterMember<IStructMethodMember, IStructType>, IStructMethodMember, IStructType>.TypeParameter GetTypeParameter(int index, ICliMetadataGenericParameterTableRow metadataEntry)
            {
                return new TypeParameter(this, metadataEntry, index);
            }
            private new class TypeParameter :
                CliMethodSignatureBase<IMethodParameterMember<IStructMethodMember, IStructType>, IStructMethodMember, IStructType>.TypeParameter
            {
                public TypeParameter(MethodMember parent, ICliMetadataGenericParameterTableRow metadataEntry, int position)
                    : base(parent, metadataEntry, position)
                {
                }

                public new MethodMember Parent { get { return (MethodMember)base.Parent; } }

                protected override IAssembly OnGetAssembly()
                {
                    return this.Parent.Assembly;
                }

                protected override IIdentityManager OnGetManager()
                {
                    return this.Parent.IdentityManager;
                }
            }

            public IEnumerable<IInterfaceType> Implementations
            {
                get {
                    return GetImplementations(((ICliType)(this.Parent)).MetadataEntry, this.MetadataEntry, this.IdentityManager, this.Parent, this, this.Assembly);
                }
            }

        }

        private class IndexerMember :
            CliIndexerMember<IStructIndexerMember, IStructType>, 
            IStructIndexerMember
        {
            internal IndexerMember(IStructType parent, ICliMetadataPropertyTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(parent, metadataEntry, uniqueIdentifier)
            {
            }

            private class MethodMember :
                CliStructType.MethodMember,
                IPropertyMethodMember
            {
                private PropertyMethodType methodType;
                public MethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, IStructType parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier, PropertyMethodType methodType)
                    : base(metadataEntry, assembly, parent, uniqueIdentifier)
                {
                    this.methodType = methodType;
                }

                public PropertyMethodType MethodType
                {
                    get { return this.methodType; }
                }
            }

            protected override IPropertyMethodMember GetIndexerMethod(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.GetMethod:
                        return new MethodMember(this.MetadataEntry.GetMethod, (_ICliAssembly)this.Parent.Assembly, this.Parent, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.GetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.GetMethod), methodType);
                    case PropertyMethodType.SetMethod:
                        return new MethodMember(this.MetadataEntry.SetMethod, (_ICliAssembly)this.Parent.Assembly, this.Parent, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.SetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.SetMethod), methodType);
                }
                throw new ArgumentOutOfRangeException("methodType");
            }

            public override bool LastIsParams
            {
                get
                {
                    return this.IsLastParams(this.Assembly, ((_ICliAssembly)this.Assembly).IdentityManager);
                }
            }
        }

        private class PropertyMember :
            CliPropertyMember<IStructPropertyMember, IStructType>,
            IStructPropertyMember
        {
            internal PropertyMember(IStructType parent, ICliMetadataPropertyTableRow metadataEntry)
                : base(parent, metadataEntry)
            {
            }

            private class MethodMember :
                CliStructType.MethodMember,
                IStructPropertyMethodMember
            {
                private PropertyMethodType methodType;
                public MethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, IStructType parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier, PropertyMethodType methodType)
                    : base(metadataEntry, assembly, parent, uniqueIdentifier)
                {
                    this.methodType = methodType;
                }

                public PropertyMethodType MethodType
                {
                    get { return this.methodType; }
                }
            }
            protected override IPropertyMethodMember GetPropertyMethod(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.GetMethod:
                        return new MethodMember(this.MetadataEntry.GetMethod, (_ICliAssembly)this.Parent.Assembly, this.Parent, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.GetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.GetMethod), methodType);
                    case PropertyMethodType.SetMethod:
                        return new MethodMember(this.MetadataEntry.SetMethod, (_ICliAssembly)this.Parent.Assembly, this.Parent, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.SetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.SetMethod), methodType);
                }
                throw new ArgumentOutOfRangeException("methodType");
            }

            internal override _ICliManager IdentityManager { get { return (_ICliManager)this.Parent.IdentityManager; } }

            protected override IType OnGetPropertyType()
            {
                return this.MetadataEntry.PropertyType.ObtainTypeReference(this.IdentityManager, (ICliType)this.Parent);
            }
        }

        private class FieldMember :
            CliFieldMember<IStructFieldMember, IStructType>,
            IStructFieldMember
        {
            public FieldMember(IStructType parent, ICliMetadataFieldTableRow metadataEntry)
                : base(parent, metadataEntry, TypeSystemIdentifiers.GetMemberIdentifier(metadataEntry.Name))
            {
            }

            private CliManager IdentityManager { get { return (CliManager)this.Parent.IdentityManager; } }


            InstanceMemberAttributes IInstanceMember.Attributes
            {
                get
                {
                    return (InstanceMemberAttributes)(this.Attributes & InstanceFieldMemberAttributes.FlagsMask);
                }
            }

            public bool IsHideBySignature
            {
                get { return false; }
            }

            public bool IsStatic
            {
                get { return (this.Attributes & InstanceFieldMemberAttributes.Static) == InstanceFieldMemberAttributes.Static; }
            }

            public AccessLevelModifiers AccessLevel
            {
                get
                {
                    return CliCommon.GetFieldAccessModifiers(this.MetadataEntry.FieldAttributes);
                }
            }

            public override string ToString()
            {
                return string.Format("field {0}::{1}", this.Parent, this.UniqueIdentifier);
            }

            protected override IType OnGetFieldType()
            {
                return IdentityManager.ObtainTypeReference(this.MetadataEntry.FieldType, this.MetadataEntry.FieldType.Type, this.Parent, null);
            }

            protected override _ICliManager _IdentityManager
            {
                get { return this.IdentityManager; }
            }

            public new InstanceFieldMemberAttributes Attributes
            {
                get
                {
                    return CliCommon.GetFieldInstanceFlags(this.MetadataEntry.FieldAttributes);
                }
            }

        }

        private class EventMember :
            CliEventMember<IStructEventMember, IStructType>,
            IStructEventMember
        {
            internal EventMember(IStructType parent, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier, ICliMetadataEventTableRow metadataEntry)
                : base(parent, metadataEntry, uniqueIdentifier)
            {
            }

            private class EventMethodMember :
                MethodMember
            {
                public EventMethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, EventMember parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier, EventMethodType methodType)
                    : base(metadataEntry, assembly, parent.Parent, uniqueIdentifier)
                {
                    this.MethodType = methodType;
                }

                public EventMethodType MethodType { get; private set; }
            }

            protected override IMethodMember InitializeOnAddMethod()
            {
                return new EventMethodMember(this.MetadataEntry.OnAdd, (_ICliAssembly)this.Parent.Assembly, this, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnAdd, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.OnAddMethod), EventMethodType.Add);
            }

            protected override IMethodMember InitializeOnRemoveMethod()
            {
                return new EventMethodMember(this.MetadataEntry.OnRemove, (_ICliAssembly)this.Parent.Assembly, this, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnRemove, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.OnRemoveMethod), EventMethodType.Remove);
            }

            protected override IMethodMember InitializeOnRaiseMethod()
            {
                return new EventMethodMember(this.MetadataEntry.OnFire, (_ICliAssembly)this.Parent.Assembly, this, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnFire, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.OnRaiseMethod), EventMethodType.Fire);
            }
        }
    }
}
