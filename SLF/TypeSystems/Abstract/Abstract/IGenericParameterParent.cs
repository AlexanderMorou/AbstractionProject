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
    }
    /// <summary>
    /// Defines properties and methods for working with a parent that contains
    /// generic parameters.
    /// </summary>
    public interface IGenericParamParent
    {
        /// <summary>
        /// Returns the <see cref="IGenericParameterDictionary"/> used by the <see cref="IGenericParamParent"/>.
        /// </summary>
        IGenericParameterDictionary TypeParameters { get; }
    }
}
