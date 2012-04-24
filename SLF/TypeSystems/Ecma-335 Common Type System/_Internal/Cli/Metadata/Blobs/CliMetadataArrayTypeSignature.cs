using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataArrayTypeSignature :
        ICliMetadataArrayTypeSignature
    {
        public CliMetadataArrayTypeSignature(ICliMetadataTypeSignature elementType, ICliMetadataArrayShapeSignature shape)
        {
            this.ElementType = elementType;
            this.Shape = shape;
        }

        //#region ICliMetadataArrayTypeSignature Members

        public ICliMetadataArrayShapeSignature Shape { get; private set; }

        //#endregion

        //#region ICliMetadataElementTypeSignature Members

        public TypeElementClassification Classification
        {
            get { return TypeElementClassification.Array; }
        }

        public ICliMetadataTypeSignature ElementType { get; private set; }

        //#endregion

        public override string ToString()
        {
            return string.Format("{0}{1}", ElementType, Shape);
        }
    }
}
