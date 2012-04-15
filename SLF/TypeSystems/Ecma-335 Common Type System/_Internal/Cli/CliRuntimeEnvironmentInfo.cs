using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
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
        internal CliRuntimeEnvironmentInfo(bool resolveCurrent, FrameworkPlatform platform, FrameworkVersion version, bool useCoreLibrary)
        {
            this.UseCoreLibrary = useCoreLibrary;
            this.ResolveCurrent = resolveCurrent;
            this.Platform = platform;
            this.Version = version;
        }

        internal CliRuntimeEnvironmentInfo(bool resolveCurrent, FrameworkPlatform platform, FrameworkVersion version, string[] additionalResolutionPaths, bool useCoreLibrary)
            : this(resolveCurrent, platform, version, useCoreLibrary)
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
                var runtimeDir = RuntimeEnvironment.GetRuntimeDirectory();
                var parts = runtimeDir.Split(Path.DirectorySeparatorChar);
                int frIndex = -1, vIndex = -1;

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
                if (frIndex != -1 && vIndex != -1)
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
                            goto case FrameworkVersion.v2_0_50727;
                        case FrameworkVersion.v3_5:
                            parts[vIndex] = v3_5;
                            yield return gdi(parts);
                            goto case FrameworkVersion.v3_0;
                        case FrameworkVersion.v4_0_30319:
                            parts[vIndex] = v4_0_30319;
                            yield return gdi(parts);
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
            get {
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
            get {
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
