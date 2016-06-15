using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli.Modules
{
    public interface ICliModule :
        IModule
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataModuleTableRow"/>
        /// in which the <see cref="ICliModule"/> is defined.
        /// </summary>
        ICliMetadataModuleTableRow Metadata { get; }
        new ICliAssembly Parent { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeDefinitionTableRow"/> which
        /// denotes the global scope of the module.
        /// </summary>
        ICliMetadataTypeDefinitionTableRow GlobalScope { get; }
    }
}
