using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Defines properties and methods for working with a series of assembly 
    /// references paired with one or more aliases.
    /// </summary>
    public interface IAggregateAliasReferenceGroups :
        IControlledStateDictionary<string, IAssemblyReferenceIdentityAggregate>,
        IDisposable
    {
        /// <summary>
        /// Returns whether the <see cref="IAggregateAliasReferenceGroups"/>
        /// has been disposed.
        /// </summary>
        bool IsDisposed { get; }
    }
}
