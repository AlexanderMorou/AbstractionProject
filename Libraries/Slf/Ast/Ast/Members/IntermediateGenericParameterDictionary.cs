using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Properties;
//using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
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
        IntermediateDeclarationDictionary<IGenericParameterUniqueIdentifier, TGenericParameter, TIntermediateGenericParameter>,
        IIntermediateGenericParameterDictionary<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
        IIntermediateGenericParameterDictionary
        where TGenericParameter :
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TGenericParameter
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

        internal abstract IType Disambiguate(IType ambiguousType);

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
                throw new ArgumentException("name");
            this._Add(result.UniqueIdentifier, result);
            return result;
        }

        public TIntermediateGenericParameter Add(GenericParameterData genericParameterData)
        {
            if (this.Locked)
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
            if (string.IsNullOrEmpty(genericParameterData.Name))
                throw new ArgumentException("genericParameterData");
            int index = this.Count;
            var defaultParamId = TypeSystemIdentifiers.GetGenericParameterIdentifier(index, this.Parent is IType);
            var result = this.GetNew(genericParameterData.Name);
            if (this.ContainsKey(defaultParamId))
                throw new ArgumentException("genericParameterData");
            foreach (var ctorSig in genericParameterData.Constructors.Signatures)
                result.Constructors.Add(TransposeTypedNames(ctorSig.Parameters.ToSeries(), result));
            var disambiguatedEvents = TransposeTypedNames(genericParameterData.Events, result);
            foreach (var eventGroup in disambiguatedEvents)
                result.Events.Add(eventGroup);
            IControlledTypeCollection typeParameters = null;
            IControlledTypeCollection methodTypeParameters = null;
            foreach (var constraint in genericParameterData.Constraints)
                if (constraint.ContainsSymbols())
                    if (constraint.ContainsGenericParameters())
                        result.Constraints.Add(Disambiguate(constraint).SimpleSymbolDisambiguation(result));
                    else
                        result.Constraints.Add(constraint.SimpleSymbolDisambiguation(result));
                else if (constraint.ContainsGenericParameters())
                    result.Constraints.Add(Disambiguate(constraint));
                else
                    result.Constraints.Add(constraint);
            result.SpecialConstraint = genericParameterData.SpecialConstraint;
            //foreach (var propertyGroup in genericParameterData.Properties)
                //result.Properties.Add
            foreach (var method in genericParameterData.Methods.Signatures)
                result.Methods.Add(new TypedName(method.Name, method.ReturnType), method.Parameters.ToSeries());
            this._Add(defaultParamId, result);
            this.Keys[index] = result.UniqueIdentifier;
            return result;
        }

        private TypedNameSeries TransposeTypedNames(TypedNameSeries series, TIntermediateGenericParameter target)
        {
            return new TypedNameSeries((from s in series
                                        select TransposeTypedName(s, target)).ToArray());
                   
        }

        private TypedName TransposeTypedName(TypedName original, TIntermediateGenericParameter target)
        {
            if (original.Source == TypedNameSource.TypeReference)
                return TransposeTypedName(new TypedName(original.Name, original.SymbolReference.GetSymbolType(), original.Direction), target);
            else if (original.Source == TypedNameSource.TypeReference)
            {
                var type = original.TypeReference;
                type = TransposeType(type, target);
                return new TypedName(original.Name, type, original.Direction);
            }
            return original;
        }

        private IType TransposeType(IType type, TIntermediateGenericParameter target)
        {
            //if (type.ContainsGenericParameters())
            //    type = Disambiguate(type);
            if (type.ContainsSymbols())
                type = type.SimpleSymbolDisambiguation(target);
            return type;
        }

        #endregion

        /// <summary>
        /// Obtains the <typeparamref name="TIntermediateGenericParameter"/>
        /// by the <paramref name="name"/> provided..
        /// </summary>
        /// <param name="name">The <see cref="String"/> value used to differentiate the 
        /// <typeparamref name="TIntermediateGenericParameter"/> elements.</param>
        /// <returns>The <typeparamref name="TIntermediateGenericParameter"/>
        /// with the <paramref name="name"/> provided.</returns>
        public TIntermediateGenericParameter this[string name]
        {
            get
            {
                for (int i = 0, c = this.Count; i < c; i++)
                    if (this.Keys[i].Name == name)
                        return this.Values[i];
                throw new KeyNotFoundException();
            }
        }

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
            this.SkipEvents = true;
            foreach (var k in items)
                base._Add(k.Key, k.Value);
            this.SkipEvents = false;
            this.OnRearranged(new GenericParameterMovedEventArgs(from, to, (TIntermediateGenericParameter)item.Value));
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
            IGenericParameterUniqueIdentifier[] currentKeys = new IGenericParameterUniqueIdentifier[result.Length];

            //Parallel.For(0, genericParameterData.Length, i =>
            for (int i = 0; i < genericParameterData.Length; i++)
            {
                var currentName = genericParameterData[i].Name;
                var current = this.GetNew(currentName);
                var currentUniqueId = TypeSystemIdentifiers.GetGenericParameterIdentifier(i + this.Count, currentName, Parent is IType);
                if (this.ContainsKey(currentUniqueId) ||
                    currentKeys.Contains(currentUniqueId))
                    throw new ArgumentException("genericParameterData");
                currentKeys[i] = currentUniqueId;
                result[i] = current;
            }//**/);
            this._AddRange((from i in 0.RangeTo(genericParameterData.Length)
                            select new KeyValuePair<IGenericParameterUniqueIdentifier, TGenericParameter>(currentKeys[i], result[i])));
            //Parallel.For(0, genericParameterData.Length, i=>
            for (int i  = 0; i < genericParameterData.Length; i++)
            {
                var currentParameterData = genericParameterData[i];
                var current = result[i];
                foreach (var constraint in currentParameterData.Constraints)
                    current.Constraints.Add(TransposeType(constraint, current));
                foreach (var ctorSig in currentParameterData.Constructors.Signatures)
                    current.Constructors.Add(TransposeTypedNames(ctorSig.Parameters.ToSeries(), current));
                foreach (var eventGroup in TransposeTypedNames(currentParameterData.Events, current))
                    current.Events.Add(eventGroup);
                foreach (var indexerSig in currentParameterData.Indexers.Signatures)
                    current.Indexers.Add(TransposeTypedName(new TypedName(indexerSig.Name, indexerSig.ReturnType), current), TransposeTypedNames(indexerSig.Parameters.ToSeries(), current));
                //result.Properties.Add
                current.SpecialConstraint = currentParameterData.SpecialConstraint;
                foreach (var method in currentParameterData.Methods.Signatures)
                    current.Methods.Add(TransposeTypedName(new TypedName(method.Name, method.ReturnType), current), TransposeTypedNames(method.Parameters.ToSeries(), current));
            }//*/);
            
            //Parallel.For(0, genericParameterData.Length, i =>
            //for (int i = 0; i < genericParameterData.Length; i++)
            //{
            //    var currentParameterData = genericParameterData[i];
            //    var current = result[i];
            //    foreach (var constraint in currentParameterData.Constraints)
            //        if (constraint.ContainsSymbols())
            //            current.Constraints.Add(constraint.SimpleSymbolDisambiguation(current));
            //        else
            //            current.Constraints.Add(constraint);
            //    foreach (var ctor in current.Constructors.Values)
            //        foreach (var param in ctor.Parameters.Values)
            //            if (param.ParameterType.ContainsSymbols())
            //                param.ParameterType = param.ParameterType.SimpleSymbolDisambiguation(current);
            //    foreach (var method in current.Methods.Values)
            //    {
            //        foreach (var param in method.Parameters.Values)
            //            if (param.ParameterType.ContainsSymbols())
            //                param.ParameterType = param.ParameterType.SimpleSymbolDisambiguation(current);
            //        if (method.ReturnType.ContainsSymbols())
            //            method.ReturnType = method.ReturnType.SimpleSymbolDisambiguation(current);
            //    }
            //    foreach (var property in current.Properties.Values)
            //        if (property.PropertyType.ContainsSymbols())
            //            property.PropertyType = property.PropertyType.SimpleSymbolDisambiguation(current);

            //    foreach (var indexer in current.Indexers.Values)
            //    {
            //        if (indexer.PropertyType.ContainsSymbols())
            //            indexer.PropertyType = indexer.PropertyType.SimpleSymbolDisambiguation(current);
            //        foreach (var param in indexer.Parameters.Values)
            //            if (param.ParameterType.ContainsSymbols())
            //                param.ParameterType = param.ParameterType.SimpleSymbolDisambiguation(current);
            //    }
            //}//);
            return result;
        }

        #endregion


        protected override sealed bool ShouldDispose(TIntermediateGenericParameter declaration)
        {
            return true;
        }

        public override IEnumerable<KeyValuePair<IGenericParameterUniqueIdentifier, TIntermediateGenericParameter>> ExclusivelyOnParent()
        {
            foreach (var element in this)
                yield return new KeyValuePair<IGenericParameterUniqueIdentifier, TIntermediateGenericParameter>(element.Key, element.Value);
        }
    }
}