using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
//using AllenCopeland.Abstraction.OldCodeGen.Expression;
//using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Abstract;
namespace CliMetadataReader
{
    public interface IMetadataTableBlobHeapDataType :
        IMetadataTableFieldHeapDataType
    {
        /// <summary>
        /// Returns the <see cref="IExpression"/> which represents
        /// the signature blob from the <see cref="SignatureKinds"/>
        /// enumeration.
        /// </summary>
        IExpression SignatureKind { get; }
        /// <summary>
        /// Returns the <see cref="IType"/> which represents
        /// the actual type of the signature.
        /// </summary>
        IType SignatureType { get; }
    }
}
