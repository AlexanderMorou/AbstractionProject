using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
            ICreatableType<TCtor, TCtorParent>
    {
        /// <summary>
        /// Data member for <see cref="MemberInfo"/>.
        /// </summary>
        private ConstructorInfo ctorInfo;
        /// <summary>
        /// Data member for <see cref="DeclarationBase.UniqueIdentifier"/>
        /// </summary>
        private string uniqueIdentifier;
        private bool lastIsParams;
        public CompiledConstructorMemberBase(ConstructorInfo ctorInfo, TCtorParent parent)
            : base(parent)
        {
            this.uniqueIdentifier = ctorInfo.GetUniqueIdentifier();
            this.ctorInfo = ctorInfo;
            this.lastIsParams = ctorInfo.LastParameterIsParams();
        }

        protected override IParameterMemberDictionary<TCtor, IConstructorParameterMember<TCtor, TCtorParent>> InitializeParameters()
        {
            return new ParametersDictionary(this, this.MemberInfo.GetParameters().OnAll(paramInfo => (IConstructorParameterMember<TCtor, TCtorParent>)new ParameterMember(paramInfo, this)));
        }

        private class ParameterMember :
            CompiledParameterMemberBase<TCtor>,
            IConstructorParameterMember<TCtor, TCtorParent>
        {
            public ParameterMember(ParameterInfo paramInfo, CompiledConstructorMemberBase<TCtor, TCtorParent> parent)
                : base(paramInfo, (TCtor)(object)parent)
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

        public override string UniqueIdentifier
        {
            get
            {
                return uniqueIdentifier;
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