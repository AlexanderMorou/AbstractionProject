using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Modules;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal enum BaseKindCacheType
    {
        ObjectBase,
        ValueTypeBase,
        EnumBase,
        DelegateBase,
    }

    internal interface _ICliManager :
        ICliManager
    {
        ICliAssembly GetRelativeAssembly(CliMetadataRoot root);
        IDictionary<ICliMetadataTypeDefinitionTableRow, IType> TypeCache { get; }
        IDictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType> BaseTypeKinds { get; }
        IDictionary<ICliMetadataTypeDefOrRefRow, BaseKindCacheType> RefBaseTypeKinds { get; }
    }
}
