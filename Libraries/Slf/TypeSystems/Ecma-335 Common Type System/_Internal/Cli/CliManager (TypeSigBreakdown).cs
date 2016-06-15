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
        private Dictionary<IAssembly, Dictionary<RuntimeCoreType, IType>> lookupCache = new Dictionary<IAssembly, Dictionary<RuntimeCoreType, IType>>();
        public IType ObtainTypeReference(RuntimeCoreType coreType, IAssembly assembly)
        {
            Dictionary<RuntimeCoreType, IType> currentCache = null;
            lock (lookupCache)
            {
                CheckLUC(assembly);
                if (assembly != null)
                {
                    currentCache = lookupCache[assembly];
                    lock (currentCache)
                    {
                        if (currentCache != null)
                        {
                            IType result;
                            if (currentCache.TryGetValue(coreType, out result))
                                return result;
                        }
                    }
                }
            }
            ICliRuntimeEnvironmentInfo runtimeEnvironmentInfo = this.runtimeEnvironment;

            if (assembly is ICliAssembly)
            {
                ICliAssembly cliAssembly = (ICliAssembly)assembly;
                if (cliAssembly.RuntimeEnvironment.Version != runtimeEnvironmentInfo.Version)
                    runtimeEnvironmentInfo = cliAssembly.RuntimeEnvironment;
            }
            var resultType = this.ObtainTypeReference(runtimeEnvironmentInfo.GetCoreIdentifier(coreType), assembly);
            if (currentCache != null)
            {
                lock (lookupCache)
                    lock (currentCache)
                        currentCache.Add(coreType, resultType);
            }
            return resultType;
        }

        private void CheckLUC(IAssembly assembly)
        {
            if (assembly == null)
                return;
            lock (lookupCache)
                if (!lookupCache.ContainsKey(assembly))
                {
                    assembly.Disposed += lookupCacheAssembly_Disposed;
                    lookupCache.Add(assembly, new Dictionary<RuntimeCoreType, IType>());
                }
        }

        void lookupCacheAssembly_Disposed(object sender, EventArgs e)
        {
            if (sender is IAssembly)
            {
                var assembly = sender as IAssembly;
                assembly.Disposed -= lookupCacheAssembly_Disposed;
                lock (lookupCache)
                    if (lookupCache.ContainsKey(assembly))
                        lookupCache.Remove(assembly);
            }
        }

        public IType ObtainTypeReference(RuntimeCoreType coreType)
        {
            return this.ObtainTypeReference(this.RuntimeEnvironment.GetCoreIdentifier(coreType));
        }

        public IType ObtainTypeReference(CliRuntimeCoreType coreType)
        {
            return this.ObtainTypeReference(this.RuntimeEnvironment.GetCoreIdentifier(coreType));
        }

        public IType ObtainTypeReference(CliRuntimeCoreType coreType, ICliAssembly assembly)
        {
            return this.ObtainTypeReference(this.RuntimeEnvironment.GetCoreIdentifier(coreType, assembly), assembly);
        }

        //#region ITypeIdentityManager<ICliMetadataTypeSpecificationTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeSpecificationTableRow typeIdentity, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            if (typeIdentity.Signature is ICliMetadataTypeSignature)
                return this.ObtainTypeReference((ICliMetadataTypeSignature)typeIdentity.Signature, activeType, activeMethod, activeAssembly);
            throw new NotSupportedException();
        }

        public IType ObtainTypeReference(ICliMetadataTypeSpecificationTableRow typeIdentity)
        {
            return this.ObtainTypeReference(typeIdentity, null, null);
        }

        //#endregion

        public IType ObtainTypeReference(ICliMetadataNativeTypeSignature typeIdentity, ICliAssembly assembly)
        {
            var runtimeEnvironmentInfo = assembly.RuntimeEnvironment;
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
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "TypedReference"), assembly);
                    else
                        return this.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "TypedReference"), assembly);
                case CliMetadataNativeTypes.NativeInteger:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "IntPtr"), assembly);
                    else
                        return this.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IntPtr"), assembly);
                case CliMetadataNativeTypes.NativeUnsignedInteger:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(this.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "UIntPtr"), assembly);
                    else
                        return this.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "UIntPtr"), assembly);
                case CliMetadataNativeTypes.Object:
                    if (this.RuntimeEnvironment.UseCoreLibrary)
                        return this.ObtainTypeReference(RuntimeCoreType.RootType);
                    else
                        return this.ObtainTypeReference(RuntimeCoreType.RootType, assembly);
                case CliMetadataNativeTypes.Type:
                    return this.ObtainTypeReference(RuntimeCoreType.Type, assembly);
                default:
                    throw new NotSupportedException("Native type not supported.");
            }
            throw new NotSupportedException();
        }

        #region ITypeIdentityManager<ITypeUniqueIdentifier> Members

        public IType ObtainTypeReference(IGeneralTypeUniqueIdentifier typeIdentity)
        {
            return ObtainTypeReference(typeIdentity, null);
            /* *
             * With no assembly as a guide, a guess has to be made.
             * */
            if (typeIdentity.Assembly == null)
            {
                if (this.RuntimeEnvironment.UseCoreLibrary)
                {
                    var coreLibraryId = this.RuntimeEnvironment.CoreLibraryIdentifier;
                    var coreLibrary = this.ObtainAssemblyReference(coreLibraryId);
                    var type = coreLibrary.GetType(typeIdentity);
                    if (type != null)
                        return type;
                    foreach (var assembly in this.loadedAssemblies.Values)
                        if (assembly.UniqueIdentifier == coreLibraryId)
                            continue;
                        else if ((type = assembly.GetType(typeIdentity)) != null)
                            return type;
                }
            }
            else
            {
                var assembly = this.ObtainAssemblyReference(typeIdentity.Assembly);
                var type = assembly.GetType(typeIdentity);
                if (type != null)
                    return type;
            }
            throw new TypeLoadException(string.Format("Could not load {0}.", typeIdentity.ToString()));
        }


        public IType ObtainTypeReference(IGeneralTypeUniqueIdentifier typeIdentity, IAssembly originatingAssembly)
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
                    var cliCoreLibrary = (ICliAssembly)coreLibrary;
                    var type = cliCoreLibrary.GetType(typeIdentity);
                    if (type != null)
                        return type;
                }
                if (originatingAssembly != null)
                {
                    var originatingSearch = originatingAssembly.GetType(typeIdentity);
                    if (originatingSearch != null)
                        return originatingSearch;
                    foreach (var assembly in originatingAssembly.References.Values)
                    {
                        var test = assembly.GetType(typeIdentity);
                        if (test != null)
                            return test;
                    }
                }
            }
            else
            {
                var assembly = this.ObtainAssemblyReference(typeIdentity.Assembly);
                var type = assembly.GetType(typeIdentity);
                if (type != null)
                    return type;
            }
            throw new TypeLoadException(string.Format("Could not load {0}.", typeIdentity.ToString()));
        }

        #endregion

    }
}
