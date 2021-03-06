﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
//using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
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
    /// <typeparam name="TMethodSignature">The type of method signature used within the current
    /// type system.</typeparam>
    public abstract partial class IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodSignature> :
        IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent, TMethodSignature>,
        IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEvent :
            IEventSignatureMember<TEvent, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
            TEventParent
        where TMethodSignature :
            class,
            IIntermediateMethodSignatureMember
    {
        public IntermediateEventSignatureMemberBase(TIntermediateEventParent parent) :
            base(parent)
        {
        }

        protected override IntermediateParameterMemberDictionary<TEvent, TIntermediateEvent, IEventSignatureParameterMember<TEvent, TEventParent>, IIntermediateEventSignatureParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>> InitializeParameters()
        {
            return new _Parameters((TIntermediateEvent)(object)this);
        }

        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
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
    /// <typeparam name="TMethodSignature">The type of method signature used within the current
    /// type system.</typeparam>
    public abstract class IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent, TMethodSignature> :
        IntermediateSignatureMemberBase<IGeneralSignatureMemberUniqueIdentifier, TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent>,
        _IIntermediateEventSignatureMember
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
        where TMethodSignature :
            IIntermediateMethodSignatureMember
    {
        private EventSignatureSource signatureSource;
        private IType returnType;
        private IDelegateType signatureType;
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private TMethodSignature addMethod;
        private TMethodSignature removeMethod;
        private IMetadataDefinitionCollection metadata;
        private IMetadataCollection metadataBack;
        /// <summary>
        /// Creates a new <see cref="IntermediateEventSignatureMemberBase{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent, TMethodSignature}"/>
        /// instance with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">A <typeparamref name="TIntermediateEventParent"/> instance
        /// which owns the current <see cref="IntermediateEventSignatureMemberBase{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent, TMethodSignature}"/>.</param>
        public IntermediateEventSignatureMemberBase(TIntermediateEventParent parent)
            : base(parent, parent.Assembly)
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

        /// <summary>
        /// Obtains the <typeparamref name="TMethodSignature"/> for a given
        /// <paramref name="type"/> of event method.
        /// </summary>
        /// <param name="type">The <see cref="EventMethodType"/> which designates
        /// which of the types of event method members the method needs to be.</param>
        /// <returns>A new <typeparamref name="TMethodSignature"/> structured for
        /// the <paramref name="type"/> provided.</returns>
        protected abstract TMethodSignature GetMethodSignatureMember(EventMethodType type);

        public TMethodSignature OnAddMethod
        {
            get
            {
                if (this.addMethod == null)
                    this.addMethod = this.GetMethodSignatureMember(EventMethodType.Add);
                return this.addMethod;
            }
        }

        public TMethodSignature OnRemoveMethod
        {
            get
            {
                if (this.removeMethod == null)
                    this.removeMethod = this.GetMethodSignatureMember(EventMethodType.Remove);
                return this.removeMethod;
            }
        }

        IMethodSignatureMember IEventSignatureMember.OnAddMethod { get { return this.OnAddMethod; } }
        IMethodSignatureMember IEventSignatureMember.OnRemoveMethod { get { return this.OnRemoveMethod; } }

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
        
        protected override void OnIdentifierChanged(IGeneralSignatureMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (uniqueIdentifier == null)
                    {
                        var iiem = this as IIntermediateEventMember;
                        var service = this.Assembly.GetUniqueIdentifierService();
                        if (iiem != null)
                            this.uniqueIdentifier = service.HandlesEventMemberIdentifier
                                                    ? service.GetIdentifier(iiem)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(iiem);
                        else
                            this.uniqueIdentifier = service.HandlesEventSignatureMemberIdentifier
                                                    ? service.GetIdentifier(this)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                    }
                    return this.uniqueIdentifier;
                }
            }
        }

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadataBack != null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                        else
                            this.metadataBack = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                    return this.metadataBack;
                }
            }
        }

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadata == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.metadata = new MetadataDefinitionCollection(this, this.Parent.Assembly);
                    return this.metadata;
                }
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }
        protected override void ClearIdentifier()
        {
            lock (this.SyncObject) 
                this.uniqueIdentifier = null;
        }

        bool _IIntermediateEventSignatureMember.IsDisposed
        {
            get { return this.IsDisposed; }
        }

        bool _IIntermediateEventSignatureMember.AreParametersInitialized
        {
            get { return this.AreParametersInitialized; }
        }
    }
    internal interface _IIntermediateEventSignatureMember
    {
        bool IsDisposed { get; }
        bool AreParametersInitialized { get; }
    }
}
