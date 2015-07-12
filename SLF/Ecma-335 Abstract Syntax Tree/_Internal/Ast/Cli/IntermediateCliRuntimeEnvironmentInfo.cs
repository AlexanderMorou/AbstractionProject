using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Cli
{
    /// <summary>
    /// Provides a base instance for runtime environment information for the 
    /// Common Language Infrastructure (ECMA-335) within an intermediate
    /// format.
    /// </summary>
    internal class IntermediateCliRuntimeEnvironmentInfo :
        CliRuntimeEnvironmentInfo,
        IIntermediateCliRuntimeEnvironmentInfo
    {
        public string ApplicationDirectory { get; set; }
        /// <summary>
        /// Returns whether to use the global access cache.
        /// </summary>
        public new bool UseGlobalAccessCache { get { return base.UseGlobalAccessCache; } set { base.UseGlobalAccessCache = value; } }
        /// <summary>
        /// Returns the framework platform identity resolution
        /// occurs on.
        /// </summary>
        public new CliFrameworkPlatform Platform { get { return base.Platform; } set { base.Platform = value; } }
        /// <summary>
        /// Returns whether the core library identified by
        /// <see cref="CoreLibraryIdentifier"/> is present.
        /// </summary>
        /// <remarks>If false, the <see cref="CoreLibraryIdentifier"/>
        /// will return null, type identity resolution from it
        /// will be unavailable.</remarks>
        public new bool UseCoreLibrary { get { return base.UseCoreLibrary; } set { base.UseCoreLibrary = value; } }
        /// <summary>
        /// Returns the <see cref="CliFrameworkVersion"/> on which identity
        /// resolution occurs.
        /// </summary>
        public new CliFrameworkVersion Version { get { return base.Version; } set { base.Version = value; } }
        /// <summary>
        /// Creates a new <see cref="IntermediateCliRuntimeEnvironmentInfo"/>
        /// instance with the <paramref name="resolveCurrent"/>, <paramref name="platform"/>,
        /// <paramref name="version"/>, <paramref name="useCoreLibrary"/>, and
        /// <paramref name="useGlobalAccessCache"/> provided.
        /// </summary>
        /// <param name="resolveCurrent">
        /// A <see cref="Boolean"/> value denoting whether to resolve identities
        /// within the <see cref="ApplicationDirectory"/>.</param>
        /// <param name="platform">A <see cref="CliFrameworkPlatform"/> value
        /// denoting the base platform to use.</param>
        /// <param name="version">A <see cref="CliFrameworkVersion"/> denoting the
        /// target framework.</param>
        /// <param name="useCoreLibrary">A <see cref="Boolean"/> value denoting
        /// whether to use the core library identifier to resolve standard identities.
        /// </param>
        /// <param name="useGlobalAccessCache">A <see cref="Boolean"/>
        /// value denoting whether to use the global access cache.</param>
        public IntermediateCliRuntimeEnvironmentInfo(
            bool resolveCurrent, CliFrameworkPlatform platform, CliFrameworkVersion version,
            bool useCoreLibrary, bool useGlobalAccessCache)
            : base(resolveCurrent, platform, version, useCoreLibrary, useGlobalAccessCache)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateCliRuntimeEnvironmentInfo"/>
        /// instance with the <paramref name="resolveCurrent"/>, <paramref name="platform"/>,
        /// <paramref name="version"/>, <paramref name="additionalResolutionPaths"/>, 
        /// <paramref name="useCoreLibrary"/>, and <paramref name="useGlobalAccessCache"/>
        /// provided.
        /// </summary>
        /// <param name="resolveCurrent">
        /// A <see cref="Boolean"/> value denoting whether to resolve identities
        /// within the <see cref="ApplicationDirectory"/>.</param>
        /// <param name="platform">A <see cref="CliFrameworkPlatform"/> value
        /// denoting the base platform to use.</param>
        /// <param name="version">A <see cref="CliFrameworkVersion"/> denoting the
        /// target framework.</param>
        /// <param name="additionalResolutionPaths">A series of <see cref="String"/>
        /// values denoting the additional paths to perform identity resolution.</param>
        /// <param name="useCoreLibrary">A <see cref="Boolean"/> value denoting
        /// whether to use the core library identifier to resolve standard identities.
        /// </param>
        /// <param name="useGlobalAccessCache">A <see cref="Boolean"/>
        /// value denoting whether to use the global access cache.</param>
        internal IntermediateCliRuntimeEnvironmentInfo(bool resolveCurrent, CliFrameworkPlatform platform, CliFrameworkVersion version, string[] additionalResolutionPaths, bool useCoreLibrary, bool useGlobalAccessCache)
            : base(resolveCurrent, platform, version, additionalResolutionPaths, useCoreLibrary, useGlobalAccessCache)
        {
        }
    }
}
