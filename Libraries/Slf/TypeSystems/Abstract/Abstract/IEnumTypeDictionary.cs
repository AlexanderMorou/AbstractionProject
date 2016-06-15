using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of enum types subordinated to a full dictionary 
    /// of types.
    /// </summary>
    public interface IEnumTypeDictionary :
        IGroupedTypeDictionary<IGeneralTypeUniqueIdentifier, IEnumType>
    {
        /// <summary>
        /// Returns the <see cref="ITypeParent"/>
        /// which contains the <see cref="IEnumTypeDictionary"/>.
        /// </summary>
        ITypeParent Parent { get; }
    }
}
