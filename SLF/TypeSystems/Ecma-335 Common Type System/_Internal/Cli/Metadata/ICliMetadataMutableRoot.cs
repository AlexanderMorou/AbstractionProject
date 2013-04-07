using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal interface ICliMetadataMutableRoot :
        ICliMetadataRoot
    {
        /// <summary>
        /// Returns/sets the <see cref="CliHeader"/> which defines information about the
        /// module.
        /// </summary>
        /// <remarks>The <see cref="CliHeader"/> contains a relative virtual address
        /// from which the <see cref="ICliMetadataRoot"/> is derived.</remarks>
        new CliHeader Header { get; set; }
        /// <summary>
        /// Returns the <see cref="String"/> value which denotes version
        /// information about the target runtime of the module.
        /// </summary>
        new string Version { get; set; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataMutableStringsHeaderAndHeap"/>
        /// which denotes the header information for the strings heap and
        /// the data within the strings heap.
        /// </summary>
        new ICliMetadataMutableStringsHeaderAndHeap StringsHeap { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataUserStringsHeaderAndHeap"/> which
        /// denotes the header information for the user strings heap and
        /// the data within the user strings heap.
        /// </summary>
        new ICliMetadataUserStringsHeaderAndHeap UserStringsHeap { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataGuidHeaderAndHeap"/> which
        /// denotes the header information for the guid heap and the
        /// data within the guid heap.
        /// </summary>
        new ICliMetadataGuidHeaderAndHeap GuidHeap { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataBlobHeaderAndHeap"/> which
        /// denotes the header information for the blob heap and the
        /// data within the blob heap.
        /// </summary>
        new ICliMetadataBlobHeaderAndHeap BlobHeap { get; }

        /// <summary>
        /// Returns the <see cref="ICliMetadataTableStreamAndHeader"/>
        /// which denotes the header information for the table stream
        /// and the metadata within the table heap.
        /// </summary>
        new ICliMetadataTableStreamAndHeader TableStream { get; }

        /// <summary>
        /// Returns the <see cref="PEImage"/> from which the
        /// <see cref="ICliMetadataRoot"/> was derived.
        /// </summary>
        new PEImage SourceImage { get; }

    }
}
