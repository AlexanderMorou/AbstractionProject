using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataElementTypeAndModifiersSignature :
        CliMetadataElementTypeSignature,
        ICliMetadataElementTypeAndModifiersSignature
    {
        public CliMetadataElementTypeAndModifiersSignature(TypeElementClassification classification, ICliMetadataTypeSignature elementType, ICliMetadataCustomModifierSignature[] customModifiers)
            : base(classification, elementType)
        {
            if (customModifiers == null || customModifiers.Length == 0)
                this.CustomModifiers = ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>.Empty;
            else
                this.CustomModifiers = new ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers.ToArray());
        }

        //#region ICliMetadataCustomModifierTypeSignature Members

        public IControlledCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        //#endregion


        public override string ToString()
        {
            return string.Format("{0}{1}{2}", string.Join<ICliMetadataCustomModifierSignature>(" ", CustomModifiers), CustomModifiers.Count == 0 ? string.Empty : " ", base.ToString());
        }

        #region ICliMetadataTypeSignature Members

        public CliMetadataTypeSignatureKind TypeSignatureKind
        {
            get { return CliMetadataTypeSignatureKind.ElementTypeAndModifiers; }
        }

        #endregion

        public override SignatureKinds SignatureKind
        {
            get
            {
                return SignatureKinds.ModifiedTypeWithElementType;
            }
        }

    }
}
