using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _MembersBase<TParent, TMemberIdentifier, TMember, TDictionary> :
        _DeclarationsBase<IGeneralMemberUniqueIdentifier, IMember, TMemberIdentifier, TMember, TParent, TDictionary>,
        IMemberDictionary<TParent, TMemberIdentifier, TMember>,
        IMemberDictionary
        where TMemberIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TMember :
            class,
            IMember<TMemberIdentifier, TParent>
        where TParent :
            IMemberParent
        where TDictionary :
            class,
            IMemberDictionary<TParent, TMemberIdentifier, TMember>
    {
        protected _MembersBase(TParent parent, TDictionary original)
            : base(parent, original)
        {

        }
        #region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get
            {
                return this.Parent;
            }
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            if (!(member is TMember))
                return -1;
            return this.IndexOf((TMember)(member));
        }

        #endregion

    }
}
