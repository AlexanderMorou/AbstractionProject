﻿using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class LockedFullDeclarations<TMItemIdentifier, TMItem> :
        MasterDictionaryBase<TMItemIdentifier, TMItem>,
        _LockedRelativeHelper<TMItemIdentifier, TMItem>
        where TMItemIdentifier :
            IDeclarationUniqueIdentifier<TMItemIdentifier>
        where TMItem :
            class,
            IDeclaration,
            IDisposable
    {
        private const int USE_FETCH = 0;
        private const int USE_BASE = 1;
        private int state;
        private _KeysCollection _fetchKeys;
        private _ValuesCollection _fetchValues;
        private object syncObject = new object();
        private List<MasterDictionaryEntry<object>> sourceData;
        /// <summary>
        /// Creates a new <see cref="LockedFullDeclarations{TMItem}"/> 
        /// initialized to a default state.
        /// </summary>
        public LockedFullDeclarations()
            : base()
        {
            this.state = USE_BASE;
        }

        internal void RelateSubordinateMembers<TSItemIdentifier, TSItem>(object[] sourceData, ISubordinateDictionary<TSItemIdentifier, TMItemIdentifier, TSItem, TMItem> subordinate)
            where TSItemIdentifier :
                TMItemIdentifier
            where TSItem :
                TMItem
        {
            if (sourceData == null)
                throw new ArgumentNullException("sourceData");
            if (subordinate == null)
                throw new ArgumentNullException("subordinate");
            if (!this.Subordinates.Contains((ISubordinateDictionary)subordinate))
                throw new ArgumentException("subordinate");
            lock (syncObject)
                if (this.state != USE_FETCH)
                    this.state = USE_FETCH;
            if (this.sourceData == null)
                this.sourceData = new List<MasterDictionaryEntry<object>>();
            this.sourceData.AddRange(sourceData.OnAll(u => new MasterDictionaryEntry<object>((ISubordinateDictionary)subordinate, u)));
            if (this._fetchKeys != null)
                this._fetchKeys.SetRange(this._fetchKeys.Count + sourceData.Length);
            if (this._fetchValues != null)
                this._fetchValues.SetRange(this._fetchValues.Count + sourceData.Length);
        }

        public override bool ContainsKey(TMItemIdentifier key)
        {
            int state;
            lock (syncObject)
                state = this.state;
            if (state == USE_FETCH)
            {
                this.FetchKeysCheck();
                return this._fetchKeys.Contains(key);
            }
            else
                return base.ContainsKey(key);
        }

        protected override KeysCollection InitializeKeysCollection()
        {
            lock (syncObject)
                if (this.state == USE_FETCH)
                {
                    FetchKeysCheck();
                    return this._fetchKeys;
                }
            return base.InitializeKeysCollection();
        }

        private void FetchKeysCheck()
        {
            lock (syncObject)
                if (this.state == USE_FETCH && this._fetchKeys == null)
                    this._fetchKeys = new _KeysCollection(this);
        }

        protected override ValuesCollection InitializeValuesCollection()
        {

            lock (syncObject)
                if (this.state == USE_FETCH)
                {
                    FetchValuesCheck();
                    return this._fetchValues;
                }
            return base.InitializeValuesCollection();
        }


        private void FetchValuesCheck()
        {
            lock (syncObject)
                if (this.state == USE_FETCH && this._fetchValues == null)
                    this._fetchValues = new _ValuesCollection(this);
        }

        internal MasterDictionaryEntry<TMItem> Fetch(MasterDictionaryEntry<object> sourceElement)
        {
            if (sourceElement.Subordinate == null)
                throw new ArgumentException("sourceElement");
            ISubordinateDictionary isd = sourceElement.Subordinate;
            if (!(isd is _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>))
                throw new ArgumentException("sourceElement");
            _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem> lgdmp = (_LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>)isd;
            return new MasterDictionaryEntry<TMItem>(sourceElement.Subordinate, (TMItem)lgdmp.Fetch(sourceElement.Entry));
        }

        internal TMItemIdentifier FetchKey(MasterDictionaryEntry<object> sourceElement)
        {
            if (sourceElement.Subordinate == null)
                throw new ArgumentException("sourceElement");
            ISubordinateDictionary isd = sourceElement.Subordinate;
            if (!(isd is _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>))
                throw new ArgumentException("sourceElement");
            _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem> lgdmp = (_LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>)isd;
            return lgdmp.FetchKey(sourceElement.Entry);
        }
        
        public override int Count
        {
            get
            {
                int state;
                lock (syncObject)
                    state = this.state;
                if (state == USE_FETCH)
                    return this.sourceData.Count;
                else
                    return base.Count;
            }
        }

        public override IEnumerator<KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>> GetEnumerator()
        {
            int state;
            lock (syncObject)
                state = this.state;
            if (state != USE_FETCH)
                return base.GetEnumerator();
            else
                return FetchEnumerator();
        }

        private IEnumerator<KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>> FetchEnumerator()
        {
            FetchValuesCheck();
            FetchKeysCheck();
            for (int i = 0; i < this.sourceData.Count; i++)
                yield return new KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>(this._fetchKeys[i], this._fetchValues[i]);
            yield break;
        }

        protected sealed override void CopyToArray(Array array, int arrayIndex)
        {
            int state;
            lock (syncObject)
                state = this.state;
            if (state == USE_FETCH)
            {
                if (arrayIndex + this.Count > array.Length)
                    throw new ArgumentException("array");
                this.FetchKeysCheck();
                this.FetchValuesCheck();
                for (int i = 0; i < this.Count; i++)
                    array.SetValue(new KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>(this._fetchKeys[i], this._fetchValues[i]), i + arrayIndex);
            }
            else
                base.CopyToArray(array, arrayIndex);
        }

        #region _LockedRelativeHelper<TMItem> Members

        void _LockedRelativeHelper<TMItemIdentifier, TMItem>.RelateSubordinateMembers<TSItemIdentifier, TSItem, TRItem>(TRItem[] sourceData, ISubordinateDictionary<TSItemIdentifier, TMItemIdentifier, TSItem, TMItem> subordinate)
        {
            this.RelateSubordinateMembers<TSItemIdentifier, TSItem>(sourceData.Cast<object>().ToArray(), subordinate);
        }

        #endregion

        internal IEnumerable<IGeneralDeclarationUniqueIdentifier> GetAggregateIdentifiers()
        {
            /* *
             * Aggregate them through the locked grouped declarations master pass interface.
             * Members which yield a null or empty value are special kinds of members, like 
             * constructors, type/binary/unary coercion members.
             * *
             * Later, do analysis to determine whether type-checking the members or the
             * yielding a null value through a method call per member is quicker.
             * *
             * This method is a near hack compared to the intermediate variant; however,
             * it makes more sense than constructing a heavy-weight member specific wrapper
             * around the members purely to retrieve their name.
             * */
            return this.Keys.Cast<IGeneralDeclarationUniqueIdentifier>();
        }
    }
}