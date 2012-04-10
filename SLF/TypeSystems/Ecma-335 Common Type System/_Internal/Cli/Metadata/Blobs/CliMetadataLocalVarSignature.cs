using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataLocalVarSignature :
        ReadOnlyCollection<ICliMetadataLocalVarEntrySignature>,
        ICliMetadataLocalVarSignature
    {
        public CliMetadataLocalVarSignature(IEnumerable<ICliMetadataLocalVarEntrySignature> localVariables)
            : base(localVariables.ToArray())
        {
        }
    }
}
