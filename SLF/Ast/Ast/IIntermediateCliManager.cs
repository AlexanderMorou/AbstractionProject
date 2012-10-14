using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an identity manager 
    /// which handles both compiled Cli-based identities and intermediate 
    /// identities.
    /// </summary>
    public interface IIntermediateCliManager :
        ICliManager
    {
        /// <summary>
        /// Returns whether the assembly associated to the <paramref name="assemblyIdentity"/>
        /// is intermediate or compiled.
        /// </summary>
        /// <param name="assemblyIdentity">The <see cref="IAssemblyUniqueIdentifier"/>
        /// to determine the intermediate state of.</param>
        /// <returns>true, if the assembly relative to <paramref name="assemblyIdentity"/>
        /// is in an intermediate state; false, otherwise.</returns>
        bool IsIntermediateAssembly(IAssemblyUniqueIdentifier assemblyIdentity);
    }
}
