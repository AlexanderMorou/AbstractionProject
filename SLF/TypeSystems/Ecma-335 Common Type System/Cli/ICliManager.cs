using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for resolving the identities 
    /// of entities from the Common Language Infrastructure.
    /// </summary>
    public interface ICliManager :
        IIdentityManager<Type, Assembly, ICliAssembly>,
        IIdentityManager<string, IAssemblyUniqueIdentifier, ICliAssembly>,
        IIdentityManager<ICliMetadataTypeDefinitionTableRow, ICliMetadataAssemblyTableRow, ICliAssembly>,
        IIdentityManager<ICliMetadataTypeRefTableRow, ICliMetadataAssemblyRefTableRow, ICliAssembly>,
        ITypeIdentityManager<ICliMetadataTypeSpecificationTableRow>
    {
        /// <summary>
        /// Obtains a <see cref="ICopmiledAssembly"/> reference by the filename.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> value
        /// which denotes the location of the assembly image.</param>
        /// <returns>A <see cref="ICliAssembly"/> which denotes the assembly in
        /// question.</returns>
        /// <exception cref="System.IO.FileNotFoundException">thrown when
        /// <paramref name="filename"/> was not found.</exception>
        ICliAssembly ObtainAssemblyReference(string filename);
        /// <summary>
        /// Returns the <see cref="ICliRuntimeEnvironmentInfo"/> the
        /// which details the framework version and runtimeEnvironment
        /// <see cref="ICliManager"/> is targeting.
        /// </summary>
        new ICliRuntimeEnvironmentInfo RuntimeEnvironment { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataModuleTableRow"/> from the module
        /// defined within the <paramref name="metadata"/> reference.
        /// </summary>
        /// <param name="metadata">The <see cref="ICliMetadataModuleReferenceTableRow"/>
        /// which represents the module reference.</param>
        /// <returns>The <see cref="ICliMetadataModuleTableRow"/> which represents
        /// the actual module's metadata.</returns>
        ICliMetadataModuleTableRow LoadModule(ICliMetadataModuleReferenceTableRow metadata);
    }
}
