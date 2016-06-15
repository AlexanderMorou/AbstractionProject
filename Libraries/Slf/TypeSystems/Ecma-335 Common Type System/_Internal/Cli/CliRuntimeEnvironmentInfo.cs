using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction._Internal.Utilities.Security;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliRuntimeEnvironmentInfo :
        ICliRuntimeEnvironmentInfo
    {
        private IGeneralTypeUniqueIdentifier _arrayType;
        private IGeneralTypeUniqueIdentifier _boolean;
        private IGeneralTypeUniqueIdentifier _decimal;
        private IGeneralTypeUniqueIdentifier _single;
        private IGeneralTypeUniqueIdentifier _double;
        private IGeneralTypeUniqueIdentifier _sByte;
        private IGeneralTypeUniqueIdentifier _byte;
        private IGeneralTypeUniqueIdentifier _char;
        private IGeneralTypeUniqueIdentifier _rootEnum;
        private IGeneralTypeUniqueIdentifier _int16;
        private IGeneralTypeUniqueIdentifier _uInt16;
        private IGeneralTypeUniqueIdentifier _int32;
        private IGeneralTypeUniqueIdentifier _uInt32;
        private IGeneralTypeUniqueIdentifier _int64;
        private IGeneralTypeUniqueIdentifier _uInt64;
        private IGeneralTypeUniqueIdentifier _rootType;
        private IGeneralTypeUniqueIdentifier _string;
        private IGeneralTypeUniqueIdentifier _rootStruct;
        private IGeneralTypeUniqueIdentifier _voidType;
        private IGeneralTypeUniqueIdentifier _typeType;
        private IGeneralTypeUniqueIdentifier _extensionMetadatum;
        private IGeneralTypeUniqueIdentifier _asynchronousTask;
        private IGeneralTypeUniqueIdentifier _asynchronousTaskOfT;
        private IGeneralTypeUniqueIdentifier _compilerGeneratedMetadatum;
        private IGeneralTypeUniqueIdentifier _delegate;
        private IGeneralTypeUniqueIdentifier _multicastDelegate;
        private IGeneralTypeUniqueIdentifier _nullableBaseType;
        private IGeneralTypeUniqueIdentifier _nullableType;
        private IGeneralTypeUniqueIdentifier _paramArrayMetadatum;
        private IGeneralTypeUniqueIdentifier _rootMetadatum;

        // {System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a}
        //{(00:00:00.0819367, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=5c561934e089, C:\Windows\Microsoft.NET\Framework\v1.1.4322\mscorlib.dll)}
        private const string fr86 = "Framework";
        private const string fr64 = "Framework64";
        const string s_assembly = "assembly";
        const string s_gacEarly = "GAC";
        const string s_gacAny = "GAC_MSIL";
        const string s_gac32 = "GAC_32";
        const string s_gac64 = "GAC_64";


        private static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv1 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", TypeSystemIdentifiers.GetVersion(1, 0, 3300, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        private static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv1_1 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", TypeSystemIdentifiers.GetVersion(1, 0, 5000, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        private static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv2 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", TypeSystemIdentifiers.GetVersion(2, 0, 0, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        private static readonly IAssemblyUniqueIdentifier mscorlibIdentifierv4 = TypeSystemIdentifiers.GetAssemblyIdentifier("mscorlib", TypeSystemIdentifiers.GetVersion(4, 0, 0, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        private static readonly IAssemblyUniqueIdentifier systemIdentifierV1 = TypeSystemIdentifiers.GetAssemblyIdentifier("System", TypeSystemIdentifiers.GetVersion(1, 0, 3300, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        private static readonly IAssemblyUniqueIdentifier systemIdentifierV1_1 = TypeSystemIdentifiers.GetAssemblyIdentifier("System", TypeSystemIdentifiers.GetVersion(1, 0, 5000, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        private static readonly IAssemblyUniqueIdentifier systemIdentifierV2 = TypeSystemIdentifiers.GetAssemblyIdentifier("System", TypeSystemIdentifiers.GetVersion(2, 0, 0, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);
        private static readonly IAssemblyUniqueIdentifier systemIdentifierV4 = TypeSystemIdentifiers.GetAssemblyIdentifier("System", TypeSystemIdentifiers.GetVersion(4, 0, 0, 0), CultureIdentifiers.None, StrongNameKeyPairHelper.StandardPublicKeyToken);

        private List<string> additionalResolutionPaths;

        private object syncObject = new object();

        /// <summary>
        /// Creates a new <see cref="CliRuntimeEnvironmentInfo"/> with the <paramref name="resolveCurrent"/> flag,
        /// <paramref name="platform"/>, <paramref name="version"/>, and whether to <paramref name="useCoreLibrary"/>.
        /// </summary>
        /// <param name="resolveCurrent">Whether to use the current directory via <see cref="Directory.GetCurrentDirectory()"/>.</param>
        /// <param name="platform">The <see cref="CliFrameworkPlatform"/> to target.</param>
        /// <param name="version">The <see cref="CliFrameworkVersion"/> to target.</param>
        /// <param name="useCoreLibrary">Whether to use the core library, typically mscorlib.</param>
        internal CliRuntimeEnvironmentInfo(bool resolveCurrent, CliFrameworkPlatform platform, CliFrameworkVersion version, bool useCoreLibrary, bool useGlobalAccessCache)
        {
            this.UseCoreLibrary = useCoreLibrary;
            this.ResolveCurrent = resolveCurrent;
            this.Platform = platform;
            this.Version = version;
            this.UseGlobalAccessCache = useGlobalAccessCache;
        }

        internal CliRuntimeEnvironmentInfo(bool resolveCurrent, CliFrameworkPlatform platform, CliFrameworkVersion version, string[] additionalResolutionPaths, bool useCoreLibrary, bool useGlobalAccessCache)
            : this(resolveCurrent, platform, version, useCoreLibrary, useGlobalAccessCache)
        {
            if (additionalResolutionPaths != null)
                this.additionalResolutionPaths = new List<string>(additionalResolutionPaths);
        }

        //#region ICliRuntimeEnvironmentInfo Members

        public bool ResolveCurrent { get; protected set; }

        public CliFrameworkPlatform Platform { get; protected set; }

        public CliFrameworkVersion Version { get; protected set; }

        //#endregion

        //#region ICliRuntimeEnvironmentInfo Members

        public IEnumerable<DirectoryInfo> ResolutionPaths
        {
            get
            {
                return GetDirectoryInformationSet(this.GetResolutionPaths());
            }
        }

        private IEnumerable<DirectoryInfo> GetDirectoryInformationSet(IEnumerable<string> paths)
        {
            foreach (var path in paths)
                if (Directory.Exists(path))
                    yield return new DirectoryInfo(path);
        }

        private IEnumerable<string> GetResolutionPaths()
        {
            if (!UseCoreLibrary)
            {
                if (ResolveCurrent)
                    yield return Directory.GetCurrentDirectory();
                string[] rdCopy;
                lock (this.syncObject)
                    if (this.additionalResolutionPaths != null)
                        rdCopy = additionalResolutionPaths.ToArray();
                    else
                        rdCopy = null;
                if (rdCopy != null && rdCopy.Length > 0)
                    foreach (var path in rdCopy)
                        yield return path;
            }
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
                    case CliFrameworkVersion.v1_0_3705:
                        if (Platform == CliFrameworkPlatform.x64Platform)
                            break;
                        parts[vIndex] = CliCommon.VersionString_1_0_3705;
                        yield return gdi(parts);
                        break;
                    case CliFrameworkVersion.v1_1_4322:
                        if (Platform == CliFrameworkPlatform.x64Platform)
                            break;
                        parts[vIndex] = CliCommon.VersionString_1_1_4322;
                        yield return gdi(parts);
                        break;
                    case CliFrameworkVersion.v2_0_50727:
                        parts[vIndex] = CliCommon.VersionString_2_0_50727;
                        yield return gdi(parts);
                        break;
                    case CliFrameworkVersion.v3_0:
                        parts[vIndex] = CliCommon.VersionString_3_0;
                        yield return gdi(parts);
                        yield return gdi(parts.Add("WPF"));
                        goto case CliFrameworkVersion.v2_0_50727;
                    case CliFrameworkVersion.v3_5:
                    case CliFrameworkVersion.v3_5 | CliFrameworkVersion.ClientProfile:
                        parts[vIndex] = CliCommon.VersionString_3_5;
                        yield return gdi(parts);
                        goto case CliFrameworkVersion.v3_0;
                    case CliFrameworkVersion.v4_0_30319:
                    case CliFrameworkVersion.v4_0_30319 | CliFrameworkVersion.ClientProfile:
                        parts[vIndex] = CliCommon.VersionString_4_0_30319;
                        yield return gdi(parts);
                        yield return gdi(parts.Add("WPF"));
                        break;
                    case CliFrameworkVersion.v4_5:
                        parts[vIndex] = CliCommon.VersionString_4_5;
                        yield return gdi(parts);
                        break;
                    case CliFrameworkVersion.v4_6:
                        parts[vIndex] = CliCommon.VersionString_4_6;
                        yield return gdi(parts);
                        break;
                }
            }
            else
                yield return runtimeDir;
            if (UseCoreLibrary)
            {
                if (ResolveCurrent)
                    yield return Directory.GetCurrentDirectory();
                string[] rdCopy;
                lock (this.syncObject)
                    if (this.additionalResolutionPaths != null)
                        rdCopy = additionalResolutionPaths.ToArray();
                    else
                        rdCopy = null;
                if (rdCopy != null && rdCopy.Length > 0)
                    foreach (var path in rdCopy)
                        yield return path;
            }
        }

        private void AdjustFrameworkPath(string[] parts, int frIndex)
        {
            switch (Platform)
            {
                case CliFrameworkPlatform.x86Platform:
                    parts[frIndex] = fr86;
                    break;
                case CliFrameworkPlatform.x64Platform:
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
                        case CliCommon.VersionString_1_1_4322:
                        case CliCommon.VersionString_1_0_3705:
                        case CliCommon.VersionString_2_0_50727:
                        case CliCommon.VersionString_3_0:
                        case CliCommon.VersionString_3_5:
                        case CliCommon.VersionString_4_0_30319:
                        case CliCommon.VersionString_4_5:
                        case CliCommon.VersionString_4_6:
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
                switch (Version & ~CliFrameworkVersion.ClientProfile)
                {
                    case CliFrameworkVersion.v1_0_3705:
                        return mscorlibIdentifierv1;
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
                return null;
            }
        }

        //#endregion

        private static string gdi(string[] parts)
        {
            return string.Join(Path.DirectorySeparatorChar.ToString(), parts);
        }

        //#region ICliRuntimeEnvironmentInfo Members


        public bool UseGlobalAccessCache { get; protected set; }

        private IEnumerable<string> _gacLocationFor(IAssemblyUniqueIdentifier uniqueIdentifier)
        {
            string wFldr = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            string pktHex = uniqueIdentifier.PublicKeyToken.FormatHexadecimal();
            switch (this.Version & ~CliFrameworkVersion.ClientProfile)
            {
                case CliFrameworkVersion.v1_0_3705:
                case CliFrameworkVersion.v1_1_4322:
                    if (Platform == CliFrameworkPlatform.x64Platform)
                        break;
                    yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gacEarly, uniqueIdentifier.Name, uniqueIdentifier.Version, pktHex);
                    break;
                case CliFrameworkVersion.v2_0_50727:
                case CliFrameworkVersion.v3_0:
                case CliFrameworkVersion.v3_5:
                    switch (Platform)
                    {
                        case CliFrameworkPlatform.x86Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gac32, uniqueIdentifier.Name, uniqueIdentifier.Version, pktHex);
                            break;
                        case CliFrameworkPlatform.x64Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gac64, uniqueIdentifier.Name, uniqueIdentifier.Version, pktHex);
                            break;
                    }
                    yield return string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}__{6}", wFldr, Path.DirectorySeparatorChar, s_assembly, s_gacAny, uniqueIdentifier.Name, uniqueIdentifier.Version, pktHex);
                    goto case CliFrameworkVersion.v1_0_3705;
                case CliFrameworkVersion.v4_6:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_0_30319:
                    string fVer = "v4.0";//(Version & ~CliFrameworkVersion.ClientProfile) == CliFrameworkVersion.v4_0_30319 ? "v4.0" : "v4.5";
                    string runtimeDir;
                    string[] parts;
                    int frIndex;
                    int vIndex;
                    GetFrameworkVersionAndPlatform(out runtimeDir, out parts, out frIndex, out vIndex);
                    parts = parts.Take(parts.Length - 2).Concat(new[] { s_assembly }).ToArray();
                    switch (Platform)
                    {
                        case CliFrameworkPlatform.x86Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}_{5}__{6}", string.Join(Path.DirectorySeparatorChar.ToString(), parts), Path.DirectorySeparatorChar, s_gac32, uniqueIdentifier.Name, fVer, uniqueIdentifier.Version, pktHex);
                            break;
                        case CliFrameworkPlatform.x64Platform:
                            yield return string.Format("{0}{1}{2}{1}{3}{1}{4}_{5}__{6}", string.Join(Path.DirectorySeparatorChar.ToString(), parts), Path.DirectorySeparatorChar, s_gac64, uniqueIdentifier.Name, fVer, uniqueIdentifier.Version, pktHex);
                            break;
                        default:
