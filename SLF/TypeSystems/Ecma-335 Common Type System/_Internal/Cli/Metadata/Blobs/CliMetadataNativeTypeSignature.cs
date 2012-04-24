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
        internal CliMetadataNativeTypeSignature(NativeTypes typeKind)
        {
            this.TypeKind = typeKind;
        }

        //#region ICliMetadataNativeTypeSignature Members

        public NativeTypes TypeKind { get; private set; }

        //#endregion

        public override string ToString()
        {
            switch (TypeKind)
            {
                case NativeTypes.Void:
                    return "void";
                case NativeTypes.Boolean:
                    return "bool";
                case NativeTypes.Char:
                    return "char";
                case NativeTypes.SByte:
                    return "sbyte";
                case NativeTypes.Byte:
                    return "byte";
                case NativeTypes.Int16:
                    return "short";
                case NativeTypes.UInt16:
                    return "ushort";
                case NativeTypes.Int32:
                    return "int";
                case NativeTypes.UInt32:
                    return "uint";
                case NativeTypes.Int64:
                    return "long";
                case NativeTypes.UInt64:
                    return "ulong";
                case NativeTypes.Single:
                    return "float";
                case NativeTypes.Double:
                    return "double";
                case NativeTypes.String:
                    return "string";
                case NativeTypes.TypedByReference:
                    return "System.TypedReference";
                case NativeTypes.NativeInteger:
                    return "System.IntPtr";
                case NativeTypes.NativeUnsignedInteger:
                    return "System.UIntPtr";
                case NativeTypes.Object:
                    return "object";
                case NativeTypes.Type:
                    return "System.Type";
            }
            return null;
        }
    }
}
