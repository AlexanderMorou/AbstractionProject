using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    /// <summary>
    /// Defines properties and methods for working with a specific version of a given versioned runtime environment.
    /// </summary>
    /// <typeparam name="TEnvironment">
    /// The type used in place of the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/> 
    /// which implements <typeparamref name="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/>.
    /// </typeparam>
    /// <typeparam name="TVersion">
    /// The type used in place of the <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> within the model
    /// that represent a unique version of the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}"/>.
    /// </typeparam>
    /// <typeparam name="TIdentityManager">The type of <see cref="IIdentityManager"/> used to resolve
    /// identities within the <typeparamref name="TEnvironment"/>.</typeparam>
    public interface IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Returns the <typeparamref name="TEnvironment"/> instance of which the current
        /// <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> is a part of.
        /// </summary>
        TEnvironment Environment { get; }
        /// <summary>
        /// Returns the <see cref="VreEnvironmentSubversionSource"/> which denotes the origin of the <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        VreEnvironmentSubversionSource SubversionSource { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> which represents the initial release version of the 
        /// <see cref="Environment"/>.
        /// </summary>
        TVersion InitialVersion { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> which represents the current major version of the <see cref="Environment"/>.
        /// </summary>
        TVersion MajorVersion { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> which represents the previous version
        /// of the <see cref="Environment"/>.
        /// </summary>
        /// <remarks>If the current <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> is the
        /// <see cref="InitialVersion"/> this will be null.</remarks>
        TVersion PreviousVersion { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> which represents the previous major version of the <see cref="Environment"/>.
        /// </summary>
        TVersion PreviousMajorVersion { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> which represents the next version of the <see cref="Environment"/>.
        /// </summary>
        /// <remarks>
        /// If the current <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> is the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}.CurrentVersion"/>
        /// this may return null.
        /// </remarks>
        TVersion NextVersion { get; }
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> which represents the next major version of the <see cref="Environment"/>.
        /// </summary>
        /// <remarks>
        /// If the current <see cref="MajorVersion"/> is the <see cref="IVreEnvironment{TEnvironment, TVersion, TIdentityManager}.CurrentVersion"/>'s MajorVersion
        /// this may return null.
        /// </remarks>
        TVersion NextMajorVersion { get; }
        /// <summary>
        /// Returns the <see cref="String"/> value which qualifies this <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> uniquely from the
        /// other versions of the <see cref="Environment"/>.
        /// </summary>
        string VersionQualifier { get; }
        /// <summary>
        /// Returns whether the current <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> is a service pack by comparing the
        /// <see cref="SubversionSource"/> against <see cref="VreEnvironmentSubversionSource.ServicePackExtension"/>.
        /// </summary>
        /// <remarks>Used to simplify <see cref="ServicePackLevel"/> logic.</remarks>
        bool IsServicePack { get; }
        /// <summary>
        /// Returns the <see cref="Nullable{T}"/> <see cref="Int32"/> value which denotes the level of service pack
        /// the <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> is.
        /// </summary>
        /// <remarks>If <see cref="IsServicePack"/> is false, this will return null.</remarks>
        int? ServicePackLevel { get; }
        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="String"/> values which denotes a possible locations where the libraries for 
        /// a given <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> can be found.
        /// </summary>
        IEnumerable<string> HintPaths { get; }
        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of <see cref="String"/> values which denote possible version strings associated to a given library that identify
        /// the <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/> the library runs on.
        /// </summary>
        IEnumerable<string> VersionStrings { get; }
        /// <summary>
        /// Returns the <typeparamref name="TIdentityManager"/> which handles library, and type identity resolution for the current <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        TIdentityManager IdentityManager { get; }
        string VersionText { get; }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of deprecated <see cref="IVreType{TEnvironment, TVersion, TIdentityManager}"/> elements contained with the current <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> DeprecatedTypes { get; }
        IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> AllTypes { get; }
        /// <summary>
        /// Returns the <see cref="IVreLibraryDictionaryVersion{TEnvironment, TVersion, TIdentityManager}"/> which contains the libraries
        /// contained within this <see cref="IVreEnvironmentVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        IVreLibraryDictionaryVersion<TEnvironment, TVersion, TIdentityManager> Libraries { get; }
    }
}
