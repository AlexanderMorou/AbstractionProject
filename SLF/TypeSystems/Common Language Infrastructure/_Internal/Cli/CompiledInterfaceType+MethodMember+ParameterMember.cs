using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
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


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        partial class MethodMember
        {
            private sealed class ParameterMember :
                CompiledParameterMemberBase<IInterfaceMethodMember>,
                IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>
            {
                internal ParameterMember(ParameterInfo parameterInfo, IInterfaceMethodMember parent)
                    : base(parameterInfo, parent)
                {
                }

                #region IMethodSignatureParameterMember Members
                IMethodSignatureMember IMethodSignatureParameterMember.Parent
                {
                    get
                    {
                        return this.Parent;
                    }
                }
                #endregion

                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion
            }
        }
    }
}
