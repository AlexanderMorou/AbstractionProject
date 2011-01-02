using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _MethodMemberBase<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private new class _Parameters :
            _MethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>._Parameters
        {
            internal _Parameters(_MethodMemberBase<TMethod, TMethodParent> parent, IParameterMemberDictionary<TMethod, IMethodParameterMember<TMethod, TMethodParent>> original)
                : base(parent, original)
            {
            }

            private new class _Parameter :
                _MethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>._Parameters._Parameter,
                IMethodParameterMember<TMethod, TMethodParent>
            {
                internal _Parameter(IMethodParameterMember<TMethod, TMethodParent> original, TMethod parent)
                    : base(original, parent)
                {
                }

                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                #region IMethodParameterMember Members

                IMethodMember IMethodParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                #region IMethodSignatureParameterMember Members
                IMethodSignatureMember IMethodSignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }
                #endregion

            }

            protected override IMethodParameterMember<TMethod, TMethodParent> GetWrapper(IMethodParameterMember<TMethod, TMethodParent> original, TMethod parent)
            {
                return new _Parameter(original, parent);
            }
        }
    }
}
