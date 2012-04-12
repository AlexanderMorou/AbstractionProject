using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataElementTypeAndModifiersSignature :
        CliMetadataElementTypeSignature,
        ICliMetadataElementTypeAndModifiersSignature
    {
        public CliMetadataElementTypeAndModifiersSignature(TypeElementClassification classification, ICliMetadataTypeSignature elementType, IEnumerable<ICliMetadataCustomModifierSignature> customModifiers)
            : base(classification, elementType)
        {
            if (customModifiers == null)
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>();
            else
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers.ToArray());
        }

        //#region ICliMetadataCustomModifierTypeSignature Members

        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        //#endregion


        public override string ToString()
        {
            return string.Format("{0}{1}{2}", string.Join<ICliMetadataCustomModifierSignature>(" ", CustomModifiers), CustomModifiers.Count == 0 ? string.Empty : " ", base.ToString());
        }
    }
}
