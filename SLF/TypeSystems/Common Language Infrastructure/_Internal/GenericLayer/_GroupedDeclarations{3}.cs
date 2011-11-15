using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal abstract partial class _GroupedDeclarations<TDeclarationSpecificIdentifier, TDeclarationSpecific, TParent, TDeclarationIdentifier, TDeclaration, TDictionary> :
        _DeclarationsBase<TDeclarationIdentifier, TDeclaration, TDeclarationSpecificIdentifier, TDeclarationSpecific, TParent, TDictionary>,
        ISubordinateDictionary<TDeclarationSpecificIdentifier, TDeclarationIdentifier, TDeclarationSpecific, TDeclaration>,
        IGroupedDeclarationDictionary<TDeclarationSpecificIdentifier, TDeclarationSpecific>
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
            ISubordinateDictionary<TDeclarationSpecificIdentifier, TDeclarationIdentifier, TDeclarationSpecific, TDeclaration>,
            IGroupedDeclarationDictionary<TDeclarationSpecificIdentifier, TDeclarationSpecific>
    {
        /// <summary>
        /// Creates a new <see cref="_GroupedDeclarations{TDeclarationSpecific, TParent, TDeclaration, TDictionary}"/>
        /// instance with the <paramref name="originalSet"/>
        /// provided.
        /// </summary>
        /// <param name="originalSet">The <typeparamref name="TDictionary"/>
        /// from which the <see cref="_GroupedDeclarations{TDeclarationSpecific, TParent, TDeclaration, TDictionary}"/>
        /// operates.</param>
        protected _GroupedDeclarations(_GroupedMasterBase<TDeclarationIdentifier, TDeclaration> master, TDictionary originalSet, TParent parent)
            : base(master, parent, originalSet)
        {
        }

        #region IDisposable Members

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

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

        public override TDeclarationSpecific this[TDeclarationSpecificIdentifier key]
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

        protected override KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific> OnGetThis(int index)
        {
            if (index < 0 && index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            return new KeyValuePair<TDeclarationSpecificIdentifier, TDeclarationSpecific>(this.Keys[index], this.Values[index]);
        }

        protected abstract TDeclarationSpecific ObtainWrapper(TDeclarationSpecific item);

        protected override TDeclarationSpecific GetWrapper(TDeclarationSpecific original, TParent parent)
        {
            return ObtainWrapper(original);
        }

    }
}
