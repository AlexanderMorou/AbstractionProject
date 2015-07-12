using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Defines properties and methods for working with an assembly's unique identifier
    /// under the assumption that it is defined targeting the .NET CLI.
    /// </summary>
    public interface ICliAssemblyUniqueIdentifier :
        IAssemblyUniqueIdentifier
    {
        /// <summary>
        /// Returns the <see cref="CliFrameworkVersion"/> which the
        /// <see cref="ICliAssembly"/> targets.
        /// </summary>
        CliFrameworkVersion TargetFramework { get; }
    }
}
