using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs
{
    public interface ICliMetadataValueOrClassTypeSignature :
        ICliMetadataTypeSignature
    {
        bool IsClass { get; }
        bool IsValueType { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataTypeDefOrRefRow"/> which
        /// is referenced by the 
        /// <see cref="ICliMetadataValueOrClassTypeSignature"/>.
        /// </summary>
        ICliMetadataTypeDefOrRefRow Target { get; }
    }
}
