using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    internal abstract partial class _GroupedDeclarations<TDeclarationSpecific, TParent, TDeclaration, TDictionary> :
        _DeclarationsBase<TDeclaration, TDeclarationSpecific, TParent, TDictionary>,
        ISubordinateDictionary<string, TDeclarationSpecific, TDeclaration>,
        IGroupedDeclarationDictionary<TDeclarationSpecific>
        where TDeclarationSpecific :
            class,
            TDeclaration
        where TDeclaration :
            class,
            IDeclaration
        where TDictionary :
            class,
            ISubordinateDictionary<string, TDeclarationSpecific, TDeclaration>,
            IGroupedDeclarationDictionary<TDeclarationSpecific>
    {
        /// <summary>
        /// Creates a new <see cref="_GroupedDeclarations{TDeclarationSpecific, TParent, TDeclaration, TDictionary}"/>
        /// instance with the <paramref name="originalSet"/>
        /// provided.
        /// </summary>
        /// <param name="originalSet">The <typeparamref name="TTypes"/>
        /// from which the <see cref="_GroupedDeclarations{TDeclarationSpecific, TParent, TDeclaration, TDictionary}"/>
        /// operates.</param>
        protected _GroupedDeclarations(_GroupedMasterBase<TDeclaration> master, TDictionary originalSet, TParent parent)
            : base(master, parent, originalSet)
        {
        }

        #region IDisposable Members

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

        protected override void Clear()
        {
            throw new NotSupportedException("Immutable.");
        }

        public override bool ContainsKey(string key)
        {
            return this.Keys.Contains(key);
        }

        public override bool Contains(KeyValuePair<string, TDeclarationSpecific> item)
        {
            if (this.keysCollection.Contains(item.Key))
                return this.Values[this.Keys.GetIndexOf(item.Key)] == item.Value;
            return false;
        }

        protected override void Add(string key, TDeclarationSpecific value)
        {
            throw new NotSupportedException("Immutable");
        }

        protected override void RemoveImpl(int index)
        {
            throw new NotSupportedException("Immutable");
        }

        protected override bool Remove(string key)
        {
            throw new NotSupportedException("Immutable");
        }

        public override TDeclarationSpecific this[string key]
        {
            get
            {
                return base[key];
            }
            protected set
            {
                throw new NotSupportedException("Immutable");
            }
        }

        public override KeyValuePair<string, TDeclarationSpecific> this[int index]
        {
            get
            {
                if (index < 0 && index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                return new KeyValuePair<string, TDeclarationSpecific>(this.Keys[index], this.Values[index]);
            }
        }

        public override KeyValuePair<string, TDeclarationSpecific>[] ToArray()
        {
            return System.Linq.Enumerable.ToArray(this);
        }

        public override IEnumerator<KeyValuePair<string, TDeclarationSpecific>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
                yield return new KeyValuePair<string, TDeclarationSpecific>(this.Keys[i], this.Values[i]);
            yield break;
        }

        protected override TDeclarationSpecific OnGetThis(string key)
        {
            if (!this.Keys.Contains(key))
                throw new KeyNotFoundException();
            return this.Values[this.Keys.GetIndexOf(key)];
        }

        protected abstract TDeclarationSpecific ObtainWrapper(TDeclarationSpecific item);

        protected override TDeclarationSpecific GetWrapper(TDeclarationSpecific original, TParent parent)
        {
            return ObtainWrapper(original);
        }

    }
}
