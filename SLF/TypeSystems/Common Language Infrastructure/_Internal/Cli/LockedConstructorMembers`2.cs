﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedConstructorMembers<TCtor, TCtorParent> :
        LockedSignatureMembersBase<TCtor, IConstructorParameterMember<TCtor, TCtorParent>, TCtorParent, ConstructorInfo>,
        IConstructorMemberDictionary<TCtor, TCtorParent>, 
        IConstructorMemberDictionary
        where TCtor :
            class,
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableType<TCtor, TCtorParent>
    {
        /// <summary>
        /// Creates a new <see cref="LockedConstructorMembers{TCtor, TCtorParent}"/> initialized to a 
        /// default state.
        /// </summary>
        public LockedConstructorMembers(LockedFullMembersBase master)
            : base(master)
        {
        }

        internal LockedConstructorMembers(LockedFullMembersBase master, TCtorParent parent, ConstructorInfo[] seriesData, Func<ConstructorInfo, TCtor> fetchImpl)
            : base(master, parent, seriesData, fetchImpl)
        {
        }

        internal LockedConstructorMembers(LockedFullMembersBase master, TCtorParent parent)
            : base(master, parent)
        {
        }

        protected override string FetchKey(ConstructorInfo item)
        {
            return item.GetUniqueIdentifier();
        }
    }
}