﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using System.Runtime.InteropServices;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using System.IO;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public static class CliGateway
    {
        public const FrameworkVersion CurrentVersion = FrameworkVersion.v4_0_30319;
        public static ICliManager CreateIdentityManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            return new CliManager(runtimeEnvironment);
        }

        public static ICliManager CreateIdentityManager(FrameworkPlatform platform, FrameworkVersion version = CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true)
        {
            return CreateIdentityManager(GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary));
        }

        public static ICliManager CreateIdentityManager(FrameworkPlatform platform, FrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, params string[] additionalResolutionPaths)
        {
            return CreateIdentityManager(GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, additionalResolutionPaths));
        }

        public static IEnumerable<ICliRuntimeEnvironmentInfo> GetRuntimeEnvironmentInfos()
        {
            foreach (var version in new FrameworkVersion[] { FrameworkVersion.v1_0_3705 ,FrameworkVersion.v1_1_4322, FrameworkVersion.v2_0_50727 , FrameworkVersion.v3_0 , FrameworkVersion.v3_5 , FrameworkVersion.v4_0_30319 , FrameworkVersion.v4_5 ,
                                                             FrameworkVersion.v1_0_3705 | FrameworkVersion.ClientProfile,  
                                                             FrameworkVersion.v1_1_4322 | FrameworkVersion.ClientProfile,  
                                                             FrameworkVersion.v2_0_50727  | FrameworkVersion.ClientProfile,  
                                                             FrameworkVersion.v3_0  | FrameworkVersion.ClientProfile,  
                                                             FrameworkVersion.v3_5  | FrameworkVersion.ClientProfile,  
                                                             FrameworkVersion.v4_0_30319  | FrameworkVersion.ClientProfile, 
                                                             FrameworkVersion.v4_5 | FrameworkVersion.ClientProfile})
            {

                yield return new CliRuntimeEnvironmentInfo(false, FrameworkPlatform.x86Platform, version, true);
                yield return new CliRuntimeEnvironmentInfo(true, FrameworkPlatform.x86Platform, version, true);
                if (version == FrameworkVersion.v1_0_3705 || /* Didn't support 64-bit runtimeEnvironment. */
                    version == FrameworkVersion.v1_1_4322)
                    continue;
                yield return new CliRuntimeEnvironmentInfo(false, FrameworkPlatform.x64Platform, version, true);
                yield return new CliRuntimeEnvironmentInfo(true, FrameworkPlatform.x64Platform, version, true);
            }
        }

        public static ICliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(FrameworkPlatform platform, FrameworkVersion version = CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true)
        {
            return new CliRuntimeEnvironmentInfo(resolveCurrent, platform, version, useCoreLibrary);
        }

        public static ICliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(FrameworkPlatform platform, FrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, params string[] additionalResolutionPaths)
        {
            return new CliRuntimeEnvironmentInfo(resolveCurrent, platform, version, additionalResolutionPaths, useCoreLibrary);
        }

        public static bool IsFullAssembly(string filename)
        {
            return _IsFullAssembly(filename);
        }

        private unsafe static bool _IsFullAssembly(string filename)
        {
            FileStream peStream = null;
            PEImage image = null;
            CliMetadataRoot metadataRoot = null;
            try
            {
                image = PEImage.LoadImage(filename, out peStream, false);
                if (image.OptionalHeader.CliHeader.RelativeVirtualAddress == 0)
                    return false;
                var headerSectionScan = image.ResolveRelativeVirtualAddress(image.OptionalHeader.CliHeader.RelativeVirtualAddress);
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
                metadataRoot = new CliMetadataRoot();
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

    }
}