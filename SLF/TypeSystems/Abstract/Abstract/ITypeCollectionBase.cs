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
    public interface ITypeCollectionBase :
        IControlledStateCollection<IType>,
        IEquatable<ITypeCollectionBase>
    {
        /// <summary>
        /// Determines the index of a specific <paramref name="item"/>
        /// in the <see cref="ITypeCollection"/>.
        /// </summary>
        /// <param name="item">The <see cref="IType"/> to locate in the 
        /// <see cref="ITypeCollection"/>.</param>
        /// <returns>The index of <paramref name="item"/> if 
        /// found in the <see cref="ITypeCollection"/>; 
        /// otherwise, -1.</returns>
        new int IndexOf(IType item);
    }
}
