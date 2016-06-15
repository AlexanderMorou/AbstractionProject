using AllenCopeland.Abstraction._Internal.Utilities.Security;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
//using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public class StandardLibraryIdentifiers
    {
        private const string systemLibName = "System";
        private const string systemCoreLibName = "System.Core";
        private const string systemXmlLibName = "System.Xml";
        private const string systemWindowsFormsLibName = "System.Windows.Forms";
        private const string microsoftVisualBasicLibName = "Microsoft.VisualBasic";
        private static readonly IVersion vbV7 = TypeSystemIdentifiers.GetVersion(7, CliGateway.v1Version.Minor, CliGateway.v1Version.Build, CliGateway.v1Version.Revision);
        private static readonly IVersion vbV7_1 = TypeSystemIdentifiers.GetVersion(7, CliGateway.v1_1Version.Minor, CliGateway.v1_1Version.Build, CliGateway.v1_1Version.Revision);
        private static readonly IVersion vbV8 = TypeSystemIdentifiers.GetVersion(8, 0, 0, 0);
        private static readonly IVersion vbV10 = TypeSystemIdentifiers.GetVersion(10, 0, 0, 0);
        public static IAssemblyUniqueIdentifier GetVersionedCommonLanguageRuntimeLibrary(CliFrameworkVersion frameworkVersion = CliFrameworkVersion.CurrentVersion)
        {
            return CliGateway.GetCoreLibraryIdentifier(frameworkVersion);
        }

        public static IAssemblyUniqueIdentifier CommonLanguageRuntimeLibrary { get { return GetVersionedCommonLanguageRuntimeLibrary(); } }

        public static IAssemblyUniqueIdentifier System
        {
            get
            {
                return GetVersionedSystemLibrary();
            }
        }

        public static IAssemblyUniqueIdentifier SystemCore
        {
            get
            {
                return GetVersionedSystemCoreLibrary();
            }
        }

        public static IAssemblyUniqueIdentifier MicrosoftVisualBasic { get { return GetVersionedMicrosoftVisualBasicLibrary(); } }

        public static IAssemblyUniqueIdentifier GetVersionedMicrosoftVisualBasicLibrary(CliFrameworkVersion version)
        {
            switch (version & ~CliFrameworkVersion.ClientProfile)
            {
                case CliFrameworkVersion.v1_0_3705:
                    return GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.Version07);
                case CliFrameworkVersion.v1_1_4322:
                    return GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.Version07_1);
                case CliFrameworkVersion.v2_0_50727:
                    return GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.Version08);
                case CliFrameworkVersion.v3_0:
                case CliFrameworkVersion.v3_5:
                    return GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.Version09);
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                    return GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.Version10);
                case CliFrameworkVersion.v4_6:
                    return GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.Version12);
            }
            return GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion.CurrentVersion);
        }

        public static IAssemblyUniqueIdentifier GetVersionedMicrosoftVisualBasicLibrary(VisualBasicVersion languageVersion = VisualBasicVersion.CurrentVersion)
        {
            switch (languageVersion)
            {
                case VisualBasicVersion.Version07:
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(microsoftVisualBasicLibName, vbV7, CultureIdentifiers.None, MicrosoftLanguageVendor.stdLibPublicKeyToken);
                case VisualBasicVersion.Version07_1:
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(microsoftVisualBasicLibName, vbV7_1, CultureIdentifiers.None, MicrosoftLanguageVendor.stdLibPublicKeyToken);
                case VisualBasicVersion.Version08:
                case VisualBasicVersion.Version09:
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(microsoftVisualBasicLibName, vbV8, CultureIdentifiers.None, MicrosoftLanguageVendor.stdLibPublicKeyToken);
                case VisualBasicVersion.Version10:
                case VisualBasicVersion.Version11:
                case VisualBasicVersion.Version12:
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(microsoftVisualBasicLibName, vbV10, CultureIdentifiers.None, MicrosoftLanguageVendor.stdLibPublicKeyToken);
                default:
                    goto case VisualBasicVersion.CurrentVersion;
            }
        }

        public static IAssemblyUniqueIdentifier SystemXml { get { return GetVersionedSystemXmlLibrary(); } }

        public static IAssemblyUniqueIdentifier SystemWindowsForms { get { return GetVersionedSystemWindowsFormsLibrary(); } }

        public static IAssemblyUniqueIdentifier GetVersionedSystemWindowsFormsLibrary(CliFrameworkVersion frameworkVersion = CliFrameworkVersion.CurrentVersion)
        {
            return GetVersionedStandardLibrary(systemWindowsFormsLibName, frameworkVersion, CliFrameworkVersion.v1_0_3705, true);
        }

        public static IAssemblyUniqueIdentifier GetVersionedSystemXmlLibrary(CliFrameworkVersion frameworkVersion = CliFrameworkVersion.CurrentVersion)
        {
            return GetVersionedStandardLibrary(systemXmlLibName, frameworkVersion, CliFrameworkVersion.v1_0_3705, true);
        }

        public static IAssemblyUniqueIdentifier GetVersionedSystemCoreLibrary(CliFrameworkVersion frameworkVersion = CliFrameworkVersion.CurrentVersion)
        {
            return GetVersionedStandardLibrary(systemCoreLibName, frameworkVersion, CliFrameworkVersion.v3_5, true);
        }

        private static IAssemblyUniqueIdentifier GetVersionedStandardLibrary(string libraryName, CliFrameworkVersion version, CliFrameworkVersion introducedVersion, bool ecmaLibrary, byte[] alternatePublicKey = null, IVersion[] alternateVersions = null)
        {
            switch (version & CliFrameworkVersion.VersionMask)
            {
                case CliFrameworkVersion.v1_0_3705:
                    switch (introducedVersion & CliFrameworkVersion.VersionMask)
                    {
                        case CliFrameworkVersion.v1_1_4322:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v1_1Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v2_0_50727:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v2Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v3_0:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v3_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_0_30319:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_6:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_6Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                    }
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v1Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                case CliFrameworkVersion.v1_1_4322:
                    switch (introducedVersion & CliFrameworkVersion.VersionMask)
                    {
                        case CliFrameworkVersion.v2_0_50727:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v2Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v3_0:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v3_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_0_30319:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_6:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_6Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                    }
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v1_1Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                case CliFrameworkVersion.v2_0_50727:
                    switch (introducedVersion & CliFrameworkVersion.VersionMask)
                    {
                        case CliFrameworkVersion.v3_0:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v3_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_0_30319:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_6:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_6Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                    }
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v2Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                case CliFrameworkVersion.v3_0:
                    switch (introducedVersion & CliFrameworkVersion.VersionMask)
                    {
                        case CliFrameworkVersion.v1_0_3705:
                        case CliFrameworkVersion.v1_1_4322:
                        case CliFrameworkVersion.v2_0_50727:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v2Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v3_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_0_30319:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_6:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_6Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                    }
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                case CliFrameworkVersion.v3_5:
                    switch (introducedVersion & CliFrameworkVersion.VersionMask)
                    {
                        case CliFrameworkVersion.v1_0_3705:
                        case CliFrameworkVersion.v1_1_4322:
                        case CliFrameworkVersion.v2_0_50727:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v2Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v3_0:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_0_30319:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_5:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                        case CliFrameworkVersion.v4_6:
                            return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_6Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                    }
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v3_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                case CliFrameworkVersion.v4_0_30319:
                case CliFrameworkVersion.v4_5:
                case CliFrameworkVersion.v4_6:
                    if ((introducedVersion & CliFrameworkVersion.VersionMask) == CliFrameworkVersion.v4_6)
                        return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_6Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                    else if ((introducedVersion & CliFrameworkVersion.VersionMask) == CliFrameworkVersion.v4_5)
                        return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4_5Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
                    return TypeSystemIdentifiers.GetAssemblyIdentifier(libraryName, CliGateway.v4Version, CultureIdentifiers.None, ecmaLibrary ? StrongNameKeyPairHelper.StandardPublicKeyToken : alternatePublicKey);
            }
            /* *
                * If the version doesn't match the valid set of values,
                * yield the introduced version by a recursive call.
                * */
            if (version == introducedVersion)
                throw new ArgumentOutOfRangeException("No version of introducedVersion yielded a library.", "introducedVersion");
            return GetVersionedStandardLibrary(libraryName, introducedVersion, introducedVersion, ecmaLibrary);
        }

        public static IAssemblyUniqueIdentifier GetVersionedSystemLibrary(CliFrameworkVersion frameworkVersion = CliFrameworkVersion.CurrentVersion)
        {
            return GetVersionedStandardLibrary(systemLibName, frameworkVersion, CliFrameworkVersion.v1_0_3705, true);
        }

    }
}
