using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.IO;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    partial class CliMetadataBlobHeaderAndHeap
    {
        private class SmallBlobEntry :
            _ICliMetadataBlobEntry
        {
            sbyte length;
            public SmallBlobEntry(sbyte length)
            {
                this.length = length;
            }
            public int Length { get { return this.length; } }

            public byte LengthByteCount { get { return 1; } }

            public ICliMetadataSignature Signature { get; set; }

            public byte[] BlobData { get; set; }

        }
        private class MediumBlobEntry :
            _ICliMetadataBlobEntry
        {
            short length;
            public MediumBlobEntry(short length)
            {
                this.length = length;
            }
            public int Length { get { return this.length; } }

            public byte LengthByteCount { get { return 2; } }

            public ICliMetadataSignature Signature { get; set; }

            public byte[] BlobData { get; set; }

        }
        private class LargeBlobEntry :
            _ICliMetadataBlobEntry
        {
            int length;
            public LargeBlobEntry(int length)
            {
                this.length = length;
            }
            public int Length { get { return this.length; } }

            public byte LengthByteCount { get { return 4; } }

            public ICliMetadataSignature Signature { get; set; }

            public byte[] BlobData { get; set; }

        }
    }
}
