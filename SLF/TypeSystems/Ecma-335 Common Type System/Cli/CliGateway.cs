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

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public static class CliGateway
    {
        public const CliFrameworkVersion CurrentVersion = CliFrameworkVersion.v4_5;
#if x86
        public const CliFrameworkPlatform CurrentPlatform = CliFrameworkPlatform.x86Platform;
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


        public static ICliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version = CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
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
            foreach (var version in new CliFrameworkVersion[] { CliFrameworkVersion.v1_0_3705 ,CliFrameworkVersion.v1_1_4322, CliFrameworkVersion.v2_0_50727 , CliFrameworkVersion.v3_0 , CliFrameworkVersion.v3_5 , CliFrameworkVersion.v4_0_30319 , CliFrameworkVersion.v4_5 ,
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
            return _IsFullAssembly(filename);
        }

        private unsafe static bool _IsFullAssembly(string filename)
        {
            FileStream peStream = null;
            PEImage image = null;
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
                metadataRoot = new CliMetadataFixedRoot();
                metadataRoot.Read(header, peStream, headerSection.SectionDataReader, header.Metadata.RelativeVirtualAddress, image);
                if (metadataRoot.TableStream.AssemblyTable == null)
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
    }
}
