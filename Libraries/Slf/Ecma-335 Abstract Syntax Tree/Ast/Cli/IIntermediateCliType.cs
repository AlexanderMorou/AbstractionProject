using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    /// <summary>Defines properties and methods for working with an intermediate type targeting the Common Language Infrastructure.</summary>
    public interface IIntermediateCliType :
        ICliType
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateCliAssembly"/> from which the <see cref="ICliType"/> derives.
        /// </summary>
        new IIntermediateCliAssembly Assembly { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeDefinitionMutableTableRow"/> from which the current <see cref="ICliType"/>
        /// is derived.
        /// </summary>
        new ICliMetadataTypeDefinitionMutableTableRow MetadataEntry { get; }
    }
}
