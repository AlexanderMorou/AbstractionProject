﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base dictionary implementation for a series
    /// of intermediate generic parameters.
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
    public abstract class IntermediateGenericParameterDictionary<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> :
        IntermediateDeclarationDictionary<TGenericParameter, TIntermediateGenericParameter>,
        IIntermediateGenericParameterDictionary<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
        IIntermediateGenericParameterDictionary
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
        /// Creates a new <see cref="IntermediateGenericParameterDictionary{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/></param>
        public IntermediateGenericParameterDictionary(TIntermediateParent parent)
        {
            this.Parent = parent;
        }

        #region IIntermediateGenericParameterDictionary<TGenericParameter,TIntermediateGenericParameter,TParent,TIntermediateParent> Members

        /// <summary>
        /// Returns the  <typeparamref name="TIntermediateParent"/> which contains the 
        /// <typeparamref name="TIntermediateGenericParameter"/> series. 
        /// </summary>
        public TIntermediateParent Parent { get; private set; }

        #endregion

        #region IGenericParameterDictionary<TGenericParameter,TParent> Members

        TParent IGenericParameterDictionary<TGenericParameter, TParent>.Parent
        {
            get { return this.Parent; }
        }

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
        public TIntermediateGenericParameter Add(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            TIntermediateGenericParameter result = this.GetNew(name);
            if (this.ContainsKey(result.UniqueIdentifier))
                throw new ArgumentException("Name");
            this.Add(result.UniqueIdentifier, result);
            return result;
        }

        public TIntermediateGenericParameter Add(GenericParameterData genericParameterData)
        {
            if (string.IsNullOrEmpty(genericParameterData.Name))
                throw new ArgumentException("genericParameterData");
            var result = this.GetNew(genericParameterData.Name);
            if (this.ContainsKey(result.UniqueIdentifier))
                throw new ArgumentException("genericParameterData");
            if (genericParameterData.RequiresBlankConstructor)
                result.RequiresNewConstructor = true;
            foreach (var ctorSig in genericParameterData.Constructors.Signatures)
                result.Constructors.Add(ctorSig.Parameters.ToSeries());
            foreach (var eventGroup in genericParameterData.Events)
                result.Events.Add(eventGroup);
            //foreach (var propertyGroup in genericParameterData.Properties)
                //result.Properties.Add
            foreach (var method in genericParameterData.Methods.Signatures)
                result.Methods.Add(new TypedName(method.Name, method.ReturnType), method.Parameters.ToSeries());
            this.Add(result.UniqueIdentifier, result);
            return result;
        }

        #endregion

        /// <summary>
        /// Obtains a new <typeparamref name="TIntermediateGenericParameter"/> with
        /// the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name of the
        /// type-parameter.</param>
        /// <returns>A new <typeparamref name="TIntermediateGenericParameter"/> instance.</returns>
        protected abstract TIntermediateGenericParameter GetNew(string name);

        #region IIntermediateGenericParameterDictionary Members

        IIntermediateGenericParameterParent IIntermediateGenericParameterDictionary.Parent
        {
            get { return this.Parent; }
        }

        IIntermediateGenericParameter IIntermediateGenericParameterDictionary.Add(string name)
        {
            return this.Add(name);
        }

        IIntermediateGenericParameter IIntermediateGenericParameterDictionary.Add(GenericParameterData genericParameterData)
        {
            return this.Add(genericParameterData);
        }

        public void Rearrange(int from, int to)
        {
            if (from < 0 || from >= this.Count)
                throw new ArgumentOutOfRangeException("from");
            if (to < 0 || to >= this.Count)
                throw new ArgumentOutOfRangeException("to");
            if (from == to)
                return;
            var items = this.ToArray();
            bool backwards = from > to;
            var item = items[from];
            if (backwards)
                for (int i = from; i > to; i--)
                    items[i] = items[i - 1];
            else
                for (int i = from; i < to; i++)
                    items[i] = items[i + 1];
            items[to] = item;
            this.Clear();
            foreach (var k in items)
                base.Add(k.Key, k.Value);
            this.OnRearranged(new GenericParameterMovedEventArgs(from,to, (TIntermediateGenericParameter)item.Value));
        }

        /// <summary>
        /// Occurs when a member of the listing is moved.
        /// </summary>
        public event EventHandler<GenericParameterMovedEventArgs> Rearranged;

        #endregion

        protected virtual void OnRearranged(GenericParameterMovedEventArgs e)
        {
            if (!e.InRange(this.Count))
                return;
            if (this.Rearranged != null)
                this.Rearranged(this, e);
        }



    }
}