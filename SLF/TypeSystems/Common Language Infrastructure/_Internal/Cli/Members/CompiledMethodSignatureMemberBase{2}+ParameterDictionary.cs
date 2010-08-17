using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledMethodSignatureMemberBase<TMethod, TMethodParent>
    {
        private class ParametersDictionary :
            LockedParameterMembersBase<TMethod, IMethodSignatureParameterMember<TMethod, TMethodParent>>
        {
            internal ParametersDictionary(CompiledMethodSignatureMemberBase<TMethod, TMethodParent> parent, IEnumerable<IMethodSignatureParameterMember<TMethod, TMethodParent>> parameters)
                : base((TMethod)(object)parent, parameters)
            {

            }
        }
    }
}
