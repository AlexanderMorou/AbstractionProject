using AllenCopeland.Abstraction.Slf.Abstract;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides the master locked members dictionary
    /// with access to the creator delegate from
    /// the subordinates it owns.
    /// </summary>
    /// <remarks>Necessary
    /// because of the level of abstraction difference
    /// between the 'general' main dictionary and the 
    /// subordinates.  The subordinates are, a factor of 
    /// one, higher in abstraction through generic
    /// parameters.</remarks>
    internal interface _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>
        where TMItem :
            class,
            IDeclaration
        where TMItemIdentifier :
            IDeclarationUniqueIdentifier<TMItemIdentifier>
    {
        TMItem Fetch(object source);
        TMItemIdentifier FetchKey(object source);
        /// <summary>
        /// Obtains the name of a given source object in an event to 
        /// aggregate identifiers across a larger set.
        /// </summary>
        /// <param name="source">The <see cref="Object"/> on which
        /// to source the name of.</param>
        /// <returns>A <see cref="String"/> value relative to the 
        /// name of the <paramref name="source"/>.</returns>
        string FetchName(object source);
    }
}
