using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataParamSignature :
        ICliMetadataParamSignature
    {
        internal CliMetadataParamSignature(ICliMetadataTypeSignature parameterType, ICliMetadataCustomModifierSignature[] customModifiers)
        {
            if (customModifiers == null || customModifiers.Length == 0)
                this.CustomModifiers = ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>.Empty;
            else
                this.CustomModifiers = new ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers);
            this.ParameterType = parameterType;
        }

        //#region ICliMetadataParamSignature Members

        public IControlledCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        public ICliMetadataTypeSignature ParameterType { get; private set; }

        //#endregion

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", string.Join<ICliMetadataCustomModifierSignature>(" ", CustomModifiers), CustomModifiers.Count == 0 ? string.Empty : " ", ParameterType);
        }
    }
}
