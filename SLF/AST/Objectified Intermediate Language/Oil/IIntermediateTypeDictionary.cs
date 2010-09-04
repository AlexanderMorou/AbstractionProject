using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines properties and methods for working with a series of specifically
    /// typed <see cref="IIntermediateType"/> instances.
    /// </summary>
    /// <typeparam name="TType">The type used in the dictionary by the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type used in the dictionary by the
    /// intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateTypeDictionary<TType, TIntermediateType> :
        IIntermediateGroupedDeclarationDictionary<TType, TIntermediateType>
        where TType :
            IType<TType>
        where TIntermediateType :
            IIntermediateType,
            TType
    {
        /// <summary>
        /// Creates and adds a new <typeparamref name="TIntermediateType"/> 
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <typeparamref name="TIntermediateType"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when a type by the <paramref name="name"/>
        /// provided already exists in the containing type parent, or <paramref name="name"/>
        /// is <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        TIntermediateType Add(string name);
        /// <summary>
        /// Creates and inserts a new <typeparamref name="TIntermediateType"/> instance
        /// into the current <see cref="IIntermediateTypeDictionary{TType, TIntermediateType}"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> that defines the name of the
        /// new <typeparamref name="TIntermediateType"/> to create.</param>
        /// <param name="module">The <see cref="IIntermediateModule"/> to add the 
        /// new <typeparamref name="TIntermediateType"/> to.</param>
        /// <returns>A new <typeparamref name="TIntermediateType"/> instance.</returns>
        /// <remarks>Using this method creates a new partial instance 
        /// of the assembly to ensure the class can be translated into
        /// a file of its own.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>, or <paramref name="module"/>, is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when a type by the <paramref name="name"/>
        /// provided already exists in the containing type parent, or <paramref name="name"/> 
        /// is <see cref="String.Empty"/>.</exception>
        TIntermediateType Add(string name, IIntermediateModule module);
        /// <summary>
        /// Removes the provided <typeparamref name="TIntermediateType"/> from the
        /// <see cref="IIntermediateTypeDictionary{TType, TIntermediateType}"/>.
        /// </summary>
        /// <param name="type">The <typeparamref name="TIntermediateType"/> 
        /// to remove.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="type"/> is null.</exception>
        void Remove(TIntermediateType type);
        /// <summary>
        /// Removes the <typeparamref name="TIntermediateType"/> instance
        /// at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based <see cref="Int32"/> index
        /// of the <typeparamref name="TIntermediateType"/> element to remove.</param>
        void RemoveAt(int index);
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeParent"/>
        /// which contains the <see cref="IIntermediateTypeDictionary{TType, TIntermediateType}"/>.
        /// </summary>
        IIntermediateTypeParent Parent { get; }
        /// <summary>
        /// Removes the provided <paramref name="TIntermediateType"/> from the
        /// <see cref="IIntermediateTypeDictionary{TType, TIntermediateType}"/>
        /// </summary>
        /// <param name="type">The <typeparamref name="TIntermediateType"/> 
        /// to remove.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="type"/> is null or not
        /// contained within the <see cref="IIntermediateTypeDictionary{TType, TIntermediateType}"/>.</exception>
        void RemoveSoft(TIntermediateType type);
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateType"/> which 
        /// has the <paramref name="uniqueID"/> provided.
        /// </summary>
        /// <param name="uniqueID">The <see cref="String"/> form of the type's name
        /// and potentially the number of generic parameters delimited with a grave accent.</param>
        /// <returns>A <typeparamref name="TIntermediateType"/> which contains the 
        /// <paramref name="uniqueID"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="uniqueID"/> is null.</exception>
        new TIntermediateType this[string uniqueID] { get; }
    }
}
