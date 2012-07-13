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
        public IType ObtainTypeReference(RuntimeCoreType coreType, ICliAssembly assembly)
        {
            return this.ObtainTypeReference(this.RuntimeEnvironment.GetCoreIdentifier(coreType), assembly);
        }

        public IType ObtainTypeReference(RuntimeCoreType coreType)
        {
            return this.ObtainTypeReference(this.RuntimeEnvironment.GetCoreIdentifier(coreType));
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
                    return this.ObtainTypeReference(RuntimeCoreType.Boolean, assembly);
                case CliMetadataNativeTypes.Char:
                    return this.ObtainTypeReference(RuntimeCoreType.Char, assembly);
                case CliMetadataNativeTypes.SByte:
                    return this.ObtainTypeReference(RuntimeCoreType.SByte, assembly);
                case CliMetadataNativeTypes.Byte:
                    return this.ObtainTypeReference(RuntimeCoreType.Byte, assembly);
                case CliMetadataNativeTypes.Int16:
                    return this.ObtainTypeReference(RuntimeCoreType.Int16, assembly);
                case CliMetadataNativeTypes.UInt16:
                    return this.ObtainTypeReference(RuntimeCoreType.UInt16, assembly);
                case CliMetadataNativeTypes.Int32:
                    return this.ObtainTypeReference(RuntimeCoreType.Int32, assembly);
                case CliMetadataNativeTypes.UInt32:
                    return this.ObtainTypeReference(RuntimeCoreType.UInt32, assembly);
                case CliMetadataNativeTypes.Int64:
                    return this.ObtainTypeReference(RuntimeCoreType.Int64, assembly);
                case CliMetadataNativeTypes.UInt64:
                    return this.ObtainTypeReference(RuntimeCoreType.UInt64, assembly);
                case CliMetadataNativeTypes.Single:
                    return this.ObtainTypeReference(RuntimeCoreType.Single, assembly);
                case CliMetadataNativeTypes.Double:
                    return this.ObtainTypeReference(RuntimeCoreType.Double, assembly);
                case CliMetadataNativeTypes.String:
                    return this.ObtainTypeReference(RuntimeCoreType.String, assembly);
                case CliMetadataNativeTypes.Void:
                    return this.ObtainTypeReference(RuntimeCoreType.VoidType, assembly);
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
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(RuntimeCoreType.RootType);
                    else
                        return this.ObtainTypeReference(RuntimeCoreType.RootType, assembly);
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

        public IType ObtainTypeReference(IGeneralTypeUniqueIdentifier typeIdentity, ICliAssembly originatingAssembly)
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
