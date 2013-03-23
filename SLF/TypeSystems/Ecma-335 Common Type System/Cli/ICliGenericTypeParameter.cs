using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliGenericParameter :
        IGenericParameter
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataGenericParameterTableRow"/> which represents the metadata
        /// from which the <see cref="ICliGenericParameter"/> is derived.
        /// </summary>
        ICliMetadataGenericParameterTableRow MetadataEntry { get; }
    }
}
