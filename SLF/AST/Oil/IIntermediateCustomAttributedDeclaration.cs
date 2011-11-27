using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a declaration that can contain
    /// a series of custom attribute definition series.
    /// </summary>
    public interface IIntermediateCustomAttributedDeclaration :
        IIntermediateDeclaration,
        ICustomAttributedDeclaration
    {
        /// <summary>
        /// Returns the <see cref="ICustomAttributeDefinitionCollectionSeries"/> associated
        /// to the current <see cref="IIntermediateCustomAttributedDeclaration"/>.
        /// </summary>
        new ICustomAttributeDefinitionCollectionSeries CustomAttributes { get; }
    }
}
