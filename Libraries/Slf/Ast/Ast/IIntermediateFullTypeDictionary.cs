using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// <see cref="IIntermediateTypeParent"/>'s 
    /// grouped types.
    /// </summary>
    /// <remarks>
    /// Used to provide verbatim emission of types as they were 
    /// added to the respective type-specific dictionaries and occlude,
    /// differing kind, name collisions.</remarks>
    public interface IIntermediateFullTypeDictionary :
        IIntermediateFullDeclarationDictionary<IGeneralTypeUniqueIdentifier, IType, IIntermediateType>,
        IFullTypeDictionary,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateTypeParent"/> that contains the 
        /// <see cref="IIntermediateFullTypeDictionary"/>.
        /// </summary>
        IIntermediateTypeParent Parent { get; }

        /// <summary>
        /// Removes the <paramref name="type"/> provided from the 
        /// <see cref="IIntermediateFullTypeDictionary"/> and the subordinate 
        /// dictionary that contains it.
        /// </summary>
        /// <param name="type">The <see cref="IIntermediateType"/>
        /// to remove.</param>
        /// <remarks>Does not dispose the <paramref name="type"/>.</remarks>
        void RemoveSoft(IIntermediateType type);

        /// <summary>
        /// Adds a new <see cref="IIntermediateType"/> with the
        /// <paramref name="name"/> and of the <paramref name="kind"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name the type is identified by from the other
        /// types.</param>
        /// <param name="kind">The <see cref="TypeKind"/> of the type.</param>
        /// <returns>An <see cref="IIntermediateType"/> instance with the
        /// <paramref name="name"/> and of the <paramref name="kind"/>
        /// provided.</returns>
        IIntermediateType Add(string name, TypeKind kind);

        /// <summary>
        /// Adds a new <see cref="IIntermediateGenericType"/> with the
        /// <paramref name="name"/>, <paramref name="genParamData"/> 
        /// and of the <paramref name="kind"/> provided.
        /// </summary>
        /// <param name="name">The name the type is identified by from the other
        /// types.</param>
        /// <param name="kind">The <see cref="TypeKindGeneric"/> of the type.</param>
        /// <param name="genParamData">The series of 
        /// <see cref="GenericParameterData"/> associated to the 
        /// <see cref="IIntermediateGenericType"/> to be generated.</param>
        /// <returns>An <see cref="IIntermediateGenericType"/> with the
        /// <paramref name="name"/>, <paramref name="genParamData"/> 
        /// and of the <paramref name="kind"/> provided.</returns>
        IIntermediateGenericType Add(string name, TypeKindGeneric kind, params GenericParameterData[] genParamData);

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

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/> elements
        /// which are exclusively defined on the owning context, if applicable.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/> elements
        /// which are exclusively defined on the owning context, if applicable.</returns>
        /// <remarks>If not applicable, all members are returned.</remarks>
        IEnumerable<KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IIntermediateType>>> ExclusivelyOnParent();
    }
}
