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
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    //docsis - DocsDiag
    internal partial class CliStructType :
        CliGenericInstantiableTypeBase<IStructCtorMember, IStructEventMember, IStructFieldMember,
             IStructIndexerMember, IStructMethodMember, IStructPropertyMember, IStructType>,
        IStructType
    {

        private class MethodMember :
            CliMethodMemberBase<IStructMethodMember, IStructType>,
            IStructMethodMember
        {
            private IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier;

            internal MethodMember(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, IStructType parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(metadataEntry, assembly, parent)
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

            public override IStructMethodMember MakeGenericClosure(IControlledTypeCollection genericReplacements)
            {
                return new _StructTypeBase._MethodsBase._Method(this, genericReplacements);
            }

            protected override CliParameterMemberDictionary<IStructMethodMember, IMethodParameterMember<IStructMethodMember, IStructType>> InitializeParameters()
            {
                return new CliMethodMemberBase<IStructMethodMember, IStructType>.ParameterMemberDictionary(this);
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
                    return (this.InstanceFlags & ExtendedMethodMemberFlags.Async) == ExtendedMethodMemberFlags.Async;
                }
            }

            public ExtendedMethodMemberFlags InstanceFlags
            {
                get { throw new NotImplementedException(); }
            }

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
                    return (this.InstanceFlags & ExtendedMethodMemberFlags.Abstract) == ExtendedMethodMemberFlags.Abstract;
                }
            }

            public bool IsVirtual
            {
                get
                {
                    return (this.InstanceFlags & ExtendedMethodMemberFlags.Virtual) == ExtendedMethodMemberFlags.Virtual;
                }
            }

            public bool IsFinal
            {
                get
                {
                    return (this.InstanceFlags & ExtendedMethodMemberFlags.Final) == ExtendedMethodMemberFlags.Final;
                }
            }

            public bool IsOverride
            {
                get
                {
                    return (this.InstanceFlags & ExtendedMethodMemberFlags.Override) == ExtendedMethodMemberFlags.Override;
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
                    return (this.InstanceFlags & ExtendedMethodMemberFlags.HideBySignature) == ExtendedMethodMemberFlags.HideBySignature;
                }
            }

            public bool IsStatic
            {
                get
                {
                    return (this.InstanceFlags & ExtendedMethodMemberFlags.Static) == ExtendedMethodMemberFlags.Static;
                }
            }

            protected override IType ActiveType
            {
                get { return this.Parent; }
            }
        }

        private class FieldMember :
            CliMemberBase<IGeneralMemberUniqueIdentifier, IStructType, ICliMetadataFieldTableRow>,
            IStructFieldMember
        {
            public FieldMember(IStructType parent, ICliMetadataFieldTableRow metadataEntry)
                : base(parent, metadataEntry)
            {
            }

            protected override string OnGetName()
            {
                return this.MetadataEntry.Name;
            }

            public override IGeneralMemberUniqueIdentifier UniqueIdentifier
            {
                get { return AstIdentifier.GetMemberIdentifier(this.Name); }
            }

            private CliManager IdentityManager { get { return (CliManager)this.Parent.IdentityManager; } }

            public IType FieldType
            {
                get
                {
                    return IdentityManager.ObtainTypeReference(this.MetadataEntry.FieldType, this.MetadataEntry.FieldType.Type, this.Parent, null);
                }
            }

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
        }

        private class EventMember :
            CliMemberBase<IGeneralSignatureMemberUniqueIdentifier, IStructType, ICliMetadataEventTableRow>,
            IStructEventMember
        {
            private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
            private IMethodMember onAddMethod;
            private IMethodMember onRemoveMethod;
            private IMethodMember onFireMethod;

            internal EventMember(IStructType parent, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier, ICliMetadataEventTableRow metadataEntry)
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

            public IParameterMemberDictionary<IStructEventMember, IEventParameterMember<IStructEventMember, IStructType>> Parameters
            {
                get { throw new NotImplementedException(); }
            }

            IParameterMemberDictionary IParameterParent.Parameters
            {
                get { return (IParameterMemberDictionary)this.Parameters; }
            }

            private new CliStructType Parent { get { return (CliStructType)base.Parent; } }

            public bool LastIsParams
            {
                get
                {
                    return this.IsLastParams(this.Parent.Assembly, this.Parent.IdentityManager);
                }
            }

            internal _ICliManager IdentityManager { get { return this.Parent.IdentityManager; } }

            public EventSignatureSource SignatureSource
            {
                get
                {
                    var attr = typeof(DelegateIsEventCompanionAttribute).GetTypeReference(this.IdentityManager);
                    if (this.SignatureType.IsDefined(attr))
                        return EventSignatureSource.Declared;
                    return EventSignatureSource.Delegate;
                }
            }

            public IDelegateType SignatureType
            {
                get
                {
                    var type = this.IdentityManager.ObtainTypeReference(this.MetadataEntry.SignatureType);
                    if (type is IDelegateType)
                        return (IDelegateType)type;
                    throw new BadImageFormatException("Events must reference delegate types as their signature.");
                }
            }

            public IType ReturnType
            {
                get
                {
                    return this.SignatureType.ReturnType;
                }
            }

            public IMethodMember OnAddMethod
            {
                get 
                {
                    if (this.onAddMethod == null &&
                        this.MetadataEntry.OnAdd != null)
                        this.onAddMethod = new EventMethodMember(this.MetadataEntry.OnAdd, (_ICliAssembly)this.Parent.Assembly, this, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnAdd, this.Parent, this.Parent.IdentityManager, () => this.OnAddMethod), EventMethodType.Add);
                    return this.onAddMethod;
                }
            }

            public IMethodMember OnRemoveMethod
            {
                get
                {
                    if (this.onRemoveMethod == null &&
                        this.MetadataEntry.OnRemove != null)
                        this.onRemoveMethod = new EventMethodMember(this.MetadataEntry.OnRemove, (_ICliAssembly)this.Parent.Assembly, this, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnRemove, this.Parent, this.Parent.IdentityManager, () => this.OnRemoveMethod), EventMethodType.Remove);
                    return this.onRemoveMethod;
                }
            }

            public IMethodMember OnRaiseMethod
            {
                get
                {
                    if (this.onFireMethod == null &&
                        this.MetadataEntry.OnFire != null)
                        this.onFireMethod = new EventMethodMember(this.MetadataEntry.OnFire, (_ICliAssembly)this.Parent.Assembly, this, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnFire, this.Parent, this.Parent.IdentityManager, () => this.OnRaiseMethod), EventMethodType.Fire);
                    return this.onFireMethod;
                }
            }

            public bool CanRaise
            {
                get
                {
                    return this.OnRaiseMethod != null;
                }
            }

            public ExtendedInstanceMemberFlags InstanceFlags
            {
                get { throw new NotImplementedException(); }
            }


            public bool IsAbstract
            {
                get
                {
                    return (this.InstanceFlags & ExtendedInstanceMemberFlags.Abstract) == ExtendedInstanceMemberFlags.Abstract;
                }
            }

            public bool IsVirtual
            {
                get
                {
                    return (this.InstanceFlags & ExtendedInstanceMemberFlags.Virtual) == ExtendedInstanceMemberFlags.Virtual;
                }
            }

            public bool IsFinal
            {
                get
                {
                    return (this.InstanceFlags & ExtendedInstanceMemberFlags.Final) == ExtendedInstanceMemberFlags.Final;
                }
            }

            public bool IsOverride
            {
                get
                {
                    return (this.InstanceFlags & ExtendedInstanceMemberFlags.Override) == ExtendedInstanceMemberFlags.Override;
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
                    return (this.InstanceFlags & ExtendedInstanceMemberFlags.HideBySignature) == ExtendedInstanceMemberFlags.HideBySignature;
                }
            }

            public bool IsStatic
            {
                get
                {
                    return ((this.InstanceFlags & ExtendedInstanceMemberFlags.Static) == ExtendedInstanceMemberFlags.Static);
                }
            }

            public AccessLevelModifiers AccessLevel
            {
                get { throw new NotImplementedException(); }
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
        }
    }
}
