using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataLocalVarEntrySignature :
        ICliMetadataLocalVarEntrySignature
    {
        internal CliMetadataLocalVarEntrySignature(CliMetadataLocalVarEntryKind variableKind)
        {
            this.VariableKind = variableKind;
        }

        #region ICliMetadataLocalVarEntrySignature Members

        /// <summary>
        /// Returns the <see cref="CliMetadataLocalVarEntryKind"/>
        /// which determines whether it contains full metadata about the local
        /// variable, or whether it is a TypedReference.
        /// </summary>
        public CliMetadataLocalVarEntryKind VariableKind { get; set; }

        #endregion
    }
}
