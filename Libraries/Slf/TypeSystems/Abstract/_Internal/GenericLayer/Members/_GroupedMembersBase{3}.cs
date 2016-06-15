using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _GroupedMembersBase<TParent, TMemberIdentifier, TMember, TMembers> :
        _GroupedDeclarations<TMemberIdentifier, TMember, TParent, IGeneralMemberUniqueIdentifier, IMember, TMembers>,
        IGroupedMemberDictionary<TParent, TMemberIdentifier, TMember>,
        IGroupedMemberDictionary
        where TMemberIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TMember :
            class,
            IMember<TMemberIdentifier, TParent>
        where TParent :
            IMemberParent
        where TMembers :
            class,
            IGroupedMemberDictionary<TParent, TMemberIdentifier, TMember>
    {
        internal _GroupedMembersBase(_FullMembersBase master, TMembers originalSet, TParent parent)
            : base(master, originalSet, parent)
        {
        }

        #region IMemberDictionary<TParent,TMember> Members

        public new TParent Parent
        {
            get { return ((TParent)(base.Parent)); }
        }

        #endregion

        #region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            if (!(member is TMember))
                return -1;
            return this.IndexOf(((TMember)(member)));
        }

        #endregion

    }
}
