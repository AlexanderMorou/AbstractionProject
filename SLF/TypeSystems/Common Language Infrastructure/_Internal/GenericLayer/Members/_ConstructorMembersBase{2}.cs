using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _ConstructorMembersBase<TCtor, TType> :
        _SignatureMembersBase<TCtor, IConstructorParameterMember<TCtor, TType>, TType, IConstructorMemberDictionary<TCtor, TType>>,
        IConstructorMemberDictionary<TCtor, TType>
        where TCtor :
            class,
            IConstructorMember<TCtor, TType>
        where TType :
            ICreatableType<TCtor, TType>
    {
        internal _ConstructorMembersBase(_FullMembersBase fullMembers, IConstructorMemberDictionary<TCtor, TType> originalSet, TType parent)
            : base(fullMembers, originalSet, (IGenericType)parent)
        {

        }
    }
}
