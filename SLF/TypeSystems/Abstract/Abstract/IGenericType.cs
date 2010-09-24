using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a type 
    /// which contains generic parameters.
    /// </summary>
    public interface IGenericType :
        IType,
        IGenericParamParent,
        IMassTargetHandler
    {

        /// <summary>
        /// Returns whether the current type is an open generic 
        /// type that can be made into other closed generic type 
        /// instances.
        /// </summary>
        bool IsGenericTypeDefinition { get; }

        /// <summary>
        /// Returns whether the <see cref="IGenericType"/> contains
        /// generic parameters.
        /// </summary>
        bool ContainsGenericParameters { get; }

        /// <summary>
        /// Returns a <see cref="ITypeCollection"/> which relates 
        /// to the current generic type's type-parameters.
        /// </summary>
        /// <remarks>Differs from <see cref="IGenericParamParent.TypeParameters"/>
        /// by containing a full series of type-parameters.</remarks>
        ILockedTypeCollection GenericParameters { get; }
        
        /// <summary>
        /// Returns a <see cref="IGenericType"/> instance that is 
        /// the closed generic form of the current <see cref="IGenericType"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">
        /// The locked <see cref="IType"/> collection used to fill in 
        /// the type-parameters.</param>
        /// <returns>
        /// A new closed <see cref="IGenericType"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericType"/>'s 
        /// <seealso cref="IsGenericTypeDefinition"/> is false.</exception>
        IGenericType MakeGenericType(ITypeCollectionBase typeParameters);

        /// <summary>
        /// Returns a <see cref="IGenericType"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericType"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <see cref="IGenericType"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericType"/>'s <seealso cref="IsGenericTypeDefinition"/>
        /// is false.</exception>
        IGenericType MakeGenericType(params IType[] typeParameters);

        /// <summary>
        /// Makes a verified generic type instance that bypasses
        /// the type-parameter constraint check for testing purposes.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/>
        /// which contains the series of <see cref="IType"/>
        /// instances to instantiate the closed generic type with.</param>
        /// <returns>A <see cref="IType"/> instance that represents the 
        /// current generic <see cref="IType"/></returns>
        /// <remarks>Improper use of this method can result in translator 
        /// producing bad code.  Primarily intended for internal use.</remarks>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericType"/>'s
        /// <seealso cref="IsGenericTypeDefinition"/> is false.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IGenericType MakeVerifiedGenericType(ITypeCollectionBase typeParameters);

        /// <summary>
        /// Forces the system to revalidate the type-parameters used
        /// for the given <see cref="IGenericType"/> instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when the 
        /// <see cref="IGenericType"/> is a generic type definition.</exception>
        void ReverifyTypeParameters();
    }
}
