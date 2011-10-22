using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledGenericParameterMethodMember<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        protected class ParameterMember :
            CompiledParameterMemberBase<IGenericParameterMethodMember<TGenericParameter>>,
            IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>
        {
            internal ParameterMember(ParameterInfo paramInfo, CompiledGenericParameterMethodMember<TGenericParameter> parent)
                : base(paramInfo, parent)
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
                get { return Parent; }
            }

            #endregion

        }
    }
}
