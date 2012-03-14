using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledMethodSignatureMemberBase<TMethod, TMethodParent>
    {
        private class ParameterMember :
            CompiledParameterMemberBase<TMethod>,
            IMethodSignatureParameterMember<TMethod, TMethodParent>,
            IMethodSignatureParameterMember
        {
            public ParameterMember(ParameterInfo paramInfo, CompiledMethodSignatureMemberBase<TMethod, TMethodParent> parent)
                : base(paramInfo, (TMethod)(object)parent, parent.Manager)
            {
            }

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return base.Parent; }
            }

            #endregion
            #region IMethodSignatureParameterMember Members
            IMethodSignatureMember IMethodSignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }
            #endregion
        }

    }
}
