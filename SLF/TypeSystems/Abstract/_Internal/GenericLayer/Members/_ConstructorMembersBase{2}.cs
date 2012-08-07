using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _ConstructorMembersBase<TCtor, TCtorParent> :
        _SignatureMembersBase<IGeneralSignatureMemberUniqueIdentifier, TCtor, IConstructorParameterMember<TCtor, TCtorParent>, TCtorParent, IConstructorMemberDictionary<TCtor, TCtorParent>>,
        IConstructorMemberDictionary<TCtor, TCtorParent>,
        IConstructorMemberDictionary
        where TCtor :
            class,
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
        internal _ConstructorMembersBase(_FullMembersBase fullMembers, IConstructorMemberDictionary<TCtor, TCtorParent> originalSet, TCtorParent parent)
            : base(fullMembers, originalSet, parent)
        {

        }
    }
}
