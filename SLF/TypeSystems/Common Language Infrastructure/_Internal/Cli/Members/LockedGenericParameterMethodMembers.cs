using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class LockedGenericParameterMethodMembers<TGenericParameter> :
        LockedMethodSignatureMembersBase<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterMethodMemberDictionary<TGenericParameter>,
        IGenericParameterMethodMemberDictionary
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        public LockedGenericParameterMethodMembers(LockedFullMembersBase master, TGenericParameter parent, MethodInfo[] methods, Func<MethodInfo, IGenericParameterMethodMember<TGenericParameter>> fetchImpl)
            : base(master, parent, methods, fetchImpl)
        {
        }

        protected override string FetchKey(MethodInfo item)
        {
            return item.GetUniqueIdentifier();
        }

        IGenericParameter IGenericParameterMethodMemberDictionary.Parent
        {
            get
            {
                return base.Parent;
            }
        }

    }
}
