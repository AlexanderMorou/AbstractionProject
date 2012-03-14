using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CompiledConstructorMemberBase<TCtor, TCtorParent> :
        ConstructorMemberBase<TCtor, TCtorParent>,
        ICompiledCtorMember
        where TCtor :
            IConstructorMember<TCtor, TCtorParent>
        where TCtorParent :
            ICreatableParent<TCtor, TCtorParent>
    {
        /// <summary>
        /// Data member for <see cref="MemberInfo"/>.
        /// </summary>
        private ConstructorInfo ctorInfo;
        /// <summary>
        /// Data member for <see cref="DeclarationBase.UniqueIdentifier"/>
        /// </summary>
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private bool lastIsParams;
        public CompiledConstructorMemberBase(ConstructorInfo ctorInfo, TCtorParent parent)
            : base(parent)
        {
            this.ctorInfo = ctorInfo;
            this.lastIsParams = ctorInfo.LastParameterIsParams();
        }

        protected override IParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TCtorParent>> InitializeParameters()
        {
            return new ParametersDictionary(this, from paramInfo in this.MemberInfo.GetParameters()
                                                  select new ParameterMember(paramInfo, this, ((ICompiledType) this.Parent).Manager));
        }

        private class ParameterMember :
            CompiledParameterMemberBase<TCtor>,
            IConstructorParameterMember<TCtor, TCtorParent>
        {
            public ParameterMember(ParameterInfo paramInfo, CompiledConstructorMemberBase<TCtor, TCtorParent> parent, ICliManager manager)
                : base(paramInfo, (TCtor)(object)parent, manager)
            {
            }

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return base.Parent; }
            }

            #endregion
        }

        private class ParametersDictionary :
            LockedParameterMembersBase<TCtor, IConstructorParameterMember<TCtor, TCtorParent>>
        {
            internal ParametersDictionary(CompiledConstructorMemberBase<TCtor, TCtorParent> parent, IEnumerable<IConstructorParameterMember<TCtor, TCtorParent>> parameters)
                : base((TCtor)(object)parent, parameters)
            {
            }
        }

        #region ICompiledCtorMember Members

        /// <summary>
        /// Returns the <see cref="ConstructorInfo"/> associated to the <see cref="CompiledConstructorMemberBase{TCtor, TCtorParent}"/>.
        /// </summary>
        public ConstructorInfo MemberInfo
        {
            get { return this.ctorInfo; }
        }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = ctorInfo.GetUniqueIdentifier(((ICompiledType) this.Parent).Manager);
                return this.uniqueIdentifier;
            }
        }

        protected override AccessLevelModifiers AccessLevelImpl
        {
            get
            {
                return this.MemberInfo.GetAccessModifiers();
            }
        }

        protected override bool LastIsParamsImpl
        {
            get {
                return this.lastIsParams;
            }
        }

    }
}