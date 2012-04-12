using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public interface ICliMetadataRoot
    {
        /// <summary>
        /// Returns the <see cref="PEImage"/> which ponts to the
        /// <see cref="ICliMetadataRoot"/>.
        /// </summary>
        PEImage BaseImage { get; }
    }
}
