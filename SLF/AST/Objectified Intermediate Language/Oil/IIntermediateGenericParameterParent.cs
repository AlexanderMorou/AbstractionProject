using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Defines generic properties and methods for working with 
    /// a parent of generic parameters in an intermediate context.
    /// </summary>
    /// <typeparam name="TGenericParameter"></typeparam>
    /// <typeparam name="TIntermediateGenericParameter"></typeparam>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TIntermediateParent"></typeparam>
    public interface IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> :
        IGenericParamParent<TGenericParameter, TParent>,
        IIntermediateGenericParameterParent
        where TGenericParameter :
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
    {
        /// <summary>
        /// Returns the generic parameter dictionary used by the 
        /// <see cref="IIntermediateGenericParameterParent{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        new IIntermediateGenericParameterDictionary<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> TypeParameters { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a parent of
    /// generic parameters in an intermediate context.
    /// </summary>
    public interface IIntermediateGenericParameterParent :
        IGenericParamParent
    {
        /// <summary>
        /// Returns the generic parameter dictionary used by the
        /// <see cref="IIntermediateGenericParameterParent"/>.
        /// </summary>
        new IIntermediateGenericParameterDictionary TypeParameters { get; }
    }
}
