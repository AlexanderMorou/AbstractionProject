using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Events;
using System.Linq;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base declaration dictionary.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of declaration in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract partial class IntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration> :
        ControlledStateDictionary<string, TDeclaration>,
        IIntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration>,
        IIntermediateDeclarationDictionary
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            TDeclaration,
            IIntermediateDeclaration
    {
        private new ValuesCollection valuesCollection;

        /// <summary>
        /// Creates a new <see cref="IntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/> initialized to its default state.
        /// </summary>
        public IntermediateDeclarationDictionary() :
            base()
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/> 
        /// with the <see cref="Dictionary{TKey, TValue}"/> <paramref name="toWrap"/>.
        /// </summary>
        /// <param name="toWrap">The <see cref="Dictionary{TKey, TValue}"/> to encapsulate.</param>
        public IntermediateDeclarationDictionary(IntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration> toWrap) :
            base(toWrap)
        {
        }

        #region IIntermediateDeclarationDictionary<TDeclaration,TIntermediateDeclaration> Members

        public new IControlledStateCollection<TIntermediateDeclaration> Values
        {
            get
            {
                if (this.valuesCollection == null)
                    this.valuesCollection = new IntermediateDeclarationDictionary<TDeclaration, TIntermediateDeclaration>.ValuesCollection(this);
                return this.valuesCollection;
            }
        }

        public new TIntermediateDeclaration this[string identifier]
        {
            get
            {
                if (!this.Keys.Contains(identifier))
                    throw new ArgumentOutOfRangeException("identifier");
                return (TIntermediateDeclaration)base[identifier];
            }
        }

        public new KeyValuePair<string, TIntermediateDeclaration> this[int index]
        {
            get {
                var original = base[index];
                return new KeyValuePair<string, TIntermediateDeclaration>(original.Key, (TIntermediateDeclaration)original.Value);
            }
        }

        public new IEnumerator<KeyValuePair<string, TIntermediateDeclaration>> GetEnumerator()
        {
            foreach (var item in ((IEnumerable<KeyValuePair<string, TDeclaration>>)(this)))
                yield return new KeyValuePair<string, TIntermediateDeclaration>(item.Key, (TIntermediateDeclaration)item.Value);
            yield break;
        }

        public event EventHandler<EventArgsR1<TIntermediateDeclaration>> ItemAdded;

        public event EventHandler<EventArgsR1<TIntermediateDeclaration>> ItemRemoved;

        /// <summary>
        /// Invokes the <see cref="ItemAdded"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgsR1{T}"/> which
        /// designate the <typeparamref name="TIntermediateDeclaration"/> added.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="e"/> is
        /// null.</exception>
        protected virtual void OnItemAdded(EventArgsR1<TIntermediateDeclaration> e)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            if (this.ItemAdded != null)
                this.ItemAdded(this, e);
        }

        /// <summary>
        /// Invokes the <see cref="ItemRemoved"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgsR1{T}"/> which
        /// designate the <typeparamref name="TIntermediateDeclaration"/> removed.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="e"/> is
        /// null.</exception>
        protected virtual void OnItemRemoved(EventArgsR1<TIntermediateDeclaration> e)
        {
            if (e == null)
                throw new ArgumentNullException("e");
            if (this.ItemRemoved != null)
                this.ItemRemoved(this, e);
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Disposes the <see cref="IntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion

        /// <summary>
        /// Disposes the current <see cref="IntermediateDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        /// <param name="disposing">Whether to release managed memory.  If true, all data should be disposed; otherwise, only unmanaged memory should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var declarationValueCopy = this.Values.ToArray();
                Parallel.For(0, declarationValueCopy.Length,
                    i =>
                    {
                        declarationValueCopy[i].Dispose();
                    });
                this._Clear();
            }
        }

        #region IDeclarationDictionary<TDeclaration> Members

        public int IndexOf(TDeclaration decl)
        {
            if (this.valuesCollection == null)
                return -1;
            int index = 0;
            foreach (var item in this.Values)
                if (object.ReferenceEquals(item, decl))
                    return index;
                else
                    index++;
            return -1;
        }

        #endregion

        #region IDeclarationDictionary Members

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (!(decl is TDeclaration))
                return -1;
            return this.IndexOf(((TDeclaration)(decl)));
        }

        #endregion

    }
}
