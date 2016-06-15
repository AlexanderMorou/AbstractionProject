/*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a 
    /// generic type's type parameter.
    /// </summary>
    /// <typeparam name="TType">The type of <see cref="IGenericType{TType}"/> 
    /// which contains the <see cref="IGenericTypeParameter{TType}"/> instances.</typeparam>
    public interface IGenericTypeParameter<TTypeIdentifier, TType> :
        IGenericParameter<IGenericTypeParameter<TTypeIdentifier, TType>, TType>,
        IGenericTypeParameter
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier
        where TType :
            IGenericType<TTypeIdentifier, TType>
    {

    }

    /// <summary>
    /// Defines properties and methods for working with a generic 
    /// type's type parameter.
    /// </summary>
    public interface IGenericTypeParameter :
        IGenericParameter
    {
        /// <summary>
        /// Returns the <see cref="IGenericType"/> the <see cref="IGenericTypeParameter"/>
        /// belongs to.
        /// </summary>
        new IGenericType Parent { get; }
    }
}
