using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Documentation;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliManager
    {
        public IType ObtainTypeReference(PrimitiveType typeIdentity)
        {
            ITypeUniqueIdentifier resultIdentifier;
            switch (typeIdentity)
            {
                case PrimitiveType.Boolean:
                    resultIdentifier = this.runtimeEnvironment.Boolean;
                    break;
                case PrimitiveType.Byte:
                    resultIdentifier = this.runtimeEnvironment.Byte;
                    break;
                case PrimitiveType.SByte:
                    resultIdentifier = this.runtimeEnvironment.SByte;
                    break;
                case PrimitiveType.Int16:
                    resultIdentifier = this.runtimeEnvironment.Int16;
                    break;
                case PrimitiveType.UInt16:
                    resultIdentifier = this.runtimeEnvironment.UInt16;
                    break;
                case PrimitiveType.Int32:
                    resultIdentifier = this.runtimeEnvironment.Int32;
                    break;
                case PrimitiveType.UInt32:
                    resultIdentifier = this.runtimeEnvironment.UInt32;
                    break;
                case PrimitiveType.Int64:
                    resultIdentifier = this.runtimeEnvironment.Int64;
                    break;
                case PrimitiveType.UInt64:
                    resultIdentifier = this.runtimeEnvironment.UInt64;
                    break;
                case PrimitiveType.Decimal:
                    resultIdentifier = this.runtimeEnvironment.Decimal;
                    break;
                case PrimitiveType.Float:
                    resultIdentifier = this.runtimeEnvironment.Single;
                    break;
                case PrimitiveType.Double:
                    resultIdentifier = this.runtimeEnvironment.Double;
                    break;
                case PrimitiveType.Char:
                    resultIdentifier = this.runtimeEnvironment.Char;
                    break;
                case PrimitiveType.String:
                    resultIdentifier = this.runtimeEnvironment.String;
                    break;
                case PrimitiveType.Null:
                default:
                    throw new ArgumentOutOfRangeException("typeIdentity");
            }
            return this.ObtainTypeReference(resultIdentifier);
        }

        //#region ITypeIdentityManager<ICliMetadataTypeSpecificationTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeSpecificationTableRow typeIdentity)
        {
            var relativeAssembly = GetRelativeAssembly(typeIdentity.MetadataRoot);
            throw new NotImplementedException();
        }

        //#endregion

        public IType ObtainTypeReference(ICliMetadataNativeTypeSignature typeIdentity, ICliAssembly assembly)
        {
            switch (typeIdentity.TypeKind)
            {
                case CliMetadataNativeTypes.Boolean:
                    return this.ObtainTypeReference(PrimitiveType.Boolean);
                case CliMetadataNativeTypes.Char:
                    return this.ObtainTypeReference(PrimitiveType.Char);
                case CliMetadataNativeTypes.SByte:
                    return this.ObtainTypeReference(PrimitiveType.SByte);
                case CliMetadataNativeTypes.Byte:
                    return this.ObtainTypeReference(PrimitiveType.Byte);
                case CliMetadataNativeTypes.Int16:
                    return this.ObtainTypeReference(PrimitiveType.Int16);
                case CliMetadataNativeTypes.UInt16:
                    return this.ObtainTypeReference(PrimitiveType.UInt16);
                case CliMetadataNativeTypes.Int32:
                    return this.ObtainTypeReference(PrimitiveType.Int32);
                case CliMetadataNativeTypes.UInt32:
                    return this.ObtainTypeReference(PrimitiveType.UInt32);
                case CliMetadataNativeTypes.Int64:
                    return this.ObtainTypeReference(PrimitiveType.Int64);
                case CliMetadataNativeTypes.UInt64:
                    return this.ObtainTypeReference(PrimitiveType.UInt64);
                case CliMetadataNativeTypes.Single:
                    return this.ObtainTypeReference(PrimitiveType.Float);
                case CliMetadataNativeTypes.Double:
                    return this.ObtainTypeReference(PrimitiveType.Double);
                case CliMetadataNativeTypes.String:
                    return this.ObtainTypeReference(PrimitiveType.String);
                case CliMetadataNativeTypes.Void:
                    return this.ObtainTypeReference(this.RuntimeEnvironment.VoidType);
                case CliMetadataNativeTypes.TypedByReference:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "TypedReference"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "TypedReference"));
                case CliMetadataNativeTypes.NativeInteger:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "IntPtr"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "IntPtr"));
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "UIntPtr"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "UIntPtr"));
                case CliMetadataNativeTypes.Object:
                    return this.ObtainTypeReference(this.RuntimeEnvironment.RootType);
                case CliMetadataNativeTypes.Type:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "Type"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "Type"));
                default:
                    throw new NotSupportedException("Native type not supported.");
            }
            throw new NotSupportedException();
        }

    }
}
