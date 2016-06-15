using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
    /// Provides a base for an event member in its nearly complete generic form.
    /// </summary>
    /// <typeparam name="TEvent">The type of event used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEvent">The type of event used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TEventParent">The type which contains the <typeparamref name="TEvent"/>
    /// instances in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateEventParent">The type which contains the <typeparamref name="TIntermediateEvent"/>
    /// instances in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TMethodMember">The type of <see cref="IIntermediateEventMethodMember"/>
    /// within the current implementation.</typeparam>
    public abstract partial class IntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember> :
        IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent, TMethodMember>,
        IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TMethodMember :
            class,
            IIntermediateEventMethodMember
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TIntermediateEvent :
            IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
            TEvent
        where TEventParent :
            IEventParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>,
            TEventParent
    {
        private ExtendedMemberAttributes instanceFlags;
        private IntermediateEventManagementType generationType;
        private TMethodMember raiseMethod;
        private bool emitRaiseMethod;
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private TypeCollectionWithEvents<IInterfaceType> implementationTypes;
        /// <summary>
        /// Creates a new <see cref="IntermediateEventMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember}"/> instance
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/> which
        /// contains the <see cref="IntermediateEventMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember}"/></param>
        public IntermediateEventMember(TIntermediateEventParent parent)
            : base(parent)
        {
        }

        public ITypeCollection Implementations
        {
            get { return this.implementationTypes ?? (this.implementationTypes = new TypeCollectionWithEvents<IInterfaceType>(this.implementationTypes_GlobalDelta)); }
        }

        private void implementationTypes_GlobalDelta(object sender, EventArgs e)
        {
            this.OnIdentifierChanged(this._UniqueIdentifier, DeclarationChangeCause.IdentityCardinality);
        }

        IEnumerable<IInterfaceType> IExtendedInstanceMember.Implementations
        {
            get
            {
                foreach (IType t in this.Implementations)
                    yield return (IInterfaceType)t;
            }
        }

        protected override IntermediateParameterMemberDictionary<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>> InitializeParameters()
        {
            return new ParameterDictionary(this);
        }

        internal IGeneralSignatureMemberUniqueIdentifier _UniqueIdentifier
        {
            get
            {
                return this.uniqueIdentifier;
            }
        }

        /// <summary>
        /// Obtains the <typeparamref name="TMethodMember"/> for a given
        /// <paramref name="type"/> of event method.
        /// </summary>
        /// <param name="type">The <see cref="EventMethodType"/> which designates
        /// which of the types of event method members the method needs to be.</param>
        /// <returns>A new <typeparamref name="TMethodMember"/> structured for
        /// the <paramref name="type"/> provided.</returns>
        protected abstract TMethodMember GetMethodMember(EventMethodType type);

        protected sealed override TMethodMember GetMethodSignatureMember(EventMethodType type)
        {
            return this.GetMethodMember(type);
        }

        #region IIntermediateEventMember<TEvent,TIntermediateEvent,TEventParent,TIntermediateEventParent> Members
        /// <summary>
        /// Returns/sets the type of management the event receives</summary>
        /// <remarks>If set to <see cref="IntermediateEventManagementType.Automatic"/>, 
        /// <see cref="IntermediateEventSignatureMemberBase{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent, TMethodSignature}.OnAddMethod"/>,
        /// <see cref="IntermediateEventSignatureMemberBase{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent, TMethodSignature}.OnRemoveMethod"/>,
        /// and <see cref="OnRaiseMethod"/> will be locked, and immutable.</remarks>
        public IntermediateEventManagementType GenerationType
        {
            get
            {
                return this.generationType;
            }
            set
            {
                this.generationType = value;
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TMethodMember"/> which is responsible for adding a handler
        /// of the event.
        /// </summary>
        /// <remarks><para>Parameters are read-only when <see cref="IntermediateEventSignatureMemberBase{TEvent, TIntermediateEvent, TEventParameter, TIntermediateEventParameter, TEventParent, TIntermediateEventParent, TMethodSignature}.SignatureSource"/>
        /// is <see cref="EventSignatureSource.Delegate"/>.</para>
        /// <para>Statements emitted by the raise method are read-only when
        /// <see cref="GenerationType"/> is <see cref="IntermediateEventManagementType.Automatic"/>.
        /// </para></remarks>
        /// <returns>null if <see cref="EmitRaiseMethod"/> is false;
        /// an instance of <typeparamref name="TMethodMember"/> if <see cref="EmitRaiseMethod"/>
        /// is true.</returns>
        public TMethodMember OnRaiseMethod
        {
            get
            {
                if (this.emitRaiseMethod)
                {
                    if (this.raiseMethod == null)
                        this.raiseMethod = this.GetMethodMember(EventMethodType.Fire);
                    return this.raiseMethod;
                }
                else
                    return default(TMethodMember);
            }
        }

        IIntermediateMethodMember IIntermediateEventMember.OnAddMethod
        {
            get
            {
                return this.OnAddMethod;
            }
        }

        IIntermediateMethodMember IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>.OnAddMethod
        {
            get
            {
                return this.OnAddMethod;
            }
        }

        IIntermediateMethodMember IIntermediateEventMember.OnRemoveMethod
        {
            get
            {
                return this.OnRemoveMethod;
            }
        }

        IIntermediateMethodMember IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>.OnRemoveMethod
        {
            get
            {
                return this.OnRemoveMethod;
            }
        }

        IIntermediateMethodMember IIntermediateEventMember.OnRaiseMethod
        {
            get
            {
                return this.OnRaiseMethod;
            }
        }

        IIntermediateMethodMember IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>.OnRaiseMethod
        {
            get
            {
                return this.OnRaiseMethod;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediateEventMember"/> will emit
        /// the necessary code for the raise method.
        /// </summary>
        /// <remarks>Support for this will be an external method in languages
        /// which don't support methods beyond add and remove, such as C&#9839;.</remarks>
        public bool EmitRaiseMethod
        {
            get
            {
                return this.emitRaiseMethod;
            }
            set
            {
                if (value == this.emitRaiseMethod)
                    return;
                this.emitRaiseMethod = value;
                if (!value && this.raiseMethod != null)
                {
                    this.raiseMethod.Dispose();
                    this.raiseMethod = null;
                }
            }
        }

        #endregion

        #region IIntermediateExtendedInstanceMember Members

        public bool IsAbstract
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Abstract) == ExtendedMemberAttributes.Abstract);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Static | ExtendedMemberAttributes.Virtual | ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
                    this.instanceFlags |= ExtendedMemberAttributes.Abstract;
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Abstract;
            }
        }

        public bool IsVirtual
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Virtual) == ExtendedMemberAttributes.Virtual);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Static | ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
                    this.instanceFlags |= ExtendedMemberAttributes.Virtual;
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Virtual;
            }
        }

        public bool IsFinal
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Final) == ExtendedMemberAttributes.Final);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Virtual | ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Static);
                    this.instanceFlags |= (ExtendedMemberAttributes.Final | ExtendedMemberAttributes.Override);
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Final;
            }
        }

        public bool IsOverride
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Override) == ExtendedMemberAttributes.Override);
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Static | ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Virtual);
                    this.instanceFlags |= ExtendedMemberAttributes.Override;
                }
                else
                    this.instanceFlags &= (ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
            }
        }

        #endregion

        #region IIntermediateInstanceMember Members

        public bool IsHideBySignature
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.HideBySignature) == ExtendedMemberAttributes.HideBySignature);
            }
            set
            {
                if (value == IsHideBySignature)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedMemberAttributes.HideBySignature;
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.HideBySignature;
            }
        }

        public bool IsStatic
        {
            get
            {
                if (Parent is IIntermediateClassType)
                {
                    var intermediateParent = Parent as IIntermediateClassType;
                    var specialModifier = intermediateParent.SpecialModifier;
                    if ((specialModifier & SpecialClassModifier.Static) == SpecialClassModifier.Static ||
                        (specialModifier & SpecialClassModifier.Module) == SpecialClassModifier.Module)
                        return true;
                }
                return IsExplicitStatic;
            }
            set
            {
                if (value)
                {
                    this.instanceFlags &= ~(ExtendedMemberAttributes.Abstract | ExtendedMemberAttributes.Virtual | ExtendedMemberAttributes.Override | ExtendedMemberAttributes.Final);
                    this.instanceFlags |= ExtendedMemberAttributes.Static;
                }
                else
                    this.instanceFlags &= ~ExtendedMemberAttributes.Static;
            }
        }

        public bool IsExplicitStatic
        {
            get
            {
                return ((this.instanceFlags & ExtendedMemberAttributes.Static) == ExtendedMemberAttributes.Static);
            }
        }
        #endregion

        #region IInstanceMember Members

        InstanceMemberAttributes IInstanceMember.Attributes
        {
            get { return ((InstanceMemberAttributes)(this.Attributes)); }
        }

        #endregion

        #region IExtendedInstanceMember Members

        public ExtendedMemberAttributes Attributes
        {
            get { return this.instanceFlags; }
        }

        #endregion

        #region IIntermediateScopedDeclaration Members

        public AccessLevelModifiers AccessLevel { get; set; }

        #endregion

        #region IEventMember Members

        IMethodMember IEventMember.OnAddMethod
        {
            get { return this.OnAddMethod; }
        }

        IMethodMember IEventMember.OnRemoveMethod
        {
            get { return this.OnRemoveMethod; }
        }

        IMethodMember IEventMember.OnRaiseMethod
        {
            get { return this.OnRaiseMethod; }
        }

        public bool CanRaise
        {
            get { return this.EmitRaiseMethod; }
        }

        #endregion

        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            lock (this.SyncObject)
                if (this.implementationTypes != null)
                {
                    this.implementationTypes.Dispose();
                    this.implementationTypes.Clear();
                    this.implementationTypes = null;
                }
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    if (this.AreParametersInitialized)
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetSignatureIdentifier(this.Name, this.Parameters.ParameterTypes.ToArray());
                    else
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetSignatureIdentifier(this.Name);
                return this.uniqueIdentifier;
            }
        }
    }
}
