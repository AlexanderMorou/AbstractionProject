using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataPropertySignature : 
        ICliMetadataPropertySignature
    {
        internal CliMetadataPropertySignature(bool instance, IEnumerable<ICliMetadataParamSignature> parameters, IEnumerable<ICliMetadataCustomModifierSignature> customModifiers, ICliMetadataTypeSignature propertyType)
        {
            this.Instance = instance;
            this.Parameters = new ReadOnlyCollection<ICliMetadataParamSignature>(parameters.ToArray());
            if (customModifiers == null)
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>();
            else
                this.CustomModifiers = new ReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers.ToArray());
            this.PropertyType = propertyType;
        }

        #region ICliMetadataPropertySignature Members

        public bool Instance { get; private set; }

        public IReadOnlyCollection<ICliMetadataParamSignature> Parameters { get; private set; }

        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        public ICliMetadataTypeSignature PropertyType { get; private set; }

        #endregion

    }
}
