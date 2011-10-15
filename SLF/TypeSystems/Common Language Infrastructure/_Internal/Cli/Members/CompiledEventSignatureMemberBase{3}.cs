using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
/*----------------------------------------\
| Copyright © 2007 Allen Copeland Jr.     |
|-----------------------------------------|
| This code is provided as-is without any |
| warranties of any kind.                 |
|-----------------------------------------|
|   For learning purposes only and        |
|   Distribution on CodePlex, further use |
|   available only upon request and where |
|   permission is granted.                |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParentIdentifier, TEventParent> :
        EventSignatureMemberBase<TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        private EventInfo memberInfo;
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        public CompiledEventSignatureMemberBase(TEventParent parent, EventInfo source)
            : base(parent)
        {
            this.memberInfo = source;
        }

        protected override EventSignatureSource SignatureSourceImpl
        {
            get { return EventSignatureSource.Declared; }
        }

        protected override IParameterMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>> InitializeParameters()
        {
            return new ParameterMemberDictionary(this, from delegateParameter in SignatureType.Parameters.Values
                                                       select new ParameterMember(delegateParameter, this));
        }

        protected override bool LastIsParamsImpl
        {
            get { return SignatureType.LastIsParams; }
        }

        protected override string OnGetName()
        {
            return memberInfo.Name;
        }

        protected override IDelegateType SignatureTypeImpl
        {
            get { return this.memberInfo.EventHandlerType.GetTypeReference<IDelegateUniqueIdentifier, IDelegateType>(); }
        }

        public override IType ReturnType
        {
            get { return this.SignatureTypeImpl.ReturnType; }
        }
        
        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = this.memberInfo.GetUniqueIdentifier();
                return this.uniqueIdentifier;
            }
        }
    }
}
