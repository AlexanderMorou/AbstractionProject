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

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CompiledFullTypeDictionary :
        MasterDictionaryBase<string, IType>,
        IFullTypeDictionary,
        IDisposable
    {
        private ICompiledTypeParent parent;
        private _KC kc;
        private _VC vc;
        public CompiledFullTypeDictionary(ICompiledTypeParent parent)
            : base()
        {
            this.parent = parent;
        }
        public override bool ContainsKey(string key)
        {
            foreach (IType t in this.parent.UnderlyingSystemTypes)
                if (t.Name == key)
                    return true;
            return false;
        }

        protected override KeyValuePair<string, MasterDictionaryEntry<IType>> OnGetThis(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            var key = this.Keys[index];
            return new KeyValuePair<string, MasterDictionaryEntry<IType>>(key, ((_VC)this.Values)[index]);
            throw new KeyNotFoundException();
        }

        protected override MasterDictionaryEntry<IType> OnGetThis(string key)
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

        public override IEnumerator<KeyValuePair<string, MasterDictionaryEntry<IType>>> GetEnumerator()
        {
            foreach (IGroupedDeclarationDictionary igd in this.Subordinates)
                foreach (var t in igd.Values.Cast<IType>())
                    yield return new KeyValuePair<string, MasterDictionaryEntry<IType>>(t.Name, new MasterDictionaryEntry<IType>((ISubordinateDictionary)igd, t));
            yield break;
        }

        protected override void ICollection_CopyTo(Array array, int arrayIndex)
        {
            if (this.Count == 0)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (this.Count + arrayIndex > array.Length)
                throw new ArgumentException("array");
            var kCopy = this.Keys.ToArray();
            var vCopy = this.Values.ToArray();
            for (int i = 0; i < kCopy.Length; i++)
                array.SetValue(new KeyValuePair<string, MasterDictionaryEntry<IType>>(kCopy[i], vCopy[i]), arrayIndex + i);
        }

        public override void CopyTo(KeyValuePair<string, MasterDictionaryEntry<IType>>[] array, int arrayIndex)
        {
            if (this.Count == 0)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (this.Count + arrayIndex > array.Length)
                throw new ArgumentException("array");
            var kCopy = this.Keys.ToArray();
            var vCopy = this.Values.ToArray();
            for (int i = 0; i < kCopy.Length; i++)
                array[arrayIndex + i] = new KeyValuePair<string, MasterDictionaryEntry<IType>>(kCopy[i], vCopy[i]);
        }

        protected override ControlledStateDictionary<string, MasterDictionaryEntry<IType>>.KeysCollection InitializeKeysCollection()
        {
            if (kc == null)
                this.kc = new _KC(this);
            return this.kc;
        }

        protected override ControlledStateDictionary<string, MasterDictionaryEntry<IType>>.ValuesCollection InitializeValuesCollection()
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
    }
}
