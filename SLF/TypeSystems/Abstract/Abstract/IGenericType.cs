/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// Returns whether the current <see cref="IGenericType"/>
        /// even constitutes a construct which can contain generic closures.
        /// </summary>
        new bool IsGenericConstruct { get; }

        /// <summary>
        /// Forces the system to revalidate the type-parameters used
        /// for the given <see cref="IGenericType"/> instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when the 
        /// <see cref="IGenericType"/> is a generic type definition.</exception>
        void ReverifyTypeParameters();

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
        /// <seealso cref="IGenericParamParent.IsGenericDefinition"/> is false.</exception>
        new IGenericType MakeGenericClosure(ITypeCollectionBase typeParameters);

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
        /// The current <see cref="IGenericType"/>'s <seealso cref="IGenericParamParent.IsGenericDefinition"/>
        /// is false.</exception>
        new IGenericType MakeGenericClosure(params IType[] typeParameters);
        
    }
}
