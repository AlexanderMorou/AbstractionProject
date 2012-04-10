using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataParamSignature :
        ICliMetadataParamSignature
    {
        internal CliMetadataParamSignature(ICliMetadataTypeSignature parameterType, IEnumerable<ICliMetadataCustomModifierSignature> customModifiers)
        {
            if (customModifiers == null)
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>();
            else
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers.ToArray());
            this.ParameterType = parameterType;
        }

        #region ICliMetadataParamSignature Members

        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        public ICliMetadataTypeSignature ParameterType { get; private set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", string.Join<ICliMetadataCustomModifierSignature>(" ", CustomModifiers), CustomModifiers.Count == 0 ? string.Empty : " ", ParameterType);
        }
    }
}
