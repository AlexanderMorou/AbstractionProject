using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base grouped declaration dictionary.
    /// </summary>
    /// <typeparam name="TDeclarationIdentifier">The kind of unique identifier
    /// used to differentiate the <typeparamref name="TDeclaration"/>
    /// elements from their siblings.</typeparam>
    /// <typeparam name="TDeclaration">The specific kind of declaration in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TMDeclarationIdentifier">The kind of unique identifier
    /// used to differentiate the <typeparamref name="TMDeclaration"/>
    /// elements from all of their siblings.</typeparam>
    /// <typeparam name="TMDeclaration">The kind of declaration used to represent
    /// all of the elements combined.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The intermediate variant of the
    /// current <typeparamref name="TDeclaration"/>.</typeparam>
    public abstract partial class IntermediateGroupedDeclarationDictionary<TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration> :
        SubordinateDictionary<TDeclarationIdentifier, TMDeclarationIdentifier, TDeclaration, TMDeclaration>,
        IIntermediateGroupedDeclarationDictionary<TDeclarationIdentifier, TDeclaration, TIntermediateDeclaration>,
        IIntermediateGroupedDeclarationDictionary
        where TDeclarationIdentifier :
            TMDeclarationIdentifier,
            IDeclarationUniqueIdentifier<TDeclarationIdentifier>
        where TMDeclarationIdentifier :
            IDeclarationUniqueIdentifier<TMDeclarationIdentifier>
        where TDeclaration :
            TMDeclaration
        where TMDeclaration :
            class,
            IDeclaration
        where TIntermediateDeclaration :
            IIntermediateDeclaration,
            TDeclaration
    {
        private bool isLockApplied = false;
        private int suspensionLevel = 0;
        private List<TDeclaration> suspendedMembers = new List<TDeclaration>();
        //private IDictionary<TDeclarationIdentifier, TDeclaration> suspendedMembers = new Dictionary<TDeclarationIdentifier,TDeclaration>();
        
        private ValuesCollection valuesCollection;
        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// with the <paramref name="master"/> dictionary provided.
        /// </summary>
        /// <param name="master">The <see cref="MasterDictionaryBase{TKey, TValue}"/> which owns
        /// the full grouping of members the <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// will contain.</param>
        public IntermediateGroupedDeclarationDictionary(MasterDictionaryBase<TMDeclarationIdentifier, TMDeclaration> master)
            : base(master)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// with the <paramref name="master"/> dictionary provided.
        /// </summary>
        /// <param name="master">The <see cref="MasterDictionaryBase{TKey, TValue}"/> which owns
        /// the full grouping of members the <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// will contain.</param>
        /// <param name="root">The <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// which the current is based upon.</param>
        /// <remarks>To establish separation of parent, in multi-instance parents, and unity amongst the 
        /// elements.</remarks>
        public IntermediateGroupedDeclarationDictionary(MasterDictionaryBase<TMDeclarationIdentifier, TMDeclaration> master, IntermediateGroupedDeclarationDictionary<TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration> root)
            : base(master, root)
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

        public new TIntermediateDeclaration this[TDeclarationIdentifier identifier]
        {
            get
            {
                if (!this.Keys.Contains(identifier))
                    throw new ArgumentOutOfRangeException("identifier");
                return ((TIntermediateDeclaration)(base.Values[this.Keys.IndexOf(identifier)]));
            }
        }

        public new virtual KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>[] ToArray()
        {
            KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>[] result = new KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>[this.Count];
            this.CopyTo(result);
            return result;
        }

        public void CopyTo(KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>[] result, int index = 0)
        {
            var copy = base.ToArray();
            for (int i = 0; i < this.Count; i++)
            {
                var copyElement = copy[i];
                result[i + index] = new KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>(copyElement.Key, (TIntermediateDeclaration)copyElement.Value);
            }
        }
        public new KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration> this[int index]
        {
            get { return new KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>(this.Keys[index], this.Values[index]); }
        }

        public new IEnumerator<KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>> GetEnumerator()
        {
            foreach (var item in ((IEnumerable<KeyValuePair<TDeclarationIdentifier, TDeclaration>>)(this)))
                yield return new KeyValuePair<TDeclarationIdentifier, TIntermediateDeclaration>(item.Key, (TIntermediateDeclaration)item.Value);
            yield break;
        }

        /// <summary>
        /// Occurs when an item is added to the 
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        public event EventHandler<EventArgsR1<TIntermediateDeclaration>> ItemAdded;

        /// <summary>
        /// Occurs when an item is removed from the 
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
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
        /// Disposes the <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.Dispose(true);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        /// <summary>
        /// Disposes the current 
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// with further context on whether managed resources should be disposed.
        /// </summary>
        /// <param name="disposing">Whether to release managed memory.  If true, all data
        /// should be disposed; otherwise, only unmanaged memory should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.Suspended)
                    {
                        Parallel.For(0, this.suspendedMembers.Count, i =>
                            this.suspendedMembers[i].Dispose());
                        this.suspendedMembers.Clear();
                        this.suspensionLevel = 0;
                    }

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
                GC.SuppressFinalize(this);
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

        /// <summary>
        /// Returns the index of the <paramref name="decl"/> provided.
        /// </summary>
        /// <param name="decl">The <see cref="IDeclaration"/> in the 
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>
        /// to return the index of.</param>
        /// <returns>An <see cref="Int32"/> value representing the index of the <paramref name="decl"/> in the
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>,
        /// if present; -1, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="decl"/> is null.</exception>
        public int IndexOf(TDeclaration decl)
        {
            if (this.valuesCollection == null && this.Count == 0)
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

        /// <summary>
        /// Returns whether the dictionary is suspended.
        /// </summary>
        protected bool Suspended { get { return this.suspensionLevel > 0; } }

        /// <summary>
        /// Suspends the duality of the 
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        protected internal void Suspend()
        {
            this.suspensionLevel++;
        }

        /// <summary>
        /// Resumes the duality of the 
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        protected internal void Resume()
        {
            if (this.suspensionLevel == 0)
                return;
            this.suspensionLevel--;
            if (suspensionLevel == 0)
            {
                this.ProcessSuspendedDeclarations(this.suspendedMembers);
                this.suspendedMembers.Clear();
            }
        }

        /// <summary>
        /// Adds an element of the provided <paramref name="key"/>
        /// and <paramref name="value"/> to the
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        /// <param name="key">The <see cref="TDeclarationIdentifier"/> key of the
        /// current <paramref name="value"/> inserted.</param>
        /// <param name="value">The <typeparamref name="TDeclaration"/>
        /// to insert.</param>
        protected internal override void _Add(TDeclarationIdentifier key, TDeclaration value)
        {
            this.OnItemAdded(new EventArgsR1<TIntermediateDeclaration>(((TIntermediateDeclaration)(value))));
            if (!this.Suspended)
                base._Add(key, value);
            else
                lock (this.suspendedMembers)
                    this.suspendedMembers.Add(value);
        }

        /// <summary>
        /// Removes an element with the specified <paramref name="index"/>
        /// from the
        /// <see cref="IntermediateGroupedDeclarationDictionary{TDeclarationIdentifier, TDeclaration, TMDeclarationIdentifier, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        /// <param name="index">The <see cref="Int32"/> value of the ordinal index of 
        /// the <typeparamref name="TIntermediateDeclaration"/> to remove.</param>
        /// <returns>true if the element was successfully removed; false otherwise.</returns>
        protected internal override bool _Remove(int index)
        {
            if (Suspended)
            {
                if (index > base.Count)
                    if (index < this.Count)
                    {
                        var suspendedInex = index - this.Count;
                        var suspendedMember = this.suspendedMembers[suspendedInex];
                        this.OnItemRemoved(new EventArgsR1<TIntermediateDeclaration>((TIntermediateDeclaration)suspendedMember));
                        suspendedMember.Dispose();
                        this.suspendedMembers.RemoveAt(suspendedInex);
                        return true;
                    }
                    else
                        return false;
            }
            return base._Remove(index);
        }

        /// <summary>
        /// Processes the <typeparamref name="TDeclaration"/> set which
        /// was inserted when the master/subordinate duality was suspended.
        /// </summary>
        /// <param name="declarations">The <see cref="IEnumerable{T}"/>
        /// set of elements that was inserted during the suspension.</param>
        protected virtual void ProcessSuspendedDeclarations(IEnumerable<TDeclaration> declarations)
        {
            List<KeyValuePair<TDeclarationIdentifier, TDeclaration>> processedDeclarations = new List<KeyValuePair<TDeclarationIdentifier, TDeclaration>>();
            foreach (var declaration in declarations)
                processedDeclarations.Add(new KeyValuePair<TDeclarationIdentifier, TDeclaration>((TDeclarationIdentifier)declaration.UniqueIdentifier, declaration));
            if (this.Master != null)
                this.Master.Subordinate_ItemsAdded(this, processedDeclarations);
            base._AddRange(processedDeclarations);
        }

        protected override TDeclaration OnGetThis(TDeclarationIdentifier key)
        {
            if (Suspended)
            {
                if (base.ContainsKey(key))
                    return base[key];
                foreach (var suspendedMember in this.suspendedMembers)
                    if (suspendedMember.UniqueIdentifier.Equals(key))
                        return suspendedMember;
                throw new KeyNotFoundException("key");
            }
            return base.OnGetThis(key);
        }

        public override bool ContainsKey(TDeclarationIdentifier key)
        {
            if (this.Suspended)
            {
                if (base.ContainsKey(key))
                    return true;
                foreach (var member in this.suspendedMembers)
                    if (member.UniqueIdentifier.Equals(key))
                        return true;
                return false;
            }
            return base.ContainsKey(key);
        }

        public override int Count
        {
            get
            {
                if (this.Suspended)
                {
                    return this.suspendedMembers.Count + BaseCount;
                }
                else
                    return base.Count;
            }
        }

        private int BaseCount
        {
            get
            {
                return base.Count;
            }
        }

        protected internal override void _Clear()
        {
            if (Suspended)
                if (this.Suspended)
                {
                    for (int i = 0; i < this.suspendedMembers.Count; i++)
                        this.suspendedMembers[i].Dispose();
                    this.suspendedMembers.Clear();
                }
            base._Clear();
        }

        protected void AddDeclarations(TIntermediateDeclaration[] declarations)
        {
            if (this.isLockApplied)
                throw new InvalidOperationException(string.Format(Resources.LockableChangeThrowMessage, "dictionary"));
            if (declarations == null)
                throw new ArgumentNullException("declarations");
            if (this.Suspended)
                lock (suspendedMembers)
                    suspendedMembers.AddRange(declarations.Cast<TDeclaration>());
            else
            {
                base._AddRange(from declaration in declarations
                               select new KeyValuePair<TDeclarationIdentifier, TDeclaration>((TDeclarationIdentifier)declaration.UniqueIdentifier, declaration));
            }
        }

        protected internal void AddDeclaration(TIntermediateDeclaration declaration)
        {
            if (this.isLockApplied)
                throw new InvalidOperationException(string.Format(Resources.LockableChangeThrowMessage, "dictionary"));
            if (declaration == null)
                throw new ArgumentNullException("declaration");
            if (this.Suspended)
                lock (suspendedMembers)
                    this.suspendedMembers.Add(declaration);
            else
                base._Add(new KeyValuePair<TDeclarationIdentifier, TDeclaration>((TDeclarationIdentifier)declaration.UniqueIdentifier, declaration));
        }

        internal void Lock()
        {
            this.isLockApplied = true;
        }

        internal void Unlock()
        {
            this.isLockApplied = false;
        }
    }
}
