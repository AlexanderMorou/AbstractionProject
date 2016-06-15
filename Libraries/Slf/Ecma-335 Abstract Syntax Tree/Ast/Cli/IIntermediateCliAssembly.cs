using AllenCopeland.Abstraction.Slf.Ast.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// assembly defined for the Common Language Infrastructure (ECMA-335.)
    /// </summary>
    public interface IIntermediateCliAssembly :
        IIntermediateAssembly,
        ICliAssembly
    {
        /// <summary>
        /// Returns the <see cref="CliFrameworkVersion"/> which the <see cref="ICliAssembly"/>
        /// targets.
        /// </summary>
        new CliFrameworkVersion FrameworkVersion { get; set; }
        /// <summary>
        /// Returns the <see cref="IIntermediateCliManager"/> which handles
        /// type and assembly identity resolution.
        /// </summary>
        new IIntermediateCliManager IdentityManager { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateCliRuntimeEnvironmentInfo"/> the which
        /// details the framework version and platform <see cref="IIntermediateCliAssembly"/>
        /// targets.
        /// </summary>
        IIntermediateCliRuntimeEnvironmentInfo RuntimeEnvironment { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataMutableRoot"/> which the <see cref="IIntermediateCliAssembly"/>
        /// builds as the assembly is created.
        /// </summary>
        new ICliMetadataMutableRoot MetadataRoot { get; }
        /// <summary>
        /// Returns the <see cref="ICliMetadataAssemblyMutableTableRow"/> from which represents the 
        /// <see cref="IIntermediateCliAssembly"/> in the <see cref="MetadataRoot"/>.
        /// </summary>
        new ICliMetadataAssemblyMutableTableRow MetadataEntry { get; }
    }
}
