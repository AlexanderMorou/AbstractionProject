using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
    public interface IIntermediateFullDeclarationDictionary<TDeclaration, TIntermediateDeclaration> :
        IFullDeclarationDictionary<TDeclaration>
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
        /// <see cref="IIntermediateFullDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>
        /// </summary>
        new IControlledStateCollection<MasterDictionaryEntry<TIntermediateDeclaration>> Values { get; }
        /// <summary>
        /// Returns an <see cref="IEnumerator{T}"/> for the 
        /// <see cref="IIntermediateDeclarationDictionary"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> instance for the 
        /// <see cref="IIntermediateDeclarationDictionary"/>.</returns>
        new IEnumerator<KeyValuePair<string, MasterDictionaryEntry<TIntermediateDeclaration>>> GetEnumerator();
    }
}
