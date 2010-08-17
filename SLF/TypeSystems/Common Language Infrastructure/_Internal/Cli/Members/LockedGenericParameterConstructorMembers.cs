using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class LockedGenericParameterConstructorMembers<TGenericParameter> :
        LockedConstructorMembers<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterConstructorMemberDictionary<TGenericParameter>,
        IGenericParameterConstructorMemberDictionary
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        internal LockedGenericParameterConstructorMembers(LockedFullMembersBase master, TGenericParameter parent, ConstructorInfo[] ctorInfo, Func<ConstructorInfo, IGenericParameterConstructorMember<TGenericParameter>> fetchImpl)
            : base(master, parent, ctorInfo, fetchImpl)
        {
        }
        IGenericParameter IGenericParameterConstructorMemberDictionary.Parent
        {
            get
            {
                return base.Parent;
            }
        }
    }
}
