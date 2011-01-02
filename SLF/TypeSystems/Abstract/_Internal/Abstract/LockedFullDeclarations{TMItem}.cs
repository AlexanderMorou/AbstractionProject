using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal partial class LockedFullDeclarations<TMItem> :
        MasterDictionaryBase<string, TMItem>,
        _LockedRelativeHelper<TMItem>
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

        internal void RelateSubordinateMembers<TSItem>(object[] sourceData, ISubordinateDictionary<string, TSItem, TMItem> subordinate)
            where TSItem :
                TMItem
        {
            if (sourceData == null)
                throw new ArgumentNullException("sourceData");
            if (subordinate == null)
                throw new ArgumentNullException("subordinate");
            if (!this.Subordinates.Contains((ISubordinateDictionary)subordinate))
                throw new ArgumentException("subordinate");
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

        public override bool ContainsKey(string key)
        {
            if (this.state == USE_FETCH)
            {
                this.FetchKeysCheck();
                return this._fetchKeys.Contains(key);
            }
            else
                return base.ContainsKey(key);
        }

        protected override ControlledStateDictionary<string, MasterDictionaryEntry<TMItem>>.KeysCollection InitializeKeysCollection()
        {
            if (this.state == USE_FETCH)
            {
                FetchKeysCheck();
                return this._fetchKeys;
            }
            return base.InitializeKeysCollection();
        }

        private void FetchKeysCheck()
        {
            if (this._fetchKeys == null)
                this._fetchKeys = new _KeysCollection(this);
        }
        protected override ControlledStateDictionary<string, MasterDictionaryEntry<TMItem>>.ValuesCollection InitializeValuesCollection()
        {
            if (this.state == USE_FETCH)
            {
                FetchValuesCheck();
                return this._fetchValues;
            }

            return base.InitializeValuesCollection();
        }


        private void FetchValuesCheck()
        {
            if (this._fetchValues == null)
                this._fetchValues = new _ValuesCollection(this);
        }

        internal MasterDictionaryEntry<TMItem> Fetch(MasterDictionaryEntry<object> sourceElement)
        {
            if (sourceElement.Subordinate == null)
                throw new ArgumentException("sourceElement");
            ISubordinateDictionary isd = sourceElement.Subordinate;
            if (!(isd is _LockedGroupDeclarationsMasterPass))
                throw new ArgumentException("sourceElement");
            _LockedGroupDeclarationsMasterPass lgdmp = (_LockedGroupDeclarationsMasterPass)isd;
            return new MasterDictionaryEntry<TMItem>(sourceElement.Subordinate, (TMItem)lgdmp.Fetch(sourceElement.Entry));
        }

        internal string FetchKey(MasterDictionaryEntry<object> sourceElement)
        {
            if (sourceElement.Subordinate == null)
                throw new ArgumentException("sourceElement");
            ISubordinateDictionary isd = sourceElement.Subordinate;
            if (!(isd is _LockedGroupDeclarationsMasterPass))
                throw new ArgumentException("sourceElement");
            _LockedGroupDeclarationsMasterPass lgdmp = (_LockedGroupDeclarationsMasterPass)isd;
            return lgdmp.FetchKey(sourceElement.Entry);
        }
        public override int Count
        {
            get
            {
                if (this.state == USE_FETCH)
                    return this.sourceData.Count;
                else
                    return base.Count;
            }
        }

        public override IEnumerator<KeyValuePair<string, MasterDictionaryEntry<TMItem>>> GetEnumerator()
        {
            if (state != USE_FETCH)
                return base.GetEnumerator();
            else
                return FetchEnumerator();
        }
        private IEnumerator<KeyValuePair<string, MasterDictionaryEntry<TMItem>>> FetchEnumerator()
        {
            FetchValuesCheck();
            FetchKeysCheck();
            for (int i = 0; i < this.sourceData.Count; i++)
                yield return new KeyValuePair<string, MasterDictionaryEntry<TMItem>>(this._fetchKeys[i], this._fetchValues[i]);
            yield break;
        }

        protected sealed override void ICollection_CopyTo(Array array, int arrayIndex)
        {
            if (arrayIndex + this.Count > array.Length)
                throw new ArgumentException("array");
            this.FetchKeysCheck();
            this.FetchValuesCheck();
            for (int i = 0; i < this.Count; i++)
                array.SetValue(new KeyValuePair<string, MasterDictionaryEntry<TMItem>>(this._fetchKeys[i], this._fetchValues[i]), i + arrayIndex);
        }

        #region _LockedRelativeHelper<TMItem> Members

        void _LockedRelativeHelper<TMItem>.RelateSubordinateMembers<TSItem, TRItem>(TRItem[] sourceData, ISubordinateDictionary<string, TSItem, TMItem> subordinate)
        {
            this.RelateSubordinateMembers<TSItem>(sourceData.Cast<object>().ToArray(), subordinate);
        }

        #endregion
    }
}
