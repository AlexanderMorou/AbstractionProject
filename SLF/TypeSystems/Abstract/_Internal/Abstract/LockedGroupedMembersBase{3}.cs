using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{

    internal abstract partial class LockedGroupedMembersBase<TParent, TItem, TSourceItem> :
        LockedGroupedDeclarationsBase<TItem, IMember, TSourceItem>,
        IGroupedMemberDictionary<TParent, TItem>,
        IGroupedMemberDictionary
        where TParent :
            IMemberParent
        where TItem :
            class,
            IMember<TParent>,
            IDisposable
        where TSourceItem :
            class
    {
        private TParent parent;

        protected LockedGroupedMembersBase(LockedFullMembersBase master, TParent parent)
            : base(master)
        {
            this.parent = parent;
        }

        protected LockedGroupedMembersBase(LockedFullMembersBase master)
            : base(master)
        {
        }

        protected LockedGroupedMembersBase(LockedFullMembersBase master, TSourceItem[] sourceData, Func<TSourceItem, TItem> fetchImpl)
            : base(master, sourceData, fetchImpl)
        {
        }

        protected LockedGroupedMembersBase(LockedFullMembersBase master, TParent parent, TSourceItem[] sourceData, Func<TSourceItem, TItem> fetchImpl)
            : base(master, sourceData, fetchImpl)
        {
            this.parent = parent;
        }

        protected LockedGroupedMembersBase(LockedFullMembersBase master, IEnumerable<TItem> source)
            : base(master, source)
        {
        }

        protected LockedGroupedMembersBase(LockedFullMembersBase master, TParent parent, IEnumerable<TItem> source)
            : base(master, source)
        {
            this.parent = parent;
        }

        #region IMemberDictionary<TParent,TItem> Members

        /// <summary>
        /// Returns the <typeparamref name="TParent"/> which contains the <see cref="IMemberDictionary{TParent, TItem}"/>.
        /// </summary>
        public TParent Parent
        {
            get { 
                return this.parent; 
            }
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
            return this.IndexOf((TItem)member);
        }

        #endregion
    }
}
