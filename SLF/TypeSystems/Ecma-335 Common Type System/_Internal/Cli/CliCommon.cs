using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
#if x86
using SlotType = System.UInt32;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System.Reflection;
#elif x64
using SlotType = System.UInt64;
#endif

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    static partial class CliCommon
    {

        unsafe internal static Tuple<PEImage, CliMetadataRoot> LoadAssemblyMetadata(string filename)
        {
            const int slotSize = sizeof(SlotType);
#if x86
            const int slotSizeIndex = 2;
#elif x64
            const int slotSizeIndex = 3;
#endif
            const int bitCountIndex = 3;
            FileStream peStream;
            var image = PEImage.LoadImage(filename, out peStream, true);
            return LoadAssemblyMetadata(peStream, image);
        }

        unsafe private static Tuple<PEImage, CliMetadataRoot> LoadAssemblyMetadata(FileStream peStream, PEImage image)
        {
            /* *
             * Resolve the virtual address of the CliHeader, which yields
             * the offset in the section's publicKey, and the section itself.
             * */
            var headerSectionScan = image.ResolveRelativeVirtualAddress(image.OptionalHeader.CliHeader.RelativeVirtualAddress);
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

        internal static unsafe Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> CheckFilename(string directory, string filename, string extension)
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
                if (image.OptionalHeader.CliHeader.RelativeVirtualAddress == 0)
                    return null;
                var headerSectionScan = image.ResolveRelativeVirtualAddress(image.OptionalHeader.CliHeader.RelativeVirtualAddress);
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
                    var loadedUniqueId = GetAssemblyUniqueIdentifier(new Tuple<PEImage, CliMetadataRoot>(image, metadataRoot));
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

        public static Tuple<IStrongNamePublicKeyInfo, IAssemblyUniqueIdentifier, ICliMetadataAssemblyTableRow> GetAssemblyUniqueIdentifier(Tuple<PEImage, CliMetadataRoot> peAndMetadata)
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
            IAssemblyUniqueIdentifier assemblyUniqueIdentifier = AstIdentifier.GetAssemblyIdentifier(firstAssemblyRow.Name, new _Version(firstAssemblyRow.Version), cultureId, publicKeyInfo == null ? null : publicKeyInfo.PublicToken.Token);
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
            IAssemblyUniqueIdentifier assemblyUniqueIdentifier = AstIdentifier.GetAssemblyIdentifier(entry.Name, new _Version(entry.Version), cultureId, publicKeyInfo == null ? publicKeyToken : publicKeyInfo.PublicToken.Token);
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
                                    var identifier = CliCommon.GetAssemblyUniqueIdentifier(new Tuple<PEImage, CliMetadataRoot>(typeRef.MetadataRoot.SourceImage, typeRef.MetadataRoot));
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
                            if (specSignature is ICliMetadataGenericTypeInstanceSignature)
                            {
                                var genericSig = (ICliMetadataGenericTypeInstanceSignature) specSignature;
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
            if (typeIdentity.ExtendsIndex == 0 &&
                typeIdentity.NamespaceIndex == 0 &&
                (typeIdentity.TypeAttributes & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic &&
                typeIdentity.Name == "<Module>")
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
                        #region bad idea
                        //var nativeSig = signature.Type as ICliMetadataNativeTypeSignature;
                        //switch (nativeSig.TypeKind)
                        //{
                        //    case CliMetadataNativeTypes.Byte:
                        //    case CliMetadataNativeTypes.SByte:
                        //    case CliMetadataNativeTypes.Int16:
                        //    case CliMetadataNativeTypes.UInt16:
                        //    case CliMetadataNativeTypes.Int32:
                        //    case CliMetadataNativeTypes.UInt32:
                        //    case CliMetadataNativeTypes.Int64:
                        //    case CliMetadataNativeTypes.UInt64:
                        //        continue;
                        //    default:
                        //        return false;
                        //}
                        #endregion
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
            string typeName = typeIdentity.Name,
                   typeNamespace = typeIdentity.Namespace;
            bool result = ((typeIdentity.TypeAttributes & TypeAttributes.Abstract) == TypeAttributes.Abstract) && typeNamespace == "System" && (typeName == "Delegate" || typeName == "MulticastDelegate") &&
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
                current = ResolveScope(current.Extends, manager);
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
    }
}
