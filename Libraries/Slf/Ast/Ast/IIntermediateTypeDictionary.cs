using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a series of specifically
    /// typed <see cref="IIntermediateType"/> instances.
    /// </summary>
    /// <typeparam name="TTypeIdentifier">The kind of type identifier used
    /// to differentiate the <typeparamref name="TIntermediateType"/>
    /// instances from one another.</typeparam>
    /// <typeparam name="TType">The type used in the dictionary by the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type used in the dictionary by the
    /// intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateTypeDictionary<TTypeIdentifier, TType, TIntermediateType> :
        IIntermediateGroupedDeclarationDictionary<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier :
            ITypeUniqueIdentifier
        where TType :
            IType<TTypeIdentifier, TType>
        where TIntermediateType :
            IIntermediateType,
            TType
    {
        /// <summary>
        /// Adds the <typeparamref name="TIntermediateType"/>
        /// <paramref name="type"/> provided.
        /// </summary>
        /// <param name="type">The <typeparamref name="TIntermediateType"/>
        /// instance to insert.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="type"/>'s <see cref="IIntermediateType.Parent"/> is invalid.</exception>
        void Add(TIntermediateType type);
        /// <summary>
        /// Adds a series of <typeparamref name="TIntermediateType"/> instances
        /// through the <paramref name="types"/> provided.
        /// </summary>
        /// <param name="types">The <see cref="IEnumerable{T}"/>
        /// of <typeparamref name="TIntermediateType"/> elements
        /// to insert.</param>
        void AddRange(IEnumerable<TIntermediateType> types);
        /// <summary>
        /// Adds a series of <typeparamref name="TIntermediateType"/> instances
        /// through the <paramref name="types"/> provided.
        /// </summary>
        /// <param name="types">The <typeparamref name="TIntermediateType"/>
        /// array to insert into the 
        /// <see cref="IIntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}"/>.</param>
        void AddRange(params TIntermediateType[] types);
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
        /// Creates and adds a new <typeparamref name="TIntermediateType"/> 
        /// instance with the <paramref name="format"/> and 
        /// <paramref name="replacements"/> provided.
        /// </summary>
        /// <param name="format">The <see cref="String"/> value denoting the format to manage
        /// the name of the result <typeparamref name="TIntermediateType"/>.</param>
        /// <param name="replacements">The <see cref="String"/> array denoting the 
        /// replacements within <paramref name="format"/> to use.</param>
        /// <returns>A <typeparamref name="TIntermediateType"/> with the name constructed from
        /// <paramref name="format"/> and <paramref name="replacements"/>.</returns>
        TIntermediateType Add(string format, params object[] replacements);
        /// <summary>
        /// Creates and inserts a new <typeparamref name="TIntermediateType"/> instance
        /// into the current <see cref="IIntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}"/>.
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
        /// <see cref="IIntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}"/>.
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
        /// which contains the <see cref="IIntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        IIntermediateTypeParent Parent { get; }
        /// <summary>
        /// Removes the provided <typeparamref name="TIntermediateType"/> from the
        /// <see cref="IIntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}"/>
        /// </summary>
        /// <param name="type">The <typeparamref name="TIntermediateType"/> 
        /// to remove.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="type"/> is null or not
        /// contained within the <see cref="IIntermediateTypeDictionary{TTypeIdentifier, TType, TIntermediateType}"/>.</exception>
        void RemoveSoft(TIntermediateType type);
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateType"/> which 
        /// has the <paramref name="uniqueID"/> provided.
        /// </summary>
        /// <param name="uniqueID">The <typeparamref name="TTypeIdentifier"/> form of the type's name
        /// and potentially the number of generic parameters (depending on the type identifier
        /// used.)</param>
        /// <returns>A <typeparamref name="TIntermediateType"/> which contains the 
        /// <paramref name="uniqueID"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="uniqueID"/> is null.</exception>
        new TIntermediateType this[TTypeIdentifier uniqueID] { get; }

        /// <summary>
        /// Returns an <see cref="Int32"/> value denoting the
        /// nubmer of types which belong to the 
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the namespaces.</param>
        /// <returns>An <see cref="Int32"/> value denoting the
        /// nubmer of types which belong to the 
        /// <paramref name="parent"/> provided.</returns>
        int GetCountFor(IIntermediateTypeParent parent);
    }
}
