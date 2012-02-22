using System;
using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
            IDeclarationUniqueIdentifier
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
                throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.subordinate));
            if (!this.Subordinates.Contains((ISubordinateDictionary)subordinate))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.subordinate, ExceptionMessageId.SubordinateDoesNotExist);
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

        /// <summary>
        /// Returns whether the <paramref name="key"/> is present within the
        /// <see cref="LockedFullDeclarations{TMItemIdentifier, TMItem}"/>.
        /// </summary>
        /// <param name="key">The <typeparamref name="TMItemIdentifier"/> which denotes
        /// the unique identifier of the member to check for.</param>
        /// <returns></returns>
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
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.sourceElement, ExceptionMessageId.SubordinateNull);
            ISubordinateDictionary isd = sourceElement.Subordinate;
            if (!(isd is _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.sourceElement, ExceptionMessageId.SubordinateInvalid);
            _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem> lgdmp = (_LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>)isd;
            return new MasterDictionaryEntry<TMItem>(sourceElement.Subordinate, (TMItem)lgdmp.Fetch(sourceElement.Entry));
        }

        internal TMItemIdentifier FetchKey(MasterDictionaryEntry<object> sourceElement)
        {
            if (sourceElement.Subordinate == null)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.sourceElement, ExceptionMessageId.SubordinateNull);
            ISubordinateDictionary isd = sourceElement.Subordinate;
            if (!(isd is _LockedGroupDeclarationsMasterPass<TMItemIdentifier, TMItem>))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.sourceElement, ExceptionMessageId.SubordinateInvalid);
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

        internal override IEnumerable<KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>> GetSubordinateClearedItems<TSKey, TSValue>(ISubordinateDictionary<TSKey, TMItemIdentifier, TSValue, TMItem> subordinate)
        {
            var values = this.valuesInstance as _ValuesCollection;
            if (values == null)
                yield break;
            for (int i = 0; i < this.Count; i++)
                if (values.Loaded(i))
                {
                    var value = values[i];
                    if (value.Subordinate != subordinate)
                        continue;
                    if (!(this.Keys[i] is TSKey))
                        continue;
                    if (!(value is TSValue))
                        continue;
                    yield return new KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>(this.Keys[i], value);
                }
        }

        protected internal override void Subordinate_ItemRemoved<TSKey, TSValue>(ISubordinateDictionary<TSKey, TMItemIdentifier, TSValue, TMItem> subordinate, TSKey key)
        {
            int state;
            lock (syncObject)
                state = this.state;
            if (state == USE_FETCH)
            {
                int index = this.Keys.IndexOf(key);
                if (index == -1)
                    return;
                MasterDictionaryEntry<TMItem> entry = this.Values[index];

                if (entry.Subordinate != subordinate)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.subordinate, ExceptionMessageId.SubordinateMismatch);
                var valueInst = (this.valuesInstance as _ValuesCollection);
                if (valueInst == null)
                    return;
                if (valueInst.Loaded(index))
                    valueInst.dataCopy[index] = null;
            }
            else
                base.Subordinate_ItemRemoved<TSKey, TSValue>(subordinate, key);
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
                    throw new ArgumentOutOfRangeException("arrayIndex");
                this.FetchKeysCheck();
                this.FetchValuesCheck();
                for (int i = 0; i < this.Count; i++)
                    array.SetValue(new KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>(this._fetchKeys[i], this._fetchValues[i]), i + arrayIndex);
            }
            else
                base.CopyToArray(array, arrayIndex);
        }

        protected override KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>> OnGetThis(int index)
        {
            if (state == USE_FETCH)
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                return new KeyValuePair<TMItemIdentifier, MasterDictionaryEntry<TMItem>>(this.Keys[index], this.Values[index]);
            }
            else
                return base.OnGetThis(index);
        }

        protected override MasterDictionaryEntry<TMItem> OnGetThis(TMItemIdentifier key)
        {
            if (state == USE_FETCH)
            {
                int index = this.Keys.IndexOf(key);
                if (index == -1)
                    throw new KeyNotFoundException();
                return this.Values[index];
            }
            else
                return base.OnGetThis(key);
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
