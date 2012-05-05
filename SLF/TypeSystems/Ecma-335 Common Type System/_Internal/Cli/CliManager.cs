using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
    internal partial class CliManager :
        _ICliManager
    {
        private Dictionary<string, IAssemblyUniqueIdentifier> fileIdentifiers = new Dictionary<string, IAssemblyUniqueIdentifier>();
        private Dictionary<IAssemblyUniqueIdentifier, CliAssembly> loadedAssemblies = new Dictionary<IAssemblyUniqueIdentifier, CliAssembly>();
        private Dictionary<string, Tuple<PEImage, CliMetadataRoot, string>> loadedModules = new Dictionary<string, Tuple<PEImage, CliMetadataRoot, string>>();
        private MultikeyedDictionary<CliAssembly, string, CliModule> moduleAssemblyIdentities = new MultikeyedDictionary<CliAssembly, string, CliModule>();
        private IDictionary<ICliMetadataTypeDefinitionTableRow, IType> typeCache = new Dictionary<ICliMetadataTypeDefinitionTableRow, IType>();

        private IDictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType> baseTypeKinds = new Dictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType>();
        private IDictionary<ITypeDefOrRefRow, BaseKindCacheType> refBaseTypeKinds = new Dictionary<ITypeDefOrRefRow, BaseKindCacheType>();
        //"System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"

        private ICliRuntimeEnvironmentInfo runtimeEnvironment;
        /// <summary>
        /// Creates a new <see cref="CliManager"/> with the 
        /// <paramref name="runtimeEnvironment"/> specified.
        /// </summary>
        /// <param name="runtimeEnvironment">The <see cref="ICliRuntimeEnvironmentInfo"/>
        /// which the <see cref="CliManager"/> targets.</param>
        public CliManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            this.runtimeEnvironment = runtimeEnvironment;
        }

        //#region ITypeIdentityManager<Type> Members

        public IType ObtainTypeReference(Type typeIdentity)
        {
            throw new NotImplementedException();
        }

        //#endregion

        //#region ITypeIdentityManager Members

        public bool IsMetadatumInheritable(IType metadatumType)
        {
            throw new NotImplementedException();
        }

        public IType ObtainTypeReference(object typeIdentity)
        {
            if (typeIdentity is string)
                return ObtainTypeReference((string) typeIdentity);
            else if (typeIdentity is Type)
                return ObtainTypeReference((Type) typeIdentity);
            else if (typeIdentity is PrimitiveType)
                return ObtainTypeReference((PrimitiveType) typeIdentity);
            else if (typeIdentity is ITypeDefOrRefRow)
            {
            }
            throw new ArgumentOutOfRangeException("typeIdentity");
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            lock (this.loadedAssemblies)
            {
                foreach (var assembly in this.loadedAssemblies.Values)
                    assembly.Dispose();
                this.loadedAssemblies.Clear();
                this.fileIdentifiers.Clear();
            }
        }

        //#endregion
        //#region IAssemblyIdentityManager<Assembly,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(Assembly assemblyIdentity)
        {
            /* *
             * Return the assembly relative to the current runtime environment,
             * using its location might load the wrong one.
             * */
            var name = assemblyIdentity.GetName();
            return this.ObtainAssemblyReference(AstIdentifier.GetAssemblyIdentifier(name.Name, name.Version, CultureIdentifiers.GetIdentifierById((CultureIdentifiers.NumericIdentifiers) name.CultureInfo.LCID), name.GetPublicKeyToken()));
        }

        //#endregion

        //#region ICliManager Members
        public ICliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }


        public ICliMetadataModuleTableRow LoadModule(ICliMetadataModuleReferenceTableRow metadata)
        {
            if (metadata.MetadataRoot == null)
                throw new ArgumentException("metadata must contain proper root.");
            ICliMetadataFileTable fileTable;
            var relativeAssembly = GetRelativeAssembly(metadata.MetadataRoot);
            if (relativeAssembly == null)
                throw new ArgumentException("No assembly loaded for metadata.", "metadata");
            if ((fileTable = metadata.MetadataRoot.TableStream.FileTable) != null)
            {
                fileTable.Read();
                ICliMetadataFileTableRow toCheck = null;
                foreach (var file in fileTable)
                {
                    /* *
                     * Valid metadata.
                     * */
                    if (file.NameIndex == metadata.NameIndex &&
                        file.Flags == CliMetadataFileAttributes.ContainsMetadata)
                    {
                        toCheck = file;
                        break;
                    }
                }
                if (toCheck == null)
                    throw new BadImageFormatException("There is no file entry in the metadata for the module.");
                Tuple<PEImage, CliMetadataRoot, string> valid = null;

                foreach (var path in (from dirInfo in this.RuntimeEnvironment.ResolutionPaths
                                      select dirInfo.FullName).Concat<string>(new string[] { Path.GetDirectoryName(relativeAssembly.Location) }).Distinct().ToArray())
                {
                    var currentFilename = string.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, toCheck.Name);
                    if (!File.Exists(currentFilename))
                        continue;
                    if (this.loadedModules.ContainsKey(path))
                    {
                        CliModule result;
                        if (this.moduleAssemblyIdentities.TryGetValue(relativeAssembly, currentFilename, out result))
                            return result.Metadata;
                    }
                    var current = CliCommon.CheckFilename(currentFilename, false);
                    if (current == null)
                        continue;
                    else
                    {
                        valid = Tuple.Create(current.Item3, current.Item4, current.Item6);
                        break;
                    }
                }
                if (valid == null)
                    throw new FileNotFoundException("The file associated to the module to load cannot be found.", toCheck.Name);
                if (valid.Item2.TableStream.ModuleTable == null ||
                    valid.Item2.TableStream.ModuleTable.Count == 0)
                    throw new BadImageFormatException("No module module table present in the metadata.");
                this.loadedModules.Add(valid.Item3, valid);
                return valid.Item2.TableStream.ModuleTable[1];

            }
            throw new NotImplementedException();
        }


        private CliAssembly GetRelativeAssembly(CliMetadataRoot root)
        {
            if (root != null)
                foreach (var assembly in this.loadedAssemblies.Values)
                    if (assembly.MetadataRoot == root)
                        return assembly;
                    else
                        foreach (var module in assembly.Modules.Values.Skip(1))
                        {
                            var cliModule = module as ICliModule;
                            if (cliModule == null)
                                continue;
                            if (cliModule.Metadata.MetadataRoot == root)
                                return assembly;
                        }
            return null;
        }

        //#endregion

        //#region ITypeIdentityManager<string> Members

        public IType ObtainTypeReference(string typeIdentity)
        {
            throw new NotImplementedException();
        }

        //#endregion

        #region ICliManager Members


        public IType ObtainTypeReference(PrimitiveType typeIdentity, ICliAssembly relativeSource)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITypeIdentityManager<ITypeUniqueIdentifier> Members

        public IType ObtainTypeReference(ITypeUniqueIdentifier typeIdentity)
        {
            throw new NotImplementedException();
        }

        #endregion
        //#region ITypeIdentityManager<CliMetadataTypeDefinitionTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            if (CliCommon.IsSpecialModule(typeIdentity))
                return null;
            IType result;
            lock (this.typeCache)
            {
                ITypeParent parent = null;
                if (!typeCache.TryGetValue(typeIdentity, out result))
                {
                    if (CliCommon.IsBaseObject(this, typeIdentity))
                        result = new M_T<IGeneralGenericTypeUniqueIdentifier>(TypeKind.Class, typeIdentity);
                    else if ((typeIdentity.TypeAttributes & TypeAttributes.Interface) == TypeAttributes.Interface &&
                     (typeIdentity.TypeAttributes & TypeAttributes.Sealed) != TypeAttributes.Sealed)
                        result = new M_T<IGeneralGenericTypeUniqueIdentifier>(TypeKind.Interface, typeIdentity);
                    else if (CliCommon.IsBaseObject(this, typeIdentity.Extends))
                        result = new M_T<IGeneralGenericTypeUniqueIdentifier>(TypeKind.Class, typeIdentity);
                    else if (CliCommon.IsEnum(this, typeIdentity))
                        result = new M_T<IGeneralTypeUniqueIdentifier>(TypeKind.Enumerator, typeIdentity);
                    else if (CliCommon.IsValueType(this, typeIdentity))
                        result = new M_T<IGeneralGenericTypeUniqueIdentifier>(TypeKind.Struct, typeIdentity);
                    else if (CliCommon.IsDelegate(this, typeIdentity))
                        result = new M_T<IGeneralGenericTypeUniqueIdentifier>(TypeKind.Delegate, typeIdentity);
                    else
                        result = new M_T<IGeneralGenericTypeUniqueIdentifier>(TypeKind.Class, typeIdentity);
                    this.typeCache.Add(typeIdentity, result);
                }
            }
            return result;
        }

        private ICliMetadataTypeDefinitionTableRow ResolveScope(ITypeDefOrRefRow typeIdentity, Func<_ICliManager, ICliMetadataTypeDefinitionTableRow, bool> selectionPredicate = null, bool typeSpec = false)
        {
            return CliCommon.ResolveScope(typeIdentity, this, selectionPredicate, typeSpec);
        }

        private ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeSignature typeIdentity, Func<ICliMetadataTypeDefinitionTableRow, bool> selectionPredicate, bool typeSpec)
        {
            throw new NotImplementedException();
        }
        //#endregion

        //#region ITypeIdentityManager<CliMetadataTypeRefTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeRefTableRow typeIdentity)
        {
            switch (typeIdentity.ResolutionScope)
            {
                case CliMetadataResolutionScopeTag.Module:
                    {
                        /* *
                         * Shouldn't appear in compliant compilers which
                         * compress the metadata into the smallest form.
                         * *
                         * This should support compilers that are not CLS
                         * compliant.
                         * */
                        var assembly = this.GetRelativeAssembly(typeIdentity.MetadataRoot);
                        if (assembly == null)
                        {
                            var identifier = CliCommon.GetAssemblyUniqueIdentifier(new Tuple<PEImage, CliMetadataRoot>(typeIdentity.MetadataRoot.SourceImage, typeIdentity.MetadataRoot));
                            if (identifier == null)
                            {
                                /* *
                                 * Must be from a module not loaded by this manager.
                                 * */
                                var typeTable = typeIdentity.MetadataRoot.TableStream.TypeDefinitionTable;
                                if (typeTable == null)
                                    throw new TypeLoadException("Type reference not resolveable.");
                                typeTable.Read();
                                foreach (var typeDef in typeTable)
                                    if (typeDef.NameIndex == typeIdentity.NameIndex &&
                                        typeDef.NamespaceIndex == typeIdentity.NamespaceIndex)
                                        return this.ObtainTypeReference(typeDef);
                                /* *
                                 * First pass failed, since it's already not a compressed metadata
                                 * stream, it's likely they didn't condense the names either.
                                 * *
                                 * Start off by caching the name/namespace, to reduce the number of
                                 * string heap fetches.
                                 * */
                                string tidN = typeIdentity.Name,
                                    tidNS = typeIdentity.Namespace;
                                foreach (var typeDef in typeTable)
                                    if (typeDef.Name == tidN &&
                                        typeDef.Namespace == tidNS)
                                        return this.ObtainTypeReference(typeDef);
                                throw new TypeLoadException("Type reference not resolveable.");
                            }
                            else
                            {
                                CliAssembly identitySource;
                                this.loadedAssemblies.Add(identifier.Item2, identitySource = new CliAssembly(identifier.Item3.MetadataRoot.SourceImage.Filename, this, identifier.Item3, identifier.Item2, identifier.Item1));
                                var localizedType = identitySource.FindType(typeIdentity.Namespace, typeIdentity.Name);
                                if (localizedType == null)
                                    throw new TypeLoadException(string.Format("Cannot find type \"{0}.{1}, {2}\"", typeIdentity.Namespace, typeIdentity.Name, identitySource.UniqueIdentifier));
                                return this.ObtainTypeReference(localizedType);
                            }
                        }
                        break;
                    }
                case CliMetadataResolutionScopeTag.ModuleReference:
                    {
                        var assembly = this.GetRelativeAssembly(typeIdentity.MetadataRoot);
                        if (assembly == null)
                        {
                            var identifier = CliCommon.GetAssemblyUniqueIdentifier(new Tuple<PEImage, CliMetadataRoot>(typeIdentity.MetadataRoot.SourceImage, typeIdentity.MetadataRoot));
                            if (identifier == null)
                            {
                                var loadedModule = this.LoadModule((ICliMetadataModuleReferenceTableRow) typeIdentity.Source);
                                if (loadedModule == null)
                                    throw new TypeLoadException(string.Format("Module {0} containing type {1} could not be loaded.", typeIdentity.Source.Name, typeIdentity.Name));

                                var typeTable = typeIdentity.MetadataRoot.TableStream.TypeDefinitionTable;
                                if (typeTable == null)
                                    throw new TypeLoadException("Type reference not resolveable.");
                                typeTable.Read();
                                /* *
                                 * Name index quick check doesn't work here, they're from
                                 * different string heaps.
                                 * */
                                string tidN = typeIdentity.Name,
                                       tidNS = typeIdentity.Namespace;
                                foreach (var typeDef in typeTable)
                                    if (typeDef.Name == tidN &&
                                        typeDef.Namespace == tidNS)
                                        return this.ObtainTypeReference(typeDef);
                                throw new TypeLoadException("Type reference not resolveable.");
                            }
                        }
                        {
                            var typeDef = assembly.FindType(typeIdentity.Namespace, typeIdentity.Name, typeIdentity.Source.Name);
                            return this.ObtainTypeReference(typeDef);
                        }
                    }
                case CliMetadataResolutionScopeTag.AssemblyReference:
                    {
                        var assemblyRef = typeIdentity.Source as ICliMetadataAssemblyRefTableRow;
                        if (assemblyRef == null)
                            throw new TypeLoadException("Assembly reference for type identity could not be resolved.");
                        var assembly = this.ObtainAssemblyReference(assemblyRef);
                        var typeDef = assembly.FindType(typeIdentity.Namespace, typeIdentity.Name);
                        return this.ObtainTypeReference(typeDef);
                    }
                case CliMetadataResolutionScopeTag.TypeReference:
                    {
                        var declaringTypeRef = this.ObtainTypeReference((ICliMetadataTypeRefTableRow) typeIdentity.Source);
                        if (declaringTypeRef is ICliTypeParent)
                        {
                            var dtp = declaringTypeRef as ICliTypeParent;
                            var typeDef = dtp.FindType(typeIdentity.Namespace, typeIdentity.Name);
                            return this.ObtainTypeReference(typeDef);
                        }
                        else
                            throw new TypeLoadException("Resulted type exists as a child of a type which should not contain nested types.");
                    }
            }
            throw new TypeLoadException("Invalid resolution scope.");
        }

        //#endregion

        //#region IAssemblyIdentityManager<IAssemblyUniqueIdentifier,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(IAssemblyUniqueIdentifier assemblyIdentity)
        {
            CliAssembly result;
            string[] extensions = new string[] { "exe", "dll" };
            bool gacAssembly = false;
            if (!this.loadedAssemblies.TryGetValue(assemblyIdentity, out result))
            {
                var baseName = assemblyIdentity.Name;
                Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
                /* *
                 * Resolution method:
                 * Runtime folder, GAC Cache
                 * */
                if (runtimeEnvironment.UseGlobalAccessCache)
                    foreach (var path in runtimeEnvironment.GetGacLocationsFor(assemblyIdentity))
                    {
                        string extension = "exe";
                    checkAgain:
                        var check = CliCommon.CheckFilename(path.FullName, baseName, extension);
                        if (check != null && check.Item1.Equals(assemblyIdentity))
                        {
                            validResult = check;
                            gacAssembly = true;
                            break;
                        }
                        if (validResult == null && extension == "exe")
                        {
                            extension = "dll";
                            goto checkAgain;
                        }
                    }
                if (validResult == null)
                {
                    foreach (var path in runtimeEnvironment.ResolutionPaths)
                    {
                        string extension = "exe";
                    checkAgain:
                        var check = CliCommon.CheckFilename(path.FullName, baseName, extension);
                        if (check != null && check.Item1.Equals(assemblyIdentity))
                        {
                            validResult = check;
                            break;
                        }
                        if (validResult == null && extension == "exe")
                        {
                            extension = "dll";
                            goto checkAgain;
                        }
                    }
                }
                if (validResult != null)
                {
                    this.loadedAssemblies.Add(validResult.Item1, result = new CliAssembly(validResult.Item6, this, validResult.Item5, validResult.Item1, validResult.Item2));
                    if (gacAssembly)
                        result.IsFromGlobalAssemblyCache = true;
                    //result.InitializeCommon();
                    this.fileIdentifiers.Add(validResult.Item6, validResult.Item1);
                    return result;
                }
                else
                    throw new FileNotFoundException(string.Format("Assembly {0} not found.", assemblyIdentity));
            }
            else
                return result;
            return this.loadedAssemblies[assemblyIdentity];
        }

        //#endregion


        //#region IAssemblyIdentityManager<string,ICompiledAssembly> Members


        /// <summary>
        /// Obtains a <see cref="ICliAssembly"/> reference by
        /// the filename.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> value
        /// which denotes the location of the assembly image.</param>
        /// <returns>A <see cref="ICliAssembly"/>
        /// which denotes the assembly in question.</returns>
        /// <exception cref="System.IO.FileNotFoundException">thrown when 
        /// <paramref name="filename"/> was not found.</exception>
        public ICliAssembly ObtainAssemblyReference(string filename)
        {
            filename = CliCommon.MinimizeFilename(filename);
            IAssemblyUniqueIdentifier uniqueIdentifier;
            if (!this.fileIdentifiers.TryGetValue(filename, out uniqueIdentifier))
            {
                if (File.Exists(filename))
                {
                    var peAndMetadata = CliCommon.LoadAssemblyMetadata(filename);
                    var peImage = peAndMetadata.Item1;
                    var metadataRoot = peAndMetadata.Item2;
                    using (peImage)
                    {
                        var imageKind = peImage.OptionalHeader.ImageKind;
                        bool supportedOnPlatform = false;
                        if (this.runtimeEnvironment.Platform == FrameworkPlatform.x64Platform)
                        {
                            /* *
                             * x64 is backwards compatible with x86, therefore
                             * if it doesn't require a 32 bit mode, it should be valid, 
                             * since it targets the Any runtimeEnvironment, but the PE Header
                             * is PE32.
                             * */
                            CliHeader header;
                            if (imageKind == PEImageKind.x86Image &&
                                ((((header = metadataRoot.Header).Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly) &&
                                  ((header.Flags & CliRuntimeFlags.Requires32BitProcess) != CliRuntimeFlags.Requires32BitProcess)))
                                supportedOnPlatform = true;
                            /* *
                             * In this case regardless of whether they have IL only 
                             * */
                            else if (imageKind == PEImageKind.x64Image)
                                supportedOnPlatform = true;
                        }
                        else if (this.runtimeEnvironment.Platform == FrameworkPlatform.x86Platform)
                        {
                            /* *
                             * ... however, the reverse is not true.
                             * */
                            if (imageKind == PEImageKind.x64Image)
                                supportedOnPlatform = false;
                            else if (imageKind == PEImageKind.x86Image)
                                supportedOnPlatform = true;
                        }
                        else if (this.runtimeEnvironment.Platform == FrameworkPlatform.AnyPlatform)
                        {
                            CliHeader header = metadataRoot.Header;
                            if (imageKind == PEImageKind.x64Image &&
                                ((header.Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly))
                                supportedOnPlatform = true;
                            else if (imageKind == PEImageKind.x86Image &&
                                ((header.Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly) &&
                                (header.Flags & CliRuntimeFlags.Requires32BitProcess) != CliRuntimeFlags.Requires32BitProcess)
                                supportedOnPlatform = true;
                        }
                        if (supportedOnPlatform)
                        {
                            if (metadataRoot.TableStream.ContainsKey(CliMetadataTableKinds.Assembly))
                            {
                                var pubKeyId = CliCommon.GetAssemblyUniqueIdentifier(metadataRoot.TableStream.AssemblyTable[1]);
                                var firstAssemblyRow = pubKeyId.Item3;
                                IAssemblyUniqueIdentifier assemblyUniqueIdentifier = pubKeyId.Item2;
                                if (assemblyUniqueIdentifier == null)
                                    this.ThrowNoMetadataFound();
                                IStrongNamePublicKeyInfo publicKeyInfo = pubKeyId.Item1;
                                CliAssembly result;
                                if (this.loadedAssemblies.ContainsKey(assemblyUniqueIdentifier))
                                {
                                    peImage.Dispose();
                                    metadataRoot.Dispose();
                                    return this.loadedAssemblies[assemblyUniqueIdentifier];
                                }
                                loadedAssemblies.Add(assemblyUniqueIdentifier, result = new CliAssembly(filename, this, firstAssemblyRow, assemblyUniqueIdentifier, publicKeyInfo));
                                //result.InitializeCommon();
                                this.fileIdentifiers.Add(filename, assemblyUniqueIdentifier);
                                return result;
                            }
                        }
                        else
                            throw new BadImageFormatException(string.Format("Expecting {0} image, but got {1}.", this.runtimeEnvironment.Platform, imageKind));
                    }
                }
                else
                    throw new FileNotFoundException("AssemblyIdentity not found.", filename);
            }
            return this.loadedAssemblies[this.fileIdentifiers[filename]];
        }

        private void ThrowNoMetadataFound()
        {
            throw new BadImageFormatException("No Assembly metadata entry found.");
        }

        //#endregion

        //#region IAssemblyIdentityManager<CliMetadataAssemblyTableRow,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(ICliMetadataAssemblyTableRow assemblyIdentity)
        {
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            CliAssembly result;
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
            {
                result = new CliAssembly(assemblyIdentity.MetadataRoot.SourceImage.Filename, this, assemblyIdentity, identity.Item2, identity.Item1);
                //result.InitializeCommon();
                this.loadedAssemblies.Add(identity.Item2, result);
            }
            return this.loadedAssemblies[identity.Item2];
        }

        //#endregion

        //#region IAssemblyIdentityManager<CliMetadataAssemblyRefTableRow,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(ICliMetadataAssemblyRefTableRow assemblyIdentity)
        {
            CliAssembly result;
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
            {
                var relativeAssembly = GetRelativeAssembly(assemblyIdentity.MetadataRoot);
                string relativePath = Path.GetDirectoryName(relativeAssembly.Location);
                var baseName = identity.Item2.Name;
                Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
                var check = CliCommon.CheckFilename(relativePath, baseName, "exe");
                if (check != null && check.Item1.Equals(assemblyIdentity))
                    validResult = check;
                else
                {
                    check = CliCommon.CheckFilename(relativePath, baseName, "dll");
                    if (check != null && check.Item1.Equals(assemblyIdentity))
                        validResult = check;
                }
                if (validResult != null)
                {
                    this.loadedAssemblies.Add(validResult.Item1, result = new CliAssembly(validResult.Item6, this, validResult.Item5, validResult.Item1, validResult.Item2));
                    //result.InitializeCommon();
                    this.fileIdentifiers.Add(validResult.Item6, validResult.Item1);
                }
                else
                    return this.ObtainAssemblyReference(identity.Item2);
                return this.loadedAssemblies[identity.Item2];
            }
            return result;
        }

        //#endregion

        //#region ITypeIdentityManager Members

        IStandardRuntimeEnvironmentInfo ITypeIdentityManager.RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }

        //#endregion

        private class M_T<T> : TypeBase<T>
        where T :
            ITypeUniqueIdentifier
        {
            private ICliMetadataTypeDefinitionTableRow metadata;
            internal M_T(TypeKind kind, ICliMetadataTypeDefinitionTableRow metadata)
            {
                this.TypeKind = kind;
                this.metadata = metadata;
            }
            protected override IType OnGetDeclaringType()
            {
                throw new NotImplementedException();
            }

            private TypeKind TypeKind { get; set; }

            protected override TypeKind TypeImpl
            {
                get { return TypeKind; }
            }

            protected override bool CanCacheImplementsList
            {
                get { throw new NotImplementedException(); }
            }

            protected override ILockedTypeCollection OnGetImplementedInterfaces()
            {
                throw new NotImplementedException();
            }

            protected override IFullMemberDictionary OnGetMembers()
            {
                throw new NotImplementedException();
            }

            protected override INamespaceDeclaration OnGetNamespace()
            {
                throw new NotImplementedException();
            }

            protected override AccessLevelModifiers OnGetAccessLevel()
            {
                throw new NotImplementedException();
            }

            protected override IAssembly OnGetAssembly()
            {
                throw new NotImplementedException();
            }

            protected override IArrayType OnMakeArray(int rank)
            {
                throw new NotImplementedException();
            }

            protected override IArrayType OnMakeArray(params int[] lowerBounds)
            {
                throw new NotImplementedException();
            }

            protected override IType OnMakeByReference()
            {
                throw new NotImplementedException();
            }

            protected override IType OnMakePointer()
            {
                throw new NotImplementedException();
            }

            protected override IType OnMakeNullable()
            {
                throw new NotImplementedException();
            }

            public override bool IsGenericConstruct
            {
                get { throw new NotImplementedException(); }
            }

            protected override bool IsSubclassOfImpl(IType other)
            {
                throw new NotImplementedException();
            }

            protected override string OnGetNamespaceName()
            {
                throw new NotImplementedException();
            }

            protected override IType BaseTypeImpl
            {
                get { throw new NotImplementedException(); }
            }

            protected override T OnGetUniqueIdentifier()
            {
                throw new NotImplementedException();
            }

            protected override IMetadataCollection InitializeCustomAttributes()
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
            {
                get { throw new NotImplementedException(); }
            }

            protected override ITypeIdentityManager OnGetManager()
            {
                throw new NotImplementedException();
            }

            protected override string OnGetName()
            {
                throw new NotImplementedException();
            }

            public override string FullName
            {
                get
                {
                    if (this.metadata.NamespaceIndex == 0)
                        return string.Format("{0}", this.metadata.Name);
                    else
                        return string.Format("{0}.{1}", this.metadata.Namespace, this.metadata.Name);
                }
            }
        }


        public ICliMetadataTypeDefinitionTableRow ResolveScope(ITypeDefOrRefRow scope)
        {
            return ResolveScope(scope, null, true);
        }
        //#region _ICliManager Members


        IDictionary<ICliMetadataTypeDefinitionTableRow, IType> _ICliManager.TypeCache
        {
            get { return this.typeCache; }
        }

        IDictionary<ICliMetadataTypeDefinitionTableRow, BaseKindCacheType> _ICliManager.BaseTypeKinds
        {
            get { return this.baseTypeKinds; }
        }

        IDictionary<ITypeDefOrRefRow, BaseKindCacheType> _ICliManager.RefBaseTypeKinds
        {
            get { return this.refBaseTypeKinds; }
        }

        ICliAssembly _ICliManager.GetRelativeAssembly(CliMetadataRoot root)
        {
            return GetRelativeAssembly(root);
        }
        //#endregion
    }
}
