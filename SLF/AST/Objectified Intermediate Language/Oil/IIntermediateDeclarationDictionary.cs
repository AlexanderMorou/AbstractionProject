using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with
    /// an intermediate series of declarations.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of identifier used to differentiate
    /// the <typeparamref name="TIntermediateDeclaration"/> instances from one another.</typeparam>
    /// <typeparam name="TDeclaration">The type of declaration in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> :
        IDeclarationDictionary<TIdentifier, TDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            TDeclaration, 
            IIntermediateDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IControlledStateCollection{T}"/> of <typeparamref name="TIntermediateDeclaration"/>
        /// instances related to the current <see cref="IIntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        new IControlledStateCollection<TIntermediateDeclaration> Values { get; }
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateDeclaration"/> by the given
        /// <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">The unique <typeparamref name="TIdentifier"/> given to the member
        /// to retrieve.</param>
        /// <returns>A <typeparamref name="TIntermediateDeclaration"/> instance if
        /// a member exists in the <see cref="IIntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>.</returns>
        new TIntermediateDeclaration this[TIdentifier identifier] { get; }
        /// <summary>
        /// Returns the <see cref="KeyValuePair{TKey, TValue}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based <see cref="Int32"/> index of the member to retrieve.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> if successful.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="index"/> is below zero
        /// or beyond the end of the <see cref="IIntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>.</exception>
        new KeyValuePair<TIdentifier, TIntermediateDeclaration> this[int index] { get; }
        /// <summary>
        /// Returns an <see cref="IEnumerator{T}"/> for the 
        /// <see cref="IIntermediateDeclarationDictionary"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> instance for the 
        /// <see cref="IIntermediateDeclarationDictionary"/>.</returns>
        new IEnumerator<KeyValuePair<TIdentifier, TIntermediateDeclaration>> GetEnumerator();
        event EventHandler<EventArgsR1<TIntermediateDeclaration>> ItemAdded;
        event EventHandler<EventArgsR1<TIntermediateDeclaration>> ItemRemoved;
        /// <summary>
        /// Returns whether a member of the <see cref="IIntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>
        /// has a <paramref name="name"/> that equals the one provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the name to check for.</param>
        /// <returns>true, if there exists an element within the <see cref="IIntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>
        /// that has the <paramref name="name"/> provided; false, otherwise.</returns>
        bool ContainsName(string name);
    }
    /// <summary>
    /// Defines properties and methods for working with 
    /// an intermediate series of declarations.
    /// </summary>
    public interface IIntermediateDeclarationDictionary :
        IDeclarationDictionary
    {
        /// <summary>
        /// Returns whether a member of the <see cref="IIntermediateDeclarationDictionary"/>
        /// has a <paramref name="name"/> that equals the one provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the name to check for.</param>
        /// <returns>true, if there exists an element within the <see cref="IIntermediateDeclarationDictionary"/>
        /// that has the <paramref name="name"/> provided; false, otherwise.</returns>
        bool ContainsName(string name);
    }
}
