using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with the private implementation details of a
    /// library.
    /// </summary>
    public interface IPrivateImplementationDetails :
        IIntermediateClassType
    {
        /// <summary>
        /// Returns the <see cref="IAnonymousTypeDictionary"/> which enables
        /// declaration and management of anonymous types within the 
        /// <see cref="IPrivateImplementationDetails"/>.
        /// </summary>
        IAnonymousTypeDictionary AnonymousTypes { get; }
        Guid DetailGuid { get; set; }
    }
}
