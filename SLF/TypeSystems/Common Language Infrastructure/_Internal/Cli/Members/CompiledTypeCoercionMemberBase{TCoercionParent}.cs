using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CompiledTypeCoercionMemberBase<TCoercionParent> :
        TypeCoercionMemberBase<TCoercionParent>,
        ITypeCoercionMember<TCoercionParent>,
        ICompiledTypeCoercionMember
        where TCoercionParent :
            ICoercibleType<ITypeCoercionMember<TCoercionParent>, TCoercionParent>
    {
        private IType coercionType;
        private TypeConversionDirection direction;
        public CompiledTypeCoercionMemberBase(MethodInfo memberInfo, TCoercionParent parent)
            : base(parent)
        {
            this.MemberInfo = memberInfo;
            var declaringType = MemberInfo.DeclaringType; 
            var firstParam = this.MemberInfo.GetParameters().First();
            if (declaringType == this.MemberInfo.ReturnType)
            {
                coercionType = firstParam.ParameterType.GetTypeReference();
                direction = TypeConversionDirection.ToContainingType;
            }
            else
            {
                coercionType = MemberInfo.ReturnType.GetTypeReference();
                direction = TypeConversionDirection.FromContainingType;
            }
        }

        public override TypeConversionRequirement Requirement
        {
            get
            {
                if (this.MemberInfo != null &&
                   !string.IsNullOrEmpty(this.MemberInfo.Name) &&
                    this.MemberInfo.Name.Length > 3)
                    switch (this.MemberInfo.Name.Substring(3))
                    {
                        case "Explicit":
                            return TypeConversionRequirement.Explicit;
                        case "Implicit":
                            return TypeConversionRequirement.Implicit;
                    }
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
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

        public override string UniqueIdentifier
        {
            get { return this.MemberInfo.GetUniqueIdentifier(); }
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
