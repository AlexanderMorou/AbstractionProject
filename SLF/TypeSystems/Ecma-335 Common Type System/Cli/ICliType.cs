using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliType :
        IType
    {
        /// <summary>
        /// Returns the <see cref="ICliAssembly"/>
        /// from which the <see cref="ICliType"/> derives.
        /// </summary>
        new ICliAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="CliMetadataTypeDefinitionTableRow"/>
        /// from which the current <see cref="ICliType"/>
        /// is derived.
        /// </summary>
        ICliMetadataTypeDefinitionTableRow Metadata { get; }
    }
}
