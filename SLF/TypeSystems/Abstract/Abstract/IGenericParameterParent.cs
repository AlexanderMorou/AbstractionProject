/*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a <see cref="IGenericParamParent"/> that
    /// has a need for <typeparamref name="TGenericParameter"/>
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of <see cref="IGenericParameter{TGenericParameter, TParent}"/>
    /// that is contained by the <see cref="IGenericParamParent{TGenericParameter, TParent}"/>.</typeparam>
    /// <typeparam name="TParent">The type of <see cref="IGenericParamParent{TGenericParameter, TParent}"/> in the
    /// current implmentation.</typeparam>
    public interface IGenericParamParent<TGenericParameter, TParent> :
        IGenericParamParent
        where TGenericParameter :
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameterDictionary{TGenericParameter, TParent}"/> used by the <see cref="IGenericParamParent{TGenericParameter, TParent}"/>.
        /// </summary>
        new IGenericParameterDictionary<TGenericParameter, TParent> TypeParameters { get; }

        /// <summary>
        /// Returns a <typeparamref name="TParent"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericParamParent{TGenericParameter, TParent}"/>
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ITypeCollectionBase"/> 
        /// used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TParent"/> instance with
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericParamParent{TGenericParameter, TParent}"/>'s 
        /// <seealso cref="IGenericParamParent.IsGenericConstruct"/>
        /// is false.</exception>
        new TParent MakeGenericClosure(ITypeCollectionBase typeParameters);

        /// <summary>
        /// Returns a <typeparamref name="TParent"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericParamParent{TGenericParameter, TParent}"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TParent"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <seealso cref="IGenericParamParent.IsGenericConstruct"/> 
        /// of the current instance is false.</exception>
        new TParent MakeGenericClosure(params IType[] typeParameters);
    }

    /// <summary>
    /// Defines properties and methods for working with a parent that contains
    /// generic parameters.
    /// </summary>
    public interface IGenericParamParent :
        IDeclaration
    {
        /// <summary>
        /// Returns whether the current type is an open generic 
        /// type that can be made into other closed generic type 
        /// instances.
        /// </summary>
        bool IsGenericDefinition { get; }

        /// <summary>
        /// Returns whether the current <see cref="IGenericParamParent"/>
        /// even constitutes a construct which can contain generic closures.
        /// </summary>
        bool IsGenericConstruct { get; }
        
        /// <summary>
        /// Returns the <see cref="IGenericParameterDictionary"/> used by the <see cref="IGenericParamParent"/>.
        /// </summary>
        IGenericParameterDictionary TypeParameters { get; }

        /// <summary>
        /// Returns a <see cref="IGenericParamParent"/> instance that is 
        /// the closed generic form of the current <see cref="IGenericParamParent"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">
        /// The locked <see cref="IType"/> collection used to fill in 
        /// the type-parameters.</param>
        /// <returns>
        /// A new closed <see cref="IGenericParamParent"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericParamParent"/>'s 
        /// <seealso cref="IsGenericDefinition"/> is false.</exception>
        IGenericParamParent MakeGenericClosure(ITypeCollectionBase typeParameters);

        /// <summary>
        /// Returns a <see cref="IGenericParamParent"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericType"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <see cref="IGenericType"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericType"/>'s <seealso cref="IsGenericDefinition"/>
        /// is false.</exception>
        IGenericParamParent MakeGenericClosure(params IType[] typeParameters);

        /// <summary>
        /// Returns whether the <see cref="IGenericParamParent"/> contains
        /// generic parameters.
        /// </summary>
        bool ContainsGenericParameters { get; }

        /// <summary>
        /// Returns a <see cref="ITypeCollection"/> which relates 
        /// to the current generic parent's type-parameters.
        /// </summary>
        /// <remarks>Differs from <see cref="TypeParameters"/>
        /// by containing the full series of type-parameters within
        /// a potentially layered hierarchy.</remarks>
        ILockedTypeCollection GenericParameters { get; }
    }
}
