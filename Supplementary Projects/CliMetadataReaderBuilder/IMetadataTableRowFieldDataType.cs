using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace CliMetadataReader
{
    public enum FieldDataKind
    {
        Encoding,
        HeapIndex,
        DataType,
        SelfsufficientDataType,
        CastDataType,
        TableReference,
    }

    public interface IMetadataTableFieldDataType
    {
        /// <summary>
        /// Returns the <see cref="FieldDataKind"/> which
        /// determines how to handle the field's
        /// data.
        /// </summary>
        FieldDataKind DataKind { get; }


    }

    public interface IMetadataTableEncodedField {
        string EncodingIdName { get; }

        IFieldMember EncodedField { get; set; }

        IPropertyMember EncodingProperty { get; set; }
    }

    public interface IMetadataTableFieldEncodingDataType :
        IEnumerable<MetadataTable>
    {
        ITypeReference EncodingType { get; }
        string Name { get; }

        IInterfaceType EncodingCommonType { get; set; }

        IStatementBlockLocalMember WordSizeLocal { get; set; }

        bool Contains(MetadataTable target);

        byte BitEncodingSize { get; }

        IEnumerable<Tuple<IExpression, MetadataTable>> Values { get; }

    }
}
