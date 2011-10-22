using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a full declaration dictionary base class.
    /// </summary>
    /// <typeparam name="TMItem">The base type of 
    /// <see cref="IDeclaration"/>.</typeparam>
    internal class FullDeclarationDictionaryBase<TIdentifier, TMItem> :
        MasterDictionaryBase<TIdentifier, TMItem>,
        IFullDeclarationDictionary<TIdentifier, TMItem>,
        IFullDeclarationDictionary
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
        where TMItem :
            class,
            IDeclaration
    {
        /// <summary>
        /// Creates a new <see cref="FullDeclarationDictionaryBase{TMItem}"/> 
        /// initialized to a default state.
        /// </summary>
        public FullDeclarationDictionaryBase()
            : base()
        {
        }
    }
}
