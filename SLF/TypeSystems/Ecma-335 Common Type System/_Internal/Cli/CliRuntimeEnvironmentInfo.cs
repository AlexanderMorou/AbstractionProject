using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliRuntimeEnvironmentInfo :
        ICliRuntimeEnvironmentInfo
    {
        // {System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a}
        //{(00:00:00.0819367, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=5c561934e089, C:\Windows\Microsoft.NET\Framework\v1.1.4322\mscorlib.dll)}
        private const string v1_0_3705 = "v1.0.3705";
        private const string v1_1_4322 = "v1.1.4322";
        private const string v2_0_50727 = "v2.0.50727";
        private const string v3_0 = "v3.0";
        private const string v3_5 = "v3.5";
        private const string v4_0_30319 = "v4.0.30319";
        private const string v4_5 = "v4.5";
        private const string fr86 = "Framework";
        private const string fr64 = "Framework64";
        const string s_assembly = "assembly";
        const string s_gacEarly = "GAC";
        const string s_gacAny = "GAC_MSIL";
        const string s_gac32 = "GAC_32";
        const string s_gac64 = "GAC_64";

        private static readonly byte[] corLibKey = new byte[] { 0xb7, 0x7a, 0x5c, 0x56, 0x19, 0x34, 0xe0, 0x89 };
        private static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv1 = AstIdentifier.GetAssemblyIdentifier("mscorlib", AstIdentifier.GetVersion(1, 0, 5000, 0), CultureIdentifiers.None, corLibKey);
        private static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv2 = AstIdentifier.GetAssemblyIdentifier("mscorlib", AstIdentifier.GetVersion(2, 0, 0, 0), CultureIdentifiers.None, corLibKey);
        private static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv4 = AstIdentifier.GetAssemblyIdentifier("mscorlib", AstIdentifier.GetVersion(4, 0, 0, 0), CultureIdentifiers.None, corLibKey);
        private static readonly IAssemblyUniqueIdentifier systemIdentifierV1 = AstIdentifier.GetAssemblyIdentifier("System", AstIdentifier.GetVersion(1, 0, 5000, 0), CultureIdentifiers.None, corLibKey);
        private static readonly IAssemblyUniqueIdentifier systemIdentifierV2 = AstIdentifier.GetAssemblyIdentifier("System", AstIdentifier.GetVersion(2, 0, 0, 0), CultureIdentifiers.None, corLibKey);
        private static readonly IAssemblyUniqueIdentifier systemIdentifierV4 = AstIdentifier.GetAssemblyIdentifier("System", AstIdentifier.GetVersion(4, 0, 0, 0), CultureIdentifiers.None, corLibKey);
        private string[] additionalResolutionPaths;


        /// <summary>
        /// Creates a new <see cref="CliRuntimeEnvironmentInfo"/> with the <paramref name="resolveCurrent"/> flag,
        /// <paramref name="platform"/>, <paramref name="version"/>, and whether to <paramref name="useCoreLibrary"/>.
        /// </summary>
        /// <param name="resolveCurrent">Whether to use the current directory via <see cref="Directory.GetCurrentDirectory()"/>.</param>
        /// <param name="platform">The <see cref="FrameworkPlatform"/> to target.</param>
        /// <param name="version">The <see cref="FrameworkVersion"/> to target.</param>
        /// <param name="useCoreLibrary">Whether to use the core library, typically mscorlib.</param>
        internal CliRuntimeEnvironmentInfo(bool resolveCurrent, FrameworkPlatform platform, FrameworkVersion version, bool useCoreLibrary, bool useGlobalAccessCache)
        {
            this.UseCoreLibrary = useCoreLibrary;
            this.ResolveCurrent = resolveCurrent;
            this.Platform = platform;
            this.Version = version;
            this.UseGlobalAccessCache = useGlobalAccessCache;
        }

        internal CliRuntimeEnvironmentInfo(bool resolveCurrent, FrameworkPlatform platform, FrameworkVersion version, string[] additionalResolutionPaths, bool useCoreLibrary, bool useGlobalAccessCache)
            : this(resolveCurrent, platform, version, useCoreLibrary, useGlobalAccessCache)
        {
            this.additionalResolutionPaths = additionalResolutionPaths;
        }

        //#region ICliRuntimeEnvironmentInfo Members

        public bool ResolveCurrent { get; private set; }

        public FrameworkPlatform Platform { get; private set; }

        public FrameworkVersion Version { get; private set; }

        //#endregion

        //#region ICliRuntimeEnvironmentInfo Members

        public IEnumerable<DirectoryInfo> ResolutionPaths
        {
            get
            {
                string runtimeDir;
                string[] parts;
                int frIndex;
                int vIndex;
                GetFrameworkVersionAndPlatform(out runtimeDir, out parts, out frIndex, out vIndex);
                if (frIndex != -1 && vIndex != -1)
                {
                    AdjustFrameworkPath(parts, frIndex);
                    switch (this.Version)
                    {
                        case FrameworkVersion.v1_0_3705:
                            if (Platform == FrameworkPlatform.x64Platform)
                                break;
                            parts[vIndex] = v1_0_3705;
                            yield return gdi(parts);
                            break;
                        case FrameworkVersion.v1_1_4322:
                            if (Platform == FrameworkPlatform.x64Platform)
                                break;
                            parts[vIndex] = v1_1_4322;
                            yield return gdi(parts);
                            break;
                        case FrameworkVersion.v2_0_50727:
                            parts[vIndex] = v2_0_50727;
                            yield return gdi(parts);
                            break;
                        case FrameworkVersion.v3_0:
                            parts[vIndex] = v3_0;
                            yield return gdi(parts);
                            yield return gdi(parts.Add("WPF"));
                            goto case FrameworkVersion.v2_0_50727;
                        case FrameworkVersion.v3_5:
                            parts[vIndex] = v3_5;
                            yield return gdi(parts);
                            goto case FrameworkVersion.v3_0;
                        case FrameworkVersion.v4_0_30319:
                            parts[vIndex] = v4_0_30319;
                            yield return gdi(parts);
                            yield return gdi(parts.Add("WPF"));
                            break;
                        case FrameworkVersion.v4_5:
                            parts[vIndex] = v4_5;
                            yield return gdi(parts);
                            break;
                    }
                }
                else
                    yield return new DirectoryInfo(runtimeDir);

                if (ResolveCurrent)
                    yield return new DirectoryInfo(Directory.GetCurrentDirectory());
                if (additionalResolutionPaths != null && additionalResolutionPaths.Length > 0)
                {
                    foreach (var path in additionalResolutionPaths)
                    {
                        if (Directory.Exists(path))
                            yield return new DirectoryInfo(path);
                    }
                }
            }
        }

        private void AdjustFrameworkPath(string[] parts, int frIndex)
        {
            switch (Platform)
            {
                case FrameworkPlatform.x86Platform:
                    parts[frIndex] = fr86;
                    break;
                case FrameworkPlatform.x64Platform:
                    parts[frIndex] = fr64;
                    break;
            }
        }

        private static void GetFrameworkVersionAndPlatform(out string runtimeDir, out string[] parts, out int frIndex, out int vIndex)
        {
            runtimeDir = RuntimeEnvironment.GetRuntimeDirectory();
            parts = runtimeDir.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            frIndex = -1;
            vIndex = -1;


            for (int i = 0; i < parts.Length; i++)
            {
                string current = parts[i];
                if (current == fr86 || current == fr64)
                    frIndex = i;
                else
                {
                    switch (current)
                    {
                        case v1_1_4322:
                        case v1_0_3705:
                        case v2_0_50727:
                        case v3_0:
                        case v3_5:
                        case v4_0_30319:
                        case v4_5:
                            vIndex = i;
                            break;
                    }
                }
            }
        }


        public IAssemblyUniqueIdentifier CoreLibraryIdentifier
        {
            get
            {
                if (!UseCoreLibrary)
                    return null;
                switch (Version & ~FrameworkVersion.ClientProfile)
                {
                    case FrameworkVersion.v1_0_3705:
                    case FrameworkVersion.v1_1_4322:
                        return mscorlibIdentifierv1;
                    case FrameworkVersion.v2_0_50727:
                    case FrameworkVersion.v3_0:
                    case FrameworkVersion.v3_5:
                        return mscorlibIdentifierv2;
                    case FrameworkVersion.v4_0_30319:
                    case FrameworkVersion.v4_5:
                        return mscorlibIdentifierv4;
                }
                return null;
            }
        }

        //#endregion

        private static DirectoryInfo gdi(string[] parts)
        {
            return new DirectoryInfo(string.Join(Path.DirectorySeparatorChar.ToString(), parts));
        }

        //#region ICliRuntimeEnvironmentInfo Members


        public bool UseGlobalAccessCache { get; private set; }

        private IEnumerable<string> _gacLocationFor(IAssemblyUniqueIdentifier uniqueIdentifier)
        {
            string wFldr = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            switch (this.Version & ~FrameworkVersion.ClientProfile)
            {
                case FrameworkVersion.v1_0_3705:
                case FrameworkVersion.v1_1_4322:
                    if (Platform == FrameworkPlatform.x64Platform)
                        break;
                    yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gacEarly, uniqueIdentifier.Name, uniqueIdentifier.Version, uniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                    break;
                case FrameworkVersion.v2_0_50727:
                case FrameworkVersion.v3_0:
                case FrameworkVersion.v3_5:
                    switch (Platform)
                    {
                        case FrameworkPlatform.x86Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gac32, uniqueIdentifier.Name, uniqueIdentifier.Version, uniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                            break;
                        case FrameworkPlatform.x64Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gac64, uniqueIdentifier.Name, uniqueIdentifier.Version, uniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                            break;
                    }
                    yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gacAny, uniqueIdentifier.Name, uniqueIdentifier.Version, uniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                    goto case FrameworkVersion.v1_0_3705;
                case FrameworkVersion.v4_5:
                case FrameworkVersion.v4_0_30319:
                    string fVer = (Version & ~FrameworkVersion.ClientProfile) == FrameworkVersion.v4_0_30319 ? "v4.0" : "v4.5";
                    string runtimeDir;
                    string[] parts;
                    int frIndex;
                    int vIndex;
                    GetFrameworkVersionAndPlatform(out runtimeDir, out parts, out frIndex, out vIndex);
                    parts = parts.Take(parts.Length - 2).Concat(new[] { s_assembly }).ToArray();
                    switch (Platform)
                    {
                        case FrameworkPlatform.x86Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}_{5}__{6}", string.Join(Path.DirectorySeparatorChar.ToString(), parts), Path.DirectorySeparatorChar, s_gac32, uniqueIdentifier.Name, fVer, uniqueIdentifier.Version, uniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                            break;
                        case FrameworkPlatform.x64Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}_{5}__{6}", string.Join(Path.DirectorySeparatorChar.ToString(), parts), Path.DirectorySeparatorChar, s_gac64, uniqueIdentifier.Name, fVer, uniqueIdentifier.Version, uniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                            break;
                    }
                    yield return string.Format("{0}{1}{2}{1}{3}{1}{4}_{5}__{6}", string.Join(Path.DirectorySeparatorChar.ToString(), parts), Path.DirectorySeparatorChar, s_gacAny, uniqueIdentifier.Name, fVer, uniqueIdentifier.Version, uniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                    goto case FrameworkVersion.v3_5;
            }

        }

        public IEnumerable<DirectoryInfo> GacLocationFor(IAssemblyUniqueIdentifier uniqueIdentifier)
        {
            foreach (var path in _gacLocationFor(uniqueIdentifier))
                if (Directory.Exists(path))
                    yield return new DirectoryInfo(path);
        }

        public bool UseCoreLibrary { get; private set; }

        //#endregion

        #region IStandardRuntimeEnvironmentInfo Members

        public ITypeUniqueIdentifier ArrayType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Array", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Array", 0);
            }
        }

        public ITypeUniqueIdentifier AsynchronousTask
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 0);
            }
        }

        public ITypeUniqueIdentifier AsynchronousTaskOfT
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 1);
                else
                    return AstIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 1);
            }
        }

        public ITypeUniqueIdentifier Byte
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Byte", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Byte", 0);
            }
        }

        public ITypeUniqueIdentifier CompilerGeneratedMetadatum
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System.Runtime.CompilerServices", "CompilerGeneratedAttribute", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System.Runtime.CompilerServices", "CompilerGeneratedAttribute", 0);
            }
        }

        public ITypeUniqueIdentifier EnumBaseType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Enum", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Enum", 0);
            }
        }

        public ITypeUniqueIdentifier Int16
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Int16", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Int16", 0);
            }
        }

        public ITypeUniqueIdentifier Int32
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Int32", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Int32", 0);
            }
        }

        public ITypeUniqueIdentifier Int64
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Int64", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Int64", 0);
            }
        }

        public ITypeUniqueIdentifier NullableBaseType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.ValueTypeBaseType;
                return null;
            }
        }

        public ITypeUniqueIdentifier NullableType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Nullable", 1);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Nullable", 1);
            }
        }

        public ITypeUniqueIdentifier RootType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Object", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Object", 0);
            }
        }

        public ITypeUniqueIdentifier SByte
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "SByte", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "SByte", 0);
            }
        }

        public ITypeUniqueIdentifier UInt16
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "UInt16", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "UInt16", 0);
            }
        }

        public ITypeUniqueIdentifier UInt32
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "UInt32", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "UInt32", 0);
            }
        }

        public ITypeUniqueIdentifier UInt64
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "UInt64", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "UInt64", 0);
            }
        }

        public ITypeUniqueIdentifier ValueTypeBaseType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "ValueType", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "ValueType", 0);
            }
        }

        public ITypeUniqueIdentifier VoidType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Void", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Void", 0);
            }
        }

        public ITypeUniqueIdentifier Delegate
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Delegate", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "Delegate", 0);
            }
        }

        public ITypeUniqueIdentifier MulticastDelegate
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "MulticastDelegate", 0);
                else
                    return AstIdentifier.GetTypeIdentifier("System", "MulticastDelegate", 0);
            }
        }

        #endregion

    }
}
