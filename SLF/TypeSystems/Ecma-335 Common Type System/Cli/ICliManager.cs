using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for resolving the identities 
    /// of entities from the Common Language Infrastructure.
    /// </summary>
    public interface ICliManager :
        IIdentityManager<Type, Assembly>,
        IIdentityManager<string, string>,
        IIdentityManager<ICliMetadataTypeDefinitionTableRow, ICliMetadataAssemblyTableRow>,
        IIdentityManager<ICliMetadataTypeRefTableRow, ICliMetadataAssemblyRefTableRow>,
        ITypeIdentityManager<ICliMetadataTypeSpecificationTableRow>,
        ITypeIdentityManager<ICliMetadataTypeDefOrRefRow>,
        IDisposable
    {
        event EventHandler Disposed;
        /// <summary>
        /// Obtains a <see cref="ICopmiledAssembly"/> reference by the filename.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> value
        /// which denotes the location of the assembly image.</param>
        /// <returns>A <see cref="IAssembly"/> which denotes the assembly in
        /// question.</returns>
        /// <exception cref="System.IO.FileNotFoundException">thrown when
        /// <paramref name="filename"/> was not found.</exception>
        IAssembly ObtainAssemblyReference(string filename);
        /// <summary>
        /// Returns the <see cref="ICliRuntimeEnvironmentInfo"/> the
        /// which details the framework version and platform
        /// <see cref="ICliManager"/> is targeting.
        /// </summary>
        new ICliRuntimeEnvironmentInfo RuntimeEnvironment { get; }
        /// <summary>
        /// Returns the <see cref="IType"/> from the <paramref name="coreType"/> provided.
        /// </summary>
        /// <param name="coreType">The <see cref="CliRuntimeCoreType"/> which denotes the
        /// core type to obtain a type reference of.</param>
        /// <param name="relativeSource">The <see cref="IAssembly"/> which represents
        /// the lookup scope for the type to retrieve.</param>
        /// <returns>A <see cref="IType"/> relative to the <paramref name="coreType"/>
        /// within the scope of <paramref name="relativeSource"/>.</returns>
        IType ObtainTypeReference(CliRuntimeCoreType coreType, IAssembly relativeSource);
        /// <summary>
        /// Returns the <see cref="IType"/> from the <paramref name="uniqueIdentifier"/> provided.
        /// </summary>
        /// <param name="uniqueIdentifier">The <see cref="IGeneralTypeUniqueIdentifier"/> of
        /// to the type to retrieve relative to the scope of
        /// <paramref name="relativeScope"/>.</param>
        /// <param name="relativeSource">The <see cref="IAssembly"/> which represents
        /// the lookup scope for the type to retrieve.</param>
        /// <returns>A <see cref="IType"/> relative to the <paramref name="uniqueIdentifier"/>
        /// within the scope of <paramref name="relativeSource"/>.</returns>
        IType ObtainTypeReference(IGeneralTypeUniqueIdentifier uniqueIdentifier, IAssembly relativeSource);
        event EventHandler<CliAssemblyLoadedEventArgs> AssemblyLoaded;
    }
}
