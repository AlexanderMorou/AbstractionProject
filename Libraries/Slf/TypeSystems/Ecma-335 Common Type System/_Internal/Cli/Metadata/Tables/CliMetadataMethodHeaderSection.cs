using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    /// <summary>
    /// Provides a root definition of a method header section.
    /// </summary>
    internal class CliMetadataMethodHeaderSection :
        ICliMetadataMethodHeaderSection
    {
        private MethodHeaderSectionFlags flags;
        /// <summary>
        /// Creates a new <see cref="MethodHeaderSection"/> with the
        /// <paramref name="flags"/> provided.
        /// </summary>
        /// <param name="flags">The <see cref="MethodHeaderSectionFlags"/>
        /// which determine information about the <see cref="MethodHeaderSection"/>.</param>
        internal CliMetadataMethodHeaderSection(MethodHeaderSectionFlags flags)
        {
            this.flags = flags;
        }

        public MethodHeaderSectionFlags Flags
        {
            get { return this.flags; }
        }
    }
}
