﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedMembersBase<TParent, TItem> :
        LockedDeclarationsBase<TItem>,
        IMemberDictionary<TParent, TItem>,
        IMemberDictionary
        where TParent :
            IMemberParent
        where TItem :
            IMember<TParent>
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