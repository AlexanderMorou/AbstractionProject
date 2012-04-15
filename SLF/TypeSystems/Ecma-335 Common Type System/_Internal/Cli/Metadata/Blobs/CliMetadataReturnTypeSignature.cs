using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataReturnTypeSignature :
        ICliMetadataReturnTypeSignature
    {
        internal CliMetadataReturnTypeSignature(ICliMetadataTypeSignature returnType, ICliMetadataCustomModifierSignature[] customModifiers)
        {
            this.ReturnType = returnType;
            if (customModifiers == null)
                this.CustomModifiers = ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>.Empty;
            else
                this.CustomModifiers = new ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers);
        }

        //#region ICliMetadataReturnTypeSignature Members

        public ICliMetadataTypeSignature ReturnType { get; private set; }

        //#endregion

        //#region ICliMetadataCustomModifierTypeSignature Members

        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        //#endregion

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", string.Join<ICliMetadataCustomModifierSignature>(" ", CustomModifiers), CustomModifiers.Count == 0 ? string.Empty : " ", ReturnType);
        }
    }
}
