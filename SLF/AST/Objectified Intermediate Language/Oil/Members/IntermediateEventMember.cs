using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public enum IntermediateEventMethodType
    {
        /// <summary>
        /// The event is an add handler method.
        /// </summary>
        Add,
        /// <summary>
        /// The event is a remove handler method.
        /// </summary>
        Remove,
        /// <summary>
        /// The event is a fire handler method.
        /// </summary>
        Fire,
    }
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
    public abstract partial class IntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember> :
        IntermediateEventSignatureMemberBase<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>, TEventParent, TIntermediateEventParent>,
        IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TMethodMember :
            class,
            IIntermediateEventMethodMember
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
        where TIntermediateEventParent :
            TEventParent,
            IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
    {
        private ExtendedInstanceMemberFlags instanceFlags;
        private IntermediateEventManagementType generationType;
        private TMethodMember addMethod;
        private TMethodMember removeMethod;
        private TMethodMember raiseMethod;
        private bool emitRaiseMethod;
        /// <summary>
        /// Creates a new <see cref="IntermediateEventMember"/> instance
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateEventParent"/> which
        /// contains the <see cref="IntermediateEventMember{TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent, TMethodMember}"/></param>
        public IntermediateEventMember(TIntermediateEventParent parent)
            : base(parent)
        {
        }

        protected override IntermediateParameterMemberDictionary<TEvent, TIntermediateEvent, IEventParameterMember<TEvent, TEventParent>, IIntermediateEventParameterMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>> InitializeParameters()
        {
            return new ParameterDictionary(this);
        }

        /// <summary>
        /// Obtains the <typeparamref name="TMethodMember"/> for a given
        /// <paramref name="type"/> of event method.
        /// </summary>
        /// <param name="type">The <see cref="IntermediateEventMethodType"/> which designates
        /// which of the types of event method members the method needs to be.</param>
        /// <returns>A new <see cref="TMethodMember"/> structured for
        /// the <paramref name="type"/> provided.</returns>
        protected abstract TMethodMember GetMethodMember(IntermediateEventMethodType type);

        #region IIntermediateEventMember<TEvent,TIntermediateEvent,TEventParent,TIntermediateEventParent> Members
        /// <summary>
        /// Returns/sets the type of management the event receives</summary>
        /// <remarks>If set to <see cref="IntermediateEventManagementType.Automatic"/>, 
        /// <see cref="OnAddMethod"/>, <see cref="OnRemoveMethod"/>, and <see cref="OnRaiseMethod"/>
        /// will be locked, and immutable.</remarks>
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

        public TMethodMember OnAddMethod
        {
            get
            {
                if (this.addMethod == null)
                    this.addMethod = this.GetMethodMember(IntermediateEventMethodType.Add);
                return this.addMethod;
            }
        }

        public TMethodMember OnRemoveMethod
        {
            get
            {
                if (this.removeMethod == null)
                    this.removeMethod = this.GetMethodMember(IntermediateEventMethodType.Remove);
                return this.removeMethod;
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TMethodMember"/> which is responsible for adding a handler
        /// of the event.
        /// </summary>
        /// <remarks><para>Parameters are read-only when <see cref="SignatureSource"/>
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
                        this.raiseMethod = this.GetMethodMember(IntermediateEventMethodType.Fire);
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
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Abstract) == ExtendedInstanceMemberFlags.Abstract);
            }
            set
            {
                if (IsAbstract == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Abstract;
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Abstract;
            }
        }

        public bool IsVirtual
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Virtual) == ExtendedInstanceMemberFlags.Virtual);
            }
            set
            {
                if (IsVirtual == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Virtual;
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Virtual;
            }
        }

        public bool IsFinal
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Final) == ExtendedInstanceMemberFlags.Final);
            }
            set
            {
                if (IsFinal == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Final;
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Final;
            }
        }

        public bool IsOverride
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Override) == ExtendedInstanceMemberFlags.Override);
            }
            set
            {
                if (IsOverride == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Override;
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Override;
            }
        }

        #endregion

        #region IIntermediateInstanceMember Members

        public bool IsHideBySignature
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.HideBySignature) == ExtendedInstanceMemberFlags.HideBySignature);
            }
            set
            {
                if (value == IsHideBySignature)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.HideBySignature;
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.HideBySignature;
            }
        }

        public bool IsStatic
        {
            get
            {
                return ((this.instanceFlags & ExtendedInstanceMemberFlags.Static) == ExtendedInstanceMemberFlags.Static);
            }
            set
            {
                if (this.IsStatic == value)
                    return;
                if (value)
                    this.instanceFlags |= ExtendedInstanceMemberFlags.Static;
                else
                    this.instanceFlags ^= ExtendedInstanceMemberFlags.Static;
            }
        }

        #endregion

        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return ((InstanceMemberFlags)(this.InstanceFlags)); }
        }

        #endregion

        #region IExtendedInstanceMember Members

        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get { return this.instanceFlags; }
        }

        #endregion

        #region IIntermediateScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

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
    }
}
