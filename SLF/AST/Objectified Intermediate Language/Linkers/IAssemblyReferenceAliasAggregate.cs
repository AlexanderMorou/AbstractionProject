using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Defines properties and methods for working with a series of assembly 
    /// references 
    /// </summary>
    public interface IAssemblyReferenceAliasAggregate :
        IControlledStateDictionary<string, IAssemblyReferenceIdentityAggregate>,
        IDisposable
    {
        /// <summary>
        /// Returns whether the <see cref="IAssemblyReferenceAliasAggregate"/>
        /// has been disposed.
        /// </summary>
        bool IsDisposed { get; }
    }
}
