using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledGenericParameterConstructorMemberBase<TGenericParameter>
    {
        internal class Parameter :
            CompiledParameterMemberBase<IGenericParameterConstructorMember<TGenericParameter>>,
            IConstructorParameterMember<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>
        {
            public Parameter(ParameterInfo parameterInfo, IGenericParameterConstructorMember<TGenericParameter> parent)
                : base(parameterInfo, parent)
            {

            }

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion
        }
    }
}
