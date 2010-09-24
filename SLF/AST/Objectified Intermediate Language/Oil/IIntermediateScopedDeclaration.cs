using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// scoped declaration.
    /// </summary>
    public interface IIntermediateScopedDeclaration :
        IIntermediateDeclaration,
        IScopedDeclaration
    {
        /// <summary>
        /// Returns/sets the access level of the
        /// <see cref="IIntermediateScopedDeclaration"/>.
        /// </summary>
        new AccessLevelModifiers AccessLevel { get; set; }
    }
}
