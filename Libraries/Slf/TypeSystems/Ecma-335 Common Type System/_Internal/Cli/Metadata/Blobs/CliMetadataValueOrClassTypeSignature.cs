using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataValueOrClassTypeSignature :
        ICliMetadataValueOrClassTypeSignature
    {
        internal CliMetadataValueOrClassTypeSignature(bool isClass, ICliMetadataTypeDefOrRefRow target)
        {
            this.IsClass = isClass;
            this.Target = target;
        }

        //#region ICliMetadataValueOrClassTypeSignature Members

        public bool IsClass { get; private set; }

        public bool IsValueType { get { return !this.IsClass; } }

        public ICliMetadataTypeDefOrRefRow Target { get; private set; }

        //#endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (IsClass)
            {
                sb.Append("class ");
            }
            else
                sb.Append("valuetype ");
            switch (Target.TypeDefOrRefEncoding)
            {
                case CliMetadataTypeDefOrRefTag.TypeDefinition:
                    sb.Append(((ICliMetadataTypeDefinitionTableRow) Target).Name);
                    break;
                case CliMetadataTypeDefOrRefTag.TypeReference:
                    sb.Append(((ICliMetadataTypeRefTableRow) Target).Name);
                    break;
                case CliMetadataTypeDefOrRefTag.TypeSpecification:
                    sb.Append(((ICliMetadataTypeSpecificationTableRow) Target).Signature);
                    break;
            }
            return sb.ToString();
        }

        #region ICliMetadataTypeSignature Members

        public CliMetadataTypeSignatureKind TypeSignatureKind
        {
            get { return CliMetadataTypeSignatureKind.ValueOrClassType; }
        }

        #endregion

        #region ICliMetadataSignature Members

        public SignatureKinds SignatureKind
        {
            get { return SignatureKinds.ClassOrValueType; }
        }

        #endregion
    }
}
