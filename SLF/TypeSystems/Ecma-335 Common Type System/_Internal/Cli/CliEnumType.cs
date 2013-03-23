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

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliEnumType :
        CliTypeBase<IGeneralTypeUniqueIdentifier>,
        IEnumType
    {
        private EnumerationBaseType? enumType;
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
            throw new NotImplementedException();
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
            get { throw new NotImplementedException(); }
        }

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        protected override IGeneralTypeUniqueIdentifier OnGetUniqueIdentifier()
        {
            if (this.Parent is IType)
                return ((IType)this.Parent).UniqueIdentifier.GetNestedIdentifier(this.MetadataEntry.Name, AstIdentifier.GetDeclarationIdentifier(this.NamespaceName));
            else
                return this.Assembly.UniqueIdentifier.GetTypeIdentifier(this.Namespace.UniqueIdentifier, this.MetadataEntry.Name);
        }

        public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get { return this.Fields.Keys.Cast<IGeneralDeclarationUniqueIdentifier>(); }
        }
    }
}
