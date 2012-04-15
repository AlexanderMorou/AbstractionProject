using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliAssembly :
        IAssembly,
        ICliDeclaration<IAssemblyUniqueIdentifier, ICliMetadataAssemblyTableRow>
    {
        /// <summary>
        /// Returns the <see cref="ICliRuntimeEnvironmentInfo"/> the
        /// which details the framework version
        /// and runtimeEnvironment <see cref="ICliAssembly"/> targets.
        /// </summary>
        ICliRuntimeEnvironmentInfo RuntimeEnvironment { get; }
        /// <summary>
        /// Returns the <see cref="ICliManager"/> the
        /// which is responsible for managing the identities
        /// resolved through the <see cref="ICliAssembly"/>.
        /// </summary>
        ICliManager IdentityManager { get; }
        /// <summary>
        /// Returns the <see cref="CliMetadataRoot"/> from which the <see cref="ICliAssembly"/>
        /// is derived.
        /// </summary>
        CliMetadataRoot MetadataRoot { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyTableRow"/> from which the
        /// <see cref="ICliAssembly"/> is derived.
        /// </summary>
        ICliMetadataAssemblyTableRow Metadata { get; }
    }
}
