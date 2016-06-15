using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction._Internal.Utilities.Security;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Instructions;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public static class CliGateway
    {
        public const CliFrameworkVersion CurrentVersion = CliFrameworkVersion.CurrentVersion;
        /// <summary>
        /// Denotes the current platform the <see cref="CliGateway"/> type was compiled against.
        /// </summary>
#if x86
#if PLATFORM_ANY
        public const CliFrameworkPlatform CurrentPlatform = CliFrameworkPlatform.AnyPlatform;
#else
        public const CliFrameworkPlatform CurrentPlatform = CliFrameworkPlatform.x86Platform;
#endif
#elif x64
        public const CliFrameworkPlatform CurrentPlatform = CliFrameworkPlatform.x64Platform;
#endif
        public static ICliManager CreateIdentityManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            return new CliManager(runtimeEnvironment);
        }

        public static IVersion ToVersion(this QWordLongVersion version)
        {
            return new _Version(version.MajorVersion, version.MinorVersion, version.BuildNumber, version.RevisionNumber);
        }

        public static ICliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version = CliGateway.CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
        {
            return CreateIdentityManager(GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache));
        }

        public static ICliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, bool useGlobalAccessCache, params string[] additionalResolutionPaths)
        {
            return CreateIdentityManager(GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache, additionalResolutionPaths));
        }

        public static IControlledTypeCollection ToCollection(this Type[] entries, ICliManager identityManager)
        {
            return new TypeCollection((from t in entries
                                       select identityManager.ObtainTypeReference(t)).ToArray());
        }

        public static IEnumerable<ICliRuntimeEnvironmentInfo> GetRuntimeEnvironmentInfos()
        {
            foreach (var version in new CliFrameworkVersion[] { CliFrameworkVersion.v1_0_3705 ,CliFrameworkVersion.v1_1_4322, CliFrameworkVersion.v2_0_50727 , CliFrameworkVersion.v3_0 , CliFrameworkVersion.v3_5 , CliFrameworkVersion.v4_0_30319 , CliFrameworkVersion.v4_5, CliFrameworkVersion.v4_6,
                                                             CliFrameworkVersion.v3_5  | CliFrameworkVersion.ClientProfile,  
                                                             CliFrameworkVersion.v4_0_30319  | CliFrameworkVersion.ClientProfile, 
                                                             CliFrameworkVersion.v4_5 | CliFrameworkVersion.ClientProfile})
            {

                yield return new CliRuntimeEnvironmentInfo(false, CliFrameworkPlatform.x86Platform, version, true, true);
                yield return new CliRuntimeEnvironmentInfo(true, CliFrameworkPlatform.x86Platform, version, true, true);
                if (version == CliFrameworkVersion.v1_0_3705 || /* Didn't support a 64-bit runtime environment. */
                    version == CliFrameworkVersion.v1_1_4322)
                    continue;
                yield return new CliRuntimeEnvironmentInfo(false, CliFrameworkPlatform.x64Platform, version, true, true);
                yield return new CliRuntimeEnvironmentInfo(true, CliFrameworkPlatform.x64Platform, version, true, true);
            }
        }

        public static ICliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(CliFrameworkPlatform platform, CliFrameworkVersion version = CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
        {
            return new CliRuntimeEnvironmentInfo(resolveCurrent, platform, version, useCoreLibrary, useGlobalAccessCache);
        }

        public static ICliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(CliFrameworkPlatform platform, CliFrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, bool useGlobalAccessCache, params string[] additionalResolutionPaths)
        {
            return new CliRuntimeEnvironmentInfo(resolveCurrent, platform, version, additionalResolutionPaths, useCoreLibrary, useGlobalAccessCache);
        }

        public static bool IsFullAssembly(string filename)
        {
            return _IsCliLibrary(filename);
        }

        public static bool IsCliLibrary(string fileName)
        {
            return _IsCliLibrary(fileName, false);
        }

        private unsafe static bool _IsCliLibrary(string filename, bool requireAssemblyTable = true)
        {
            FileStream peStream               = null;
            PEImage image                     = null;
            CliMetadataFixedRoot metadataRoot = null;
            try
            {
                image = PEImage.LoadImage(filename, out peStream, false);
                if (image.ExtendedHeader.CliHeader.RelativeVirtualAddress == 0)
                    return false;
                var headerSectionScan = image.ResolveRelativeVirtualAddress(image.ExtendedHeader.CliHeader.RelativeVirtualAddress);
                if (!headerSectionScan.Resolved)
                    return false;

                var headerSection = headerSectionScan.Section;
                if ((headerSection.SectionData.Length) < Marshal.SizeOf(typeof(CliHeader)))
                    return false;
                CliHeader header;
                /* Copy the header */
                byte[] headerBytes = new byte[Marshal.SizeOf(typeof(CliHeader))];
                headerSection.SectionData.Seek(headerSectionScan.Offset, SeekOrigin.Begin);
                headerSection.SectionData.Read(headerBytes, 0, headerBytes.Length);
                fixed (byte* sectionData = headerBytes)
                    header = *(CliHeader*) sectionData;
                var metadataSectionScan = image.ResolveRelativeVirtualAddress(header.Metadata.RelativeVirtualAddress);
                if (!metadataSectionScan.Resolved)
                    return false;
                var metadataSection = metadataSectionScan.Section;
                metadataSection.SectionDataReader.BaseStream.Seek(metadataSectionScan.Offset, SeekOrigin.Begin);
                metadataRoot        = new CliMetadataFixedRoot();
                metadataRoot.Read(header, peStream, headerSection.SectionDataReader, header.Metadata.RelativeVirtualAddress, image);
                if (requireAssemblyTable && metadataRoot.TableStream.AssemblyTable == null)
                    return false;
                return true;
            }
            catch (BadImageFormatException)
            {
                return false;
            }
            finally
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
            }
        }


        public static IType GetTypeReference(this Type target, ICliManager identityManager)
        {
            return identityManager.ObtainTypeReference(target);
        }

        public static TType GetTypeReference<TType>(this Type target, ICliManager identityManager)
            where TType :
                IType
        {
            var reference = target.GetTypeReference(identityManager);
            if (reference is TType)
                return (TType)reference;
            throw new InvalidOperationException(string.Format("{0} is not of the expected type {1}.", target.FullName, typeof(TType).Name));
        }

        internal static readonly IVersion v1Version = TypeSystemIdentifiers.GetVersion(1, 0, 3300, 0);
        internal static readonly IVersion v1_1Version = TypeSystemIdentifiers.GetVersion(1, 0, 5000, 0);
        internal static readonly IVersion v2Version = TypeSystemIdentifiers.GetVersion(2, 0, 0000, 0);
        internal static readonly IVersion v3Version = TypeSystemIdentifiers.GetVersion(3, 0, 0000, 0);
        internal static readonly IVersion v3_5Version = TypeSystemIdentifiers.GetVersion(3, 5, 0000, 0);
        internal static readonly IVersion v4Version = TypeSystemIdentifiers.GetVersion(4, 0, 0000, 0);
        internal static readonly IVersion v4_5Version = TypeSystemIdentifiers.GetVersion(4, 5, 0000, 0);
        internal static readonly IVersion v4_6Version = TypeSystemIdentifiers.GetVersion(4, 6, 0000, 0);
        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv1 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", v1Version, CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv1_1 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", v1_1Version, CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv2 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", v2Version, CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        internal static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv4 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", v4Version, CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);

        public static IType ParseTypeIdentifier(string typeIdentifier, ICliManager identityManager, IAssembly relativeAssembly = null)
        {
            var relativeAssemblyId = relativeAssembly == null ? null : relativeAssembly.UniqueIdentifier;
            TypeIdentityParser currentParser = new TypeIdentityParser(typeIdentifier, relativeAssemblyId != null ? new TIAssemblyIdentityRule(relativeAssemblyId.Name, new TIVersionRule(relativeAssemblyId.Version.Major, relativeAssemblyId.Version.Minor, relativeAssemblyId.Version.Build, relativeAssemblyId.Version.Revision), relativeAssemblyId.Culture, relativeAssemblyId.PublicKeyToken) : null);
            var typeIdentity = currentParser.ParseQualifiedTypeName();
            return typeIdentity.DecodeParsedType(identityManager, relativeAssembly);
        }

        public static IAssemblyUniqueIdentifier GetCoreLibraryIdentifier(CliFrameworkVersion version)
        {
            switch (version & CliFrameworkVersion.VersionMask)
            {
                case CliFrameworkVersion.v1_1_4322:
                    return mscorlibIdentifierv1_1;
                case CliFrameworkVersion.v2_0_50727:
                case CliFrameworkVersion.v3_0:
                case CliFrameworkVersion.v3_5:
                    return mscorlibIdentifierv2;
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_6:
                    return mscorlibIdentifierv4;
            }
            return mscorlibIdentifierv1;
        }

        public static string GetInstructionText(this CilOpcodeInstruction instruction)
        {
            return CilOpCodeDetails.OpcodeDetails[instruction].InstructionText;
        }
    }
}
