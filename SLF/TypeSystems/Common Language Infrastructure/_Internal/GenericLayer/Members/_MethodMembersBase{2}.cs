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
    internal abstract class _MethodMembersBase<TMethod, TMethodParent> :
        _MethodSignatureMembersBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent, IMethodMemberDictionary<TMethod, TMethodParent>>,
        IMethodMemberDictionary<TMethod, TMethodParent>
        where TMethod :
            class,
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        protected _MethodMembersBase(_FullMembersBase master, IMethodMemberDictionary<TMethod, TMethodParent> originalSet, IGenericType parent)
            : base(master, originalSet, parent)
        {
        }
    }
}
