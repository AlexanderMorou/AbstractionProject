using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Instructions
{
    public interface ICilLocalStackInstruction :
        ICilStackInstruction
    {
        ICliMetadataLocalVarEntrySignature LocalVariable { get; }
    }
}
