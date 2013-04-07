using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs
{
    internal class CliMetadataNativeTypeSignature :
        ICliMetadataNativeTypeSignature
    {
        internal CliMetadataNativeTypeSignature(CliMetadataNativeTypes typeKind)
        {
            this.TypeKind = typeKind;
        }

        //#region ICliMetadataNativeTypeSignature Members

        public CliMetadataNativeTypes TypeKind { get; private set; }

        //#endregion

        public override string ToString()
        {
            switch (TypeKind)
            {
                case CliMetadataNativeTypes.Void:
                    return "void";
                case CliMetadataNativeTypes.Boolean:
                    return "bool";
                case CliMetadataNativeTypes.Char:
                    return "char";
                case CliMetadataNativeTypes.SByte:
                    return "int8";
                case CliMetadataNativeTypes.Byte:
                    return "uint8";
                case CliMetadataNativeTypes.Int16:
                    return "int16";
                case CliMetadataNativeTypes.UInt16:
                    return "uint16";
                case CliMetadataNativeTypes.Int32:
                    return "int32";
                case CliMetadataNativeTypes.UInt32:
                    return "uint32";
                case CliMetadataNativeTypes.Int64:
                    return "int64";
                case CliMetadataNativeTypes.UInt64:
                    return "uint64";
                case CliMetadataNativeTypes.Single:
                    return "float32";
                case CliMetadataNativeTypes.Double:
                    return "float64";
                case CliMetadataNativeTypes.String:
                    return "string";
                case CliMetadataNativeTypes.TypedByReference:
                    return "System.TypedReference";
                case CliMetadataNativeTypes.NativeInteger:
                    return "System.IntPtr";
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                    return "System.UIntPtr";
                case CliMetadataNativeTypes.Object:
                    return "object";
                case CliMetadataNativeTypes.Type:
                    return "System.Type";
            }
            return null;
        }

        #region ICliMetadataTypeSignature Members

        public CliMetadataTypeSignatureKind TypeSignatureKind
        {
            get { return CliMetadataTypeSignatureKind.NativeType; }
        }

        #endregion

        #region ICliMetadataSignature Members

        public SignatureKinds SignatureKind
        {
            get { return SignatureKinds.NativeType; }
        }

        #endregion
    }
}
