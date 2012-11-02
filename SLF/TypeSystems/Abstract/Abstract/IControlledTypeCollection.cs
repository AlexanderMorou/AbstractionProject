using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
/*----------------------------------------\
| Copyright © 2012 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of types.
    /// </summary>
    public interface IControlledTypeCollection :
        IControlledCollection<IType>,
        IEquatable<IControlledTypeCollection>
    {
        /// <summary>
        /// Determines the index of a specific <paramref name="type"/>
        /// in the <see cref="IControlledTypeCollection"/>.
        /// </summary>
        /// <param name="type">The <see cref="IType"/> to locate in the 
        /// <see cref="IControlledTypeCollection"/>.</param>
        /// <returns>The index of <paramref name="type"/> if 
        /// found in the <see cref="IControlledTypeCollection"/>; 
        /// otherwise, -1.</returns>
        new int IndexOf(IType type);
    }
}
