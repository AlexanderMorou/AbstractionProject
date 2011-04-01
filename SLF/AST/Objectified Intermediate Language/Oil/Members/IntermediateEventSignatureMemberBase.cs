using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base class for working with the signature
    /// of an intermediate event member.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the
    /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract partial class IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> :
        IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
    {
        public IntermediateEventSignatureMemberBase(TIntermediateEventParent parent) :
            base(parent)
        {
        }

        protected override IntermediateParameterMemberDictionary<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>> InitializeParameters()
        {
            return new _Parameters((TIntermediateEvent)(object)this);
        }

        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    /// <summary>
    /// Provides a base implementation for an event signature member in its most
    /// generic form.
    /// </summary>
    /// <typeparam name="TEvent">The type of event in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParameter">The type of parameter used by the event in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParameter">The type of parameter used by the event 
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the
    /// <typeparamref name="TIntermediateEvent"/> instances in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract class IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent> :
        IntermediateSignatureMemberBase<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParameter :
            TEventParameter,
            IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>
    {
        private EventSignatureSource signatureSource;
        private IType returnType;
        private IDelegateType signatureType;
        /// <summary>
        /// Creates a new <see cref="IntermediateEventSignatureMemberBase{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>
        /// instance with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">A <typeparamref name="TIntermediateEventParent"/> instance
        /// which owns the current <see cref="IntermediateEventSignatureMemberBase{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent}"/>.</param>
        public IntermediateEventSignatureMemberBase(TIntermediateEventParent parent)
            : base(parent)
        {
        }

        #region IIntermediateEventSignatureMember Members

        public EventSignatureSource SignatureSource
        {
            get
            {
                return this.signatureSource;
            }
            set
            {
                if (value == this.signatureSource)
                    return;
                switch (value)
                {
                    case EventSignatureSource.Declared:
                    case EventSignatureSource.Delegate:
                        this.signatureSource = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
            }
        }

        public IDelegateType SignatureType
        {
            get
            {
                switch (this.signatureSource)
                {
                    case EventSignatureSource.Declared:
                        return null;
                    case EventSignatureSource.Delegate:
                        return this.signatureType;
                }
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
            }
            set
            {
                this.signatureType = value;
                this.SignatureSource = EventSignatureSource.Delegate;
            }
        }

        #endregion

        public IType ReturnType
        {
            get
            {
                switch (this.signatureSource)
                {
                    case EventSignatureSource.Declared:
                        return this.returnType;
                    case EventSignatureSource.Delegate:
                        return this.SignatureType.ReturnType;
                    default:
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                }
            }
            set 
            {
                switch (this.signatureSource)
                {
                    case EventSignatureSource.Declared:
                        this.returnType = value;
                        break;
                    case EventSignatureSource.Delegate:
                        throw new InvalidOperationException("Cannot alter the return type of the attached delegate through this means.");
                    default:
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                }
            }
        }

    }
}
