using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CliEnumType :
        CliTypeBase<IGeneralTypeUniqueIdentifier>,
        IEnumType,
        _ICliMemberParent
    {
        private EnumerationBaseType? enumType;
        private IFieldMemberDictionary<IEnumFieldMember, IEnumType> fields;
        internal CliEnumType(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadataEntry)
            : base(assembly, metadataEntry)
        {
        }

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Enumeration; }
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return new CliFullMemberDictionary(this);
        }

        public EnumerationBaseType ValueType
        {
            get {
                if (enumType == null)
                {
                    foreach (var field in this.MetadataEntry.Fields)
                    {
                        if ((field.FieldAttributes & FieldAttributes.Static) != FieldAttributes.Static)
                        {
                            if (field.FieldType.Type.TypeSignatureKind == CliMetadataTypeSignatureKind.NativeType)
                            {
                                var nativeType = (ICliMetadataNativeTypeSignature)field.FieldType.Type;
                                switch (nativeType.TypeKind)
                                {
                                    case CliMetadataNativeTypes.SByte:
                                        enumType = EnumerationBaseType.SByte;
                                        break;
                                    case CliMetadataNativeTypes.Byte:
                                        enumType = EnumerationBaseType.Byte;
                                        break;
                                    case CliMetadataNativeTypes.Int16:
                                        enumType = EnumerationBaseType.Int16;
                                        break;
                                    case CliMetadataNativeTypes.UInt16:
                                        enumType = EnumerationBaseType.UInt16;
                                        break;
                                    case CliMetadataNativeTypes.Int32:
                                        enumType = EnumerationBaseType.Int32;
                                        break;
                                    case CliMetadataNativeTypes.UInt32:
                                        enumType = EnumerationBaseType.UInt32;
                                        break;
                                    case CliMetadataNativeTypes.Int64:
                                        enumType = EnumerationBaseType.Int64;
                                        break;
                                    case CliMetadataNativeTypes.UInt64:
                                        enumType = EnumerationBaseType.UInt64;
                                        break;
                                    case CliMetadataNativeTypes.NativeInteger:
                                        enumType = EnumerationBaseType.NativeInteger;
                                        break;
                                    case CliMetadataNativeTypes.NativeUnsignedInteger:
                                        enumType = EnumerationBaseType.NativeUnsignedInteger;
                                        break;
                                }
                            }
                        }
                    }
                }
                return this.enumType.Value;
            }
        }

        protected override ITypeParent OnGetParent()
        {
            return base.OnGetParent();
        }

        public new IEnumType ElementType
        {
            get { return (IEnumType)base.ElementType; }
        }

        public IFieldMemberDictionary<IEnumFieldMember, IEnumType> Fields
        {
            get {
                if (this.fields == null)
                    this.fields = this.InitializeFields();
                return this.fields;
            }
        }

        private IFieldMemberDictionary<IEnumFieldMember, IEnumType> InitializeFields()
        {
            return new CliFieldMemberDictionary<IEnumFieldMember, IEnumType>(this, (CliFullMemberDictionary)this.Members);
        }

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        protected override IGeneralTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            if (this.Parent is IType)
                return ((IType)this.Parent).UniqueIdentifier.GetNestedIdentifier(this.MetadataEntry.Name, AstIdentifier.GetDeclarationIdentifier(this.NamespaceName));
            else if (this.Namespace == null)
                return this.Assembly.UniqueIdentifier.GetTypeIdentifier((IGeneralDeclarationUniqueIdentifier)null, this.MetadataEntry.Name);
            else
                return this.Assembly.UniqueIdentifier.GetTypeIdentifier(this.Namespace.UniqueIdentifier, this.MetadataEntry.Name);
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return this.Fields.Keys.Cast<IGeneralDeclarationUniqueIdentifier>(); }
        }

        public IControlledCollection<ICliMetadataPropertyTableRow> _Properties
        {
            get { return null; }
        }

        public IControlledCollection<ICliMetadataEventTableRow> _Events
        {
            get { return null; }
        }

        public IControlledCollection<ICliMetadataMethodDefinitionTableRow> _Methods
        {
            get { return null; }
        }

        public IControlledCollection<ICliMetadataFieldTableRow> _Fields
        {
            get { return this.MetadataEntry.Fields; }
        }

        public ISubordinateDictionary BinaryOperators
        {
            get { return null; }
        }

        public ISubordinateDictionary Constructors
        {
            get { return null; }
        }

        public ISubordinateDictionary Events
        {
            get { return null; }
        }

        ISubordinateDictionary _ICliMemberParent.Fields
        {
            get { return (ISubordinateDictionary)this.Fields; }
        }

        public ISubordinateDictionary Indexers
        {
            get { return null; }
        }

        public ISubordinateDictionary Methods
        {
            get { return null; }
        }

        public ISubordinateDictionary Properties
        {
            get { return null; }
        }

        public ISubordinateDictionary TypeCoercions
        {
            get { return null; }
        }

        public ISubordinateDictionary UnaryOperators
        {
            get { return null; }
        }

        ICliMetadataRoot _ICliMemberParent.MetadataRoot
        {
            get { return this.MetadataEntry.MetadataRoot; }
        }

        public IMember CreateItem(CliMemberType memberKind, ICliMetadataTableRow metadataEntry, IMemberUniqueIdentifier uniqueIdentifier, int index)
        {
            switch (memberKind)
            {
                case CliMemberType.Field:
                    return new FieldMember(this, (ICliMetadataFieldTableRow)metadataEntry, (IGeneralMemberUniqueIdentifier)uniqueIdentifier);
                default:
                    throw new NotSupportedException();
            }
        }

        public new _ICliManager IdentityManager
        {
            get { throw new NotImplementedException(); }
        }
    }
}
