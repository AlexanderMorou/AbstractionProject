using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal abstract partial class _DeclarationsBase<TDeclarationIdentifier, TDeclaration, TDeclarationSpecificIdentifier, TDeclarationSpecific, TOriginalContainer, TDictionary> :
        SubordinateDictionary<TDeclarationSpecificIdentifier, TDeclarationIdentifier, TDeclarationSpecific, TDeclaration>,
        IDeclarationDictionary<TDeclarationSpecificIdentifier, TDeclarationSpecific>,
        IDeclarationDictionary
        where TDeclarationIdentifier :
            IDeclarationUniqueIdentifier
        where TDeclaration :
            class,
            IDeclaration
        where TDeclarationSpecificIdentifier :
            IDeclarationUniqueIdentifier,
            TDeclarationIdentifier
        where TDeclarationSpecific :
            class,
            IDeclaration<TDeclarationSpecificIdentifier>,
            TDeclaration
        where TDictionary :
            class,
            IDeclarationDictionary<TDeclarationSpecificIdentifier, TDeclarationSpecific>
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

        internal _DeclarationsBase(MasterDictionaryBase<TDeclarationIdentifier, TDeclaration> master, TOriginalContainer parent, TDictionary original)
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

        protected override ControlledDictionary<TDeclarationSpecificIdentifier, TDeclarationSpecific>.KeysCollection InitializeKeysCollection()
        {
            return new _KeysCollection(this);
        }

        protected override ControlledDictionary<TDeclarationSpecificIdentifier, TDeclarationSpecific>.ValuesCollection InitializeValuesCollection()
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

        public override IEnumerator<KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific>> GetEnumerator()
        {
            var kE = this.Keys.GetEnumerator();
            var vE = this.Values.GetEnumerator();
            while (kE.MoveNext())
            {
                vE.MoveNext();
                yield return new KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific>(kE.Current, vE.Current);
            }
            kE.Dispose();
            vE.Dispose();
        }


        protected override KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific> OnGetThis(int index)
        {
            return new KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific>(this.Keys[index], this.Values[index]);
        }

        public override KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific>[] ToArray()
        {
            return Enumerable.ToArray(this);
        }

        public override TDeclarationSpecific this[TDeclarationSpecificIdentifier key]
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
        public override bool ContainsKey(TDeclarationSpecificIdentifier key)
        {
            return this.Keys.Contains(key);
        }

        public override bool Contains(KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific> item)
        {
            if (this.Keys.Contains(item.Key))
                return this.Values[this.Keys.IndexOf(item.Key)] == item.Value;
            return false;
        }

        protected override TDeclarationSpecific OnGetThis(TDeclarationSpecificIdentifier key)
        {
            if (this.ContainsKey(key))
                return this.Values[this.Keys.IndexOf(key)];
            throw new KeyNotFoundException();
        }


        public override void CopyTo(KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific>[] array, int arrayIndex)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }
    }
}
