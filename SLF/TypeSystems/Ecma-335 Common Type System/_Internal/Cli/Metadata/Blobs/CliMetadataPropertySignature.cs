using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataPropertySignature : 
        ICliMetadataPropertySignature
    {
        internal CliMetadataPropertySignature(bool instance, ICliMetadataParamSignature[] parameters, ICliMetadataCustomModifierSignature[] customModifiers, ICliMetadataTypeSignature propertyType)
        {
            this.Instance = instance;
            if (parameters == null || parameters.Length == 0)
                this.Parameters = ArrayReadOnlyCollection<ICliMetadataParamSignature>.Empty;
            else
                this.Parameters = new ArrayReadOnlyCollection<ICliMetadataParamSignature>(parameters);
            if (customModifiers == null)
                this.CustomModifiers = ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>.Empty;
            else
                this.CustomModifiers = new ArrayReadOnlyCollection<ICliMetadataCustomModifierSignature>(customModifiers.ToArray());
            this.PropertyType = propertyType;
        }

        //#region ICliMetadataPropertySignature Members

        public bool Instance { get; private set; }

        public IReadOnlyCollection<ICliMetadataParamSignature> Parameters { get; private set; }

        public IReadOnlyCollection<ICliMetadataCustomModifierSignature> CustomModifiers { get; private set; }

        public ICliMetadataTypeSignature PropertyType { get; private set; }

        //#endregion

    }
}
