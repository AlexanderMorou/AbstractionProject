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
        /// Returns the <see cref=""/>
        /// </summary>
        ICliMetadataModuleTableRow Metadata { get; }
    }
}
