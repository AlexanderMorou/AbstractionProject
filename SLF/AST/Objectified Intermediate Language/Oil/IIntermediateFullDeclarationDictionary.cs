using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a full series of
    /// grouped declarations merged into their total form.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of declaration in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateFullDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> :
        IFullDeclarationDictionary<TIdentifier, TDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
        where TDeclaration :
            class,
            IDeclaration
        where TIntermediateDeclaration :
            class,
            TDeclaration,
            IIntermediateDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IControlledStateCollection{T}"/> of the master entries for the
        /// <see cref="IIntermediateFullDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>
        /// </summary>
        new IControlledStateCollection<MasterDictionaryEntry<TIntermediateDeclaration>> Values { get; }
        /// <summary>
        /// Returns an <see cref="IEnumerator{T}"/> for the 
        /// <see cref="IIntermediateFullDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> instance for the 
        /// <see cref="IIntermediateFullDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>.</returns>
        new IEnumerator<KeyValuePair<TIdentifier, MasterDictionaryEntry<TIntermediateDeclaration>>> GetEnumerator();
    }
}
