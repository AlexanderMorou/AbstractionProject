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
        IIdentityManager<Type, Assembly, ICliAssembly>,
        IIdentityManager<string, string, ICliAssembly>,
        IIdentityManager<IGeneralTypeUniqueIdentifier, IAssemblyUniqueIdentifier, ICliAssembly>,
        IIdentityManager<ICliMetadataTypeDefinitionTableRow, ICliMetadataAssemblyTableRow, ICliAssembly>,
        IIdentityManager<ICliMetadataTypeRefTableRow, ICliMetadataAssemblyRefTableRow, ICliAssembly>,
        ITypeIdentityManager<ICliMetadataTypeSpecificationTableRow>,
        ITypeIdentityManager<ICliMetadataTypeDefOrRefRow>
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
        /// Returns the <see cref="IType"/> from the <paramref name="coreType"/> provided.
        /// </summary>
        /// <param name="typeIdentity"></param>
        /// <returns></returns>
        IType ObtainTypeReference(RuntimeCoreType coreType, ICliAssembly relativeSource);
        /// <summary>
        /// Returns the <see cref="IType"/> from the <paramref name="coreType"/> provided.
        /// </summary>
        /// <param name="uniqueIdentifier">The <see cref="IGeneralTypeUniqueIdentifier"/> of
        /// to the type to retrieve relative to the scope of
        /// <paramref name="relativeScope"/>.</param>
        /// <returns></returns>
        IType ObtainTypeReference(IGeneralTypeUniqueIdentifier uniqueIdentifier, ICliAssembly relativeSource);
    }
}
