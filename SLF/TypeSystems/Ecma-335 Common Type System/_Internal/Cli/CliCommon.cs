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
#if x86
using SlotType = System.UInt32;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
#elif x64
using SlotType = System.UInt64;
#endif

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    static partial class CliCommon
    {
        internal const string VersionString_1_0_3705 = "v1.0.3705";
        internal const string VersionString_1_1_4322 = "v1.1.4322";
        internal const string VersionString_2_0_50727 = "v2.0.50727";
        internal const string VersionString_3_0 = "v3.0";
        internal const string VersionString_3_5 = "v3.5";
        internal const string VersionString_4_0_30319 = "v4.0.30319";
        internal const string VersionString_4_5 = "v4.5";

        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv1   = AstIdentifier.GetAssemblyIdentifier("mscorlib", AstIdentifier.GetVersion(1, 0, 3705, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv1_1 = AstIdentifier.GetAssemblyIdentifier("mscorlib", AstIdentifier.GetVersion(1, 0, 5000, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv2   = AstIdentifier.GetAssemblyIdentifier("mscorlib", AstIdentifier.GetVersion(2, 0, 0000, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv4   = AstIdentifier.GetAssemblyIdentifier("mscorlib", AstIdentifier.GetVersion(4, 0, 0000, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);

        internal static Tuple<PEImage, CliMetadataRoot> LoadAssemblyMetadata(string filename)
        {
            FileStream peStream;
            var image = PEImage.LoadImage(filename, out peStream, true);
            return LoadAssemblyMetadata(peStream, image);
        }

        unsafe private static Tuple<PEImage, CliMetadataRoot> LoadAssemblyMetadata(FileStream peStream, PEImage image)
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
                header = *(CliHeader*) sectionData;
            var metadataSectionScan = image.ResolveRelativeVirtualAddress(header.Metadata.RelativeVirtualAddress);
            if (!metadataSectionScan.Resolved)
                throw new BadImageFormatException("Metadata Root not found within image.");
            var metadataSection = metadataSectionScan.Section;
            metadataSection.SectionDataReader.BaseStream.Seek(metadataSectionScan.Offset, SeekOrigin.Begin);
            var metadataRoot = new CliMetadataRoot();
            metadataRoot.Read(header, peStream, headerSection.SectionDataReader, header.Metadata.RelativeVirtualAddress, image);
            return new Tuple<PEImage, CliMetadataRoot>(image, metadataRoot);
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

        public static IEnumerable<TypeModification> Resolve(this IEnumerable<ICliMetadataCustomModifierSignature> modifiers, _ICliManager manager)
        {
            return from modifier in modifiers
                   select new TypeModification(manager.ObtainTypeReference(modifier.ModifierType), modifier.Required);
        }

        internal static Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> CheckFilename(string directory, string filename, string extension)
        {
            string resultedFilename = MinimizeFilename(string.Format("{0}.{1}", Path.Combine(directory, filename), extension));
            if (!File.Exists(resultedFilename))
                return null;
            return CheckFilename(resultedFilename);
        }

        internal unsafe static Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> CheckFilename(string resultedFilename, bool loadAssemblyInfo = true)
        {
            FileStream peStream = null;
            PEImage image = null;
            CliMetadataRoot metadataRoot = null;
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
                    header = *(CliHeader*) sectionData;
                var metadataSectionScan = image.ResolveRelativeVirtualAddress(header.Metadata.RelativeVirtualAddress);
                if (!metadataSectionScan.Resolved)
                    return null;
                var metadataSection = metadataSectionScan.Section;
                metadataSection.SectionDataReader.BaseStream.Seek(metadataSectionScan.Offset, SeekOrigin.Begin);
                metadataRoot = new CliMetadataRoot();
                metadataRoot.Read(header, peStream, headerSection.SectionDataReader, header.Metadata.RelativeVirtualAddress, image);
                if (loadAssemblyInfo)
                {
                    if (metadataRoot.TableStream.AssemblyTable == null)
                        return null;
                    var loadedUniqueId = GetAssemblyUniqueIdentifier(new Tuple<PEImage, ICliMetadataRoot>(image, metadataRoot));
                    if (loadedUniqueId == null)
                        return null;
                    return new Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string>(loadedUniqueId.Item2, loadedUniqueId.Item1, image, metadataRoot, loadedUniqueId.Item3, resultedFilename);
                }
                else
                    return new Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string>(null, null, image, metadataRoot, null, resultedFilename);
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
            finally
            {
            }
        }

        public static Tuple<IStrongNamePublicKeyInfo, IAssemblyUniqueIdentifier, ICliMetadataAssemblyTableRow> GetAssemblyUniqueIdentifier(Tuple<PEImage, ICliMetadataRoot> peAndMetadata)
        {
            var assemblyTable = (CliMetadataAssemblyTable) peAndMetadata.Item2.TableStream[CliMetadataTableKinds.Assembly];
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
            if (firstAssemblyRow.PublicKey.Length == 0)
                publicKeyInfo = null;
            else
                publicKeyInfo = (IStrongNamePublicKeyInfo) StrongNameKeyPairHelper.LoadStrongNameKeyData(firstAssemblyRow.PublicKey, false);
            var culture = firstAssemblyRow.Culture;
            ICultureIdentifier cultureId;
            if (culture == string.Empty)
                cultureId = CultureIdentifiers.None;
            else
            {
                culture = ValidCultureIdentifiers.TranscodeToCultureIdentifier(culture);
                cultureId = CultureIdentifiers.GetIdentifierByName(culture);
            }
            IAssemblyUniqueIdentifier assemblyUniqueIdentifier = AstIdentifier.GetAssemblyIdentifier(firstAssemblyRow.Name, firstAssemblyRow.Version.ToVersion(), cultureId, publicKeyInfo == null ? null : publicKeyInfo.PublicToken.Token);
            return Tuple.Create(publicKeyInfo, assemblyUniqueIdentifier, firstAssemblyRow);
        }

        internal static Tuple<IStrongNamePublicKeyInfo, IAssemblyUniqueIdentifier> GetAssemblyUniqueIdentifier(ICliMetadataAssemblyRefTableRow entry)
        {
            IStrongNamePublicKeyInfo publicKeyInfo;
            if ((entry.Flags & CliMetadataAssemblyFlags.PublicKey) != CliMetadataAssemblyFlags.PublicKey)
                publicKeyInfo = null;
            else
                publicKeyInfo = (IStrongNamePublicKeyInfo) StrongNameKeyPairHelper.LoadStrongNameKeyData(entry.PublicKeyOrToken, false);
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
            IAssemblyUniqueIdentifier assemblyUniqueIdentifier = AstIdentifier.GetAssemblyIdentifier(entry.Name, entry.Version.ToVersion(), cultureId, publicKeyInfo == null ? publicKeyToken : publicKeyInfo.PublicToken.Token);
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
                        var typeDef = (ICliMetadataTypeDefinitionTableRow) typeIdentity;
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
                                var typeTable = typeRef.MetadataRoot.TableStream.TypeDefinitionTable;
                                if (typeTable == null)
                                    return null;
                                typeTable.Read();
                                string tidN = typeRef.Name,
                                       tidNS = typeRef.Namespace;
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
                                        var loadedModule = manager.LoadModule((ICliMetadataModuleReferenceTableRow) typeRef.Source);
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
                            ICliMetadataTypeRefTableRow current = typeRef;
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
                                    bool found = false;
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
                                var assemblyRef = typeRef.Source as ICliMetadataAssemblyRefTableRow;
                                if (assemblyRef == null)
                                    return null;
                                var assembly = manager.ObtainAssemblyReference(assemblyRef);
                                var typeDef = assembly.FindType(typeRef.Namespace, typeRef.Name);
                                if (typeDef != null && (selectionPredicate == null || selectionPredicate(manager, typeDef)))
                                    return typeDef;
                            }
                            break;
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
                                var genericSig = (ICliMetadataGenericInstanceTypeSignature) specSignature;
                                return ResolveScope(genericSig.Target, manager, selectionPredicate, typeSpec);
                            }
                            else if (specSignature is ICliMetadataVectorArrayTypeSignature)
                            {
                                var vectorSig = (ICliMetadataVectorArrayTypeSignature) specSignature;
                                return ResolveScope(vectorSig.ElementType, manager, selectionPredicate, typeSpec);
                            }
                            else if (specSignature is ICliMetadataArrayTypeSignature)
                            {
                                var arraySig = (ICliMetadataArrayTypeSignature) specSignature;
                                return ResolveScope(arraySig.ElementType, manager, selectionPredicate, typeSpec);
                            }
                        }
                    }
                    break;
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
            return false;
        }

        internal static bool IsBaseValueType(_ICliManager manager, ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
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
            return false;
        }

        internal static bool IsBaseEnumType(_ICliManager manager, ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
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
            return false;
        }

        internal static bool IsBaseDelegateType(_ICliManager manager, ICliMetadataTypeDefOrRefRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
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
            return false;
        }

        internal static bool IsBaseEnumType(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
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

        private static bool IsBaseObject(_ICliManager manager, ICliMetadataTypeDefinitionTableRow typeIdentity)
        {
            BaseKindCacheType cachedResult;
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
                   typeIdentity.ExtendsIndex == 0;
            if (result)
                manager.BaseTypeKinds.Add(typeIdentity, BaseKindCacheType.ObjectBase);
            return result;
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
                else if (((field.FieldAttributes & FieldAttributes.RTSpecialName) == FieldAttributes.RTSpecialName) &&
                    field.Name == "value__")
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
            if (manager.BaseTypeKinds.TryGetValue(typeIdentity, out cachedResult))
            {
                if (cachedResult == BaseKindCacheType.DelegateBase)
                    return true;
                return false;
            }
            string typeName;
            bool result = ((typeIdentity.TypeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract) && typeIdentity.Namespace == "System" && ((typeName = typeIdentity.Name) == "Delegate" || typeName == "MulticastDelegate") &&
                   IsBaseObject(manager, typeIdentity.Extends);
            if (result)
                manager.BaseTypeKinds.Add(typeIdentity, BaseKindCacheType.DelegateBase);
            return result;
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
                assembly = (_ICliAssembly) manager.GetRelativeAssembly(typeMetadata.MetadataRoot);
            if (CliCommon.IsEnum(manager, typeMetadata))
                return assembly.UniqueIdentifier.GetTypeIdentifier(typeMetadata.Namespace, typeMetadata.Name);
            else
            {
                var declaringTypeMetadata = typeMetadata.DeclaringType;
                IGeneralDeclarationUniqueIdentifier @namespace = null;
                if (declaringTypeMetadata == null && typeMetadata.NamespaceIndex != 0)
                    @namespace = assembly.GetNamespace(typeMetadata.Namespace).UniqueIdentifier;
                else if (typeMetadata.NamespaceIndex != 0)
                    @namespace = AstIdentifier.GetDeclarationIdentifier(typeMetadata.Namespace);
                if (typeMetadata.MetadataRoot.TableStream.GenericParameterTable == null)
                    return assembly.UniqueIdentifier.GetTypeIdentifier(@namespace, typeMetadata.Name, 0);
                else if (declaringTypeMetadata != null)
                    return assembly.UniqueIdentifier.GetTypeIdentifier(@namespace, typeMetadata.Name, typeMetadata.TypeParameters.Count - declaringTypeMetadata.TypeParameters.Count);
                else
                    return assembly.UniqueIdentifier.GetTypeIdentifier(@namespace, typeMetadata.Name, typeMetadata.TypeParameters.Count);
            }
        }

        internal static IMetadatum GetMetadatum(_ICliManager manager, IType metadataType, IReadOnlyCollection<ICliMetadataCustomAttributeTableRow> metadata)
        {
            throw new NotImplementedException();
        }

        internal static IMetadatum GetMetadatum(_ICliManager manager, ICliMetadataCustomAttributeTableRow metadata)
        {
            throw new NotImplementedException();
        }

        internal static bool IsDefined(this IReadOnlyCollection<ICliMetadataCustomAttributeTableRow> customAttributes, IType targetType, _ICliManager manager)
        {
            throw new NotImplementedException();
        }

        internal static IModifiedType MakeModified(ICliMetadataTypeSignature original, IReadOnlyCollection<ICliMetadataCustomModifierSignature> modifiers, _ICliManager manager)
        {
            return ((_ICliType) manager.ObtainTypeReference(original)).MakeModified(modifiers);
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
        public static ICliMetadataTypeDefinitionTableRow FindTypeImplementation(IGeneralTypeUniqueIdentifier uniqueIdentifier, CliNamespaceKeyedTree topLevel)
        {
            if (uniqueIdentifier.Name.Contains('`'))
                return FindTypeImplementation(uniqueIdentifier.Namespace.Name, uniqueIdentifier.Name, topLevel);
            else if (uniqueIdentifier is IGenericTypeUniqueIdentifier)
            {
                var genericUniqueId = uniqueIdentifier as IGenericTypeUniqueIdentifier;
                if (genericUniqueId.TypeParameters > 0)
                    return FindTypeImplementation(uniqueIdentifier.Namespace.Name, string.Format("{0}`{1}", uniqueIdentifier.Name, ((IGenericTypeUniqueIdentifier) (uniqueIdentifier)).TypeParameters), topLevel);
            }
            return FindTypeImplementation(uniqueIdentifier.Namespace.Name, uniqueIdentifier.Name, topLevel);
        }


        public static ICliMetadataTypeDefinitionTableRow FindTypeImplementation(string @namespace, string name, string moduleName, CliNamespaceKeyedTree topLevel, IModuleDictionary moduleDictionary)
        {
            string ns = @namespace;

            int lastIndex = 0;
            IModule module;
            if (!moduleDictionary.TryGetValue(AstIdentifier.GetDeclarationIdentifier(moduleName), out module))
                return null;
            ICliModule cliModule = module as ICliModule;
            if (cliModule == null)
                return null;
        nextPart:
            int next = ns.IndexOf('.', lastIndex);
            if (next != -1)
            {
                string current = ns.Substring(lastIndex, next - lastIndex);
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
                lastIndex = next + 1;
                goto nextPart;
            }
            else
            {
                string current = ns.Substring(lastIndex);
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
            }
            if (topLevel.NamespaceTypes != null)
            {
                foreach (var nsType in topLevel.NamespaceTypes)
                    if (nsType.MetadataRoot == cliModule.Metadata.MetadataRoot && nsType.Name == name)
                        return nsType;
            }
            return null;
        }

        public static ICliMetadataTypeDefinitionTableRow FindTypeImplementation(string @namespace, string name, CliNamespaceKeyedTree topLevel)
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
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
                lastIndex = next + 1;
                goto nextPart;
            }
            else
            {
                string current = ns.Substring(lastIndex);
                uint currentHash = (uint) current.GetHashCode();
                if (topLevel.ContainsKey(currentHash))
                    topLevel = topLevel[currentHash];
                else
                    return null;
            }
            typeSearch:
            if (topLevel != null && topLevel.NamespaceTypes != null)
            {
                var currentTypes = topLevel.NamespaceTypes;
                foreach (var nsType in currentTypes)
                    if (nsType.Name == name)
                        return nsType;
            }
            return null;
        }

        public static IBinaryOperatorUniqueIdentifier GetBinaryOperatorUniqueIdentifier(ICliMetadataTypeDefinitionTableRow owner, ICliMetadataMethodDefinitionTableRow metadata)
        {
            CoercibleBinaryOperators op = (CoercibleBinaryOperators) (-1);
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
            throw new NotImplementedException();
        }

        public static CliFrameworkVersion GetFrameworkVersionFromString(string s)
        {
            switch (s)
            {
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
                default:
                    throw new InvalidOperationException();
            }
        }

        public static RuntimeCoreType GetCoreType(this IType type) { return type.Manager.ObtainCoreType(type); }


        internal static IEnumerable<ICliMetadataAssemblyRefTable> ObtainAssemblyRefTables(this CliAssembly owner)
        {
            return (from ICliModule m in owner.Modules.Values
                    let table = m.Metadata.MetadataRoot.TableStream.AssemblyRefTable
                    where table != null
                    select table);
        }

    }
}
