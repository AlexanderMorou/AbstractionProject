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
             * the offset in the section's data, and the section itself.
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

    }
}
