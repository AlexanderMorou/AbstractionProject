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
        public IType ObtainTypeReference(PrimitiveType typeIdentity, ICliAssembly assembly)
        {
            IGeneralTypeUniqueIdentifier resultIdentifier;
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
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("typeIdentity");
            }
            return this.ObtainTypeReference(resultIdentifier, assembly);
        }

        public IType ObtainTypeReference(PrimitiveType typeIdentity)
        {
            IGeneralTypeUniqueIdentifier resultIdentifier;
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
                    return null;
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
                    return this.ObtainTypeReference(PrimitiveType.Boolean, assembly);
                case CliMetadataNativeTypes.Char:
                    return this.ObtainTypeReference(PrimitiveType.Char, assembly);
                case CliMetadataNativeTypes.SByte:
                    return this.ObtainTypeReference(PrimitiveType.SByte, assembly);
                case CliMetadataNativeTypes.Byte:
                    return this.ObtainTypeReference(PrimitiveType.Byte, assembly);
                case CliMetadataNativeTypes.Int16:
                    return this.ObtainTypeReference(PrimitiveType.Int16, assembly);
                case CliMetadataNativeTypes.UInt16:
                    return this.ObtainTypeReference(PrimitiveType.UInt16, assembly);
                case CliMetadataNativeTypes.Int32:
                    return this.ObtainTypeReference(PrimitiveType.Int32, assembly);
                case CliMetadataNativeTypes.UInt32:
                    return this.ObtainTypeReference(PrimitiveType.UInt32, assembly);
                case CliMetadataNativeTypes.Int64:
                    return this.ObtainTypeReference(PrimitiveType.Int64, assembly);
                case CliMetadataNativeTypes.UInt64:
                    return this.ObtainTypeReference(PrimitiveType.UInt64, assembly);
                case CliMetadataNativeTypes.Single:
                    return this.ObtainTypeReference(PrimitiveType.Float, assembly);
                case CliMetadataNativeTypes.Double:
                    return this.ObtainTypeReference(PrimitiveType.Double, assembly);
                case CliMetadataNativeTypes.String:
                    return this.ObtainTypeReference(PrimitiveType.String, assembly);
                case CliMetadataNativeTypes.Void:
                    return this.ObtainTypeReference(this.RuntimeEnvironment.VoidType, assembly);
                case CliMetadataNativeTypes.TypedByReference:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "TypedReference"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "TypedReference"));
                case CliMetadataNativeTypes.NativeInteger:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "IntPtr"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "IntPtr"), assembly);
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "UIntPtr"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "UIntPtr"), assembly);
                case CliMetadataNativeTypes.Object:
                    return this.ObtainTypeReference(this.RuntimeEnvironment.RootType);
                case CliMetadataNativeTypes.Type:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "Type"));
                    else
                        return this.ObtainTypeReference(AstIdentifier.GetTypeIdentifier("System", "Type"), assembly);
                default:
                    throw new NotSupportedException("Native type not supported.");
            }
            throw new NotSupportedException();
        }

        #region ITypeIdentityManager<ITypeUniqueIdentifier> Members

        public IType ObtainTypeReference(IGeneralTypeUniqueIdentifier typeIdentity)
        {
            /* *
             * With no assembly as a guide, a guess has to be made.
             * */
            if (typeIdentity.Assembly == null)
            {
                if (this.RuntimeEnvironment.UseCoreLibrary)
                {
                    var coreLibraryId = this.RuntimeEnvironment.CoreLibraryIdentifier;
                    var coreLibrary = this.ObtainAssemblyReference(coreLibraryId);
                    var type = coreLibrary.FindType(typeIdentity);
                    if (type != null)
                        return this.ObtainTypeReference(type);
                    foreach (var assembly in this.loadedAssemblies.Values)
                        if (assembly.UniqueIdentifier == coreLibraryId)
                            continue;
                        else if ((type = assembly.FindType(typeIdentity)) != null)
                            return this.ObtainTypeReference(type);
                }
            }
            else
            {
                var assembly = this.ObtainAssemblyReference(typeIdentity.Assembly);
                var type = assembly.FindType(typeIdentity);
                if (type != null)
                    return this.ObtainTypeReference(type);
            }
            throw new TypeLoadException(string.Format("Could not load {0}.", typeIdentity.ToString()));
        }

        private IType ObtainTypeReference(IGeneralTypeUniqueIdentifier typeIdentity, ICliAssembly originatingAssembly)
        {
            /* *
             * With no assembly as a guide, a guess has to be made.
             * */
            if (typeIdentity.Assembly == null)
            {
                if (this.RuntimeEnvironment.UseCoreLibrary)
                {
                    var coreLibraryId = this.RuntimeEnvironment.CoreLibraryIdentifier;
                    var coreLibrary = this.ObtainAssemblyReference(coreLibraryId);
                    var type = coreLibrary.FindType(typeIdentity);
                    if (type != null)
                        return this.ObtainTypeReference(type);
                    foreach (var assembly in originatingAssembly.References.Values)
                        if ((type = assembly.FindType(typeIdentity)) != null)
                            return this.ObtainTypeReference(type);
                }
            }
            else
            {
                var assembly = this.ObtainAssemblyReference(typeIdentity.Assembly);
                var type = assembly.FindType(typeIdentity);
                if (type != null)
                    return this.ObtainTypeReference(type);
            }
            throw new TypeLoadException(string.Format("Could not load {0}.", typeIdentity.ToString()));
        }

        #endregion

    }
}
