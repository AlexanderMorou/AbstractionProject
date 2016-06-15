using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using AllenCopeland.Abstraction.OldCodeGen.Types;
//using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
//using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast;

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

        IIntermediateClassFieldMember EncodedField { get; set; }

        IIntermediateClassPropertyMember EncodingProperty { get; set; }
    }

    public interface IMetadataTableFilteredEncodedField :
        IMetadataTableEncodedField
    {
        string FilteredSuffix { get; set; }
    }

    public interface IMetadataTableFieldEncodingDataType :
        IEnumerable<MetadataTable>
    {
        IType EncodingType { get; }
        string Name { get; }

        IIntermediateInterfaceType EncodingCommonType { get; set; }

        ILocalMember WordSizeLocal { get; set; }

        bool Contains(MetadataTable target);

        byte BitEncodingSize { get; }

        IEnumerable<Tuple<IExpression, MetadataTable>> Values { get; }

    }
}
