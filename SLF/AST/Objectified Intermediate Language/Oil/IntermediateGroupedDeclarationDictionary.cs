using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
        private int suspensionLevel = 0;
        private IList<TDeclaration> suspendedMembers = new List<TDeclaration>();
        //private IDictionary<string, TDeclaration> suspendedMembers = new Dictionary<string,TDeclaration>();
        
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
        /// Disposes the current <see cref="IntermediateGroupedDeclarationDictionary{TDeclaration, TMDeclaration, TIntermediateDeclaration}"/>.
        /// </summary>
        /// <param name="disposing">Whether to release managed memory.  If true, all data should be disposed; otherwise, only unmanaged memory should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Suspended)
                {
                    for (int i = 0; i < this.suspendedMembers.Count; i++)
                        this.suspendedMembers[i].Dispose();
                    this.suspendedMembers.Clear();
                    this.suspensionLevel = 0;
                }
                for (int i = this.Count - 1; i >= 0; i--)
                    this.Values[i].Dispose();
                this.baseCollection.Clear();
            }
        }

        #region IDeclarationDictionary<TDeclaration> Members

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


        protected internal void Suspend()
        {
            this.suspensionLevel++;
        }

        protected internal void Resume()
        {
            if (this.suspensionLevel == 0)
                return;
            this.suspensionLevel--;
            if (suspensionLevel == 0)
            {
                this.ProcessSuspendedMembers(this.suspendedMembers);
                this.suspendedMembers.Clear();
            }
        }

        protected override string RekeyElement(KeyValuePair<string, TDeclaration> kvp)
        {
            return kvp.Value.UniqueIdentifier;
        }

        protected override void Add(string key, TDeclaration value)
        {
            if (!this.Suspended)
            {
                this.OnItemAdded(new EventArgsR1<TIntermediateDeclaration>(((TIntermediateDeclaration)(value))));
                base.Add(key, value);
            }
            else
                this.suspendedMembers.Add(value);
        }

        protected override bool RemoveImpl(string key)
        {
            if (Suspended)
            {
                if (base.ContainsKey(key))
                    return base.Remove(key);
                foreach (var member in suspendedMembers)
                    if (member.UniqueIdentifier == key)
                    {
                        member.Dispose();
                        this.OnItemRemoved(new EventArgsR1<TIntermediateDeclaration>((TIntermediateDeclaration)member));
                        suspendedMembers.Remove(member);
                        return true;
                    }
                return false;
            }
            if (this.ContainsKey(key))
                this.OnItemRemoved(new EventArgsR1<TIntermediateDeclaration>(this[key]));
            return base.RemoveImpl(key);
        }

        protected virtual void ProcessSuspendedMembers(IEnumerable<TDeclaration> members)
        {
            List<KeyValuePair<string, TDeclaration>> processedMembers = new List<KeyValuePair<string, TDeclaration>>();
            foreach (var member in members)
                processedMembers.Add(new KeyValuePair<string, TDeclaration>(member.UniqueIdentifier, member));
            if (this.Master != null)
                this.Master.Subordinate_ItemsAdded(this, processedMembers);
            foreach (var element in processedMembers)
                base.AddImpl(element);
        }

        protected override TDeclaration OnGetThis(string key)
        {
            if (Suspended)
            {
                if (base.ContainsKey(key))
                    return base[key];
                foreach (var suspendedMember in this.suspendedMembers)
                    if (suspendedMember.UniqueIdentifier == key)
                        return suspendedMember;
                throw new KeyNotFoundException("key");
            }
            return base.OnGetThis(key);
        }

        public override bool ContainsKey(string key)
        {
            if (this.Suspended)
            {
                if (base.ContainsKey(key))
                    return true;
                foreach (var member in this.suspendedMembers)
                    if (member.UniqueIdentifier == key)
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
                    return this.suspendedMembers.Count + base.Count;
                }
                else
                    return base.Count;
            }
        }

        protected override void Clear()
        {
            if (Suspended)
                if (this.Suspended)
                {
                    for (int i = 0; i < this.suspendedMembers.Count; i++)
                        this.suspendedMembers[i].Dispose();
                    this.suspendedMembers.Clear();
                }
            base.Clear();
        }

        protected override bool RemoveImpl(int index)
        {
            if (Suspended)
            {
                if (index < base.Count)
                    return base.RemoveImpl(index);
                if (index >= this.Count || index < 0)
                    throw new ArgumentOutOfRangeException("index");
                var target = this.suspendedMembers[index - base.Count];
                this.suspendedMembers.RemoveAt(index - base.Count);
                target.Dispose();
                return true;
            }
            else
                return base.RemoveImpl(index);
        }

        protected void AddDeclaration(TIntermediateDeclaration declaration)
        {
            if (declaration == null)
                throw new ArgumentNullException("declaration");
            if (this.Suspended)
                lock (suspendedMembers)
                    this.suspendedMembers.Add(declaration);
            else
                base.AddImpl(new KeyValuePair<string, TDeclaration>(declaration.UniqueIdentifier, declaration));
        }
    }
}
