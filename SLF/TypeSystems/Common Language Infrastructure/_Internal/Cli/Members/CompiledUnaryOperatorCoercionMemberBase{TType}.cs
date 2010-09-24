using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
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
            ICoercibleType<IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        /// <summary>
        /// Data member for <see cref="MemberInfo"/>.
        /// </summary>
        private MethodInfo memberInfo;
        /// <summary>
        /// Data member for <see cref="Operator"/>
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
            this.resultedType = memberInfo.ReturnType.GetTypeReference();
            switch (memberInfo.Name)
            {
                case "op_UnaryPlus":
                    this._operator = CoercibleUnaryOperators.Plus;
                    break;
                case "op_UnaryNegation":
                    this._operator = CoercibleUnaryOperators.Negative;
                    break;
                case "op_False":
                    this._operator = CoercibleUnaryOperators.EvaluatesToFalse;
                    break;
                case "op_True":
                    this._operator = CoercibleUnaryOperators.EvaluatesToTrue;
                    break;
                case "op_LogicalNot":
                    this._operator = CoercibleUnaryOperators.LogicalInvert;
                    break;
                case "op_OnesComplement":
                    this._operator = CoercibleUnaryOperators.Complement;
                    break;
                default:
                    throw new InvalidOperationException(string.Format("object in invalid state, unary operation ({0}) not supported.", memberInfo.Name.Substring(3)));
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

        public override string UniqueIdentifier
        {
            get {
                return string.Format("{0} {1}({2})", this.ResultedType, this.Operator, this.Parent);
            }
        }
    }
}
