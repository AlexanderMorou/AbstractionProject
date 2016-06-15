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
    /// Defines properties and methods for working with a series of
    /// grouped declarations that have a larger master 
    /// <see cref="IFullDeclarationDictionary"/> which indicates verbatim 
    /// order across all declaration serii.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of unique identifier
    /// used to represent the <typeparamref name="TItem"/>
    /// in simple form.</typeparam>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/>
    /// in the current implementation.</typeparam>
    public interface IGroupedDeclarationDictionary<TIdentifier, TItem> :
        ISubordinateDictionary<TIdentifier, TItem>,
        IDeclarationDictionary<TIdentifier, TItem>
        where TIdentifier :
            IDeclarationUniqueIdentifier
        where TItem :
            IDeclaration
    {
    }
    /// <summary>
    /// Defines properties and methods for working 
    /// with a series of <see cref="IDeclaration"/> 
    /// instances grouped by a master dictionary.
    /// </summary>
    public interface IGroupedDeclarationDictionary :
        ISubordinateDictionary,
        IDeclarationDictionary
    {
    }
}
