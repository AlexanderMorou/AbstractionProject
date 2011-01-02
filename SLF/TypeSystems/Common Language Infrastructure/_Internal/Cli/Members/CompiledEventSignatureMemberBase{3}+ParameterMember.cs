using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        private class ParameterMember :
            ParameterMemberBase<TEvent>,
            IEventSignatureParameterMember<TEvent, TEventParent>
        {
            private IDelegateTypeParameterMember original;

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            public ParameterMember(IDelegateTypeParameterMember original, CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParent> parent)
                : base((TEvent)(object)parent)
            {
                this.original = original;
            }

            protected override IType ParameterTypeImpl
            {
                get { return this.original.ParameterType; }
            }

            public override ParameterDirection Direction
            {
                get { return this.original.Direction; }
            }

            protected override ICustomAttributeCollection InitializeCustomAttributes()
            {
                return this.original.CustomAttributes;
            }

            protected override string OnGetName()
            {
                return this.original.Name;
            }

            public override string UniqueIdentifier
            {
                get { return this.original.UniqueIdentifier; }
            }
        }
    }
}
