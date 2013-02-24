using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Defines properties and methods for working with a method header
    /// section.
    /// </summary>
    public interface ICliMetadataMethodHeaderSection
    {
        /// <summary>
        /// Returns the <see cref="MethodHeaderSectionFlags"/> which denote
        /// information specific to the current section.
        /// </summary>
        MethodHeaderSectionFlags Flags { get; }
    }
}
