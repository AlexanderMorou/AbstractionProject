using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _ConstructorBase<TCtor, TCtorParent>
    {
        private class _Parameters :
            _ParametersBase<TCtor, IConstructorParameterMember<TCtor, TCtorParent>>
        {
            internal _Parameters(_ConstructorBase<TCtor, TCtorParent> parent, IParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TCtorParent>> original)
                : base((TCtor)(object)parent, original)
            {

            }


            private new class _Parameter :
                _ParametersBase<TCtor, IConstructorParameterMember<TCtor, TCtorParent>>._Parameter,
                IConstructorParameterMember<TCtor, TCtorParent>
            {
                internal _Parameter(IConstructorParameterMember<TCtor, TCtorParent> original, TCtor parent)
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

            protected override IConstructorParameterMember<TCtor, TCtorParent> GetWrapper(IConstructorParameterMember<TCtor, TCtorParent> parameter, TCtor parent)
            {
                return new _Parameter(parameter, parent);
            }

        }
    }
}
