using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICompiledType :
        IType
    {
        /// <summary>
        /// Returns the <see cref="ICompiledAssembly"/>
        /// from which the <see cref="ICompiledType"/> derives.
        /// </summary>
        new ICompiledAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="CliMetadataTypeDefinitionTableRow"/>
        /// from which the current <see cref="ICompiledType"/>
        /// is derived.
        /// </summary>
        ICliMetadataTypeDefinitionTableRow Metadata { get; }
    }
}
