using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
    public interface IGenericTypeParameter<TType> :
        IGenericParameter<IGenericTypeParameter<TType>, TType>,
        IType<IGenericTypeParameter<TType>>,
        IGenericTypeParameter
        where TType :
            IGenericType<TType>
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
