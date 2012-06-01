using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IStandardRuntimeEnvironmentInfo
    {

        /// <summary>
        /// Returns the <see cref="IAssemblyUniqueIdentifier"/> of
        /// the core library of the runtime environment
        /// represented by the <see cref="IStandardRuntimeEnvironmentInfo"/>.
        /// </summary>
        IAssemblyUniqueIdentifier CoreLibraryIdentifier { get; }
        /// <summary>
        /// Returns whether the core library identified by <see cref="CoreLibraryIdentifier"/>
        /// is present.
        /// </summary>
        /// <remarks>If false, the <see cref="CoreLibraryIdentifier"/>
        /// will return null, type identity resolution from it
        /// will be unavailable.</remarks>
        bool UseCoreLibrary { get; }

        /// <summary>
        /// Returns the <see cref="IGeneralTypeUniqueIdentifier"/> of the <paramref name="coreType"/>
        /// provided.
        /// </summary>
        /// <param name="coreType">The <see cref="RuntimeCoreType"/>
        /// to obtain the <see cref="IGeneralTypeUniqueIdentifier"/> of.</param>
        /// <returns>A <see cref="IGeneralTypeUniqueIdentifier"/> relative to the 
        /// <paramref name="coreType"/> provided.</returns>
        IGeneralTypeUniqueIdentifier GetCoreIdentifier(RuntimeCoreType coreType);
        /// <summary>
        /// Returns the series of <see cref="IType"/>s implemented by the
        /// <paramref name="coreType"/>
        /// </summary>
        /// <param name="coreType">The <see cref="RuntimeCoreType"/>
        /// to request the implemented interfaces of.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of the interfaces
        /// implemented by <paramref name="coreType"/>.</returns>
        IEnumerable<IType> GetCoreTypeInterfaces(RuntimeCoreType coreType);
    }
}
