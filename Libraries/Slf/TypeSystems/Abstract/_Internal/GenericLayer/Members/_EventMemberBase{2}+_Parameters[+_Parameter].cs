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
    partial class _EventMemberBase<TEvent, TEventParent>
    {
        private class _Parameters :
            _ParametersBase<TEvent, IEventParameterMember<TEvent, TEventParent>>
        {
            internal _Parameters(IParameterMemberDictionary<TEvent, IEventParameterMember<TEvent, TEventParent>> original, _EventMemberBase<TEvent, TEventParent> parent)
                : base(((TEvent)((object)(parent))), original)
            {
            }

            internal new class _Parameter :
                _ParametersBase<TEvent, IEventParameterMember<TEvent, TEventParent>>._Parameter,
                IEventParameterMember<TEvent, TEventParent>
            {
                internal _Parameter(IEventParameterMember<TEvent, TEventParent> original, _EventMemberBase<TEvent, TEventParent> parent)
                    : base(original, ((TEvent)((object)(parent))))
                {
                }

                #region IParameterMember Members

                IParameterParent IParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                #region ISignatureParameterMember Members

                ISignatureMember ISignatureParameterMember.Parent
                {
                    get { return this.Parent; }
                }

                #endregion
            }

            protected override IEventParameterMember<TEvent, TEventParent> GetWrapper(IEventParameterMember<TEvent, TEventParent> original, TEvent parent)
            {
                return new _Parameter(original, ((_EventMemberBase<TEvent, TEventParent>)((object)(parent))));
            }
        }
    }
}
