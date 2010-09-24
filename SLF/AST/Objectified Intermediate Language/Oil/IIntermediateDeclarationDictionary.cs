using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
    /// <typeparam name="TDeclaration">The type of declaration in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration> :
        IDeclarationDictionary<TDeclaration>
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            TDeclaration, 
            IIntermediateDeclaration
    {
        /// <summary>
        /// Returns the <see cref="IControlledStateCollection{T}"/> of <typeparamref name="TIntermediateDeclaration"/>
        /// instances related to the current <see cref="IIntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        new IControlledStateCollection<TIntermediateDeclaration> Values { get; }
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateDeclaration"/> by the given
        /// <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">The unique <see cref="String"/> given to the member
        /// to retrieve.</param>
        /// <returns>A <typeparamref name="TIntermediateDeclaration"/> instance if
        /// a member exists in the <see cref="IIntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>.</returns>
        new TIntermediateDeclaration this[string identifier] { get; }
        /// <summary>
        /// Returns the <see cref="KeyValuePair{TKey, TValue}"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based <see cref="Int32"/> index of the member to retrieve.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> if successful.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="index"/> is below zero
        /// or beyond the end of the <see cref="IIntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>.</exception>
        new KeyValuePair<string, TIntermediateDeclaration> this[int index] { get; }
        /// <summary>
        /// Returns an <see cref="IEnumerator{T}"/> for the 
        /// <see cref="IIntermediateDeclarationDictionary"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> instance for the 
        /// <see cref="IIntermediateDeclarationDictionary"/>.</returns>
        new IEnumerator<KeyValuePair<string, TIntermediateDeclaration>> GetEnumerator();
        event EventHandler<EventArgsR1<TIntermediateDeclaration>> ItemAdded;
        event EventHandler<EventArgsR1<TIntermediateDeclaration>> ItemRemoved;
    }
    /// <summary>
    /// Defines properties and methods for working with 
    /// an intermediate series of declarations.
    /// </summary>
    public interface IIntermediateDeclarationDictionary :
        IDeclarationDictionary
    {

    }
}
