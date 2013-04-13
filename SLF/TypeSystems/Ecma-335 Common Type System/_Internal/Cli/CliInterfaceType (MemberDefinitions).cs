using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Cli;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliInterfaceType
    {
        private class Method :
            CliMethodSignatureBase<IInterfaceMethodMember, IInterfaceType>,
            IInterfaceMethodMember
        {

            internal Method(CliInterfaceType parent, ICliMetadataMethodDefinitionTableRow metadataEntry, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(metadataEntry, parent.Assembly, parent, uniqueIdentifier)
            {
            }

            protected override IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType> CreateParameter(int index, ICliMetadataParameterTableRow metadataEntry)
            {
                return new Parameter(metadataEntry, index, this);
            }

            private class Parameter :
                ParameterMember,
                IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>
            {
                public Parameter(ICliMetadataParameterTableRow metadataEntry, int index, Method parent)
                    : base(metadataEntry, parent, index)
                {
                }

                protected override IType ActiveType
                {
                    get { return ((Method)this.Parent).ActiveType; }
                }
            }

            protected override IInterfaceMethodMember OnMakeGenericClosure(IControlledTypeCollection genericReplacements)
            {
                return new _InterfaceTypeBase._MethodsBase._Method(this, genericReplacements);
            }

            protected override IType ActiveType
            {
                get { return this.Parent; }
            }

            internal override CliMethodSignatureBase<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IInterfaceMethodMember, IInterfaceType>.TypeParameter GetTypeParameter(int index, ICliMetadataGenericParameterTableRow metadataEntry)
            {
                return new TypeParameter(this, metadataEntry, index);
            }
            private class TypeParameter :
                CliMethodSignatureBase<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IInterfaceMethodMember, IInterfaceType>.TypeParameter
            {
                public TypeParameter(Method parent, ICliMetadataGenericParameterTableRow metadataEntry, int position)
                    : base(parent, metadataEntry, position)
                {
                }

                public new Method Parent { get { return (Method)base.Parent; } }

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

        private class Property :
            CliPropertySignatureMember<IInterfacePropertyMember, IInterfaceType>,
            IInterfacePropertyMember
        {

            public Property(CliInterfaceType parent, ICliMetadataPropertyTableRow metadataEntry)
                : base(parent, metadataEntry)
            {
            }

            private sealed class Method :
                CliInterfaceType.Method,
                IPropertySignatureMethodMember
            {
                private PropertyMethodType propertyMethodType;
                internal Method(CliInterfaceType parent, ICliMetadataMethodDefinitionTableRow metadataEntry, PropertyMethodType propertyMethodType, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
                    : base(parent, metadataEntry, uniqueIdentifier)
                {
                    this.propertyMethodType = propertyMethodType;
                }

                public PropertyMethodType MethodType
                {
                    get { throw new NotImplementedException(); }
                }

            }

            internal override _ICliManager IdentityManager { get { return (_ICliManager)this.Parent.IdentityManager; } }

            protected override IPropertySignatureMethodMember GetPropertyMethod(PropertyMethodType propertyMethodType)
            {
                switch (propertyMethodType)
                {
                    case PropertyMethodType.GetMethod:
                        return new Method((CliInterfaceType)this.Parent, this.MetadataEntry.GetMethod, propertyMethodType, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.GetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.GetMethod));
                    case PropertyMethodType.SetMethod:
                        return new Method((CliInterfaceType)this.Parent, this.MetadataEntry.SetMethod, propertyMethodType, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.SetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.SetMethod));
                    default:
                        throw new NotSupportedException();
                }
            }

            protected override IType OnGetPropertyType()
            {
                return this.MetadataEntry.PropertyType.ObtainTypeReference(this.IdentityManager, (ICliType)this.Parent);
            }
        }

        private class Event :
            CliEventSignatureMember<IInterfaceEventMember, IInterfaceType>,
            IInterfaceEventMember
        {
            internal Event(IInterfaceType parent, ICliMetadataEventTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(parent, metadataEntry, uniqueIdentifier)
            {
            }

            protected override IMethodSignatureMember InitializeOnAddMethodSignature()
            {
                return new Method((CliInterfaceType)this.Parent, this.MetadataEntry.OnAdd, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnAdd, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.OnAddMethod));
            }

            protected override IMethodSignatureMember InitializeOnRemoveMethodSignature()
            {
                return new Method((CliInterfaceType)this.Parent, this.MetadataEntry.OnRemove, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.OnRemove, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.OnRemoveMethod));
            }

        }

        private class Indexer :
            CliIndexerSignatureMember<IInterfaceIndexerMember, IInterfaceType>,
            IInterfaceIndexerMember
        {
            internal Indexer(IInterfaceType parent, ICliMetadataPropertyTableRow metadataEntry, IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier)
                : base(parent, metadataEntry, uniqueIdentifier)
            {
            }

            private sealed class Method :
                CliInterfaceType.Method,
                IPropertySignatureMethodMember
            {
                private PropertyMethodType indexerMethodType;
                internal Method(CliInterfaceType parent, ICliMetadataMethodDefinitionTableRow metadataEntry, PropertyMethodType indexerMethodType, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
                    : base(parent, metadataEntry, uniqueIdentifier)
                {
                    this.indexerMethodType = indexerMethodType;
                }

                public PropertyMethodType MethodType
                {
                    get { return this.indexerMethodType; }
                }

            }
            protected override IPropertySignatureMethodMember GetIndexerMethod(PropertyMethodType propertyMethodType)
            {
                switch (propertyMethodType)
                {
                    case PropertyMethodType.GetMethod:
                        return new Method((CliInterfaceType)this.Parent, this.MetadataEntry.GetMethod, propertyMethodType, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.GetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.GetMethod));
                    case PropertyMethodType.SetMethod:
                        return new Method((CliInterfaceType)this.Parent, this.MetadataEntry.SetMethod, propertyMethodType, CliMemberExtensions.GetMethodIdentifier(this.MetadataEntry.SetMethod, this.Parent, (_ICliManager)this.Parent.IdentityManager, () => this.SetMethod));
                    default:
                        throw new NotSupportedException();
                }
            }

            public override bool LastIsParams
            {
                get
                {
                    return this.IsLastParams((ICliAssembly)this.Parent.Assembly, ((CliAssembly)this.Parent.Assembly).IdentityManager);
                }
            }

        }
    }
}
