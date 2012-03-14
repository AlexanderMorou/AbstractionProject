using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CompiledUnaryOperatorCoercionMemberBase<TCoercionParent> :
        UnaryOperatorCoercionMemberBase<TCoercionParent>,
        ICompiledUnaryOperatorCoercionMember
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        private IUnaryOperatorUniqueIdentifier uniqueIdentifier;
        /// <summary>
        /// Data member for <see cref="MemberInfo"/>.
        /// </summary>
        private MethodInfo memberInfo;
        /// <summary>
        /// Data member for <see cref="OnGetOperator()"/>
        /// </summary>
        private CoercibleUnaryOperators _operator;
        /// <summary>
        /// Data member for <see cref="ResultedType"/>.
        /// </summary>
        private IType resultedType;

        public CompiledUnaryOperatorCoercionMemberBase(MethodInfo memberInfo, TCoercionParent parent)
            : base(parent)
        {
            this.memberInfo = memberInfo;
            this.resultedType = this.Parent.Manager.ObtainTypeReference(memberInfo.ReturnType);
            switch (memberInfo.Name)
            {
                case CliCommon.UnaryOperatorNames.Plus:
                    this._operator = CoercibleUnaryOperators.Plus;
                    break;
                case CliCommon.UnaryOperatorNames.Negation:
                    this._operator = CoercibleUnaryOperators.Negation;
                    break;
                case CliCommon.UnaryOperatorNames.False:
                    this._operator = CoercibleUnaryOperators.EvaluatesToFalse;
                    break;
                case CliCommon.UnaryOperatorNames.True:
                    this._operator = CoercibleUnaryOperators.EvaluatesToTrue;
                    break;
                case CliCommon.UnaryOperatorNames.LogicalNot:
                    this._operator = CoercibleUnaryOperators.LogicalInvert;
                    break;
                case CliCommon.UnaryOperatorNames.OnesComplement:
                    this._operator = CoercibleUnaryOperators.Complement;
                    break;
                case CliCommon.UnaryOperatorNames.Increment:
                    this._operator = CoercibleUnaryOperators.Increment;
                    break;
                case CliCommon.UnaryOperatorNames.Decrement:
                    this._operator = CoercibleUnaryOperators.Decrement;
                    break;
                default:
                    throw new InvalidOperationException(string.Format("object in invalid state, unary operation ({0}) not supported.", memberInfo.Name.Substring(3)));
            }
        }

        private new ICompiledType Parent
        {
            get
            {
                return (ICompiledType) base.Parent;
            }
        }

        #region ICompiledUnaryOperatorCoercionMember Members

        public MethodInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        #endregion

        #region ICoercionMember Members

        ICoercibleType ICoercionMember.Parent
        {
            get { return base.Parent; }
        }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        #region IUnaryOperatorCoercionMember Members

        public override IType ResultedType
        {
            get { return this.resultedType; }
        }

        #endregion

        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return this.MemberInfo.GetAccessModifiers();
        }

        protected override CoercibleUnaryOperators OnGetOperator()
        {
            return this._operator;
        }

        public override IUnaryOperatorUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.UnaryOperator(this.Operator);
                return this.uniqueIdentifier;
            }
        }


        protected override string OnGetName()
        {
            string p = null;
            switch (this.Operator)
            {
                case CoercibleUnaryOperators.Plus:
                    p = CliCommon.UnaryOperatorNames.Plus;
                    break;
                case CoercibleUnaryOperators.Negation:
                    p = CliCommon.UnaryOperatorNames.Negation;
                    break;
                case CoercibleUnaryOperators.EvaluatesToFalse:
                    p = CliCommon.UnaryOperatorNames.False;
                    break;
                case CoercibleUnaryOperators.EvaluatesToTrue:
                    p = CliCommon.UnaryOperatorNames.True;
                    break;
                case CoercibleUnaryOperators.LogicalInvert:
                    p = CliCommon.UnaryOperatorNames.LogicalNot;
                    break;
                case CoercibleUnaryOperators.Complement:
                    p = CliCommon.UnaryOperatorNames.OnesComplement;
                    break;
                case CoercibleUnaryOperators.Increment:
                    p = CliCommon.UnaryOperatorNames.Increment;
                    break;
                case CoercibleUnaryOperators.Decrement:
                    p = CliCommon.UnaryOperatorNames.Decrement;
                    break;
                default:
                    return null;
            }

            return string.Format("unary operator {0}", p);
        }
    }
}
