using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataVectorArrayTypeSignature :
        CliMetadataElementTypeSignature,
        ICliMetadataVectorArrayTypeSignature
    {

        internal CliMetadataVectorArrayTypeSignature(ICliMetadataCustomModifierSignature[] customModifiers, ICliMetadataTypeSignature elementType)
            : base(TypeElementClassification.Array, elementType)
        {
            if (customModifiers == null)
                this.CustomModifiers = ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>.Empty;
            else
                this.CustomModifiers = new ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers);
        }

        //#region ICliMetadataCustomModifierTypeSignature Members

        public IControlledCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        //#endregion


        public override string ToString()
        {
            return string.Format("{0}{1}{2}[]", string.Join<ICliMetadataCustomModifierSignature>(" ", CustomModifiers), CustomModifiers.Count == 0 ? string.Empty : " ", ElementType);
        }

        public override CliMetadataTypeSignatureKind TypeSignatureKind
        {
            get
            {
                return CliMetadataTypeSignatureKind.VectorArrayType;
            }
        }
    }
}
