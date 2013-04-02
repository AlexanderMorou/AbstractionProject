using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a series
    /// of class types subordinated to a full dictionary 
    /// of types.
    /// </summary>
    public interface IClassTypeDictionary :
        IGroupedTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IClassType>
    {
        /// <summary>
        /// Returns the <see cref="ITypeParent"/>
        /// which contains the <see cref="IClassTypeDictionary"/>.
        /// </summary>
        ITypeParent Parent { get; }
    }
}
