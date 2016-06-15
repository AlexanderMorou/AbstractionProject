using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Parsers
{
    /// <summary>
    /// Defines properties and methods for working with a named
    /// stream which acts as the source of a given stream to parse.
    /// </summary>
    public interface INamedStreamInfo
    {
        /// <summary>
        /// Returns the <see cref="Uri"/> value which denotes where the 
        /// <see cref="INamedStreamInfo.Stream"/> originates.
        /// </summary>
        Uri Uri { get; }

        /// <summary>
        /// Returns the <see cref="Stream"/> used to read the structure
        /// of the file.
        /// </summary>
        Stream Stream { get; }
    }
}
