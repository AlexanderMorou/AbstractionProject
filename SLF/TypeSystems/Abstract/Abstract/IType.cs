using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.ComponentModel;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a type.
    /// </summary>
    public interface IType :
        IEquatable<IType>,
        ICustomAttributedDeclaration,
        IScopedDeclaration
    {
        /// <summary>
        /// Returns the special classification given to <see cref="ElementType"/>.
        /// </summary>
        TypeElementClassification ElementClassification { get; }

        /// <summary>
        /// Returns the element type of special classification types.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when
        /// <see cref="ElementClassification"/> is <see cref="TypeElementClassification.None"/>.</exception>
        IType ElementType { get; }

        /// <summary>
        /// Returns whether the <see cref="IType"/> is 
        /// an <see cref="IGenericParameter"/>.
        /// </summary>
        bool IsGenericTypeParameter { get; }

        /// <summary>
        /// Returns whether the current type is a generic type with <see cref="IGenericType.GenericParameters"/>.
        /// </summary>
        bool IsGenericConstruct { get; }

        /// <summary>
        /// Returns the <see cref="IType"/> in which the current <see cref="IType"/> is declared.
        /// </summary>
        IType DeclaringType { get; }

        /// <summary>
        /// Returns the kind of type the <see cref="IType"/> is.
        /// </summary>
        TypeKind Type { get; }

        /// <summary>
        /// Returns whether the current <see cref="IType"/> is nullable.
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        /// Creates a new <see cref="IArrayType"/> with the <paramref name="rank"/> provided.
        /// </summary>
        /// <param name="rank">The array rank.</param>
        /// <returns>A new <see cref="IType"/> as an array with the 
        /// <paramref name="rank"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="rank"/> is zero or below.</exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the current <see cref="IType"/> is a pointer 
        /// or by-reference type.</exception>
        IArrayType MakeArray(int rank);

        /// <summary>
        /// Creates a new single-dimension <see cref="IArrayType"/>.
        /// </summary>
        /// <returns>A new single-dimension <see cref="IArrayType"/>.</returns>
        IArrayType MakeArray();

        /// <summary>
        /// Creates a new non-standard multi-dimensional 
        /// or single-dimension array with the 
        /// <paramref name="lowerBounds"/> of each dimension
        /// specified.
        /// </summary>
        /// <param name="lowerBounds">The <see cref="Int32"/> which
        /// represents the lower-bounds of the <see cref="IArrayType"/> resulted.</param>
        /// <returns>A <see cref="IArrayType"/> </returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="lowerBounds"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="lowerBounds"/>
        /// had zero elements.</exception>
        IArrayType MakeArray(params int[] lowerBounds);

        /// <summary>
        /// Creates a new pointer <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> as a pointer type.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is a by-reference type.</exception>
        IType MakePointer();

        /// <summary>
        /// Creates a new by-reference <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> by-reference.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the current
        /// <see cref="IType"/> is already a by-reference type.</exception>
        IType MakeByReference();

        /// <summary>
        /// Creates a new nullable <see cref="IType"/>.
        /// </summary>
        /// <returns>A new <see cref="IType"/> as a nullable type.</returns>
        /// <exception cref="System.InvalidOperationException">thrown when the current <see cref="IType"/>
        /// is a poinoter, array, generic type definition, by-reference, or when 
        /// <see cref="Type"/> is something other than <see cref="TypeKind.Struct"/>.</exception>
        IType MakeNullable();

        /// <summary>
        /// Returns whether the current <see cref="IType"/> is a sub-class of <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The <see cref="IType"/> to check if the current type is a
        /// sub-class of.</param>
        /// <returns>true if the current <see cref="IType"/> is a sub-class of <paramref name="other"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="other"/> is null.</exception>
        bool IsSubclassOf(IType other);

        /// <summary>
        /// Returns whether the current <see cref="IType"/> is assignable from the
        /// <paramref name="target"/> provided.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to see if the current type is assignable from.</param>
        /// <returns>True if <paramref name="target"/> contains the current <see cref="IType"/> in its
        /// implemented interfaces, if <paramref name="target"/> inherits the current <see cref="IType"/>, and
        /// if <paramref name="target"/> equals the current <see cref="IType"/>; false otherwise.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="target"/> is null.</exception>
        bool IsAssignableFrom(IType target);

        /// <summary>
        /// Returns the full name of the <see cref="IType"/>.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Returns the namespace in which the <see cref="IType"/> is declared.
        /// </summary>
        [DebuggerDisplay("{NamespaceName}")]
        INamespaceDeclaration Namespace { get; }

        /// <summary>
        /// Returns the name of the namespace in which the <see cref="IType"/>
        /// is declared.
        /// </summary>
        string NamespaceName { get; }

        /// <summary>
        /// Returns the base type of the current <see cref="IType"/>.
        /// </summary>
        IType BaseType { get; }

        /// <summary>
        /// Returns a collection of <see cref="IType"/> instances that are implemented by the current
        /// <see cref="IType"/>.
        /// </summary>
        ILockedTypeCollection ImplementedInterfaces { get; }

        /// <summary>
        /// Returns the <see cref="IAssembly"/> in which the <see cref="IType"/> is declared
        /// </summary>
        IAssembly Assembly { get; }

        /// <summary>
        /// Returns the <see cref="IFullMemberDictionary"/> of 
        /// a series of <see cref="IGroupedMemberDictionary"/>.
        /// </summary>
        /// <exception cref="System.NotSupportedException">The <see cref="IType"/>
        /// does not support members.</exception>
        IFullMemberDictionary Members { get; }


        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of the elements contained within the 
        /// <see cref="IType"/>.
        /// </summary>
        IEnumerable<IDeclaration> Declarations { get; }
    }
}
