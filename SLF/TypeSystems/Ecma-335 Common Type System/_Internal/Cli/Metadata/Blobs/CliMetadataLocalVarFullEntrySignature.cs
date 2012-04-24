using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataLocalVarFullEntrySignature :
        CliMetadataLocalVarEntrySignature,
        ICliMetadataLocalVarFullEntrySignature
    {
        public CliMetadataLocalVarFullEntrySignature(ICliMetadataTypeSignature localType, ICliMetadataCustomModifierSignature[] customModifiers, bool pinned)
            : base(CliMetadataLocalVarEntryKind.Full)
        {
            this.LocalType = localType;
            if (customModifiers == null || customModifiers.Length == 0)
                this.CustomModifiers = ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>.Empty;
            else
                this.CustomModifiers = new ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers);
            this.IsPinned = pinned;
        }

        //#region ICliMetadataLocalVarFullEntrySignature Members

        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        public ICliMetadataTypeSignature LocalType { get; private set; }

        public bool IsPinned { get; private set; }

        //#endregion
    }
}
