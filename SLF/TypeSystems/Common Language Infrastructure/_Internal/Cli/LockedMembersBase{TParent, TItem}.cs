using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class LockedMembersBase<TParent, TItemIdentifier, TItem> :
        LockedDeclarationsBase<TItemIdentifier, TItem>,
        IMemberDictionary<TParent, TItemIdentifier, TItem>,
        IMemberDictionary
        where TParent :
            IMemberParent
        where TItemIdentifier :
            IMemberUniqueIdentifier<TItemIdentifier>
        where TItem :
            IMember<TItemIdentifier, TParent>
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private TParent parent;

        protected LockedMembersBase(TParent parent)
            : base()
        {
            this.parent = parent;
        }

        protected LockedMembersBase(IEnumerable<TItem> series)
            : base(series)
        {
        }

        protected LockedMembersBase(TParent parent, IEnumerable<TItem> series)
            : base(series)
        {
            this.parent = parent;
        }

        protected LockedMembersBase()
            : base()
        {
        }

        #region IMemberDictionary<TParent,TItem> Members

        /// <summary>
        /// Returns the <typeparamref name="TParent"/> which contains the <see cref="LockedMembersBase{TParent, TItem}"/>.
        /// </summary>
        public TParent Parent
        {
            get { return this.parent; }
        }

        #endregion

        #region IMemberDictionary Members


        int IMemberDictionary.IndexOf(IMember member)
        {
            if (!(member is TItem))
                return -1;
            return this.IndexOf(((TItem)(member)));
        }

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion
    }
}
