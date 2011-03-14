using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledEventMemberBase<TMethod, TEvent, TEventParent>
        where TMethod :
            class,
            IMethodMember<TMethod, TEventParent>,
            IExtendedInstanceMember
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IMethodParent<TMethod, TEventParent>,
            IEventParent<TEvent, TEventParent>
    {
        private class ParameterMember :
            ParameterMemberBase<TEvent>,
            IEventParameterMember<TEvent, TEventParent>
        {
            private IDelegateTypeParameterMember original;

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            public ParameterMember(IDelegateTypeParameterMember original, CompiledEventMemberBase<TMethod, TEvent, TEventParent> parent)
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
