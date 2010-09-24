using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a 
    /// series of generic parameters in an intermediate context.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of <see cref="IGenericParameter{TGenericParameter, TParent}"/>
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateGenericParameter">The type of
    /// <see cref="IIntermediateGenericParameter{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParent">The type of <see cref="IGenericParamParent{TGenericParameter, TParent}"/>
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of 
    /// <see cref="IIntermediateGenericParameterParent{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericParameterDictionary<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> :
        IIntermediateDeclarationDictionary<TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameterDictionary<TGenericParameter, TParent>
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
        /// Returns the  <typeparamref name="TIntermediateParent"/> which contains the 
        /// <typeparamref name="TIntermediateGenericParameter"/> series. 
        /// </summary>
        new TIntermediateParent Parent { get; }

        /// <summary>
        /// Rearranges the type parameters and takes the element at <paramref name="from"/>
        /// and places it <paramref name="to"/> its destination.
        /// </summary>
        /// <param name="from">The initial location of the generic parameter.</param>
        /// <param name="to">The final location of the generic parameter.</param>
        void Rearrange(int from, int to);

        /// <summary>
        /// Occurs when a member of the listing is moved.
        /// </summary>
        event EventHandler<GenericParameterMovedEventArgs> Rearranged;

        /// <summary>
        /// Adds a new type-parameter by <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name of the
        /// type-parameter.</param>
        /// <returns>A new <typeparamref name="TIntermediateGenericParameter"/> instance.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> overlaps
        /// an existing type-parameter name.</exception>
        TIntermediateGenericParameter Add(string name);
        /// <summary>
        /// Adds a new type-parameter by specifying the generic full set of generic
        /// parameter data.
        /// </summary>
        /// <param name="genericParameterData">The <see cref="GenericParameterData"/> 
        /// which specifies the name, constructors, methods, and interface
        /// constraints of the <typeparamref name="TIntermediateGenericParameter"/>
        /// which results.</param>
        /// <returns>A new <typeparamref name="TIntermediateGenericParameter"/> instance.</returns>
        /// <exception cref="System.ArgumentException">thrown when the <see cref="GenericParameterData.Name"/>
        /// is <see cref="String.Empty"/> or null, or when the <see cref="GenericParameterData.Name"/>
        /// collides with another type-parameter name.</exception>
        TIntermediateGenericParameter Add(GenericParameterData genericParameterData);
        /// <summary>
        /// Adds a new series of type-parameters by specifying the full set of
        /// generic parameter data.
        /// </summary>
        /// <param name="genericParameterData">The <see cref="GenericParameterData"/> array
        /// associated to the elements to add.</param>
        /// <returns>A series of <typeparamref name="TIntermediateGenericParameter"/>
        /// elements resulted from the operation.</returns>
        TIntermediateGenericParameter[] AddRange(params GenericParameterData[] genericParameterData);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// generic parameters in an intermediate context.
    /// </summary>
    public interface IIntermediateGenericParameterDictionary :
        IIntermediateDeclarationDictionary,
        IGenericParameterDictionary
    {
        /// <summary>
        /// Returns the 
        /// <see cref="IIntermediateGenericParameterParent"/> 
        /// which contains the 
        /// <see cref="IIntermediateGenericParameter"/> series.
        /// </summary>
        IIntermediateGenericParameterParent Parent { get; }
        /// <summary>
        /// Rearranges the type parameters and takes the element at <paramref name="from"/>
        /// and places it <paramref name="to"/> its destination.
        /// </summary>
        /// <param name="from">The initial location of the generic parameter.</param>
        /// <param name="to">The final location of the generic parameter.</param>
        void Rearrange(int from, int to);
        /// <summary>
        /// Occurs when a member of the listing is moved.
        /// </summary>
        event EventHandler<GenericParameterMovedEventArgs> Rearranged;

        /// <summary>
        /// Adds a new type-parameter by <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name of the
        /// type-parameter.</param>
        /// <returns>A new <see cref="IIntermediateGenericParameter"/> instance.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> 
        /// is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> overlaps
        /// an existing type-parameter name.</exception>
        IIntermediateGenericParameter Add(string name);
        /// <summary>
        /// Adds a new type-parameter by specifying the generic full set of generic
        /// parameter data.
        /// </summary>
        /// <param name="genericParameterData">The <see cref="GenericParameterData"/> 
        /// which specifies the name, constructors, methods, and interface
        /// constraints of the <see cref="IIntermediateGenericParameter"/>
        /// which results.</param>
        /// <returns>A new <see cref="IIntermediateGenericParameter"/> instance.</returns>
        /// <exception cref="System.ArgumentException">thrown when the <see cref="GenericParameterData.Name"/>
        /// is <see cref="String.Empty"/> or null, or when the <see cref="GenericParameterData.Name"/>
        /// collides with another type-parameter name.</exception>
        IIntermediateGenericParameter Add(GenericParameterData genericParameterData);
        /// <summary>
        /// Adds a new series of type-parameters by specifying the full set of
        /// generic parameter data.
        /// </summary>
        /// <param name="genericParameterData">The <see cref="GenericParameterData"/> array
        /// associated to the elements to add.</param>
        /// <returns>A series of <see cref="IIntermediateGenericParameter"/>
        /// elements resulted from the operation.</returns>
        IIntermediateGenericParameter[] AddRange(params GenericParameterData[] genericParameterData);
    }
}
