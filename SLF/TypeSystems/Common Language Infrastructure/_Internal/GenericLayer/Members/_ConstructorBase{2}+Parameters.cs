using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _ConstructorBase<TCtor, TType>
    {
        private class _Parameters :
            _ParametersBase<TCtor, IConstructorParameterMember<TCtor, TType>>
        {
            internal _Parameters(_ConstructorBase<TCtor, TType> parent, IParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TType>> original)
                : base((TCtor)(object)parent, original)
            {

            }


            private new class _Parameter :
                _ParametersBase<TCtor, IConstructorParameterMember<TCtor, TType>>._Parameter,
                IConstructorParameterMember<TCtor, TType>
            {
                internal _Parameter(IConstructorParameterMember<TCtor, TType> original, TCtor parent)
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

            protected override IConstructorParameterMember<TCtor, TType> GetWrapper(IConstructorParameterMember<TCtor, TType> parameter, TCtor parent)
            {
                return new _Parameter(parameter, parent);
            }

        }
    }
}
