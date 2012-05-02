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
                    return "sbyte";
                case CliMetadataNativeTypes.Byte:
                    return "byte";
                case CliMetadataNativeTypes.Int16:
                    return "short";
                case CliMetadataNativeTypes.UInt16:
                    return "ushort";
                case CliMetadataNativeTypes.Int32:
                    return "int";
                case CliMetadataNativeTypes.UInt32:
                    return "uint";
                case CliMetadataNativeTypes.Int64:
                    return "long";
                case CliMetadataNativeTypes.UInt64:
                    return "ulong";
                case CliMetadataNativeTypes.Single:
                    return "float";
                case CliMetadataNativeTypes.Double:
                    return "double";
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
    }
}
