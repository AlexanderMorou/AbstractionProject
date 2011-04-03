using System.Collections.Generic;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        IFullDeclarationDictionary<IType>
    {
        /// <summary>
        /// Returns a <see cref="IType"/> instance
        /// by its name.
        /// </summary>
        /// <param name="typeName">The <see cref="String"/> value representing the types
        /// name.</param>
        /// <param name="typeParameterCount">The number of type-parameters in the 
        /// generic type, zero if non-generic.</param>
        /// <returns>A <see cref="IEnumerable{T}"/>
        /// of the types which contain the <see cref="TypeName"/> provided.</returns>
        IType FindTypeByName(string typeName, int typeParameterCount = 0);
    }
}
