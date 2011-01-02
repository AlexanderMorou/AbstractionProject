using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledMethodMemberBase<TMethod, TMethodParent>
    {
        private class ParametersDictionary :
            LockedParameterMembersBase<TMethod, IMethodParameterMember<TMethod, TMethodParent>>
        {
            internal ParametersDictionary(CompiledMethodMemberBase<TMethod, TMethodParent> parent, IEnumerable<IMethodParameterMember<TMethod, TMethodParent>> parameters)
                : base((TMethod)(object)parent, parameters)
            {

            }
        }
    }
}
