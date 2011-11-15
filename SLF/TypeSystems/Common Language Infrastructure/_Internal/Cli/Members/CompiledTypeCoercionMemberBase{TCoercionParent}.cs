using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CompiledTypeCoercionMemberBase<TCoercionParentIdentifier, TCoercionParent> :
        TypeCoercionMemberBase<TCoercionParentIdentifier, TCoercionParent>,
        ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>,
        ICompiledTypeCoercionMember
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, TCoercionParentIdentifier, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
    {
        private ITypeCoercionUniqueIdentifier uniqueIdentifier;
        private IType coercionType;
        private TypeConversionDirection direction;
        TypeConversionRequirement? requirement;
        public CompiledTypeCoercionMemberBase(MethodInfo memberInfo, TCoercionParent parent)
            : base(parent)
        {
            this.MemberInfo = memberInfo;
            var declaringType = MemberInfo.DeclaringType; 
            var firstParam = this.MemberInfo.GetParameters().First();
            if (declaringType == this.MemberInfo.ReturnType)
            {
                this.coercionType = firstParam.ParameterType.GetTypeReference();
                this.direction = TypeConversionDirection.ToContainingType;
            }
            else
            {
                this.coercionType = MemberInfo.ReturnType.GetTypeReference();
                this.direction = TypeConversionDirection.FromContainingType;
            }
            uniqueIdentifier = AstIdentifier.TypeOperator(Requirement, direction, coercionType);
        }

        public override TypeConversionRequirement Requirement
        {
            get
            {
                if (this.requirement == null)
                    this.requirement = this.MemberInfo.GetTypeCoercionRequirement();
                return this.requirement.Value;
            }
        }

        public override TypeConversionDirection Direction
        {
            get { return this.direction; }
        }

        public override IType CoercionType
        {
            get {
                return this.coercionType;
            }
        }

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return this.MemberInfo.GetAccessModifiers();
        }

        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }

        public override ITypeCoercionUniqueIdentifier UniqueIdentifier
        {
            get { return this.uniqueIdentifier; }
        }

        #region ICompiledTypeCoercionMember Members

        public MethodInfo MemberInfo { get; private set; }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        public override void Dispose()
        {
            this.coercionType = null;
            this.MemberInfo = null;
            base.Dispose();
        }
    }
}
