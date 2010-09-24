using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _EventSignatureMemberBase<TEvent, TEventParent>
    {
        private class _Parameters :
            _ParametersBase<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>>
        {
            internal _Parameters(IParameterMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>> original, _EventSignatureMemberBase<TEvent, TEventParent> parent)
                : base(((TEvent)((object)(parent))), original)
            {
            }

            internal new class _Parameter :
                _ParametersBase<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>>._Parameter,
                IEventSignatureParameterMember<TEvent, TEventParent>
            {
                internal _Parameter(IEventSignatureParameterMember<TEvent, TEventParent> original, _EventSignatureMemberBase<TEvent, TEventParent> parent)
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

            protected override IEventSignatureParameterMember<TEvent, TEventParent> GetWrapper(IEventSignatureParameterMember<TEvent, TEventParent> original, TEvent parent)
            {
                return new _Parameter(original, ((_EventSignatureMemberBase<TEvent, TEventParent>)((object)(parent))));
            }
        }
    }
}
