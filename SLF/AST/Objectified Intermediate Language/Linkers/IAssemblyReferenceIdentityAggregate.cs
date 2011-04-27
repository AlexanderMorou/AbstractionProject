using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Defines properties and methods for working with the 
    /// aggregate identity for the root level namespace of
    /// a series of referenced assemblies.
    /// </summary>
    public interface IAssemblyReferenceIdentityAggregate :
        IAggregateNamespaceParentIdentityNode,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="String"/> values which 
        /// represents the alias associated to the series of assembly
        /// references aggregated into the current identity set.
        /// </summary>
        string Alias { get; }
        /// <summary>
        /// Returns the <see cref="IAssemblyReference"/> set which is 
        /// collected into an aggregate identity set.
        /// </summary>
        IEnumerable<IAssemblyReference> References { get; }
        /// <summary>
        /// Returns the <see cref="IAggregateAliasReferenceGroups"/> from which the
        /// assembly namespace identities are aggregated from.
        /// </summary>
        IAggregateAliasReferenceGroups IdentitySource { get; }
    }
}
