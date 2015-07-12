using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with an identity manager 
    /// which handles both compiled Cli-based identities and intermediate 
    /// identities.
    /// </summary>
    public interface IIntermediateIdentityManager :
        IIdentityManager
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
        /// <summary>
        /// Notifies the <see cref="IIntermediateIdentityManager"/> that a new
        /// <paramref name="assembly"/> has been created.
        /// </summary>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/>
        /// which was created.</param>
        /// <remarks>The intent is to ensure that the manager has up to date
        /// knowledge of the intermediate assemblies in play.</remarks>
        void AssemblyCreated(IIntermediateAssembly assembly);
    }
}
