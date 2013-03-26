using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working with information about a
    /// specific common language infrastructure runtime environment.
    /// </summary>
    public interface ICliRuntimeEnvironmentInfo :
        IStandardRuntimeEnvironmentInfo
    {
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of <see cref="String"/> values
        /// which represent the additional paths used when creating the <see cref="ICliRuntimeEnvironmentInfo"/>.
        /// </summary>
        IEnumerable<string> AdditionalPaths { get; }

        /// <summary>
        /// Returns whether the <see cref="ICliRuntimeEnvironmentInfo"/> 
        /// should resolve information at the current entry application's
        /// directory.
        /// </summary>
        bool ResolveCurrent { get; }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of
        /// <see cref="DirectoryInfo"/> about the 
        /// <see cref="ICliRuntimeEnvironment"/> which designate where
        /// to look for assembly and type identities.
        /// </summary>
        IEnumerable<DirectoryInfo> ResolutionPaths { get; }

        /// <summary>
        /// Returns whether to use the global access cache.
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

        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> of the <paramref name="coreType"/>
        /// provided relative to <paramref name="assembly"/>.
        /// </summary>
        /// <param name="coreType">The <see cref="CliRuntimeCoreType"/>
        /// to obtain the <see cref="IGeneralTypeUniqueIdentifier"/> of.</param>
        /// <param name="relativeAssembly">The <see cref="ICliAssembly"/> which </param>
        /// <returns>A <see cref="IGeneralTypeUniqueIdentifier"/> relative to the 
        /// <paramref name="coreType"/> provided.</returns>
        /// <remarks><paramref name="relativeAssembly"/> can be null.</remarks>
        IGeneralTypeUniqueIdentifier GetCoreIdentifier(CliRuntimeCoreType coreType, ICliAssembly relativeAssembly = null);

        /// <summary>
        /// Returns the framework platform identity resolution
        /// occurs on.
        /// </summary>
        CliFrameworkPlatform Platform { get; }

        /// <summary>
        /// Returns the <see cref="IAssemblyUniqueIdentifier"/> of
        /// the core library of the runtime environment
        /// represented by the <see cref="ICliRuntimeEnvironmentInfo"/>.
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

        /// <summary>
        /// Returns the <see cref="CliFrameworkVersion"/> on which identity
        /// resolution occurs.
        /// </summary>
        CliFrameworkVersion Version { get; }
        /// <summary>
        /// Returns the definition of the extension metadatum type.
        /// </summary>
        IGeneralTypeUniqueIdentifier ExtensionMetadatum { get; }
        /// <summary>
        /// Returns the definition of all asynchronous tasks.
        /// </summary>
        IGeneralTypeUniqueIdentifier AsynchronousTask { get; }
        /// <summary>
        /// Returns the definition of all generic asynchronous tasks.
        /// </summary>
        IGeneralTypeUniqueIdentifier AsynchronousTaskOfT { get; }
        /// <summary>
        /// Returns the definition of the compiler generated metadatum.
        /// </summary>
        IGeneralTypeUniqueIdentifier CompilerGeneratedMetadatum { get; }
        /// <summary>
        /// Returns the definition of the root of all delegate types.
        /// </summary>
        IGeneralTypeUniqueIdentifier Delegate { get; }
        /// <summary>
        /// Returns the definition of the root of all multicast delegate types.
        /// </summary>
        IGeneralTypeUniqueIdentifier MulticastDelegate { get; }
        /// <summary>
        /// Returns the definition of the base type of a nullable type.
        /// </summary>
        IGeneralTypeUniqueIdentifier NullableBaseType { get; }
        /// <summary>
        /// Returns the definition a nullable type.
        /// </summary>
        IGeneralTypeUniqueIdentifier NullableType { get; }
        /// <summary>
        /// Returns the definition of the parameter array metadatum to be applied
        /// to the final parameter of a method which signifies that
        /// the method accepts multiple arguments as one in place of 
        /// the array.
        /// </summary>
        IGeneralTypeUniqueIdentifier ParamArrayMetadatum { get; }
        /// <summary>
        /// Returns the definition of the root metadatum type.
        /// </summary>
        IGeneralTypeUniqueIdentifier RootMetadatum { get; }


    }
}
