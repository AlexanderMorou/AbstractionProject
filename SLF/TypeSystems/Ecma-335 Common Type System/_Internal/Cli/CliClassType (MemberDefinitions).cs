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
    internal partial class CliClassType :
        CliGenericInstantiableTypeBase<IClassCtorMember, IClassEventMember, IClassFieldMember,
             IClassIndexerMember, IClassMethodMember, IClassPropertyMember, IClassType>,
        IClassType
    {
        private class CtorMember :
            CliConstructorMember<IClassCtorMember, IClassType>,
            IClassCtorMember
        {
            internal CtorMember(IClassType parent, ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliManager identityManager, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(parent, metadataEntry, identityManager, uniqueIdentifier)
            {
            }
        }
        private class MethodMember :
            CliMethodMemberBase<IClassMethodMember, IClassType>,
            IClassMethodMember
        {
            private IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier;

            internal MethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, IClassType parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
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

            protected override IClassMethodMember OnMakeGenericClosure(IControlledTypeCollection genericReplacements)
            {
                return new _ClassTypeBase._MethodsBase._Method(this, genericReplacements);
            }

            internal override IMethodParameterMember<IClassMethodMember, IClassType> CreateParameter(int index, ICliMetadataParameterTableRow metadata)
            {
                return new ParameterMember(metadata, this, index);
            }

            private new class ParameterMember :
                CliMethodMemberBase<IClassMethodMember, IClassType>.ParameterMember
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
                    return (this.InstanceFlags & ClassMethodMemberFlags.Async) == ClassMethodMemberFlags.Async;
                }
            }

            public ClassMethodMemberFlags InstanceFlags { get {
                ClassMethodMemberFlags flags = ClassMethodMemberFlags.None;
                if ((MetadataEntry.UsageDetails.VTableFlags & MethodVTableLayoutFlags.NewSlot) == MethodVTableLayoutFlags.NewSlot)
                    flags |= ClassMethodMemberFlags.HideByName;
                if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Abstract) == MethodUseFlags.Abstract)
                    flags |= ClassMethodMemberFlags.Abstract;

                if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Final) == MethodUseFlags.Final)
                    flags |= ClassMethodMemberFlags.Final;
                if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Static) == MethodUseFlags.Static)
                    flags |= ClassMethodMemberFlags.Static;
                if ((MetadataEntry.UsageDetails.UsageFlags & MethodUseFlags.Virtual) == MethodUseFlags.Virtual)
                    flags |= ClassMethodMemberFlags.Virtual;
                if (MetadataEntry.UsageDetails.VTableFlags == MethodVTableLayoutFlags.ReuseSlot)
                    flags |= ClassMethodMemberFlags.Override;

                return flags;

            } }

            ExtendedMethodMemberFlags IExtendedMethodMember.InstanceFlags { get { return ((ExtendedMethodMemberFlags)this.InstanceFlags) & ExtendedMethodMemberFlags.FlagsMask; } }

            ExtendedInstanceMemberFlags IExtendedInstanceMember.InstanceFlags
            {
                get
                {
                    return ((ExtendedInstanceMemberFlags)this.InstanceFlags) & ExtendedInstanceMemberFlags.FlagsMask;
                }
            }

            public bool IsAbstract
            {
                get
                {
                    return (this.InstanceFlags & ClassMethodMemberFlags.Abstract) == ClassMethodMemberFlags.Abstract;
                }
            }

            public bool IsVirtual
            {
                get
                {
                    return (this.InstanceFlags & ClassMethodMemberFlags.Virtual) == ClassMethodMemberFlags.Virtual;
                }
            }

            public bool IsFinal
            {
                get
                {
                    return (this.InstanceFlags & ClassMethodMemberFlags.Final) == ClassMethodMemberFlags.Final;
                }
            }

            public bool IsOverride
            {
                get
                {
                    return (this.InstanceFlags & ClassMethodMemberFlags.Override) == ClassMethodMemberFlags.Override;
                }
            }

            InstanceMemberFlags IInstanceMember.InstanceFlags
            {
                get
                {
                    return ((InstanceMemberFlags)this.InstanceFlags) & InstanceMemberFlags.FlagsMask;
                }
            }

            public bool IsHideBySignature
            {
                get
                {
                    return (this.InstanceFlags & ClassMethodMemberFlags.HideBySignature) == ClassMethodMemberFlags.HideBySignature;
                }
            }

            public bool IsStatic
            {
                get
                {
                    return (this.InstanceFlags & ClassMethodMemberFlags.Static) == ClassMethodMemberFlags.Static;
                }
            }

            protected override IType ActiveType
            {
                get { return this.Parent; }
            }

            public IClassMethodMember PreviousDefinition
            {
                get { throw new NotImplementedException(); }
            }

            public bool IsExtensionMethod
            {
                get
                {
                    return this.IsDefined(this.IdentityManager.ObtainTypeReference(this.IdentityManager.RuntimeEnvironment.GetCoreIdentifier(CliRuntimeCoreType.ExtensionMetadatum, this.Assembly))) &&
                        this.Signature.Parameters.Count > 0;
                }
            }

            internal override CliMethodSignatureBase<IMethodParameterMember<IClassMethodMember, IClassType>, IClassMethodMember, IClassType>.TypeParameter GetTypeParameter(int index, ICliMetadataGenericParameterTableRow metadataEntry)
            {
                return new TypeParameter(this, metadataEntry, index);
            }
            private class TypeParameter :
                CliMethodSignatureBase<IMethodParameterMember<IClassMethodMember, IClassType>, IClassMethodMember, IClassType>.TypeParameter
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

                protected override ITypeIdentityManager OnGetManager()
                {
                    return this.Parent.IdentityManager;
                }
            }

        }
        private class IndexerMember :
            CliIndexerMember<IClassIndexerMember, IClassType>,
            IClassIndexerMember
        {
            internal IndexerMember(IClassType parent, ICliMetadataPropertyTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(parent, metadataEntry, uniqueIdentifier)
            {
            }

            private class MethodMember :
                CliClassType.MethodMember,
                IPropertyMethodMember
            {
                private PropertyMethodType methodType;
                public MethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, IClassType parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier, PropertyMethodType methodType)
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

        private class FieldMember :
            CliFieldMember<IClassFieldMember, IClassType>,
            IClassFieldMember
        {
            public FieldMember(IClassType parent, ICliMetadataFieldTableRow metadataEntry)
                : base(parent, metadataEntry, TypeSystemIdentifiers.GetMemberIdentifier(metadataEntry.Name))
            {
            }

            private CliManager IdentityManager { get { return (CliManager)this.Parent.IdentityManager; } }


            public InstanceMemberFlags InstanceFlags
            {
                get
                {
                    return CliCommon.GetFieldInstanceFlags(this.MetadataEntry.FieldAttributes);
                }
            }

            public bool IsHideBySignature
            {
                get { return false; }
            }

            public bool IsStatic
            {
                get { return (this.InstanceFlags & InstanceMemberFlags.Static) == InstanceMemberFlags.Static; }
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
        }
        private class PropertyMember :
            CliPropertyMember<IClassPropertyMember, IClassType>,
            IClassPropertyMember
        {
            internal PropertyMember(IClassType parent, ICliMetadataPropertyTableRow metadataEntry)
                : base(parent, metadataEntry)
            {
            }

            private class MethodMember :
                CliClassType.MethodMember,
                IPropertyMethodMember
            {
                private PropertyMethodType methodType;
                public MethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, IClassType parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier, PropertyMethodType methodType)
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

            internal _ICliManager IdentityManager { get { return (_ICliManager)this.Parent.IdentityManager; } }

            protected override IType OnGetPropertyType()
            {
                return this.MetadataEntry.PropertyType.ObtainTypeReference(this.IdentityManager, (ICliType)this.Parent);
            }
        }

        private class EventMember :
            CliEventMember<IClassEventMember, IClassType>,
            IClassEventMember
        {
            internal EventMember(IClassType parent, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier, ICliMetadataEventTableRow metadataEntry)
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