#if x86
                            /* *
                             * Logic: if someone is requesting the ANY platform, the
                             * x86 platform is chosen because the x86 variant of the
                             * library is used.
                             * */
                            goto case CliFrameworkPlatform.x86Platform;
#elif x64
                            goto case CliFrameworkPlatform.x64Platform;
#endif
                            break;
                    }
                    yield return string.Format("{0}{1}{2}{1}{3}{1}{4}_{5}__{6}", string.Join(Path.DirectorySeparatorChar.ToString(), parts), Path.DirectorySeparatorChar, s_gacAny, uniqueIdentifier.Name, fVer, uniqueIdentifier.Version, pktHex);
                    goto case CliFrameworkVersion.v3_5;
            }

        }

        public IEnumerable<DirectoryInfo> GetGacLocationsFor(IAssemblyUniqueIdentifier uniqueIdentifier)
        {
            return this.GetDirectoryInformationSet(_gacLocationFor(uniqueIdentifier));
        }

        public bool UseCoreLibrary { get; protected set; }

        //#endregion

        //#region IStandardRuntimeEnvironmentInfo Members

        public IGeneralTypeUniqueIdentifier ArrayType
        {
            get
            {
                if (this.UseCoreLibrary)
                    return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Array", 0);
                else
                    return TypeSystemIdentifiers.GetTypeIdentifier("System", "Array", 0);
            }
        }

        public IGeneralTypeUniqueIdentifier AsynchronousTask
        {
            get
            {
                return this._asynchronousTask ?? (this._asynchronousTask = GetAsynchronousTaskIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetAsynchronousTaskIdentifier()
        {
            switch (this.Version & CliFrameworkVersion.VersionMask)
            {
                case CliFrameworkVersion.v1_0_3705:
                case CliFrameworkVersion.v1_1_4322:
                case CliFrameworkVersion.v2_0_50727:
                case CliFrameworkVersion.v3_0:
                case CliFrameworkVersion.v3_5:
                    return null;
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_6:
                    if (this.UseCoreLibrary)
                        return this.CoreLibraryIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 0);
                    else
                        return TypeSystemIdentifiers.GetTypeIdentifier("System.Threading.Tasks", "Task", 0);
            }
            return null;
        }

        public IGeneralTypeUniqueIdentifier AsynchronousTaskOfT
        {
            get
            {
                return this._asynchronousTaskOfT ?? (this._asynchronousTaskOfT = GetAsynchronousTaskOfTIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetAsynchronousTaskOfTIdentifier()
        {
            switch (this.Version & CliFrameworkVersion.VersionMask)
            {
                case CliFrameworkVersion.v1_0_3705:
                case CliFrameworkVersion.v1_1_4322:
                    return null;
                case CliFrameworkVersion.v2_0_50727:
                case CliFrameworkVersion.v3_0:
                case CliFrameworkVersion.v3_5:
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_6:
                    if (this.UseCoreLibrary)
                        return this.CoreLibraryIdentifier.GetTypeIdentifier("System.Threading.Tasks", "Task", 1);
                    else
                        return TypeSystemIdentifiers.GetTypeIdentifier("System.Threading.Tasks", "Task", 1);
            }
            return null;
        }

        public IGeneralTypeUniqueIdentifier Byte
        {
            get
            {
                return this._byte ?? (this._byte = GetByteIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetByteIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Byte", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Byte", 0);
        }

        public IGeneralTypeUniqueIdentifier CompilerGeneratedMetadatum
        {
            get
            {
                return this._compilerGeneratedMetadatum ?? (this._compilerGeneratedMetadatum = GetCompilerGeneratedMetadatum());
            }
        }

        private IGeneralTypeUniqueIdentifier GetCompilerGeneratedMetadatum()
        {
            switch (this.Version & CliFrameworkVersion.VersionMask)
            {
                case CliFrameworkVersion.v1_0_3705:
                case CliFrameworkVersion.v1_1_4322:
                    return null;
                case CliFrameworkVersion.v2_0_50727:
                case CliFrameworkVersion.v3_0:
                case CliFrameworkVersion.v3_5:
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_6:
                    if (this.UseCoreLibrary)
                        return this.CoreLibraryIdentifier.GetTypeIdentifier("System.Runtime.CompilerServices", "CompilerGeneratedAttribute", 0);
                    else
                        return TypeSystemIdentifiers.GetTypeIdentifier("System.Runtime.CompilerServices", "CompilerGeneratedAttribute", 0);
            }
            return null;
        }

        public IGeneralTypeUniqueIdentifier RootEnum
        {
            get
            {
                return this._rootEnum ?? (this._rootEnum = GetRootEnumIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetRootEnumIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Enum", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Enum", 0);
        }

        public IGeneralTypeUniqueIdentifier Int16
        {
            get
            {
                return this._int16 ?? (this._int16 = GetInt16Identifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetInt16Identifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Int16", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Int16", 0);
        }

        public IGeneralTypeUniqueIdentifier Int32
        {
            get
            {
                return this._int32 ?? (this._int32 = GetInt32Identifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetInt32Identifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Int32", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Int32", 0);
        }

        public IGeneralTypeUniqueIdentifier Int64
        {
            get
            {
                return this._int64 ?? (this._int64 = GetInt64Identifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetInt64Identifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Int64", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Int64", 0);
        }

        public IGeneralTypeUniqueIdentifier NullableBaseType
        {
            get
            {
                return this._nullableBaseType ?? (this._nullableBaseType = GetNullableBaseTypeIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetNullableBaseTypeIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.RootStruct;
            return null;
        }

        public IGeneralTypeUniqueIdentifier NullableType
        {
            get
            {
                return this._nullableType ?? (this._nullableType = GetNullableTypeIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetNullableTypeIdentifier()
        {
            switch (this.Version & CliFrameworkVersion.VersionMask)
            {
                case CliFrameworkVersion.v1_0_3705:
                case CliFrameworkVersion.v1_1_4322:
                    return null;
                case CliFrameworkVersion.v2_0_50727:
                case CliFrameworkVersion.v3_0:
                case CliFrameworkVersion.v3_5:
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_6:
                    if (this.UseCoreLibrary)
                        return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Nullable", 1);
                    else
                        return TypeSystemIdentifiers.GetTypeIdentifier("System", "Nullable", 1);
            }
            return null;
        }

        public IGeneralTypeUniqueIdentifier RootType
        {
            get
            {
                return this._rootType ?? (this._rootType = GetRootTypeIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetRootTypeIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Object", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Object", 0);
        }

        public IGeneralTypeUniqueIdentifier SByte
        {
            get
            {
                return this._sByte ?? (this._sByte = GetSByteIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetSByteIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "SByte", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "SByte", 0);
        }

        public IGeneralTypeUniqueIdentifier UInt16
        {
            get
            {
                return this._uInt16 ?? (this._uInt16 = GetUInt16Identifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetUInt16Identifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "UInt16", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "UInt16", 0);
        }

        public IGeneralTypeUniqueIdentifier UInt32
        {
            get
            {
                return this._uInt32 ?? (this._uInt32 = GetUInt32Identifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetUInt32Identifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "UInt32", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "UInt32", 0);
        }

        public IGeneralTypeUniqueIdentifier UInt64
        {
            get
            {
                return this._uInt64 ?? (this._uInt64 =  GetUInt64Identifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetUInt64Identifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "UInt64", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "UInt64", 0);
        }

        public IGeneralTypeUniqueIdentifier RootStruct
        {
            get
            {
                return this._rootStruct ?? (this._rootStruct = GetRootStructIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetRootStructIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "ValueType", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "ValueType", 0);
        }

        public IGeneralTypeUniqueIdentifier VoidType
        {
            get
            {
                return this._voidType ?? (this._voidType = GetVoidTypeIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetVoidTypeIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Void", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Void", 0);
        }

        public IGeneralTypeUniqueIdentifier TypeType
        {
            get
            {
                return this._typeType ?? (this._typeType = GetTypeTypeIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetTypeTypeIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Type", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Type", 0);
        }

        public IGeneralTypeUniqueIdentifier Delegate
        {
            get
            {
                return this._delegate ?? (this._delegate = GetDelegateIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetDelegateIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Delegate", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Delegate", 0);
        }

        public IGeneralTypeUniqueIdentifier MulticastDelegate
        {
            get
            {
                return this._multicastDelegate ?? (this._multicastDelegate = GetMulticastDelegateIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetMulticastDelegateIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "MulticastDelegate", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "MulticastDelegate", 0);
        }

        public IGeneralTypeUniqueIdentifier Boolean
        {
            get
            {
                return this._boolean ?? (this._boolean = GetBooleanIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetBooleanIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Boolean", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Boolean", 0);
        }

        public IGeneralTypeUniqueIdentifier Decimal
        {
            get
            {
                return this._decimal ?? (this._decimal = GetDecimalIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetDecimalIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Decimal", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Decimal", 0);
        }

        public IGeneralTypeUniqueIdentifier Single
        {
            get
            {
                return this._single ?? (this._single = GetSingleIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetSingleIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Single", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Single", 0);
        }

        public IGeneralTypeUniqueIdentifier Double
        {
            get
            {
                return this._double ?? (this._double = GetDoubleIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetDoubleIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Double", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Double", 0);
        }

        public IGeneralTypeUniqueIdentifier Char
        {
            get
            {
                return this._char ?? (this._char = GetCharIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetCharIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Char", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Char", 0);
        }

        public IGeneralTypeUniqueIdentifier String
        {
            get
            {
                return this._string ?? (this._string = GetStringIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetStringIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "String", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "String", 0);
        }
        //#endregion


        #region IStandardRuntimeEnvironmentInfo Members


        public IGeneralTypeUniqueIdentifier GetCoreIdentifier(RuntimeCoreType coreType)
        {
            switch (coreType)
            {
                case RuntimeCoreType.Array:
                    return ArrayType;
                case RuntimeCoreType.Boolean:
                    return Boolean;
                case RuntimeCoreType.Decimal:
                    return Decimal;
                case RuntimeCoreType.Single:
                    return Single;
                case RuntimeCoreType.Double:
                    return Double;
                case RuntimeCoreType.SByte:
                    return SByte;
                case RuntimeCoreType.Byte:
                    return Byte;
                case RuntimeCoreType.Char:
                    return Char;
                case RuntimeCoreType.RootEnum:
                    return RootEnum;
                case RuntimeCoreType.Int16:
                    return Int16;
                case RuntimeCoreType.UInt16:
                    return UInt16;
                case RuntimeCoreType.Int32:
                    return Int32;
                case RuntimeCoreType.UInt32:
                    return UInt32;
                case RuntimeCoreType.Int64:
                    return Int64;
                case RuntimeCoreType.UInt64:
                    return UInt64;
                case RuntimeCoreType.RootType:
                    return RootType;
                case RuntimeCoreType.String:
                    return String;
                case RuntimeCoreType.RootStruct:
                    return RootStruct;
                case RuntimeCoreType.VoidType:
                    return VoidType;
                case RuntimeCoreType.Type:
                    return TypeType;
                default:
                    throw new ArgumentOutOfRangeException("coreType");
            }
        }

        #endregion

        #region ICliRuntimeEnvironmentInfo Members

        public IGeneralTypeUniqueIdentifier GetCoreIdentifier(CliRuntimeCoreType coreType, IAssembly assembly)
        {
            ICliRuntimeEnvironmentInfo currentRuntime;
            if (assembly is ICliAssembly)
                currentRuntime = ((ICliAssembly)(assembly)).RuntimeEnvironment;
            else
                currentRuntime = this;
            switch (coreType)
            {
                case CliRuntimeCoreType.RootMetadatum:
                    return currentRuntime.RootMetadatum;
                case CliRuntimeCoreType.AsynchronousTask:
                    return currentRuntime.AsynchronousTask;
                case CliRuntimeCoreType.AsynchronousTaskOfT:
                    return currentRuntime.AsynchronousTaskOfT;
                case CliRuntimeCoreType.CompilerGeneratedMetadatum:
                    return currentRuntime.CompilerGeneratedMetadatum;
                case CliRuntimeCoreType.Delegate:
                    return currentRuntime.Delegate;
                case CliRuntimeCoreType.MulticastDelegate:
                    return currentRuntime.MulticastDelegate;
                case CliRuntimeCoreType.NullableBaseType:
                    return currentRuntime.NullableBaseType;
                case CliRuntimeCoreType.NullableType:
                    return currentRuntime.NullableType;
                case CliRuntimeCoreType.ParamArrayMetadatum:
                    return currentRuntime.ParamArrayMetadatum;
                case CliRuntimeCoreType.ExtensionMetadatum:
                    return currentRuntime.ExtensionMetadatum;
                default:
                    throw new ArgumentOutOfRangeException("coreType");
            }
        }

        #endregion

        public IGeneralTypeUniqueIdentifier ParamArrayMetadatum
        {
            get
            {
                return this._paramArrayMetadatum ?? (this._paramArrayMetadatum = GetParamArrayMetadatumIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetParamArrayMetadatumIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "ParamArrayAttribute", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "ParamArrayAttribute", 0);
        }

        public IGeneralTypeUniqueIdentifier RootMetadatum
        {
            get
            {
                return this._rootMetadatum ?? (this._rootMetadatum = GetRootMetadatumIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetRootMetadatumIdentifier()
        {
            if (this.UseCoreLibrary)
                return this.CoreLibraryIdentifier.GetTypeIdentifier("System", "Attribute", 0);
            else
                return TypeSystemIdentifiers.GetTypeIdentifier("System", "Attribute", 0);
        }

        public IEnumerable<string> AdditionalPaths
        {
            get
            {
                string[] rdCopy = null;
                lock (this.syncObject)
                    if (this.additionalResolutionPaths != null)
                        rdCopy = this.additionalResolutionPaths.ToArray();
                if (rdCopy == null)
                    yield break;
                foreach (var path in rdCopy)
                    yield return path;
            }
        }

        public IGeneralTypeUniqueIdentifier ExtensionMetadatum
        {
            get
            {
                return this._extensionMetadatum ?? (this._extensionMetadatum = GetExtensionMetadatumIdentifier());
            }
        }

        private IGeneralTypeUniqueIdentifier GetExtensionMetadatumIdentifier()
        {
            switch (this.Version & CliFrameworkVersion.VersionMask)
            {
                case CliFrameworkVersion.v1_0_3705:
                case CliFrameworkVersion.v1_1_4322:
                case CliFrameworkVersion.v2_0_50727:
                case CliFrameworkVersion.v3_0:
                    return null;
                case CliFrameworkVersion.v3_5:
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_6:
                    if (this.UseCoreLibrary)
                        return this.CoreLibraryIdentifier.GetTypeIdentifier("System.Runtime.CompilerServices", "ExtensionAttribute", 0);
                    else
                        return TypeSystemIdentifiers.GetTypeIdentifier("System.Runtime.CompilerServices", "ExtensionAttribute", 0);
            }
            return null;
        }


        public bool SupportsUnsignedTypes
        {
            get
            {
                return true;
            }
        }

        protected virtual void AddResolutionPath(string path)
        {
            if (Directory.Exists(path))
                lock (syncObject)
                {
                    if (this.additionalResolutionPaths == null)
                        this.additionalResolutionPaths = new List<string>();
                    this.additionalResolutionPaths.Add(path);
                }
        }

        protected virtual bool RemoveResolutionPath(string path)
        {
            lock (this.syncObject)
            {
                if (this.additionalResolutionPaths == null)
                    return false;
                return additionalResolutionPaths.Remove(path);
            }
        }
    }
}
