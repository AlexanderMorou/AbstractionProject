using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal class MembersBase<TParent, TItem> :
        DeclarationDictionaryBase<TItem>,
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

        /// <summary>
        /// Creates a new <see cref="MembersBase{TParent, TItem}"/> with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TParent"/> which contains the <see cref="MembersBase{TParent, TItem}"/>.</param>
        protected MembersBase(TParent parent)
            : base()
        {
            this.parent = parent;
        }

        internal MembersBase() { }

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
            return this.IndexOf((TItem)(member));
        }

        #endregion

    }
}
