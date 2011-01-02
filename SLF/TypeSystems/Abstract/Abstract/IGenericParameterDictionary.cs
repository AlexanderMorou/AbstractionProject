using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a
    /// generic parameter dictionary.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of 
    /// <see cref="IGenericParameter{TGenericParameter, TParent}"/>
    /// that is contained by the
    /// <see cref="IGenericParamParent{TGenericParameter, TParent}"/>.</typeparam>
    /// <typeparam name="TParent">The type of 
    /// <see cref="IGenericParamParent{TGenericParameter, TParent}"/> in the
    /// current implmentation.</typeparam>
    public interface IGenericParameterDictionary<TGenericParameter, TParent> :
        IDeclarationDictionary<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
    {
        /// <summary>
        /// Returns the <see cref="IGenericParamParent{TGenericParameter, TParent}"/> which
        /// contains the <see cref="IGenericParameter{TGenericParameter, TParent}"/>.
        /// </summary>
        TParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a series of 
    /// generic parameters.
    /// </summary>
    public interface IGenericParameterDictionary  :
        IDeclarationDictionary
    {
    }
}
