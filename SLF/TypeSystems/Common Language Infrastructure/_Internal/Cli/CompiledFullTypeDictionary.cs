using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using System.Runtime.CompilerServices;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CompiledFullTypeDictionary :
        MasterDictionaryBase<IGeneralTypeUniqueIdentifier, IType>,
        IFullTypeDictionary,
        IDisposable
    {
        private _ICompiledTypeParent parent;
        private _KC kc;
        private _VC vc;
        public CompiledFullTypeDictionary(_ICompiledTypeParent parent)
            : base()
        {
            this.parent = parent;
        }
        public override bool ContainsKey(IGeneralTypeUniqueIdentifier key)
        {
            foreach (var t in this.parent.UnderlyingSystemTypes)
                if (t.GetTypeReference().UniqueIdentifier.Equals(key))
                    return true;
            return false;
        }

        protected override KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>> OnGetThis(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            var key = this.Keys[index];
            return new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>(key, ((_VC)this.Values)[index]);
            throw new KeyNotFoundException();
        }

        protected override MasterDictionaryEntry<IType> OnGetThis(IGeneralTypeUniqueIdentifier key)
        {
            return ((_VC)this.Values)[this.Keys.IndexOf(key)];
        }

        public override int Count
        {
            get
            {
                int count = 0;
                foreach (IGroupedDeclarationDictionary igds in this.Subordinates)
                    count += igds.Count;
                return count;
            }
        }

        public override IEnumerator<KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>> GetEnumerator()
        {
            foreach (IGroupedDeclarationDictionary igd in this.Subordinates)
                foreach (var t in igd.Values.Cast<IType>())
                    yield return new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>(t.UniqueIdentifier, new MasterDictionaryEntry<IType>((ISubordinateDictionary)igd, t));
            yield break;
        }

        protected override void CopyToArray(Array array, int arrayIndex)
        {
            if (this.Count == 0)
                return;
            if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            var kCopy = this.Keys.ToArray();
            var vCopy = this.Values.ToArray();
            for (int i = 0; i < kCopy.Length; i++)
                array.SetValue(new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>(kCopy[i], vCopy[i]), arrayIndex + i);
        }

        public override void CopyTo(KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>[] array, int arrayIndex)
        {
            if (this.Count == 0)
                return;
            if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            var kCopy = this.Keys.ToArray();
            var vCopy = this.Values.ToArray();
            for (int i = 0; i < kCopy.Length; i++)
                array[arrayIndex + i] = new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>(kCopy[i], vCopy[i]);
        }

        protected override ControlledDictionary<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>.KeysCollection InitializeKeysCollection()
        {
            if (kc == null)
                this.kc = new _KC(this);
            return this.kc;
        }

        protected override ControlledDictionary<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IType>>.ValuesCollection InitializeValuesCollection()
        {
            if (this.vc == null)
                this.vc = new _VC(this);
            return this.vc;
        }

        public void Dispose()
        {
            if (this.kc != null)
            {
                this.kc.Dispose();
                this.kc = null;
            }
            if (this.vc != null)
            {
                this.vc.Dispose();
                this.vc = null;
            }
            if (this.parent != null)
                this.parent = null;
        }

        #region IFullTypeDictionary Members
        public IType[] GetTypesByName(string name)
        {
            /* *
             * Obtain the members where their unique identifiers
             * contain the name specified.
             * *
             * Much easier than change sets 565 through 9844.
             * */
            return (from identifier in this.Keys
                    where identifier.Name == name
                    select this[identifier].Entry).ToArray();
        }

        #endregion
        internal IEnumerable<IGeneralDeclarationUniqueIdentifier> GetAggregateIdentifiers()
        {
            /* *
             * Ignore types which are compiler generated and yield a null
             * name.
             * */
            return this.Keys.Cast<IGeneralDeclarationUniqueIdentifier>();
        }
    }
}
