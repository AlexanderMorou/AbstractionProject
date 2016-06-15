using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliType :
        IType,
        ICliDeclaration
    {
        /// <summary>
        /// Returns the <see cref="ICliAssembly"/>
        /// from which the <see cref="ICliType"/> derives.
        /// </summary>
        new ICliAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeDefinitionTableRow"/>
        /// from which the current <see cref="ICliType"/>
        /// is derived.
        /// </summary>
        new ICliMetadataTypeDefinitionTableRow MetadataEntry { get; }
    }
}
