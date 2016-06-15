/*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


using System.Runtime.InteropServices;
namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a <see cref="IType{TTypeIdentifier, TType}"/> which
    /// contains generic parameters.
    /// </summary>
    /// <typeparam name="TType">The type of <see cref="IType{TTypeIdentifier, TType}"/> in the current
    /// implementation.</typeparam>
    public interface IGenericType<TTypeIdentifier, TType> :
        IType<TTypeIdentifier, TType>,
        IGenericParamParent<IGenericTypeParameter<TTypeIdentifier, TType>, TType>,
        IGenericType
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier
        where TType :
            IGenericType<TTypeIdentifier, TType>
    {
        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericType{TType}"/>
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IControlledTypeCollection"/> 
        /// used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The current <see cref="IGenericType{TType}"/>'s 
        /// <seealso cref="IGenericParamParent.IsGenericConstruct"/>
        /// is false.</exception>
        new TType MakeGenericClosure(IControlledTypeCollection typeParameters);
        /// <summary>
        /// Returns a <typeparamref name="TType"/> instance that is the 
        /// closed generic form of the current <see cref="IGenericType{TType}"/> 
        /// using the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/> 
        /// collection used to fill in the type-parameters.</param>
        /// <returns>A new closed <typeparamref name="TType"/> instance with 
        /// the <paramref name="typeParameters"/> provided.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// <seealso cref="IGenericParamParent.IsGenericConstruct"/> 
        /// of the current instance is false.</exception>
        new TType MakeGenericClosure(params IType[] typeParameters);
    }
}
