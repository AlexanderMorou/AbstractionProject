using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Diagnostics;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    [DebuggerDisplay("{ToString(),nq}")]
    internal class CliMetadataArrayShapeSignature :
        ICliMetadataArrayShapeSignature
    {
        public CliMetadataArrayShapeSignature(uint rank, IEnumerable<uint> sizes, IEnumerable<uint> lowerBounds)
        {
            this.Rank = rank;
            this.Sizes = new ReadOnlyCollection<uint>(sizes.ToList());
            this.LowerBounds = new ReadOnlyCollection<uint>(lowerBounds.ToList());
        }

        //#region ICliMetadataArrayShapeSignature Members

        public uint Rank { get; private set; }

        public IReadOnlyCollection<uint> Sizes { get; private set; }

        public IReadOnlyCollection<uint> LowerBounds { get; private set; }

        //#endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            bool first = true;
            for (int i = 0; i < this.Rank; i++)
            {
                if (first)
                    first = false;
                else
                    sb.Append(",");
                if (i < LowerBounds.Count)
                {
                    if (i < Sizes.Count)
                    {
                        sb.Append(LowerBounds[i]);
                        sb.Append("...");
                        sb.Append(LowerBounds[i] + (Sizes[i] - 1));
                    }
                    else
                    {
                        sb.Append(LowerBounds[i]);
                        sb.Append("...");
                    }
                }
                else if (i < Sizes.Count)
                {
                    sb.Append("0...");
                    sb.Append(Sizes[i] - 1);
                }
            }
            sb.Append(']');
            return sb.ToString();
        }
    }
}
