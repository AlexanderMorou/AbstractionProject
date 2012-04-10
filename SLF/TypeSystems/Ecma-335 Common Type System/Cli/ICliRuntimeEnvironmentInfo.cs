using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// </summary>
    public interface ICliRuntimeEnvironmentInfo
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
        /// Returns the <see cref="IAssemblyUniqueIdentifier"/> of
        /// the core library of the Common Language Infrastructure,
        /// typically mscorlib.
        /// </summary>
        IAssemblyUniqueIdentifier CoreLibraryIdentifier { get; }

        /// <summary>
        /// Returns whether the core library identified by <see cref="CoreLibraryIdentifier"/>
        /// is present.
        /// </summary>
        /// <remarks>If false, the <see cref="CoreLibraryIdentifier"/>
        /// will return null, type identity resolution from it
        /// will be unavailable.</remarks>
        bool UseCoreLibrary { get; }
    }
}
