using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataCustomModifierSignature :
        ICliMetadataCustomModifierSignature
    {
        internal CliMetadataCustomModifierSignature(bool required, ITypeDefOrRefRow modifierType)
        {
            this.Required = required;
            this.ModifierType = modifierType;
        }

        //#region ICliMetadataCustomModifierSignature Members

        public bool Required { get; private set; }

        public ITypeDefOrRefRow ModifierType { get; private set; }

        //#endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Required)
            {
                sb.Append("modreq ");
            }
            else
                sb.Append("modopt ");
            switch (ModifierType.TypeDefOrRefEncoding)
            {
                case CliMetadataTypeDefOrRefTag.TypeDefinition:
                    sb.Append(((ICliMetadataTypeDefinitionTableRow) ModifierType).Name);
                    break;
                case CliMetadataTypeDefOrRefTag.TypeReference:
                    sb.Append(((ICliMetadataTypeRefTableRow) ModifierType).Name);
                    break;
                case CliMetadataTypeDefOrRefTag.TypeSpecification:
                    sb.Append(((ICliMetadataTypeSpecificationTableRow) ModifierType).Signature);
                    break;
            }
            return sb.ToString();
        }

    }
}
