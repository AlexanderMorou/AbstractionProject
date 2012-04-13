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
        private class BlobEntry :
            ICliMetadataBlobEntry
        {
            int length;

            public BlobEntry(int length)
            {
                this.length = length;
            }

            //#region ICliMetadataBlobEntry Members

            public int Length { get { return this.length; } }

            public byte LengthByteCount
            {
                get
                {
                    int length = this.length;
                    if ((length & 0xFFFFFF80) == 0)
                        return 1;
                    else if ((length & 0xFFFFFC00) == 0)
                        return 2;
                    else
                        return 4;
                }
            }

            public ICliMetadataSignature Signature { get; set; }

            public byte[] BlobData { get; internal set; }

            //#endregion
        }
    }
}
