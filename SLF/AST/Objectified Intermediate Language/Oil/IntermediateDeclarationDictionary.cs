using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public abstract partial class IntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> :
        ControlledStateDictionary<TIdentifier, TDeclaration>,
        IIntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration>,
        IIntermediateDeclarationDictionary
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
        where TDeclaration :
            IDeclaration
        where TIntermediateDeclaration :
            TDeclaration,
            IIntermediateDeclaration
    {
        private bool disposing;
        private object disposeSynch = new object();
        private ValuesCollection valuesCollection;
        private bool locked;

        /// <summary>
        /// Creates a new <see cref="IntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/> initialized to its default state.
        /// </summary>
        public IntermediateDeclarationDictionary() :
            base()
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/> 
        /// with the <see cref="Dictionary{TKey, TValue}"/> <paramref name="toWrap"/>.
        /// </summary>
        /// <param name="toWrap">The <see cref="Dictionary{TKey, TValue}"/> to encapsulate.</param>
        public IntermediateDeclarationDictionary(IntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> toWrap) :
            base(toWrap)
        {
        }

        #region IIntermediateDeclarationDictionary<TDeclaration,TIntermediateDeclaration> Members

        public new IControlledStateCollection<TIntermediateDeclaration> Values
        {
            get
            {
                if (this.valuesCollection == null)
                    this.valuesCollection = new IntermediateDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration>.ValuesCollection(this);
                return this.valuesCollection;
            }
        }

        public new TIntermediateDeclaration this[TIdentifier identifier]
        {
            get
            {
                if (!this.Keys.Contains(identifier))
                    throw new ArgumentOutOfRangeException("identifier");
                return (TIntermediateDeclaration)base[identifier];
            }
        }

        public new virtual KeyValuePair<TIdentifier, TIntermediateDeclaration>[] ToArray()
        {
            KeyValuePair<TIdentifier, TIntermediateDeclaration>[] result = new KeyValuePair<TIdentifier, TIntermediateDeclaration>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public void CopyTo(KeyValuePair<TIdentifier, TIntermediateDeclaration>[] result, int index = 0)
        {
            var copy = base.ToArray();
            for (int i = 0; i < this.Count; i++)
            {
                var copyElement = copy[i];
                result[i + index] = new KeyValuePair<TIdentifier, TIntermediateDeclaration>(copyElement.Key, (TIntermediateDeclaration)copyElement.Value);
            }
        }

        public new KeyValuePair<TIdentifier, TIntermediateDeclaration> this[int index]
        {
            get {
                var original = base[index];
                return new KeyValuePair<TIdentifier, TIntermediateDeclaration>(original.Key, (TIntermediateDeclaration)original.Value);
            }
        }

        public new IEnumerator<KeyValuePair<TIdentifier, TIntermediateDeclaration>> GetEnumerator()
        {
            foreach (var item in ((IEnumerable<KeyValuePair<TIdentifier, TDeclaration>>)(this)))
                yield return new KeyValuePair<TIdentifier, TIntermediateDeclaration>(item.Key, (TIntermediateDeclaration)item.Value);
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
            var itemAdded = this.ItemAdded;
            if (itemAdded != null)
                itemAdded(this, e);
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
            var itemRemoved = this.ItemRemoved;
            if (itemRemoved != null)
                itemRemoved(this, e);
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Disposes the <see cref="IntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion

        /// <summary>
        /// Disposes the current <see cref="IntermediateDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        /// <param name="disposing">Whether to release managed memory.  If true, all data should be disposed; otherwise, only unmanaged memory should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposeSynch == null)
                return;
            lock (this.disposeSynch)
            {
                if (this.disposing)
                    return;
                this.disposing = true;
            }
            try
            {
                if (disposing)
                {
                    var declarationValueCopy = (from v in this.ToArray()
                                                where ShouldDispose((TIntermediateDeclaration)v.Value)
                                                select v).SplitKeyValueSets();
                    if (declarationValueCopy.Item1.Length > 0)
                    {
                        Parallel.For(0, declarationValueCopy.Item2.Length, i =>
                            declarationValueCopy.Item2[i].Dispose());

                        this._RemoveSet(declarationValueCopy.Item1);
                    }
                }
            }
            finally
            {
                lock (this.disposeSynch)
                    this.disposing = false;
                this.disposeSynch = null;
            }
        }

        /// <summary>
        /// Determines whether the <typeparamref name="TIntermediateDeclaration"/>
        /// should be disposed.
        /// </summary>
        /// <param name="declaration">The <typeparamref name="TIntermediateDeclaration"/>
        /// to check the dispose state of.</param>
        /// <returns>true if the <paramref name="declaration"/>
        /// should be disposed; false, otherwise.</returns>
        protected abstract bool ShouldDispose(TIntermediateDeclaration declaration);

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
        protected internal override void _Add(TIdentifier key, TDeclaration value)
        {
            base._Add(key, value);
            this.OnItemAdded(new EventArgsR1<TIntermediateDeclaration>(((TIntermediateDeclaration)(value))));
        }

        protected override void _AddRange(KeyValuePair<TIdentifier, TDeclaration>[] elements)
        {
            base._AddRange(elements);
            foreach (var element in elements)
                this.OnItemAdded(new EventArgsR1<TIntermediateDeclaration>(((TIntermediateDeclaration)(element.Value))));
        }

        internal void Lock()
        {
            this.locked = true;
        }

        internal void Unlock()
        {
            this.locked = false;
        }

        internal bool Locked
        {
            get
            {
                return this.locked;
            }
        }
    }
}
