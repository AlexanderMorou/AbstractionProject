using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CompiledTypeDictionary<TTypeIdentifier, TType> :
        SubordinateDictionary<TTypeIdentifier, IGeneralTypeUniqueIdentifier, TType, IType>,
        IGroupedDeclarationDictionary<TTypeIdentifier, TType>,
        IGroupedDeclarationDictionary,
        ICompiledTypeDictionary
        where TTypeIdentifier :
            ITypeUniqueIdentifier<TTypeIdentifier>,
            IGeneralTypeUniqueIdentifier
        where TType :
            class,
            IType<TTypeIdentifier, TType>
    {
        private Type[] filteredSeries;
        internal _ICompiledTypeParent parent;
        public CompiledTypeDictionary(_ICompiledTypeParent parent, MasterDictionaryBase<IGeneralTypeUniqueIdentifier, IType> master, Type[] filteredSeries)
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

        public override void CopyTo(KeyValuePair<TTypeIdentifier, TType>[] array, int arrayIndex)
        {
            for (int i = 0; i < this.Count; i++)
            {
                array[i + arrayIndex] = new KeyValuePair<TTypeIdentifier, TType>(this.Keys[i], this.Values[i]);
            }
        }

        public override bool Contains(KeyValuePair<TTypeIdentifier, TType> item)
        {
            if (Keys.Contains(item.Key))
                return this.Values.Contains(item.Value);
            return false;
        }

        protected override ControlledStateDictionary<TTypeIdentifier, TType>.KeysCollection InitializeKeysCollection()
        {
            return new KC(this);
        }

        protected override ControlledStateDictionary<TTypeIdentifier, TType>.ValuesCollection InitializeValuesCollection()
        {
            return new VC(this);
        }

        public override IEnumerator<KeyValuePair<TTypeIdentifier, TType>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return new KeyValuePair<TTypeIdentifier, TType>(this.Keys[i], this.Values[i]);
            }
            yield break;
        }

        protected override TType OnGetThis(TTypeIdentifier key)
        {
            if (!this.Keys.Contains(key))
                throw new KeyNotFoundException("key");
            return this.Values[this.Keys.IndexOf(key)];
        }

        protected override KeyValuePair<TTypeIdentifier, TType> OnGetThis(int index)
        {
            return new KeyValuePair<TTypeIdentifier, TType>(this.Keys[index], this.Values[index]);
        }

        public override TType this[TTypeIdentifier key]
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
        public override KeyValuePair<TTypeIdentifier, TType>[] ToArray()
        {
            var result = new KeyValuePair<TTypeIdentifier, TType>[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                result[i] = new KeyValuePair<TTypeIdentifier, TType>(this.Keys[i], this.Values[i]);
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
