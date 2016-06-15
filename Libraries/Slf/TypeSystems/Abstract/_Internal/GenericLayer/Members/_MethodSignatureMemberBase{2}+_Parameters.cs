using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _MethodSignatureMemberBase<TSignature, TSignatureParent>
    {
        private new class _Parameters :
            _MethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>._Parameters
        {
            internal _Parameters(_MethodSignatureMemberBase<TSignature, TSignatureParent> parent, IParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>> original)
                : base(parent, original)
            {
            }

            private new class _Parameter :
                _MethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>._Parameters._Parameter,
                IMethodSignatureParameterMember<TSignature, TSignatureParent>
            {
                internal _Parameter(IMethodSignatureParameterMember<TSignature, TSignatureParent> original, TSignature parent)
                    : base(original, parent)
                {
                }

                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

            }

            protected override IMethodSignatureParameterMember<TSignature, TSignatureParent> GetWrapper(IMethodSignatureParameterMember<TSignature, TSignatureParent> original, TSignature parent)
            {
                return new _Parameter(original, parent);
            }
        }
    }
}
