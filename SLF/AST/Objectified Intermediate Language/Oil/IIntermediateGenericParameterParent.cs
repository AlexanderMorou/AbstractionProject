using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// <summary>
        /// Occurs when a type-parameter is inserted into the 
        /// <see cref="IIntermediateGenericParameterParent{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        new event EventHandler<EventArgsR1<TIntermediateGenericParameter>> TypeParameterAdded;
        /// <summary>
        /// Occurs when a type-parameter is removed from the 
        /// <see cref="IIntermediateGenericParameterParent{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        new event EventHandler<EventArgsR1<TIntermediateGenericParameter>> TypeParameterRemoved;
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
        /// <summary>
        /// Occurs when a type-parameter is inserted into the 
        /// <see cref="IIntermediateGenericParameterParent"/>.
        /// </summary>
        event EventHandler<EventArgsR1<IIntermediateGenericParameter>> TypeParameterAdded;
        /// <summary>
        /// Occurs when a type-parameter is removed from the 
        /// <see cref="IIntermediateGenericParameterParent"/>.
        /// </summary>
        event EventHandler<EventArgsR1<IIntermediateGenericParameter>> TypeParameterRemoved;
    }
}
