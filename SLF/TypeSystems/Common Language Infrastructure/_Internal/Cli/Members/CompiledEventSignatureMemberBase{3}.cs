using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Reflection;
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
    internal abstract class CompiledEventSignatureMemberBase<TMethod, TEvent, TEventParent> :
        EventSignatureMemberBase<TEvent, TEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
    {
        private EventInfo source;
        public CompiledEventSignatureMemberBase(TEventParent parent, EventInfo source)
            : base(parent)
        {
            this.source = source;
        }

        protected override EventSignatureSource SignatureSourceImpl
        {
            get { return EventSignatureSource.Declared; }
        }

        protected override IParameterMemberDictionary<TEvent, IEventSignatureParameterMember<TEvent, TEventParent>> InitializeParameters()
        {
            throw new NotImplementedException();
        }

        protected override bool LastIsParamsImpl
        {
            get { return SignatureType.LastIsParams; }
        }

        protected override string OnGetName()
        {
            return source.Name;
        }

        protected override IDelegateType SignatureTypeImpl
        {
            get { return this.source.EventHandlerType.GetTypeReference<IDelegateType>(); }
        }
    }
}
