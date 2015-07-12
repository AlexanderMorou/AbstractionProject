using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    /// <summary>
    /// Defines properties and methods for working with the intermediate
    /// form of a common language infrastructure's runtime environment
    /// info.
    /// </summary>
    /// <remarks>Denotes malleable aspects of a library such as its
    /// platform/runtime version.</remarks>
    public interface IIntermediateCliRuntimeEnvironmentInfo :
        ICliRuntimeEnvironmentInfo
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> value used to resolve
        /// additional libraries relative to the <see cref="ICliRuntimeEnvironmentInfo.ResolveCurrent"/>.
        /// </summary>
        string ApplicationDirectory { get; set; }
        /// <summary>
        /// Returns whether to use the global access cache.
        /// </summary>
        new bool UseGlobalAccessCache { get; set; }
        /// <summary>
        /// Returns the framework platform identity resolution
        /// occurs on.
        /// </summary>
        new CliFrameworkPlatform Platform { get; set; }
        /// <summary>
        /// Returns whether the core library identified by
        /// <see cref="CoreLibraryIdentifier"/> is present.
        /// </summary>
        /// <remarks>If false, the <see cref="CoreLibraryIdentifier"/>
        /// will return null, type identity resolution from it
        /// will be unavailable.</remarks>
        new bool UseCoreLibrary { get; set; }
        /// <summary>
        /// Returns the <see cref="CliFrameworkVersion"/> on which identity
        /// resolution occurs.
        /// </summary>
        new CliFrameworkVersion Version { get; set; }
    }
}
