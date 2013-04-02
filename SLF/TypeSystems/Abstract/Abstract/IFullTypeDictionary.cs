using System.Collections.Generic;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// type container's full set of types.
    /// </summary>
    public interface IFullTypeDictionary :
        IFullDeclarationDictionary<IGeneralTypeUniqueIdentifier, IType>
    {
        /// <summary>
        /// Returns a series of <see cref="IType"/> instances under a
        /// common <paramref name="name"/> regardless of the number of
        /// generic parameters contained within them.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// value of the type to return.</param>
        /// <returns>An <see cref="IType"/> array relative to the
        /// <paramref name="name"/> provided.</returns>
        IType[] GetTypesByName(string name);
    }
}
