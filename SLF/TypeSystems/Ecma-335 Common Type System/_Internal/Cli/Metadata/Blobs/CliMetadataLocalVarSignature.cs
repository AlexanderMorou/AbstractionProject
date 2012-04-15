using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataLocalVarSignature :
        ArrayReadOnlyCollection<ICliMetadataLocalVarEntrySignature>,
        ICliMetadataLocalVarSignature
    {
        public static readonly CliMetadataLocalVarSignature Empty = new CliMetadataLocalVarSignature(new ICliMetadataLocalVarEntrySignature[0]);
        public CliMetadataLocalVarSignature(ICliMetadataLocalVarEntrySignature[] localVariables)
            : base(localVariables)
        {
        }
    }
}
