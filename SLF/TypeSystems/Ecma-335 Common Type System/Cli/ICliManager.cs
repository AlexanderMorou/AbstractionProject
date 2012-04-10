using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    public interface ICliManager :
        IIdentityManager<Type, Assembly, ICompiledAssembly>,
        IIdentityManager<string, IAssemblyUniqueIdentifier, ICompiledAssembly>,
        IIdentityManager<ICliMetadataTypeDefinitionTableRow, ICliMetadataAssemblyTableRow, ICompiledAssembly>,
        IIdentityManager<ICliMetadataTypeRefTableRow, ICliMetadataAssemblyRefTableRow, ICompiledAssembly>
    {
        /// <summary>
        /// Obtains a <see cref="ICopmiledAssembly"/> reference by
        /// the filename.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> value
        /// which denotes the location of the assembly image.</param>
        /// <returns>A <see cref="ICompiledAssembly"/>
        /// which denotes the assembly in question.</returns>
        /// <exception cref="System.IO.FileNotFoundException">thrown when 
        /// <paramref name="filename"/> was not found.</exception>
        ICompiledAssembly ObtainAssemblyReference(string filename);
        /// <summary>
        /// Returns the <see cref="ICliRuntimeEnvironmentInfo"/> the
        /// which details the framework version
        /// and runtimeEnvironment <see cref="ICliManager"/> is targeting.
        /// </summary>
        ICliRuntimeEnvironmentInfo RuntimeEnvironment { get; }
    }
}
