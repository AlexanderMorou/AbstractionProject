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


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal class ConstructorMembers<TCtor, TCtorParent> :
        SignatureMembersBase<IGeneralSignatureMemberUniqueIdentifier, TCtor, IConstructorParameterMember<TCtor, TCtorParent>, TCtorParent>,
        IConstructorMemberDictionary<TCtor, TCtorParent>,
        IConstructorMemberDictionary
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
        /// <summary>
        /// Creates a new <see cref="ConstructorMembers{TCtor, TCtorParent}"/> initialized to a 
        /// default state.
        /// </summary>
        private ConstructorMembers(FullMembersBase master)
            : base(master)
        {
        }
        public ConstructorMembers(FullMembersBase master, TCtorParent parent)
            : base(master, parent)
        {
        }

    }
}
