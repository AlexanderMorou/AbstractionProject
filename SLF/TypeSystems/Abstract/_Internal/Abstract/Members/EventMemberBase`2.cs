using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract class EventMemberBase<TEvent, TEventParent> :
        EventSignatureMemberBase<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventMember<TEvent, TEventParent>,
        IScopedDeclaration
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
        /// <summary>
        /// Creates a new <see cref="EventMemberBase{TEvent, TEventParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The parent of the 
        /// <see cref="EventMemberBase{TEvent, TEventParent}"/>.</param>
        public EventMemberBase(TEventParent parent)
            : base(parent)
        {
        }

        #region IEventMember Members

        /// <summary>
        /// Returns the <see cref="IMethodMember"/>
        /// which is responsible for adding a handler
        /// of the event.
        /// </summary>
        public IMethodMember OnAddMethod
        {
            get {
                return this.OnGetOnAddMethod();
            }
        }

        /// <summary>
        /// Returns the <see cref="IMethodMember"/>
        /// which is responsible for removing a handler
        /// of the event.
        /// </summary>
        public IMethodMember OnRemoveMethod
        {
            get { return this.OnGetOnRemoveMethod(); }
        }

        /// <summary>
        /// Returns the <see cref="IMethodMember"/>
        /// which is responsible for raising the event.
        /// </summary>
        /// <remarks><para>Can be null; not all events use
        /// this functionality.</para>
        /// <para>One such example is C&#9839;, events do not 
        /// support nor do they allow such declaration 
        /// or usage.</para></remarks>
        public IMethodMember OnRaiseMethod
        {
            get { return this.OnGetOnRaiseMethod(); }
        }

        /// <summary>
        /// Implementation for <see cref="get_OnRaiseMethod()"/>.
        /// </summary>
        /// <returns>A <see cref="IMethodMember"/> instance.</returns>
        protected abstract IMethodMember OnGetOnRaiseMethod();
        /// <summary>
        /// Implementation for <see cref="get_OnAddMethod()"/>.
        /// </summary>
        /// <returns>A <see cref="IMethodMember"/> instance.</returns>
        protected abstract IMethodMember OnGetOnAddMethod();

        /// <summary>
        /// Implementation for <see cref="get_OnRemoveMethod()"/>.
        /// </summary>
        /// <returns>A <see cref="IMethodMember"/> instance.</returns>
        protected abstract IMethodMember OnGetOnRemoveMethod();

        public abstract bool CanRaise { get; }

        #endregion

        #region IExtendedInstanceMember Members

        /// <summary>
        /// Returns the <see cref="ExtendedInstanceMemberFlags"/> that determine how the
        /// <see cref="EventMemberBase{TEvent, TEventParent}"/> is shown in its 
        /// scope and inheritors' scopes.
        /// </summary>
        public ExtendedInstanceMemberFlags InstanceFlags
        {
            get
            {
                ExtendedInstanceMemberFlags imfs = ExtendedInstanceMemberFlags.None;
                if (this.IsStatic)
                    imfs |= ExtendedInstanceMemberFlags.Static;
                if (this.IsVirtual)
                    imfs |= ExtendedInstanceMemberFlags.Virtual;
                if (this.IsOverride)
                    imfs |= ExtendedInstanceMemberFlags.Override;
                if (this.IsFinal)
                    imfs |= ExtendedInstanceMemberFlags.Final;
                if (this.IsHideBySignature)
                    imfs |= ExtendedInstanceMemberFlags.HideBySignature;
                if (this.IsAbstract)
                    imfs |= ExtendedInstanceMemberFlags.Abstract;
                return imfs;
            }
        }

        /// <summary>
        /// Returns whether the <see cref="EventMemberBase{TEvent, TEventParent}"/> is
        /// static.
        /// </summary>
        public abstract bool IsStatic { get; }

        /// <summary>
        /// Returns whether the
        /// <see cref="EventMemberBase{TEvent, TEventParent}"/>
        /// is abstract (must be implemented, or is
        /// not yet implemented).
        /// </summary>
        public abstract bool IsAbstract { get; }

        /// <summary>
        /// Returns whether the <see cref="EventMemberBase{TEvent, TEventParent}"/> is
        /// virtual (can be overridden).
        /// </summary>
        public abstract bool IsVirtual { get; }

        /// <summary>
        /// Returns whether the <see cref="EventMemberBase{TEvent, TEventParent}"/>
        /// hides the original definition completely.
        /// </summary>
        public abstract bool IsHideBySignature { get; }

        /// <summary>
        /// Returns whether the <see cref="EventMemberBase{TEvent, TEventParent}"/>
        /// finalizes the member removing the overrideable 
        /// status.
        /// </summary>
        public abstract bool IsFinal { get; }

        /// <summary>
        /// Returns whether the <see cref="EventMemberBase{TEvent, TEventParent}"/> 
        /// is an override of a virtual member.
        /// </summary>
        public abstract bool IsOverride { get; }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.AccessLevelImpl; }
        }

        #endregion

        protected abstract AccessLevelModifiers AccessLevelImpl { get; }

        #region IInstanceMember Members

        InstanceMemberFlags IInstanceMember.InstanceFlags
        {
            get { return (InstanceMemberFlags)this.InstanceFlags; }
        }

        #endregion
    }
}
