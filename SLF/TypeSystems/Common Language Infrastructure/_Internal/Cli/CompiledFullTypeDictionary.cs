using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;

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
            public override MasterDictionaryEntry<IType> this[string key]
            {
                get
                {
                    if (this.Keys.Contains(key))
                        return ((_VC)this.Values)[this.Keys.GetIndexOf(key)];
                    throw new KeyNotFoundException();
                }
                set
                {
                    throw new NotSupportedException("Read-only.");
                }
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
            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }
            public override void Clear()
            {
                throw new InvalidOperationException("Readonly");
            }

            public override IEnumerator<KeyValuePair<string, MasterDictionaryEntry<IType>>> GetEnumerator()
            {
                foreach (IGroupedDeclarationDictionary igd in this.Subordinates)
                    foreach (var t in igd.Values.Cast<IType>())
                        yield return new KeyValuePair<string, MasterDictionaryEntry<IType>>(t.Name, new MasterDictionaryEntry<IType>((ISubordinateDictionary)igd, t));
                yield break;
            }
            protected override void ICollection_CopyTo(KeyValuePair<string, MasterDictionaryEntry<IType>>[] array, int arrayIndex)
            {
                int i = 0;
                foreach (IGroupedDeclarationDictionary igd in this.Subordinates)
                    foreach (var t in igd.Values.Cast<IType>())
                        array[i++ + arrayIndex] = new KeyValuePair<string, MasterDictionaryEntry<IType>>(t.Name, new MasterDictionaryEntry<IType>((ISubordinateDictionary)igd, t));
            }
            protected override ICollection<string> GetKeys()
            {
                if (kc == null)
                    this.kc = new _KC(this);
                return this.kc;
            }
            protected override ICollection<MasterDictionaryEntry<IType>> GetValues()
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
