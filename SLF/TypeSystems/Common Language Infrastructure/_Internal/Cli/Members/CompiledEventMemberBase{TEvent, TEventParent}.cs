using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICompiledEventMember"/> and
    /// fully implements the abstract <see cref="EventMemberBase{TEvent, TEventParent}"/>.
    /// </summary>
    /// <typeparam name="TMethod">The </typeparam>
    /// <typeparam name="TEvent">The type of event used in the current
    /// implementation.</typeparam>
    /// <typeparam name="TEventParent">The type of 
    /// <see cref="IEventParent{TEvent, TEventParent}"/>
    /// used in the current implementation.</typeparam>
    internal abstract partial class CompiledEventMemberBase<TMethod, TEvent, TEventParentIdentifier, TEventParent> :
        EventMemberBase<TEvent, TEventParent>,
        ICompiledEventMember
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
        private EventSignatureSource? source;
        private ExtendedInstanceMemberFlags memberFlags;
        private bool retrFlags;
        private TMethod raiseMethod;
        private TMethod addMethod;
        private TMethod removeMethod;
        /// <summary>
        /// Data member for <see cref="MemberInfo"/>.
        /// </summary>
        private EventInfo memberInfo;

        public CompiledEventMemberBase(EventInfo memberInfo, TEventParent parent)
            : base(parent)
        {
            this.memberInfo = memberInfo;
        }

        /// <summary>
        /// Returns the <typeparamref name="TMethod"/>
        /// which is responsible for adding a handler
        /// of the event.
        /// </summary>
        public new TMethod OnAddMethod
        {
            get
            {
                return ((TMethod)(base.OnAddMethod));
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TMethod"/>
        /// which is responsible for removing a handler
        /// of the event.
        /// </summary>
        public new TMethod OnRemoveMethod
        {
            get
            {
                return ((TMethod)(base.OnRemoveMethod));
            }
        }

        /// <summary>
        /// Returns the <typeparamref name="TMethod"/>
        /// which is responsible for raising the event.
        /// </summary>
        /// <remarks><para>Can be null; not all events use
        /// this functionality.</para>
        /// <para>One such example is C&#9839;, events do not 
        /// support nor do they allow such declaration 
        /// or usage.</para></remarks>
        public new TMethod OnRaiseMethod
        {
            get
            {
                return ((TMethod)(base.OnRaiseMethod));
            }
        }

        #region ICompiledEventMember Members

        /// <summary>
        /// Returns the <see cref="System.Reflection.EventInfo"/> 
        /// associated to the <see cref="CompiledEventMemberBase{TMethod, TEvent, TEventParent}"/>.
        /// </summary>
        public EventInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        /// <summary>
        /// Obtains a <typeparamref name="TMethod"/> for the
        /// current <see cref="CompiledEventMemberBase{TMethod, TEvent, TEventParent}"/>
        /// with the <paramref name="memberInfo"/> provided.
        /// </summary>
        /// <param name="memberInfo">The <see cref="MethodInfo"/> of the 
        /// method.</param>
        /// <returns>An <typeparamref name="TMethod"/>
        /// </returns>
        /// <remarks>Used for <see cref="IEventMember.OnAddMethod"/>,
        /// <see cref="IEventMember.OnRemoveMethod"/> and 
        /// <see cref="IEventMember.OnRaiseMethod"/>.</remarks>
        protected abstract TMethod OnGetMethod(MethodInfo memberInfo);

        protected override IMethodMember OnGetOnRaiseMethod()
        {
            if (this.raiseMethod == null)
                this.raiseMethod = this.OnGetMethod(this.memberInfo.GetRaiseMethod(true));
            return this.raiseMethod;
        }

        public override bool IsStatic
        {
            get {
                FlagCheck();
                return (memberFlags & ExtendedInstanceMemberFlags.Static) == ExtendedInstanceMemberFlags.Static;
            }
        }

        private void FlagCheck()
        {
            if (!retrFlags)
            {
                MethodInfo b = memberInfo.GetRaiseMethod(true);
                if (b == null)
                    b = memberInfo.GetAddMethod(true);
                if (b == null)
                    b = memberInfo.GetRemoveMethod(true);
                if (b == null)
                    return;
                memberFlags = ExtendedInstanceMemberFlags.None;
                if (b.IsStatic)
                    memberFlags |= ExtendedInstanceMemberFlags.Static;
                if (b.IsVirtual)
                    memberFlags |= ExtendedInstanceMemberFlags.Virtual;
                if (b.GetBaseDefinition() != b)
                    memberFlags |= ExtendedInstanceMemberFlags.Override;
                if (b.IsFinal)
                    memberFlags |= ExtendedInstanceMemberFlags.Final;
                if (b.IsHideBySig)
                    memberFlags |= ExtendedInstanceMemberFlags.HideBySignature;
                if (b.IsAbstract)
                    memberFlags |= ExtendedInstanceMemberFlags.Abstract;

                retrFlags = true;
            }
        }

        public override bool IsAbstract
        {
            get
            {
                FlagCheck();
                return (memberFlags & ExtendedInstanceMemberFlags.Abstract) == ExtendedInstanceMemberFlags.Abstract;
            }
        }

        public override bool IsVirtual
        {
            get
            {
                FlagCheck();
                return (memberFlags & ExtendedInstanceMemberFlags.Virtual) == ExtendedInstanceMemberFlags.Virtual;
            }
        }

        public override bool IsHideBySignature
        {
            get
            {
                FlagCheck();
                return (memberFlags & ExtendedInstanceMemberFlags.HideBySignature) == ExtendedInstanceMemberFlags.HideBySignature;
            }
        }

        public override bool IsFinal
        {
            get
            {
                FlagCheck();
                return (memberFlags & ExtendedInstanceMemberFlags.Final) == ExtendedInstanceMemberFlags.Final;
            }
        }

        public override bool IsOverride
        {
            get
            {
                FlagCheck();
                return (memberFlags & ExtendedInstanceMemberFlags.Override) == ExtendedInstanceMemberFlags.Override;
            }
        }

        protected override IMethodMember OnGetOnAddMethod()
        {
            if (this.addMethod == null)
                this.addMethod = this.OnGetMethod(this.memberInfo.GetAddMethod(true));
            return this.addMethod;
        }

        protected override IMethodMember OnGetOnRemoveMethod()
        {
            if (this.removeMethod == null)
                this.removeMethod = this.OnGetMethod(this.memberInfo.GetRemoveMethod(true));
            return this.removeMethod;
        }

        protected override EventSignatureSource SignatureSourceImpl
        {
            get {
                if (this.source == null)
                {
                    var signatureType = this.SignatureType;
                    if (signatureType.CustomAttributes.Contains(CommonTypeRefs.CompilerGeneratedAttribute))
                        source = EventSignatureSource.Declared;
                    else
                        source = EventSignatureSource.Delegate;
                }
                return this.source.Value;
            }
        }


        protected override IParameterMemberDictionary<TEvent, IEventParameterMember<TEvent, TEventParent>> InitializeParameters()
        {
            var delegateType = this.memberInfo.EventHandlerType.GetTypeReference<IDelegateUniqueIdentifier, IDelegateType>();
            return new ParameterMemberDictionary(this, from delegateParameter in delegateType.Parameters.Values
                                                       select new ParameterMember(delegateParameter, this));
        }

        protected override bool LastIsParamsImpl
        {
            get { return SignatureType.LastIsParams; }
        }

        protected override string OnGetName()
        {
            return this.memberInfo.Name;
        }

        public override bool CanRaise
        {
            get { return this.OnRaiseMethod != null; }
        }

        protected override AccessLevelModifiers AccessLevelImpl
        {
            get { return this.MemberInfo.GetAccessModifiers(); }
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
            get { return this.memberInfo.GetUniqueIdentifier(); }
        }
    }
}
