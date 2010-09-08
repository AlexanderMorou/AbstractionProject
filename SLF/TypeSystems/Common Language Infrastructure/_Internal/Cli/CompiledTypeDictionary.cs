using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CompiledTypeDictionary<TType> :
        SubordinateDictionary<string, TType, IType>,
        IGroupedDeclarationDictionary<TType>,
        IGroupedDeclarationDictionary,
        ICompiledTypeDictionary
        where TType :
            class,
            IType<TType>
    {
        private Type[] filteredSeries;
        internal ICompiledTypeParent parent;
        public CompiledTypeDictionary(ICompiledTypeParent parent, MasterDictionaryBase<string, IType> master, Type[] filteredSeries)
            : base(master)
        {
            this.parent = parent;
            this.filteredSeries = filteredSeries;
        }

        #region IDisposable Members
        public void Dispose()
        {
            this.parent = null;
            this.filteredSeries = null;
            if (this.keysInstance!= null)
            {
                ((KC)(this.keysInstance)).Dispose();
                this.keysInstance= null;
            }
            if (this.valuesInstance!= null)
            {
                ((VC)(this.valuesInstance)).Dispose();
                this.valuesInstance = null;
            }
        }
        #endregion

        public override int Count
        {
            get
            {
                return this.filteredSeries.Length;
            }
        }

        public override void CopyTo(KeyValuePair<string, TType>[] array, int arrayIndex)
        {
            for (int i = 0; i < this.Count; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<string, TType>(this.Keys[i], this.Values[i]);
            }
        }

        public override bool Contains(KeyValuePair<string, TType> item)
        {
            if (Keys.Contains(item.Key))
                return this.Values.Contains(item.Value);
            return false;
        }

        protected override ControlledStateDictionary<string, TType>.KeysCollection InitializeKeysCollection()
        {
            return new KC(this);
        }

        protected override ControlledStateDictionary<string, TType>.ValuesCollection InitializeValuesCollection()
        {
            return new VC(this);
        }

        public override IEnumerator<KeyValuePair<string, TType>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return new KeyValuePair<string, TType>(this.Keys[i], this.Values[i]);
            }
            yield break;
        }

        protected override TType OnGetThis(string key)
        {
            if (!this.Keys.Contains(key))
                throw new KeyNotFoundException("key");
            return this.Values[this.Keys.GetIndexOf(key)];
        }

        protected override KeyValuePair<string, TType> OnGetThis(int index)
        {
            return new KeyValuePair<string, TType>(this.Keys[index], this.Values[index]);
        }

        public override TType this[string key]
        {
            get
            {
                return base[key];
            }
            protected set
            {
                throw new InvalidOperationException("Readonly");
            }
        }
        public override KeyValuePair<string, TType>[] ToArray()
        {
            var result = new KeyValuePair<string, TType>[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                result[i] = new KeyValuePair<string, TType>(this.Keys[i], this.Values[i]);
            }
            return result;
        }


        #region _GroupedTypes Members

        public Type[] FilteredSeries
        {
            get { return this.filteredSeries; }
        }

        #endregion

        #region IDeclarationDictionary<TType> Members

        public int IndexOf(TType decl)
        {
            /* *
             * Might as well make use of the cache...
             * */
            if (this.valuesInstance == null)
                return -1;
            int index = 0;
            foreach (var item in ((VC)(this.Values)).values)
                if (item == null)
                    index++;
                else if (object.ReferenceEquals(item, decl))
                    return index;
                else
                    index++;
            return -1;
        }

        #endregion

        #region IDeclarationDictionary Members

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (!(decl is TType))
                return -1;
            return this.IndexOf((TType)(decl));
        }

        #endregion
    }
}
