using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base grouped declaration dictionary.
    /// </summary>
    /// <typeparam name="TDeclaration"></typeparam>
    /// <typeparam name="TMDeclaration"></typeparam>
    /// <typeparam name="TIntermediateDeclaration"></typeparam>
    public abstract partial class IntermediateGroupedDeclarationDictionary<TDeclaration, TMDeclaration, TIntermediateDeclaration> :
        SubordinateDictionary<string, TDeclaration, TMDeclaration>,
        IIntermediateGroupedDeclarationDictionary<TDeclaration, TIntermediateDeclaration>,
        IIntermediateGroupedDeclarationDictionary
        where TDeclaration :
            TMDeclaration
        where TMDeclaration :
            class,
            IDeclaration
        where TIntermediateDeclaration :
            IIntermediateDeclaration,
            TDeclaration
    {
        private new ValuesCollection valuesCollection;
        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>
        /// with the <paramref name="master"/> dictionary provided.
        /// </summary>
        /// <param name="master">The <see cref="MasterDictionaryBase{TKey, TValue}"/> which owns
        /// the full grouping of members the <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>
        /// will contain.</param>
        public IntermediateGroupedDeclarationDictionary(MasterDictionaryBase<string, TMDeclaration> master)
            : base(master)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>
        /// with the <paramref name="master"/> dictionary provided.
        /// </summary>
        /// <param name="master">The <see cref="MasterDictionaryBase{TKey, TValue}"/> which owns
        /// the full grouping of members the <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>
        /// will contain.</param>
        /// <param name="root">The <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>
        /// which the current is based upon.</param>
        /// <remarks>To establish separation of parent, in multi-instance parents, and unity amongst the 
        /// elements.</remarks>
        public IntermediateGroupedDeclarationDictionary(MasterDictionaryBase<string, TMDeclaration> master, IntermediateGroupedDeclarationDictionary<TDeclaration, TMDeclaration, TIntermediateDeclaration> root)
            : base(master, root.dictionaryCopy)
        {
        }


        #region IIntermediateDeclarationDictionary<TDeclaration,TIntermediateDeclaration> Members

        public new IControlledStateCollection<TIntermediateDeclaration> Values
        {
            get
            {
                if (this.valuesCollection == null)
                    this.valuesCollection = new ValuesCollection(this);
                return this.valuesCollection;
            }
        }

        public new TIntermediateDeclaration this[string identifier]
        {
            get
            {
                if (!this.Keys.Contains(identifier))
                    throw new ArgumentOutOfRangeException("identifier");
                return ((TIntermediateDeclaration)(base.Values[this.Keys.GetIndexOf(identifier)]));
            }
        }

        public new KeyValuePair<string, TIntermediateDeclaration> this[int index]
        {
            get { return new KeyValuePair<string, TIntermediateDeclaration>(this.Keys[index], this.Values[index]); }
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
        /// Disposes the <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        #endregion

        /// <summary>
        /// Disposes the current <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        /// <param name="disposing">Whether to release managed memory.  If true, all data should be disposed; otherwise, only unmanaged memory should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                for (int i = this.Count - 1; i >= 0; i--)
                    this.Values[i].Dispose();
                this.baseCollection.Clear();
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
