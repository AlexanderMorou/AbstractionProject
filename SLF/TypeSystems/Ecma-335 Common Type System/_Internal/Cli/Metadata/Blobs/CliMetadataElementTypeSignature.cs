using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataElementTypeSignature :
        ICliMetadataElementTypeSignature
    {
        internal CliMetadataElementTypeSignature(TypeElementClassification classification, ICliMetadataTypeSignature elementType)
        {
            this.Classification = classification;
            this.ElementType = elementType;
        }


        //#region ICliMetadataElementTypeSignature Members

        /// <summary>
        /// Returns the <see cref="TypeElementClassification"/>
        /// which determines the kind of entity the wrapper
        /// <see cref="CliMetadataElementTypeSignature"/> is.
        /// </summary>
        public TypeElementClassification Classification { get; private set; }

        public ICliMetadataTypeSignature ElementType { get; private set; }

        //#endregion

        public override string ToString()
        {
            switch (Classification)
            {
                case TypeElementClassification.Nullable:
                    return string.Format("{0}?", ElementType);
                case TypeElementClassification.Pointer:
                    return string.Format("{0}*", ElementType);
                case TypeElementClassification.Reference:
                    return string.Format("{0}&", ElementType);
                case TypeElementClassification.ModifiedType:
                    return ElementType.ToString();
                default:
                    break;
            }
            return null;
        }

        #region ICliMetadataTypeSignature Members

        public virtual CliMetadataTypeSignatureKind TypeSignatureKind
        {
            get { return CliMetadataTypeSignatureKind.ElementType; }
        }

        #endregion

        #region ICliMetadataSignature Members

        public virtual SignatureKinds SignatureKind
        {
            get { return SignatureKinds.Type; }
        }

        #endregion
    }
}
