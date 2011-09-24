using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
            if (this.Locked)
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
            if (name == null)
                throw new ArgumentNullException("name");
            TIntermediateGenericParameter result = this.GetNew(name);
            if (this.ContainsKey(result.UniqueIdentifier))
                throw new ArgumentException("Name");
            this._Add(result.UniqueIdentifier, result);
            return result;
        }

        public TIntermediateGenericParameter Add(GenericParameterData genericParameterData)
        {
            if (this.Locked)
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
            if (string.IsNullOrEmpty(genericParameterData.Name))
                throw new ArgumentException("genericParameterData");
            var result = this.GetNew(genericParameterData.Name);
            if (this.ContainsKey(result.UniqueIdentifier))
                throw new ArgumentException("genericParameterData");
            foreach (var ctorSig in genericParameterData.Constructors.Signatures)
                result.Constructors.Add(ctorSig.Parameters.ToSeries());
            foreach (var eventGroup in genericParameterData.Events)
                result.Events.Add(eventGroup);
            ITypeCollectionBase typeParameters = null;
            ITypeCollectionBase methodTypeParameters = null;
            foreach (var constraint in genericParameterData.Constraints)
                if (constraint.ContainsSymbols())
                    result.Constraints.Add(constraint.SimpleSymbolDisambiguation(result));
                else
                    result.Constraints.Add(constraint);
            //foreach (var propertyGroup in genericParameterData.Properties)
                //result.Properties.Add
            foreach (var method in genericParameterData.Methods.Signatures)
                result.Methods.Add(new TypedName(method.Name, method.ReturnType), method.Parameters.ToSeries());
            this._Add(result.UniqueIdentifier, result);
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
            this._Clear();
            foreach (var k in items)
                base._Add(k.Key, k.Value);
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


        IIntermediateGenericParameter[] IIntermediateGenericParameterDictionary.AddRange(params GenericParameterData[] genericParameterData)
        {
            var resultOriginal = this.AddRange(genericParameterData);
            var result = new IIntermediateGenericParameter[resultOriginal.Length];
            for (int i = 0; i < resultOriginal.Length; i++)
                result[i] = resultOriginal[i];
            return result;
        }



        #region IIntermediateGenericParameterDictionary<TGenericParameter,TIntermediateGenericParameter,TParent,TIntermediateParent> Members


        public TIntermediateGenericParameter[] AddRange(params GenericParameterData[] genericParameterData)
        {
            if (this.Locked)
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
            if (genericParameterData == null)
                throw new ArgumentNullException("genericParameterData");
            TIntermediateGenericParameter[] result = new TIntermediateGenericParameter[genericParameterData.Length];
            string[] currentKeys = new string[result.Length];
            Parallel.For(0, genericParameterData.Length, i =>
            {
                var currentParameterData = genericParameterData[i];
                var current = this.GetNew(currentParameterData.Name);
                var currentUniqueId = current.UniqueIdentifier;
                if (this.ContainsKey(currentUniqueId) ||
                    currentKeys.Contains(currentUniqueId))
                    throw new ArgumentException("genericParameterData");
                foreach (var ctorSig in currentParameterData.Constructors.Signatures)
                    current.Constructors.Add(ctorSig.Parameters.ToSeries());
                foreach (var eventGroup in currentParameterData.Events)
                    current.Events.Add(eventGroup);
                //result.Properties.Add
                foreach (var method in currentParameterData.Methods.Signatures)
                    current.Methods.Add(new TypedName(method.Name, method.ReturnType), method.Parameters.ToSeries());
                currentKeys[i] = currentUniqueId;
                result[i] = current;
            });
            //Parallel.For(0, genericParameterData.Length, i =>
            for (int i = 0; i < genericParameterData.Length; i++)
                this._Add(currentKeys[i], result[i]);
            for (int i = 0; i < genericParameterData.Length; i++)
            {
                var currentParameterData = genericParameterData[i];
                var current = result[i];
                foreach (var constraint in currentParameterData.Constraints)
                    if (constraint.ContainsSymbols())
                        current.Constraints.Add(constraint.SimpleSymbolDisambiguation(current));
                    else
                        current.Constraints.Add(constraint);
            }//);
            return result;
        }

        #endregion


        protected override sealed bool ShouldDispose(TIntermediateGenericParameter declaration)
        {
            return true;
        }
    }
}