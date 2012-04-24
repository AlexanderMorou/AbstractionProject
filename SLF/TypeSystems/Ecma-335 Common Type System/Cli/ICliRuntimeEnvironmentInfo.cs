using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// </summary>
    public interface ICliRuntimeEnvironmentInfo :
        IStandardRuntimeEnvironmentInfo
    {
        /// <summary>
        /// Returns whether the <see cref="ICliRuntimeEnvironmentInfo"/> 
        /// should resolve information at the current entry application's
        /// directory.
        /// </summary>
        bool ResolveCurrent { get; }
        /// <summary>
        /// Returns the framework runtimeEnvironment identity resolution
        /// occurs on.
        /// </summary>
        FrameworkPlatform Platform { get; }
        /// <summary>
        /// Returns the <see cref="FrameworkVersion"/> on which identity
        /// resolution occurs.
        /// </summary>
        FrameworkVersion Version { get; }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of
        /// <see cref="DirectoryInfo"/> about the 
        /// <see cref="ICliRuntimeEnvironment"/> which designate where
        /// to look for assembly and type identities.
        /// </summary>
        IEnumerable<DirectoryInfo> ResolutionPaths { get; }
        /// <summary>
        /// Returns whether 
        /// </summary>
        bool UseGlobalAccessCache { get; }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of <see cref="DirectoryInfo"/> structures which
        /// define the possible locations of a given assembly within the Global Access Cache.
        /// </summary>
        /// <param name="uniqueIdentifier">The <see cref="IAssemblyUniqueIdentifier"/> to obtain
        /// the potential GAC locations for.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="DirectoryInfo"/> structures which
        /// define the possible locations of a given assembly within the Global Access Cache.</returns>
        IEnumerable<DirectoryInfo> GetGacLocationsFor(IAssemblyUniqueIdentifier uniqueIdentifier);
    }
}
