using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataGenericParameterSignature :
        ICliMetadataGenericParameterSignature
    {
        internal CliMetadataGenericParameterSignature(CliMetadataGenericParameterParent parent , uint position)
        {
            this.Position = position;
            this.Parent = parent;
        }
        //#region ICliMetadataGenericParameterSignature Members

        public CliMetadataGenericParameterParent Parent { get; private set; }

        public uint Position { get; private set; }

        //#endregion

        public override string ToString()
        {
            return string.Format("{0}!{1}", Parent == CliMetadataGenericParameterParent.Method ? "!" : string.Empty, Position);
        }
    }
}
