using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// aggregate identity.
    /// </summary>
    /// <remarks>Aggregates the entities defined within a given 
    /// series of assemblies under a common identity.</remarks>
    public interface IAggregateIdentityNode
    {
        /// <summary>
        /// Returns the name of the aggregated identity.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="IAggregateIdentityNode"/> which
        /// contains the current <see cref="IAggregateIdentityNode"/>.
        /// </summary>
        IAggregateIdentityNode Parent { get; }
        /// <summary>
        /// Returns the full name of the namespace at the 
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Returns the <see cref="AggregateIdentityKind"/> which denotes
        /// the kind of identity represented by the current node.
        /// </summary>
        AggregateIdentityKind Kind { get; }
    }
}
