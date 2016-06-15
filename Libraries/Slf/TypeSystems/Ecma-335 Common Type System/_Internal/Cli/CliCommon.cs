using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules;
using AllenCopeland.Abstraction.Utilities.Security;
using AllenCopeland.Abstraction._Internal.Utilities.Security;
#if x86
using SlotType = System.UInt32;
#elif x64
using SlotType = System.UInt64;
#endif

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{

    static partial class CliCommon
    {
        internal const string VersionString_1_0_3705       = "v1.0.3705";
        internal const string VersionString_1_0_Alternate  = "v1.x86ret";
        internal const string VersionString_1_0_Alternate2 = "retail";
        internal const string VersionString_1_0_Alternate3 = "COMPLUS";
        internal const string VersionString_1_1_4322       = "v1.1.4322";
        internal const string VersionString_2_0_50727      = "v2.0.50727";
        internal const string VersionString_3_0            = "v3.0";
        internal const string VersionString_3_5            = "v3.5";
        internal const string VersionString_4_0_30319      = "v4.0.30319";
        internal const string VersionString_4_5            = "v4.5";
        internal const string VersionString_4_6            = "v4.6";
        
        internal const string ConstructorName              = ".ctor";
        internal const string ConstructorStaticName        = ".cctor";

        internal static Tuple<PEImage, CliMetadataFixedRoot> LoadAssemblyMetadata(string filename)
        {
            FileStream peStream;
            var image = PEImage.LoadImage(filename, out peStream, true);
            return LoadAssemblyMetadata(peStream, image);
        }

        public static IGeneralTypeUniqueIdentifier ObtainTypeIdentifier(ICliMetadataTypeDefinitionTableRow typeDefinition, IAssemblyUniqueIdentifier aId)
        {
            bool hasTypeParametrs = typeDefinition.TypeParameters != null;
            int typeParamCount = 0;
            if (hasTypeParametrs)
                typeParamCount = typeDefinition.TypeParameters.Count;
            int uniqueTCount = (typeDefinition.DeclaringType != null) ? (hasTypeParametrs ? typeParamCount - typeDefinition.DeclaringType.TypeParameters.Count : 0) : hasTypeParametrs ? typeDefinition.TypeParameters.Count : 0;
            if (typeDefinition.DeclaringType != null)
                if (typeDefinition.NamespaceIndex == 0)
                    return ObtainTypeIdentifier(typeDefinition.DeclaringType, aId).GetNestedIdentifier(typeDefinition.Name, uniqueTCount);
                else
                    return ObtainTypeIdentifier(typeDefinition.DeclaringType, aId).GetNestedIdentifier(typeDefinition.Name, uniqueTCount, TypeSystemIdentifiers.GetDeclarationIdentifier(typeDefinition.Namespace));
            else
                if (typeDefinition.NamespaceIndex == 0)
                    return aId.GetTypeIdentifier((string)null, typeDefinition.Name, uniqueTCount);
                else
                    return aId.GetTypeIdentifier(TypeSystemIdentifiers.GetDeclarationIdentifier(typeDefinition.Namespace), typeDefinition.Name, uniqueTCount);
        }

        public static IGeneralTypeUniqueIdentifier ObtainTypeIdentifier(ICliMetadataTypeRefTableRow typeReference)
        {
            throw new NotImplementedException();
        }

        unsafe private static Tuple<PEImage, CliMetadataFixedRoot> LoadAssemblyMetadata(FileStream peStream, PEImage image)
        {
            /* *
             * Resolve the virtual address of the CliHeader, which yields
             * the offset in the section's data, and the section itself.
             * */
            var headerSectionScan = image.ResolveRelativeVirtualAddress(image.ExtendedHeader.CliHeader.RelativeVirtualAddress);
            if (!headerSectionScan.Resolved)
                throw new BadImageFormatException("No Section for the CLI header found.");
            var headerSection = headerSectionScan.Section;
            if ((headerSection.SectionData.Length) < Marshal.SizeOf(typeof(CliHeader)))
                throw new BadImageFormatException("CLIHeader of invalid size.");
            CliHeader header;
            /* Copy the header */
            byte[] headerBytes = new byte[Marshal.SizeOf(typeof(CliHeader))];
            headerSection.SectionData.Seek(headerSectionScan.Offset, SeekOrigin.Begin);
            headerSection.SectionData.Read(headerBytes, 0, headerBytes.Length);
            fixed (byte* sectionData = headerBytes)
                header = *(CliHeader*)sectionData;
            var metadataSectionScan = image.ResolveRelativeVirtualAddress(header.Metadata.RelativeVirtualAddress);
            if (!metadataSectionScan.Resolved)
                throw new BadImageFormatException("Metadata Root not found within image.");
            var metadataSection = metadataSectionScan.Section;
            metadataSection.SectionDataReader.BaseStream.Seek(metadataSectionScan.Offset, SeekOrigin.Begin);
            var metadataRoot = new CliMetadataFixedRoot();
            metadataRoot.Read(header, peStream, headerSection.SectionDataReader, header.Metadata.RelativeVirtualAddress, image);
            return new Tuple<PEImage, CliMetadataFixedRoot>(image, metadataRoot);
        }

        internal static string MinimizeFilename(string filename)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                default:
                    return filename;
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                    return filename.ToLower();
            }
        }

        public static IEnumerable<TypeModification> Resolve(this IEnumerable<ICliMetadataCustomModifierSignature> modifiers, _ICliManager manager, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly = null)
        {
            return from modifier in modifiers
                   select new TypeModification(manager.ObtainTypeReference(modifier.ModifierType, activeType, activeMethod, activeAssembly), modifier.Required);
        }

        internal static Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataFixedRoot, ICliMetadataAssemblyTableRow, string> CheckFilename(string directory, string filename, string extension)
        {
            string resultedFilename = MinimizeFilename(string.Format("{0}.{1}", Path.Combine(directory, filename), extension));
            if (!File.Exists(resultedFilename))
                return null;
            return CheckFilename(resultedFilename);
        }

        internal unsafe static Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataFixedRoot, ICliMetadataAssemblyTableRow, string> CheckFilename(string resultedFilename, bool loadAssemblyInfo = true)
        {
            FileStream peStream = null;
            PEImage image = null;
            CliMetadataFixedRoot metadataRoot = null;
            try
            {
                image = PEImage.LoadImage(resultedFilename, out peStream, true);
                if (image.ExtendedHeader.CliHeader.RelativeVirtualAddress == 0)
                    return null;
                var headerSectionScan = image.ResolveRelativeVirtualAddress(image.ExtendedHeader.CliHeader.RelativeVirtualAddress);
                if (!headerSectionScan.Resolved)
                    return null;

                var headerSection = headerSectionScan.Section;
                if ((headerSection.SectionData.Length) < Marshal.SizeOf(typeof(CliHeader)))
                    return null;
                CliHeader header;
                /* Copy the header */
                byte[] headerBytes = new byte[Marshal.SizeOf(typeof(CliHeader))];
                headerSection.SectionData.Seek(headerSectionScan.Offset, SeekOrigin.Begin);
                headerSection.SectionData.Read(headerBytes, 0, headerBytes.Length);
                fixed (byte* sectionData = headerBytes)
                    header = *(CliHeader*)sectionData;
                var metadataSectionScan = image.ResolveRelativeVirtualAddress(header.Metadata.RelativeVirtualAddress);
                if (!metadataSectionScan.Resolved)
                    return null;
                var metadataSection = metadataSectionScan.Section;
                metadataSection.SectionDataReader.BaseStream.Seek(metadataSectionScan.Offset, SeekOrigin.Begin);
                metadataRoot = new CliMetadataFixedRoot();
                metadataRoot.Read(header, peStream, headerSection.SectionDataReader, header.Metadata.RelativeVirtualAddress, image);
                if (loadAssemblyInfo)
                {
                    if (metadataRoot.TableStream.AssemblyTable == null)
                        return null;
                    var loadedUniqueId = GetAssemblyUniqueIdentifier(new Tuple<PEImage, ICliMetadataRoot>(image, metadataRoot));
                    if (loadedUniqueId == null)
                        return null;
                    return new Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataFixedRoot, ICliMetadataAssemblyTableRow, string>(loadedUniqueId.Item2, loadedUniqueId.Item1, image, metadataRoot, loadedUniqueId.Item3, resultedFilename);
                }
                else
                    return new Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataFixedRoot, ICliMetadataAssemblyTableRow, string>(null, null, image, metadataRoot, null, resultedFilename);
            }
            catch (BadImageFormatException)
            {
                if (metadataRoot != null)
                    metadataRoot.Dispose();
                if (image != null)
                    image.Dispose();
                if (peStream != null)
                {
                    peStream.Close();
                    peStream.Dispose();
                }
                return null;
            }
        }

        public static Tuple<IStrongNamePublicKeyInfo, IAssemblyUniqueIdentifier, ICliMetadataAssemblyTableRow> GetAssemblyUniqueIdentifier(Tuple<PEImage, ICliMetadataRoot> peAndMetadata)
        {
            var assemblyTable = (CliMetadataAssemblyTableReader)peAndMetadata.Item2.TableStream[CliMetadataTableKinds.Assembly];
            if (assemblyTable.Count == 0)
                return null;
            var firstAssemblyRow = assemblyTable.First();

            return GetAssemblyUniqueIdentifier(firstAssemblyRow);
        }

        internal static Tuple<IStrongNamePublicKeyInfo, IAssemblyUniqueIdentifier, ICliMetadataAssemblyTableRow> GetAssemblyUniqueIdentifier(ICliMetadataAssemblyTableRow firstAssemblyRow)
        {
            IStrongNamePublicKeyInfo publicKeyInfo;
            /* *
             * Obtain the StrongName info.
             * */
            if (firstAssemblyRow.PublicKey == null || firstAssemblyRow.PublicKey.Length == 0)
                publicKeyInfo = null;
            else
                publicKeyInfo = (IStrongNamePublicKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(firstAssemblyRow.PublicKey, false);
            var culture = firstAssemblyRow.Culture;
            ICultureIdentifier cultureId;
            if (culture == string.Empty)
                cultureId = CultureIdentifiers.None;
            else
            {
                culture = ValidCultureIdentifiers.TranscodeToCultureIdentifier(culture);
                cultureId = CultureIdentifiers.GetIdentifierByName(culture);
            }
            IAssemblyUniqueIdentifier assemblyUniqueIdentifier = TypeSystemIdentifiers.GetAssemblyIdentifier(firstAssemblyRow.Name, firstAssemblyRow.Version.ToVersion(), cultureId, publicKeyInfo == null ? null : publicKeyInfo.PublicToken.Token);
            return Tuple.Create(publicKeyInfo, assemblyUniqueIdentifier, firstAssemblyRow);
        }

        internal static Tuple<IStrongNamePublicKeyInfo, IAssemblyUniqueIdentifier> GetAssemblyUniqueIdentifier(ICliMetadataAssemblyRefTableRow entry)
        {
            IStrongNamePublicKeyInfo publicKeyInfo;
            if ((entry.Flags & CliMetadataAssemblyFlags.PublicKey) != CliMetadataAssemblyFlags.PublicKey)
                publicKeyInfo = null;
            else
                publicKeyInfo = (IStrongNamePublicKeyInfo)StrongNameKeyPairHelper.LoadStrongNameKeyData(entry.PublicKeyOrToken, false);
            var culture = entry.Culture;

            ICultureIdentifier cultureId;
            if (culture == string.Empty)
                cultureId = CultureIdentifiers.None;
            else
            {
                culture = ValidCultureIdentifiers.TranscodeToCultureIdentifier(culture);
                cultureId = CultureIdentifiers.GetIdentifierByName(culture);
            }
            byte[] publicKeyToken = null;
            if (publicKeyInfo == null && entry.PublicKeyOrTokenIndex != 0)
                publicKeyToken = entry.PublicKeyOrToken;
            IAssemblyUniqueIdentifier assemblyUniqueIdentifier = TypeSystemIdentifiers.GetAssemblyIdentifier(entry.Name, entry.Version.ToVersion(), cultureId, publicKeyInfo == null ? publicKeyToken : publicKeyInfo.PublicToken.Token);
            return new Tuple<IStrongNamePublicKeyInfo, IAssemblyUniqueIdentifier>(publicKeyInfo, assemblyUniqueIdentifier);
        }

        internal static ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeDefOrRefRow typeIdentity, _ICliManager manager, Func<_ICliManager, ICliMetadataTypeDefinitionTableRow, bool> selectionPredicate = null, bool typeSpec = false)
        {
            if (typeIdentity == null)
                return null;
            /* *
             * This method intentionally yields no exceptions on seek failures,
             * since it merely asks a question about the type.
             * */
            switch (typeIdentity.TypeDefOrRefEncoding)
            {
                case CliMetadataTypeDefOrRefTag.TypeDefinition:
                    {
                        var typeDef = (ICliMetadataTypeDefinitionTableRow)typeIdentity;
                        if ((selectionPredicate == null || selectionPredicate(manager, typeDef)))
                            return typeDef;
                    }
                    break;
                case CliMetadataTypeDefOrRefTag.TypeReference:
                    var typeRef = typeIdentity as ICliMetadataTypeRefTableRow;
                    switch (typeRef.ResolutionScope)
                    {
                        case CliMetadataResolutionScopeTag.Module:
                            {
                                var typeTable   = typeRef.MetadataRoot.TableStream.TypeDefinitionTable;
                                if (typeTable  == null)
                                    return null;
                                typeTable.Read();
                                string tidN     = typeRef.Name,
                                       tidNS    = typeRef.Namespace;
                                foreach (var typeDef in typeTable)
                                    if (typeDef.Name == tidN &&
                                        typeDef.Namespace == tidNS &&
                                        (selectionPredicate == null || selectionPredicate(manager, typeDef)))
                                        return typeDef;
                                break;
                            }
                        case CliMetadataResolutionScopeTag.ModuleReference:
                            {
                                var assembly = manager.GetRelativeAssembly(typeRef.MetadataRoot);
                                if (assembly == null)
                                {
                                    var identifier = CliCommon.GetAssemblyUniqueIdentifier(new Tuple<PEImage, ICliMetadataRoot>(typeRef.MetadataRoot.SourceImage, typeRef.MetadataRoot));
                                    if (identifier == null)
                                    {
                                        var loadedModule = manager.LoadModule((ICliMetadataModuleReferenceTableRow)typeRef.Source);
                                        if (loadedModule == null)
                                            return null;

                                        var typeTable = typeRef.MetadataRoot.TableStream.TypeDefinitionTable;
                                        if (typeTable == null)
                                            return null;
                                        typeTable.Read();
                                        /* *
                                         * Name index quick check doesn't work here, they're from
                                         * different string heaps.
                                         * */
                                        string tidN = typeRef.Name,
                                               tidNS = typeRef.Namespace;
                                        foreach (var typeDef in typeTable)
                                            if (typeDef.Name == tidN &&
                                                typeDef.Namespace == tidNS &&
                                                (selectionPredicate == null || selectionPredicate(manager, typeDef)))
                                                return typeDef;
                                        return null;
                                    }
                                }
                                {
                                    var typeDef = assembly.FindType(typeRef.Namespace, typeRef.Name, typeRef.Source.Name);
                                    if (typeDef != null)
                                        if ((selectionPredicate == null || selectionPredicate(manager, typeDef)))
                                            return typeDef;
                                    return null;
                                }
                            }
                        case CliMetadataResolutionScopeTag.TypeReference:
                            Stack<ICliMetadataTypeRefTableRow> inheritanceChain = new Stack<ICliMetadataTypeRefTableRow>();
                            ICliMetadataTypeRefTableRow current                 = typeRef;
                            while (current != null && current.ResolutionScope == CliMetadataResolutionScopeTag.TypeReference)
                            {
                                inheritanceChain.Push(current);
                                current = current.Source as ICliMetadataTypeRefTableRow;
                            }
                            ICliMetadataTypeDefinitionTableRow definition = null;
                            if (current != null)
                                definition = ResolveScope(current, manager, null, typeSpec);
                            if (definition != null)
                            {
                                while (inheritanceChain.Count > 0)
                                {
                                    var currentChild = inheritanceChain.Pop();
                                    bool found       = false;
                                    foreach (var nestedType in definition.NestedClasses)
                                        if (nestedType.Name == currentChild.Name)
                                        {
                                            definition = nestedType;
                                            found = true;
                                            break;
                                        }
                                    if (!found)
                                        break;
                                }
                                if (definition.Name == typeRef.Name && inheritanceChain.Count == 0 && (selectionPredicate == null || selectionPredicate(manager, definition)))
                                    return definition;
                            }
                            break;
                        case CliMetadataResolutionScopeTag.AssemblyReference:
                            {
                                var assemblyRef  = typeRef.Source as ICliMetadataAssemblyRefTableRow;
                                if (assemblyRef == null)
                                    return null;
                                var assembly     = manager.ObtainAssemblyReference(assemblyRef);
                                string name      = typeRef.Name;
                                string nameSpace = typeRef.Namespace;
                                return ObtainExportReference(manager, selectionPredicate, assembly, name, nameSpace);
                            }
                    }
                    break;
                case CliMetadataTypeDefOrRefTag.TypeSpecification:
                    if (typeSpec)
                    {
                        var spec = typeIdentity as ICliMetadataTypeSpecificationTableRow;
                        if (spec != null)
                        {
                            var specSignature = spec.Signature;
                            if (specSignature is ICliMetadataGenericInstanceTypeSignature)
                            {
                                var genericSig = (ICliMetadataGenericInstanceTypeSignature)specSignature;
                                return ResolveScope(genericSig.Target, manager, selectionPredicate, typeSpec);
                            }
                            else if (specSignature is ICliMetadataVectorArrayTypeSignature)
                            {
                                var vectorSig = (ICliMetadataVectorArrayTypeSignature)specSignature;
                                return ResolveScope(vectorSig.ElementType, manager, selectionPredicate, typeSpec);
                            }
                            else if (specSignature is ICliMetadataArrayTypeSignature)
                            {
                                var arraySig = (ICliMetadataArrayTypeSignature)specSignature;
                                return ResolveScope(arraySig.ElementType, manager, selectionPredicate, typeSpec);
                            }
                        }
                    }
                    break;
            }
            return null;
        }

        private static ICliMetadataTypeDefinitionTableRow ObtainExportReference(_ICliManager manager, Func<_ICliManager, ICliMetadataTypeDefinitionTableRow, bool> selectionPredicate, IAssembly assembly, string name, string nameSpace, bool checkInitial = true)
        {
            ICliMetadataExportedTypeTableRow originalExport = null;
            _ICliAssembly originalAssembly = null;
            bool first = true;
        recheckExport:
            if (assembly is _ICliAssembly)
            {
                var cliAssembly = (_ICliAssembly)(assembly);


                ICliMetadataTypeDefinitionTableRow typeDef = null;
                if (originalAssembly == null)
                    originalAssembly = cliAssembly;

                if ((first && checkInitial || !first))
                {
                    typeDef = cliAssembly.FindType(nameSpace, name);
                    if (typeDef != null && (selectionPredicate == null || selectionPredicate(manager, typeDef)))
                    {
                        lock (originalAssembly.ExportTableLookup)
                            if (originalExport != null && !originalAssembly.ExportTableLookup.ContainsKey(originalExport))
                                originalAssembly.ExportTableLookup._Add(originalExport, typeDef);
                        return typeDef;
                    }
                }
                else if (first)
                    first = false;
                if (typeDef == null && cliAssembly.MetadataRoot.TableStream.ExportedTypeTable != null)
                {
                    var exportedType = cliAssembly.MetadataRoot.TableStream.ExportedTypeTable.FirstOrDefault(k => (k.Name == name) && (k.Namespace == nameSpace));
                    if (originalExport == null)
                        originalExport = exportedType;
                    lock (originalAssembly.ExportTableLookup)
                        if (originalExport != null && originalAssembly.ExportTableLookup.ContainsKey(originalExport))
                        {
                            typeDef = originalAssembly.ExportTableLookup[originalExport];
                            return typeDef;
                        }
                    if (originalExport != null && exportedType != null && exportedType.ImplementationSource == CliMetadataImplementationTag.AssemblyReference)
                    {
                        ICliMetadataAssemblyRefTableRow implementationRef = (ICliMetadataAssemblyRefTableRow)(exportedType.Implementation);
                        assembly = manager.ObtainAssemblyReference(implementationRef);
                        goto recheckExport;
                    }
                }
            }
            return null;
        }

        internal static bool IsSpecialModule(ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            if (typeIdentity.Index == 1)
                return true;
            return false;
        }

        internal static bool IsBaseValueType(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
            if (manager.BaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
            {
                if (cachedResult == BaseKindCacheType.ValueTypeBase)
                    return true;
                return false;
            }
            bool result = typeIdentity.Namespace == "System" &&
                   typeIdentity.Name == "ValueType" &&
                 ((typeIdentity.TypeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract) &&
                   IsBaseObject(manager, typeIdentity.Extends);
            if (result)
                manager.BaseTypeKinds.Add(typeIdentity, BaseKindCacheType.ValueTypeBase);
            return result;
        }

        internal static bool IsBaseObject(_ICliManager manager, ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            if (typeIdentity == null)
                return false;
            BaseKindCacheType cachedResult;
            lock (manager.RefBaseTypeKinds)
            {
                if (manager.RefBaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
                {
                    if (cachedResult == BaseKindCacheType.ObjectBase)
                        return true;
                    else
                        return false;
                }
                var resolved = ResolveScope(typeIdentity, manager, IsBaseObject);
                if (resolved != null)
                {
                    manager.RefBaseTypeKinds.Add(typeIdentity, BaseKindCacheType.ObjectBase);
                    return true;
                }
            }
            return false;
        }

        internal static bool IsBaseValueType(_ICliManager manager, ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
            lock (manager.RefBaseTypeKinds)
            {
                if (manager.RefBaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
                {
                    if (cachedResult == BaseKindCacheType.ValueTypeBase)
                        return true;
                    else
                        return false;
                }
                var resolved = ResolveScope(typeIdentity, manager, IsBaseValueType);
                if (resolved != null)
                {
                    manager.RefBaseTypeKinds.Add(typeIdentity, BaseKindCacheType.ValueTypeBase);
                    return true;
                }
            }
            return false;
        }

        internal static bool IsBaseEnumType(_ICliManager manager, ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
            lock (manager.RefBaseTypeKinds)
            {
                if (manager.RefBaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
                {
                    if (cachedResult == BaseKindCacheType.EnumBase)
                        return true;
                    else
                        return false;
                }
                var resolved = ResolveScope(typeIdentity, manager, IsBaseEnumType);
                if (resolved != null)
                {
                    manager.RefBaseTypeKinds.Add(typeIdentity, BaseKindCacheType.EnumBase);
                    return true;
                }
            }
            return false;
        }

        internal static bool IsBaseDelegateType(_ICliManager manager, ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
            lock (manager.RefBaseTypeKinds)
            {
                if (manager.RefBaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
                {
                    if (cachedResult == BaseKindCacheType.DelegateBase)
                        return true;
                    else
                        return false;
                }
                var resolved = ResolveScope(typeIdentity, manager, IsBaseDelegateType);
                if (resolved != null)
                {
                    manager.RefBaseTypeKinds.Add(typeIdentity, BaseKindCacheType.DelegateBase);
                    return true;
                }
            }
            return false;
        }

        internal static bool IsBaseEnumType(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
            lock (manager.BaseTypeKinds)
            {
                if (manager.BaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
                {
                    if (cachedResult == BaseKindCacheType.EnumBase)
                        return true;
                    return false;
                }
                bool result = typeIdentity.Namespace == "System" &&
                    typeIdentity.Name == "Enum" &&
                    ((typeIdentity.TypeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract) &&
                    IsBaseValueType(manager, typeIdentity.Extends);
                if (result)
                    manager.BaseTypeKinds.Add(typeIdentity, BaseKindCacheType.EnumBase);
                return result;
            }
        }

        private static bool IsBaseObject(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
            lock (manager.BaseTypeKinds)
            {
                if (manager.BaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
                {
                    if (cachedResult == BaseKindCacheType.ObjectBase)
                        return true;
                    return false;
                }
                bool result = typeIdentity.Namespace == "System" &&
                       typeIdentity.Name == "Object" &&
                     ((typeIdentity.TypeAttributes & TypeAttributes.Sealed) != TypeAttributes.Sealed) &&
                     ((typeIdentity.TypeAttributes & TypeAttributes.Interface) != TypeAttributes.Interface) &&
                      (typeIdentity.ExtendsIndex == 0 &&
                       typeIdentity.ExtendsSource == CliMetadataTypeDefOrRefTag.TypeDefinition) &&
                       typeIdentity.DeclaringType == null;
                if (result)
                    manager.BaseTypeKinds.Add(typeIdentity, BaseKindCacheType.ObjectBase);
                return result;
            }
        }

        internal static bool IsValueType(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            return ((typeIdentity.TypeAttributes & TypeAttributes.Sealed) == TypeAttributes.Sealed) &&
                IsBaseValueType(manager, typeIdentity.Extends);
        }

        internal static bool IsEnum(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            if ((typeIdentity.TypeAttributes & TypeAttributes.Sealed) != TypeAttributes.Sealed || !IsBaseEnumType(manager, typeIdentity.Extends))
                return false;
            foreach (var field in typeIdentity.Fields)
                if ((field.FieldAttributes & (FieldAttributes.Literal | FieldAttributes.Static)) == (FieldAttributes.Literal | FieldAttributes.Static))
                {
                    /* *
                     * Checking for valid metadata, not CLS compliance, so checking
                     * to make sure the fields are all the same type and of the
                     * type of the enumerator, is unnecessary.
                     * */
                    var signature = field.FieldType;
                    if (signature.Type is ICliMetadataValueOrClassTypeSignature)
                    {
                        var cvType = signature.Type as ICliMetadataValueOrClassTypeSignature;
                        if (cvType.Target != typeIdentity)
                            return false;
                    }
                    else
                        return false;
                }
                else if (((field.FieldAttributes & FieldAttributes.RTSpecialName) == FieldAttributes.RTSpecialName))
                    continue;
                else
                    return false;

            return typeIdentity.Methods.Count == 0;
        }

        internal static bool IsDelegate(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            return IsSubclassOf(manager, typeIdentity, IsBaseDelegateType);
        }

        internal static bool IsBaseDelegateType(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
            lock (manager.BaseTypeKinds)
            {
                if (manager.BaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
                {
                    if (cachedResult == BaseKindCacheType.DelegateBase)
                        return true;
                    return false;
                }
                string typeName = typeIdentity.Name;
                bool result = false;
                if (((typeIdentity.TypeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract))
                {
                    if (typeName == "MulticastDelegate")
                        result = IsBaseDelegateType(manager, typeIdentity.Extends);
                    else if (typeName == "Delegate")
                        result = IsBaseObject(manager, typeIdentity.Extends);
                }
                //bool result = ((typeIdentity.TypeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract) && typeIdentity.Namespace == "System" && ((typeName = typeIdentity.Name) == "Delegate" || typeName == "MulticastDelegate") &&
                //       IsBaseObject(manager, typeIdentity.Extends);
                if (result)
                    manager.BaseTypeKinds.Add(typeIdentity, BaseKindCacheType.DelegateBase);
                return result;
            }
        }

        internal static bool IsSubclassOf(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity, Func<_ICliManager, ICliMetadataTypeDefinitionTableRow, bool> baseDecision)
        {
            ICliMetadataTypeDefinitionTableRow current = typeIdentity;
            while (current != null)
            {
                current = ResolveScope(current.Extends, manager, typeSpec: true);
                if (current != null &&
                    baseDecision(manager, current))
                    return true;
            }
            return false;
        }

        internal static ICliMetadataTypeDefinitionTableRow ResolveScope(ICliMetadataTypeSignature typeIdentity, _ICliManager manager, Func<_ICliManager, ICliMetadataTypeDefinitionTableRow, bool> selectionPredicate, bool typeSpec)
        {
            throw new NotImplementedException();
        }

        internal static IGeneralTypeUniqueIdentifier GetUniqueIdentifier(ICliMetadataTypeDefinitionTableRow typeMetadata, _ICliManager manager, _ICliAssembly assembly = null)
        {
            if (assembly == null)
                assembly = (_ICliAssembly)manager.GetRelativeAssembly(typeMetadata.MetadataRoot);
            if (CliCommon.IsEnum(manager, typeMetadata))
                return assembly.UniqueIdentifier.GetTypeIdentifier(typeMetadata.Namespace, typeMetadata.Name);
            else
            {
                var declaringTypeMetadata = typeMetadata.DeclaringType;
                IGeneralDeclarationUniqueIdentifier @namespace = null;
                if (declaringTypeMetadata == null && typeMetadata.NamespaceIndex != 0)
                    @namespace = assembly.GetNamespace(typeMetadata.Namespace).UniqueIdentifier;
                else if (typeMetadata.NamespaceIndex != 0)
                    @namespace = TypeSystemIdentifiers.GetDeclarationIdentifier(typeMetadata.Namespace);
                if (typeMetadata.MetadataRoot.TableStream.GenericParameterTable == null)
                    return assembly.UniqueIdentifier.GetTypeIdentifier(@namespace, typeMetadata.Name, 0);
                else if (declaringTypeMetadata != null)
                    return assembly.UniqueIdentifier.GetTypeIdentifier(@namespace, typeMetadata.Name, typeMetadata.TypeParameters.Count - declaringTypeMetadata.TypeParameters.Count);
                else
                    return assembly.UniqueIdentifier.GetTypeIdentifier(@namespace, typeMetadata.Name, typeMetadata.TypeParameters.Count);
            }
        }

        internal static IMetadatum GetMetadatum(_ICliManager manager, IType metadataType, IControlledCollection<ICliMetadataCustomAttributeTableRow> metadata)
        {
            throw new NotImplementedException();
        }

        internal static IMetadatum GetMetadatum(_ICliManager manager, ICliMetadataCustomAttributeTableRow metadata)
        {
            throw new NotImplementedException();
        }

        internal static bool IsDefined(this IControlledCollection<ICliMetadataCustomAttributeTableRow> customAttributes, IType targetType, _ICliManager manager)
        {
            throw new NotImplementedException();
        }

        public static TypeKind DetermineTypeKind(this ICliMetadataTypeDefinitionTableRow typeIdentity, _ICliManager manager)
        {
            if (IsSpecialModule(typeIdentity))
                return TypeKind.Other;

            TypeKind result;
            lock (manager.TypeKindCache)
            {
                if (!manager.TypeKindCache.TryGetValue(typeIdentity, out result))
                {
                    if (IsBaseObject(manager, typeIdentity))
                        result = TypeKind.Class;
                    else if ((typeIdentity.TypeAttributes & TypeAttributes.Interface) == TypeAttributes.Interface &&
                             (typeIdentity.TypeAttributes & TypeAttributes.Sealed) != TypeAttributes.Sealed)
                        result = TypeKind.Interface;
                    else if (IsBaseObject(manager, typeIdentity.Extends))
                        result = TypeKind.Class;
                    else if (IsEnum(manager, typeIdentity))
                        result = TypeKind.Enumeration;
                    else if (IsValueType(manager, typeIdentity))
                        result = TypeKind.Struct;
                    else if (IsDelegate(manager, typeIdentity))
                        result = TypeKind.Delegate;
                    else
                        result = TypeKind.Class;
                    manager.TypeKindCache.Add(typeIdentity, result);
                }
            }
            return result;
        }

        public static ICliMetadataTypeDefinitionTableRow FindTypeImplementation(_ICliManager identityManager, IAssembly assembly, IGeneralTypeUniqueIdentifier uniqueIdentifier, CliNamespaceKeyedTree topLevel)
        {
            if (uniqueIdentifier.Name.Contains('`'))
                return FindTypeImplementation(identityManager, assembly, uniqueIdentifier.Namespace.Name, uniqueIdentifier.Name, topLevel);
            else if (uniqueIdentifier is IGenericTypeUniqueIdentifier)
            {
                var genericUniqueId = uniqueIdentifier as IGenericTypeUniqueIdentifier;
                if (genericUniqueId.TypeParameters > 0)
                    return FindTypeImplementation(identityManager, assembly, uniqueIdentifier.Namespace.Name, string.Format("{0}`{1}", uniqueIdentifier.Name, ((IGenericTypeUniqueIdentifier)(uniqueIdentifier)).TypeParameters), topLevel);
            }
            return FindTypeImplementation(identityManager, assembly, uniqueIdentifier.Namespace.Name, uniqueIdentifier.Name, topLevel);
        }


        public static ICliMetadataTypeDefinitionTableRow FindTypeImplementation(_ICliManager identityManager, IAssembly assembly, string @namespace, string name, string moduleName, CliNamespaceKeyedTree topLevel, IModuleDictionary moduleDictionary)
        {
            string ns = @namespace;

            int lastIndex = 0;
            IModule module;
            if (!moduleDictionary.TryGetValue(TypeSystemIdentifiers.GetDeclarationIdentifier(moduleName), out module))
                return null;
            ICliModule cliModule = module as ICliModule;
            if (cliModule == null)
                goto checkExports;
        nextPart:
            int next = ns.IndexOf('.', lastIndex);
            if (next != -1)
            {
                string current = ns.Substring(lastIndex, next - lastIndex);
                uint currentHash = (uint)current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    goto checkExports;
                lastIndex = next + 1;
                goto nextPart;
            }
            else
            {
                string current = ns.Substring(lastIndex);
                uint currentHash = (uint)current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    goto checkExports;
            }
            if (topLevel._Types != null)
            {
                foreach (var nsType in topLevel._Types)
                    if (nsType.MetadataRoot == cliModule.Metadata.MetadataRoot && nsType.Name == name)
                        return nsType;
            }
        checkExports:
            return ObtainExportReference(identityManager, null, assembly, name, @namespace, false);
        }

        public static ICliMetadataTypeDefinitionTableRow FindTypeImplementation(_ICliManager identityManager, IAssembly assembly, string @namespace, string name, CliNamespaceKeyedTree topLevel)
        {
            string ns = @namespace;

            int lastIndex = 0;
        nextPart:
            if (ns == null)
                goto typeSearch;
            int next = ns.IndexOf('.', lastIndex);
            if (next != -1)
            {
                string current = ns.Substring(lastIndex, next - lastIndex);
                uint currentHash = (uint)current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    goto checkExports;
                lastIndex = next + 1;
                goto nextPart;
            }
            else
            {
                string current = ns.Substring(lastIndex);
                uint currentHash = (uint)current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    goto checkExports;
            }
        typeSearch:
            if (topLevel != null && topLevel._Types != null)
            {
                var currentTypes = topLevel._Types;
                foreach (var nsType in currentTypes)
                    if (nsType.Name == name)
                        return nsType;
            }
        checkExports:
            return ObtainExportReference(identityManager, null, assembly, name, @namespace, false);
        }

        public static IBinaryOperatorUniqueIdentifier GetBinaryOperatorUniqueIdentifier(ICliMetadataTypeDefinitionTableRow owner, ICliMetadataMethodDefinitionTableRow metadata)
        {
            CoercibleBinaryOperators op = (CoercibleBinaryOperators)(-1);
            switch (metadata.Name)
            {
                case BinaryOperatorNames.Addition:
                    op = CoercibleBinaryOperators.Add;
                    break;
                case BinaryOperatorNames.BitwiseAnd:
                    op = CoercibleBinaryOperators.BitwiseAnd;
                    break;
                case BinaryOperatorNames.BitwiseOr:
                    op = CoercibleBinaryOperators.BitwiseOr;
                    break;
                case BinaryOperatorNames.Division:
                    op = CoercibleBinaryOperators.Divide;
                    break;
                case BinaryOperatorNames.Equality:
                    op = CoercibleBinaryOperators.IsEqualTo;
                    break;
                case BinaryOperatorNames.ExclusiveOr:
                    op = CoercibleBinaryOperators.ExclusiveOr;
                    break;
                case BinaryOperatorNames.GreaterThan:
                    op = CoercibleBinaryOperators.GreaterThan;
                    break;
                case BinaryOperatorNames.GreaterThanOrEqual:
                    op = CoercibleBinaryOperators.GreaterThanOrEqualTo;
                    break;
                case BinaryOperatorNames.Inequality:
                    op = CoercibleBinaryOperators.IsNotEqualTo;
                    break;
                case BinaryOperatorNames.LeftShift:
                    op = CoercibleBinaryOperators.LeftShift;
                    break;
                case BinaryOperatorNames.LessThan:
                    op = CoercibleBinaryOperators.LessThan;
                    break;
                case BinaryOperatorNames.LessThanOrEqual:
                    op = CoercibleBinaryOperators.LessThanOrEqualTo;
                    break;
                case BinaryOperatorNames.Modulus:
                    op = CoercibleBinaryOperators.Modulus;
                    break;
                case BinaryOperatorNames.Multiply:
                    op = CoercibleBinaryOperators.Multiply;
                    break;
                case BinaryOperatorNames.RightShift:
                    op = CoercibleBinaryOperators.RightShift;
                    break;
                case BinaryOperatorNames.Subtraction:
                    op = CoercibleBinaryOperators.Subtract;
                    break;
                default:
                    throw new ArgumentException("Invalid method presented.", "metadata");
            }
            if (metadata.Signature.Parameters.Count != 2)
            {
                throw new ArgumentException("Invalid method presented.", "metadata");
            }
            var lParamType = metadata.Signature.Parameters[0].ParameterType;
            var rParamType = metadata.Signature.Parameters[1].ParameterType;

            throw new NotImplementedException();
        }

        public static CliFrameworkVersion GetFrameworkVersionFromString(string s)
        {
            switch (s)
            {
                case CliCommon.VersionString_1_0_Alternate:
                case CliCommon.VersionString_1_0_Alternate2:
                case CliCommon.VersionString_1_0_Alternate3:
                case CliCommon.VersionString_1_0_3705:
                    return CliFrameworkVersion.v1_0_3705;
                case CliCommon.VersionString_1_1_4322:
                    return CliFrameworkVersion.v1_1_4322;
                case CliCommon.VersionString_2_0_50727:
                    return CliFrameworkVersion.v2_0_50727;
                case CliCommon.VersionString_3_0:
                    return CliFrameworkVersion.v3_0;
                case CliCommon.VersionString_3_5:
                    return CliFrameworkVersion.v3_5;
                case CliCommon.VersionString_4_0_30319:
                    return CliFrameworkVersion.v4_0_30319;
                case CliCommon.VersionString_4_5:
                    return CliFrameworkVersion.v4_5;
                case CliCommon.VersionString_4_6:
                    return CliFrameworkVersion.v4_6;
                default:
                    throw new InvalidOperationException();
            }
        }

        public static string GetFrameworkStringFromVersion(CliFrameworkVersion version)
        {
            switch (version & (CliFrameworkVersion.VersionMask))
            {
                case CliFrameworkVersion.v1_1_4322:
                    return CliCommon.VersionString_1_1_4322;
                case CliFrameworkVersion.v2_0_50727:
                    return CliCommon.VersionString_2_0_50727;
                case CliFrameworkVersion.v3_0:
                    return CliCommon.VersionString_3_0;
                case CliFrameworkVersion.v3_5:
                    return CliCommon.VersionString_3_5;
                case CliFrameworkVersion.v4_0_30319:
                    return CliCommon.VersionString_4_0_30319;
                case CliFrameworkVersion.v4_5:
                    return CliCommon.VersionString_4_5;
                case CliFrameworkVersion.v4_6:
                    return CliCommon.VersionString_4_6;
            }
            return CliCommon.VersionString_1_0_3705;
        }

        public static RuntimeCoreType GetCoreType(this IType type) { return type.IdentityManager.ObtainCoreType(type); }

        internal static IEnumerable<ICliMetadataAssemblyRefTable> ObtainAssemblyRefTables(this CliAssembly owner)
        {
            return (from ICliModule m in owner.Modules.Values
                    let table = m.Metadata.MetadataRoot.TableStream.AssemblyRefTable
                    where table != null
                    select table);
        }

        internal static int GetDepth(this IGeneralTypeUniqueIdentifier identifier)
        {
            int depth = 0;
            var id = (IGeneralTypeUniqueIdentifier)identifier;
            while (id != null)
            {
                depth++;
                id = id.ParentIdentifier;
            }
            return depth;
        }

        public static InstanceFieldMemberAttributes GetFieldInstanceFlags(FieldAttributes fieldAttrs)
        {
            return (((fieldAttrs & FieldAttributes.Static) == FieldAttributes.Static) ? InstanceFieldMemberAttributes.Static : InstanceFieldMemberAttributes.None) |
                   (((fieldAttrs & FieldAttributes.InitOnly) == FieldAttributes.InitOnly) ? InstanceFieldMemberAttributes.ReadOnly : InstanceFieldMemberAttributes.None) |
                   (((fieldAttrs & FieldAttributes.Literal) == FieldAttributes.Literal) ? InstanceFieldMemberAttributes.Constant : InstanceFieldMemberAttributes.None);
        }

        public static AccessLevelModifiers GetFieldAccessModifiers(FieldAttributes fieldAttrs)
        {
            switch (fieldAttrs & FieldAttributes.FieldAccessMask)
            {
                case FieldAttributes.Assembly:
                    return AccessLevelModifiers.Internal;
                case FieldAttributes.FamANDAssem:
                    return AccessLevelModifiers.ProtectedAndInternal;
                case FieldAttributes.FamORAssem:
                    return AccessLevelModifiers.ProtectedOrInternal;
                case FieldAttributes.Family:
                    return AccessLevelModifiers.Protected;
                case FieldAttributes.Private:
                    return AccessLevelModifiers.Private;
                case FieldAttributes.Public:
                    return AccessLevelModifiers.Public;
                case FieldAttributes.PrivateScope:
                default:
                    return AccessLevelModifiers.PrivateScope;
            }
        }

        internal static AccessLevelModifiers GetMethodAccessLevel(MethodAttributes flags)
        {
            switch (flags & MethodAttributes.MemberAccessMask)
            {
                case MethodAttributes.Assembly:
                    return AccessLevelModifiers.Internal;
                case MethodAttributes.FamANDAssem:
                    return AccessLevelModifiers.ProtectedAndInternal;
                case MethodAttributes.FamORAssem:
                    return AccessLevelModifiers.ProtectedOrInternal;
                case MethodAttributes.Family:
                    return AccessLevelModifiers.Protected;
                case MethodAttributes.Private:
                    return AccessLevelModifiers.Private;
                case MethodAttributes.Public:
                    return AccessLevelModifiers.Public;
                case MethodAttributes.PrivateScope:
                default:
                    return AccessLevelModifiers.PrivateScope;
            }
        }

        internal static IType ObtainTypeReference(this ICliMetadataPropertySignature signature, _ICliManager manager, ICliType activeType)
        {
            var result = manager.ObtainTypeReference(signature.PropertyType, activeType, null, activeType == null ? null : activeType.Assembly);
            if (signature.CustomModifiers.Count > 0)
            {
                result = result.MakeModified((from t in signature.CustomModifiers
                                              select new TypeModification(manager.ObtainTypeReference(t.ModifierType, activeType, null), t.Required)).ToArray());
            }
            return result;
        }

        internal static void GetCustomAttributeData(this ICliMetadataCustomAttributeTableRow target)
        {

        }

        public static EnumerationBaseType GetEnumBaseType(this ICliMetadataTypeDefinitionTableRow target)
        {
            foreach (var field in target.Fields)
            {
                if ((field.FieldAttributes & FieldAttributes.Static) != FieldAttributes.Static)
                {
                    if (field.FieldType.Type.TypeSignatureKind == CliMetadataTypeSignatureKind.NativeType)
                    {
                        var nativeType = (ICliMetadataNativeTypeSignature)field.FieldType.Type;
                        switch (nativeType.TypeKind)
                        {
                            case CliMetadataNativeTypes.SByte:
                                return EnumerationBaseType.SByte;
                            case CliMetadataNativeTypes.Byte:
                                return EnumerationBaseType.Byte;
                            case CliMetadataNativeTypes.Int16:
                                return EnumerationBaseType.Int16;
                            case CliMetadataNativeTypes.UInt16:
                                return EnumerationBaseType.UInt16;
                            case CliMetadataNativeTypes.Int32:
                                return EnumerationBaseType.Int32;
                            case CliMetadataNativeTypes.UInt32:
                                return EnumerationBaseType.UInt32;
                            case CliMetadataNativeTypes.Int64:
                                return EnumerationBaseType.Int64;
                            case CliMetadataNativeTypes.UInt64:
                                return EnumerationBaseType.UInt64;
                            case CliMetadataNativeTypes.NativeInteger:
                                return EnumerationBaseType.NativeInteger;
                            case CliMetadataNativeTypes.NativeUnsignedInteger:
                                return EnumerationBaseType.NativeUnsignedInteger;
                        }
                    }
                }
            }
            throw new BadImageFormatException("Not a proper enum type.");
        }


        public static string UnescapeName(string assemblyName)
        {
            if (!assemblyName.Contains('\\'))
                return assemblyName;
            StringBuilder sb = new StringBuilder();
            bool lastEscaped = false;
            for (int i = 0; i < assemblyName.Length - 1; i++)
            {
                if (assemblyName[i] == '\\')
                {
                    sb.Append(assemblyName[++i]);
                    lastEscaped = true;
                }
                else
                {
                    sb.Append(assemblyName[i]);
                    lastEscaped = false;
                }
            }
            if (!lastEscaped)
                sb.Append(assemblyName[assemblyName.Length - 1]);
            return sb.ToString();
        }

        public static IType DecodeParsedType(this ITIQualifiedTypeNameRule typeName, ICliManager identityManager, IAssembly relAssem)
        {
            var relativeAssembly = relAssem as ICliAssembly;
            IType currentType;
            var typeId                                 = typeName.TypeIdentifier;
            var assemblyId                             = typeName.AssemblyIdentifier;
            IGeneralTypeUniqueIdentifier currentTypeId = null;
            if (typeName.AssemblyIdentifier == null)
                currentTypeId                          = TypeSystemIdentifiers.GetTypeIdentifier(UnescapeName(typeId.Namespace), UnescapeName(typeId.Names.First()));
            else
                currentTypeId                          = TypeSystemIdentifiers.GetAssemblyIdentifier(UnescapeName(assemblyId.Name), assemblyId.Version, assemblyId.CultureIdentifier, assemblyId.PublicKeyToken).GetTypeIdentifier(typeId.Namespace, UnescapeName(typeId.Names.First()));
            if (typeId.Names.Count() > 1)
                foreach (var name in typeId.Names.Skip(1))
                    currentTypeId = currentTypeId.GetNestedIdentifier(UnescapeName(name));
            currentType = identityManager.ObtainTypeReference(currentTypeId, relativeAssembly);
            if (typeId.HasTypeReplacements)
            {
                if (currentType is IGenericType)
                {
                    if (currentType.IsGenericConstruct)
                    {
                        var genericCurrent = (IGenericType)currentType;
                        currentType = genericCurrent.MakeGenericClosure((from replacement in typeId.TypeReplacements
                                                                         select DecodeParsedType(replacement.TypeIdentity, identityManager, relativeAssembly)).ToArray());
                    }
                    else
                        throw new ArgumentException(string.Format("Type {0} is not a generic type.", currentType.FullName), "typeName");
                }
            }
            if (typeId.HasElementClassifications)
            {
                foreach (var classification in typeId.ElementClassifications)
                {
                    switch (classification.Classification)
                    {
                        case TypeElementClassification.Array:
                            currentType = currentType.MakeArray(classification.Rank);
                            break;
                        case TypeElementClassification.Pointer:
                            currentType = currentType.MakePointer();
                            break;
                        case TypeElementClassification.Reference:
                            currentType = currentType.MakeByReference();
                            break;
                    }
                }
            }
            return currentType;
        }

        internal static IControlledCollection<ICliMetadataParamSignature> GetParameterCollection(this ICliMetadataCustomAttributeTableRow target)
        {
            IControlledCollection<ICliMetadataParamSignature> caParams = null;
            switch (target.Ctor.CustomAttributeTypeEncoding)
            {
                case CliMetadataCustomAttributeTypeTag.MethodDefinition:
                    {
                        var mRef = (ICliMetadataMethodDefinitionTableRow)target.Ctor;
                        /* *
                         * Use the signature, instead of the method's properties, due
                         * to the whole metadata association to the parameter at sequence 0.
                         * */
                        caParams = mRef.Signature.Parameters;
                    }
                    break;
                case CliMetadataCustomAttributeTypeTag.MemberReference:
                    {
                        var mRef = (ICliMetadataMemberReferenceTableRow)target.Ctor;
                        switch (mRef.Signature.SignatureKind)
                        {
                            case SignatureKinds.MethodDefSig:
                            case SignatureKinds.MethodRefSig:
                            case SignatureKinds.StandaloneMethodSig:
                                ICliMetadataMethodSignature signature = (ICliMetadataMethodSignature)mRef.Signature;
                                caParams = signature.Parameters;
                                break;
                            default:
                                throw new BadImageFormatException("Bad custom attribute format.");
                        }
                    }
                    break;
                default:
                    throw new BadImageFormatException("Bad custom attribute format.");
            }
            return caParams;
        }


    }
}
