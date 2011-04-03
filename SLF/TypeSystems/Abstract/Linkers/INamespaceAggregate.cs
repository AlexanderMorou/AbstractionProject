using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Defines properties and methods for working with a namespace
    /// aggregate.
    /// </summary>
    /// <remarks>Aggregates the entities defined within a given 
    /// series of assemblies under a common namespace entity.</remarks>
    public interface INamespaceAggregate :
        IDictionary<string, INamespaceAggregateIdentityNode>
    {
        /// <summary>
        /// Returns the <see cref="String"/> value representing the
        /// name of the namespace 
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="INamespaceAggregate"/> which
        /// contains the current <see cref="INamespaceAggregate"/>.
        /// </summary>
        INamespaceAggregate Parent { get; }
        /// <summary>
        /// Returns the full name of the namespace at the 
        /// </summary>
        string FullName { get; }
    }
}
