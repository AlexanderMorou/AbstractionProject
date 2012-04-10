using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataLocalVarFullEntrySignature :
        CliMetadataLocalVarEntrySignature,
        ICliMetadataLocalVarFullEntrySignature
    {
        public CliMetadataLocalVarFullEntrySignature(ICliMetadataTypeSignature localType, IEnumerable<ICliMetadataCustomModifierSignature> customModifiers, bool pinned)
            : base(CliMetadataLocalVarEntryKind.Full)
        {
            this.LocalType = localType;
            if (customModifiers == null)
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>();
            else
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers.ToArray());
            this.IsPinned = pinned;
        }

        #region ICliMetadataLocalVarFullEntrySignature Members

        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        public ICliMetadataTypeSignature LocalType { get; private set; }

        public bool IsPinned { get; private set; }

        #endregion
    }
}
