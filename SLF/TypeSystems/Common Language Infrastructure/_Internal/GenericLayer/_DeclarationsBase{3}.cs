using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal abstract partial class _DeclarationsBase<TDeclaration, TDeclarationSpecific, TOriginalContainer, TDictionary> :
        SubordinateDictionary<string, TDeclarationSpecific, TDeclaration>,
        IDeclarationDictionary<TDeclarationSpecific>,
        IDeclarationDictionary
        where TDeclaration :
            class,
            IDeclaration
        where TDeclarationSpecific :
            class,
            TDeclaration
        where TDictionary :
            class,
            IDeclarationDictionary<TDeclarationSpecific>
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private TOriginalContainer parent;
        /// <summary>
        /// Data member for <see cref="Original"/>.
        /// </summary>
        private TDictionary original;
        protected _DeclarationsBase(TOriginalContainer parent, TDictionary original)
            : this(null, parent, original)
        {
        }

        internal _DeclarationsBase(MasterDictionaryBase<string, TDeclaration> master, TOriginalContainer parent, TDictionary original)
            : base(master)
        {
            this.parent = parent;
            this.original = original;
        }

        public override sealed int Count
        {
            get
            {
                if (this.Original == null)
                    return 0;
                return this.Original.Count;
            }
        }

        protected abstract TDeclarationSpecific GetWrapper(TDeclarationSpecific original, TOriginalContainer parent);

        #region IDisposable Members

        public virtual void Dispose()
        {
            this.original = null;
            if (this.valuesInstance != null)
            {
                ((_ValuesCollection)(this.valuesInstance)).Dispose();
                this.valuesInstance = null;
            }
            if (this.keysInstance != null)
            {
                ((_KeysCollection)(this.keysInstance)).Dispose();
                this.keysInstance = null;
            }
            this.parent = default(TOriginalContainer);
        }

        #endregion

        public TOriginalContainer Parent
        {
            get { return this.parent; }
        }

        public TDictionary Original
        {
            get
            {
                return this.original;
            }
        }

        protected override ControlledStateDictionary<string, TDeclarationSpecific>.KeysCollection InitializeKeysCollection()
        {
            return new _KeysCollection(this);
        }

        protected override ControlledStateDictionary<string, TDeclarationSpecific>.ValuesCollection InitializeValuesCollection()
        {
            return new _ValuesCollection(this);
        }
        #region IDeclarationDictionary Members

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (!(decl is TDeclarationSpecific))
                return -1;
            return this.IndexOf((TDeclarationSpecific)(decl));
        }

        #endregion

        public int IndexOf(TDeclarationSpecific decl)
        {
            if (this.valuesInstance == null)
                return -1;
            int index = 0;
            foreach (var item in ((_ValuesCollection)(this.Values)).values.Values)
                if (object.ReferenceEquals(item, decl))
                    return index;
                else
                    index++;
            return -1;
        }

        public override IEnumerator<KeyValuePair<string, TDeclarationSpecific>> GetEnumerator()
        {
            var kE = this.Keys.GetEnumerator();
            var vE = this.Values.GetEnumerator();
            while (kE.MoveNext())
            {
                vE.MoveNext();
                yield return new KeyValuePair<string, TDeclarationSpecific>(kE.Current, vE.Current);
            }
            kE.Dispose();
            vE.Dispose();
        }


        protected override KeyValuePair<string, TDeclarationSpecific> OnGetThis(int index)
        {
            return new KeyValuePair<string, TDeclarationSpecific>(this.Keys[index], this.Values[index]);
        }

        public override KeyValuePair<string, TDeclarationSpecific>[] ToArray()
        {
            return Enumerable.ToArray(this);
        }

        public override TDeclarationSpecific this[string key]
        {
            get
            {
                return OnGetThis(key);
            }
            protected set
            {
                throw new NotSupportedException();
            }
        }
        public override bool ContainsKey(string key)
        {
            return this.Keys.Contains(key);
        }

        public override bool Contains(KeyValuePair<string, TDeclarationSpecific> item)
        {
            if (this.Keys.Contains(item.Key))
                return this.Values[this.Keys.GetIndexOf(item.Key)] == item.Value;
            return false;
        }

        protected override TDeclarationSpecific OnGetThis(string key)
        {
            if (this.ContainsKey(key))
                return this.Values[this.Keys.GetIndexOf(key)];
            throw new KeyNotFoundException(key);
        }


        public override void CopyTo(KeyValuePair<string, TDeclarationSpecific>[] array, int arrayIndex)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }
    }
}
