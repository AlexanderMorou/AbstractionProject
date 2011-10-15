using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for 
    /// working with the full master dictionary 
    /// of a series of <see cref="IGroupedDeclarationDictionary"/>.
    /// </summary>
    /// <typeparam name="TMItem">The base type of 
    /// <see cref="IDeclaration"/>.</typeparam>
    public interface IFullDeclarationDictionary<TUniqueIdentifier, TMItem> :
        IMasterDictionary<TUniqueIdentifier, TMItem>
        where TUniqueIdentifier :
            IDeclarationUniqueIdentifier<TUniqueIdentifier>
        where TMItem :
            class,
            IDeclaration
    {
    }

    /// <summary>
    /// Defines properties and methods for 
    /// working with the full master dictionary 
    /// of a series of <see cref="IGroupedDeclarationDictionary"/>.
    /// </summary>
    public interface IFullDeclarationDictionary :
        IMasterDictionary
    {

    }
}
