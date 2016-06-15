using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Defines properties and methods for working with a method's body.
    /// </summary>
    public interface ICliMetadataMethodBody
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataMethodHeader"/> which denotes the local variables of the method,
        /// the exception tables, and so on.
        /// </summary>
        ICliMetadataMethodHeader Header { get; }
        IEnumerable<ICilStackInstruction> Instructions { get; }
    }
}
