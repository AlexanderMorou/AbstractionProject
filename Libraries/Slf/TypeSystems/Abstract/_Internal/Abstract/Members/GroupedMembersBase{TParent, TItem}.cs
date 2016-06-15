using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a root implementation for generic <see cref="IMemberDictionary{TParent, TItem}"/> for working with a series of <typeparamref name="TItem"/> 
    /// instances as a member of a <typeparamref name="TParent"/> instance.
    /// </summary>
    /// <typeparam name="TParent">The type of the parent that contains the <typeparamref name="TItem"/>
    /// instances in the current implementation.</typeparam>
    /// <typeparam name="TItem">The type of <see cref="IMember{TParent}"/> contained within the 
    /// current <see cref="IMemberDictionary{TParent, TItem}"/> implementation.</typeparam>
    internal class GroupedMembersBase<TParent, TItemIdentifier, TItem> :
        GroupedDeclarationDictionaryBase<TItemIdentifier, TItem, IGeneralMemberUniqueIdentifier, IMember>,
        IGroupedMemberDictionary<TParent, TItemIdentifier, TItem>,
        IGroupedMemberDictionary
        where TParent :
            IMemberParent
        where TItemIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TItem :
            IMember<TItemIdentifier, TParent>
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private TParent parent;

        /// <summary>
        /// Creates a new <see cref="GroupedMembersBase{TParent, TItem}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/>
        /// on which the current <see cref="GroupedMembersBase{TParent, TItem}"/>
        /// is subordinated to.</param>
        /// <param name="parent">The <typeparamref name="TParent"/> which
        /// contains a mirror copy of the elements within the new 
        /// <see cref="GroupedMembersBase{TParent, TItem}"/>.</param>
        protected GroupedMembersBase(FullMembersBase master, TParent parent)
            : base(master)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Creates a new <see cref="GroupedMembersBase{TParent, TItem}"/> with the 
        /// <paramref name="master"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="FullMembersBase"/> 
        /// which contains the current <see cref="GroupedMembersBase{TParent, TItem}"/>.</param>
        internal GroupedMembersBase(FullMembersBase master)
            : base(master)
        {
        }

        #region IMemberDictionary<TParent,TItem> Members

        /// <summary>
        /// Returns the <typeparamref name="TParent"/> which contains the <see cref="IMemberDictionary{TParent, TItem}"/>.
        /// </summary>
        public TParent Parent
        {
            get { return this.parent; }
        }

        #endregion

        #region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IMemberDictionary Members


        int IMemberDictionary.IndexOf(IMember member)
        {
            if (!(member is TItem))
                return -1;
            return this.IndexOf(((TItem)(member)));
        }

        #endregion

    }
}
