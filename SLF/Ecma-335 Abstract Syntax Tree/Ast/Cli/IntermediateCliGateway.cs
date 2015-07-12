using AllenCopeland.Abstraction.Slf._Internal.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    public static class IntermediateCliGateway
    {

        public static IIntermediateCliManager CreateIdentityManager(IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            return new IntermediateCliManager(runtimeEnvironment);
        }

        public static IIntermediateCliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version = CliGateway.CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
        {
            return CreateIdentityManager(IntermediateCliGateway.GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache));
        }

        public static IIntermediateCliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, bool useGlobalAccessCache, params string[] additionalResolutionPaths)
        {
            return CreateIdentityManager(IntermediateCliGateway.GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache, additionalResolutionPaths));
        }

        public static IIntermediateCliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(CliFrameworkPlatform platform, CliFrameworkVersion version = CliFrameworkVersion.CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
        {
            return new IntermediateCliRuntimeEnvironmentInfo(resolveCurrent, platform, version, useCoreLibrary, useGlobalAccessCache);
        }

        public static IIntermediateCliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(CliFrameworkPlatform platform, CliFrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, bool useGlobalAccessCache, params string[] additionalResolutionPaths)
        {
            return new IntermediateCliRuntimeEnvironmentInfo(resolveCurrent, platform, version, additionalResolutionPaths, useCoreLibrary, useGlobalAccessCache);
        }
    }
}
