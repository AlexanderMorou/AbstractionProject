using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working with an assembly
    /// defind within the common language infrastructure, utilizing the common
    /// type system.
    /// </summary>
    public interface ICliAssembly :
        IAssembly,
        ICliDeclaration<IAssemblyUniqueIdentifier, ICliMetadataAssemblyTableRow>,
        ICliTypeParent
    {
        /// <summary>
        /// Returns the <see cref="ICliRuntimeEnvironmentInfo"/> the
        /// which details the framework version
        /// and platform <see cref="ICliAssembly"/> targets.
        /// </summary>
        ICliRuntimeEnvironmentInfo RuntimeEnvironment { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataRoot"/> from which the <see cref="ICliAssembly"/>
        /// is derived.
        /// </summary>
        ICliMetadataRoot MetadataRoot { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyTableRow"/> from which the
        /// <see cref="ICliAssembly"/> is derived.
        /// </summary>
        ICliMetadataAssemblyTableRow MetadataEntry { get; }
        /// <summary>
        /// Returns a <see cref="IControlledDictionary{T}"/> of <see cref="ICliMetadataAssemblyRefTableRow"/>
        /// to <see cref="ICliAssembly"/> instances that are referenced by the current <see cref="ICliAssembly"/>.
        /// </summary>
        IControlledDictionary<ICliMetadataAssemblyRefTableRow, ICliAssembly> CliReferences { get; }
        /// <summary>
        /// Returns the <see cref="CliFrameworkVersion"/> which the <see cref="ICliAssembly"/>
        /// targets.
        /// </summary>
        CliFrameworkVersion FrameworkVersion { get; }
        /// <summary>
        /// The <see cref="ICliManager"/> which handles type and assembly identity resolution.
        /// </summary>
        ICliManager IdentityManager { get; }
        /// <summary>
        /// Returns the <see cref="ICliAssemblyUniqueIdentifier"/> which uniquely distinguishes the 
        /// current <see cref="ICliAssembly"/> from others.
        /// </summary>
        new ICliAssemblyUniqueIdentifier UniqueIdentifier { get; }
    }
}
