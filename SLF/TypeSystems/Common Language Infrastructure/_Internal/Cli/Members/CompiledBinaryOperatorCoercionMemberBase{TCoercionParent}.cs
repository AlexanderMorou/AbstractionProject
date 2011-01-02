using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
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
    internal class CompiledBinaryOperatorCoercionMemberBase<TCoercionParent> :
        BinaryOperatorCoercionMemberBase<TCoercionParent>,
        ICompiledBinaryOperatorCoercionMember
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        private MethodInfo memberInfo;
        private IType returnType = null;
        /// <summary>
        /// Data member for <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}.ContainingSide"/>
        /// </summary>
        private BinaryOpCoercionContainingSide containingSide;

        /// <summary>
        /// Data member for <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}.Operator"/>
        /// </summary>
        private CoercibleBinaryOperators _operator;

        /// <summary>
        /// Data member for <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}.OtherSide"/>.
        /// </summary>
        private IType otherSide = null;
        /// <summary>
        /// Data member determining whether <see cref="_operator"/>,
        /// <see cref="containingSide"/>, and <see cref="returnType"/>
        /// need filled.
        /// </summary>
        private bool obtainedData = false;

        public CompiledBinaryOperatorCoercionMemberBase(MethodInfo memberInfo, TCoercionParent parent)
            : base(parent)
        {
            this.memberInfo = memberInfo;
        }

        /// <summary>
        /// Returns the <see cref="System.Reflection.MethodInfo"/> associated to the 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        public MethodInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        /// <summary>
        /// Obtains the <see cref="IType"/> that the 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParent}"/>
        /// yields upon return.
        /// </summary>
        protected override IType OnGetReturnType()
        {
            if (!obtainedData)
                this.GetData();
            return returnType;
        }

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        /// <summary>
        /// Obtains the pertinent information for the 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        private void GetData()
        {
            Type[] _params = this.MemberInfo.GetParameters().OnAll(p => p.ParameterType).ToArray();
            if (_params.Length != 2)
                throw new InvalidOperationException("object in invalid state, member info not of proper kind.");
            Type pType = ((ICompiledType)(this.Parent)).UnderlyingSystemType;
            this.returnType = this.MemberInfo.ReturnType.GetTypeReference();
            if (_params[0] == pType)
                if (_params[1] == pType)
                    containingSide = BinaryOpCoercionContainingSide.Both;
                else
                {
                    containingSide = BinaryOpCoercionContainingSide.LeftSide;
                    //Ensure to disambiguify the type-parameters, if any.
                    otherSide = _params[1].GetTypeReference();
                }
            else if (_params[1] == pType)
            {
                containingSide = BinaryOpCoercionContainingSide.RightSide;
                otherSide = _params[0].GetTypeReference();
            }
            else
                throw new InvalidOperationException("object in invalid state, binary operation doesn't work on containing type.");
            string s = MemberInfo.Name;
            //CLI operator overload names.
            switch (s)
            {
                case "op_Addition": // '+'
                    this._operator = CoercibleBinaryOperators.Add;
                    break;
                case "op_BitwiseAnd": // '-'
                    this._operator = CoercibleBinaryOperators.BitwiseAnd;
                    break;
                case "op_BitwiseOr": // '|' or "Or"
                    this._operator = CoercibleBinaryOperators.BitwiseOr;
                    break;
                case "op_Division": // '/'
                    this._operator = CoercibleBinaryOperators.Divide;
                    break;
                case "op_ExclusiveOr": // '^'
                    this._operator = CoercibleBinaryOperators.ExclusiveOr;
                    break;
                case "op_GreaterThan": // '>'
                    this._operator = CoercibleBinaryOperators.GreaterThan;
                    break;
                case "op_GreaterThanOrEqual": // ">="
                    this._operator = CoercibleBinaryOperators.GreaterThanOrEqualTo;
                    break;
                case "op_Equality": // "==" or '='
                    this._operator = CoercibleBinaryOperators.IsEqualTo;
                    break;
                case "op_Inequality": // "!=" or "<>"
                    this._operator = CoercibleBinaryOperators.IsNotEqualTo;
                    break;
                case "op_LeftShift": // "<<"
                    this._operator = CoercibleBinaryOperators.LeftShift;
                    break;
                case "op_LessThan": // '<'
                    this._operator = CoercibleBinaryOperators.LessThan;
                    break;
                case "op_LessThanOrEqual": // "<="
                    this._operator = CoercibleBinaryOperators.LessThanOrEqualTo;
                    break;
                case "op_Modulus": // '%' or "Mod"
                    this._operator = CoercibleBinaryOperators.Modulus;
                    break;
                case "op_Multiply": // '*' 
                    this._operator = CoercibleBinaryOperators.Multiply;
                    break;
                case "op_RightShift": // ">>"
                    this._operator = CoercibleBinaryOperators.RightShift;
                    break;
                case "op_Subtraction": // '-' 
                    this._operator = CoercibleBinaryOperators.Subtract;
                    break;
                default:
                    throw new InvalidOperationException(string.Format("object in invalid state, binary operation ({0}) not supported.", s.Substring(3)));
            }
            obtainedData = true;
        }

        /// <summary>
        /// Obtains the <see cref="BinaryOpCoercionContainingSide"/> which
        /// denotes which side the type that contains the
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>
        /// is on.
        /// </summary>
        /// <returns>A <see cref="BinaryOpCoercionContainingSide"/> value
        /// indicating which side (if not both) the 
        /// <typeparamref name="TCoercionParent"/> is on.</returns>
        protected override BinaryOpCoercionContainingSide OnGetContainingSide()
        {
            if (!this.obtainedData)
                this.GetData();
            return this.containingSide;
        }

        /// <summary>
        /// Obtains the <see cref="CoercibleBinaryOperators"/> which
        /// is overridden by the <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="CoercibleBinaryOperators"/> value
        /// indicating which binary operator is coerced by the 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</returns>
        protected override CoercibleBinaryOperators OnGetOperator()
        {
            if (!this.obtainedData)
                this.GetData();
            return this._operator;
        }

        /// <summary>
        /// Obtains the alternate side's <see cref="IType"/> which
        /// is the <see cref="MemberBase{TParent}.Parent"/> if
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}.ContainingSide"/> is 
        /// <see cref="BinaryOpCoercionContainingSide.Both"/>
        /// </summary>
        /// <returns>An <see cref="IType"/> which
        /// relates to the alternate side's <see cref="IType"/> which
        /// is the <see cref="MemberBase{TParent}.Parent"/> if
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}.ContainingSide"/> is 
        /// <see cref="BinaryOpCoercionContainingSide.Both"/></returns>
        protected override IType OnGetOtherSide()
        {
            if (!this.obtainedData)
                this.GetData();
            return this.otherSide;
        }

        /// <summary>
        /// Obtains the access level of the current 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="AccessLevelModifiers"/> value representing
        /// the accessibility of the current 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</returns>
        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return this.MemberInfo.GetAccessModifiers();
        }

        public override string UniqueIdentifier
        {
            get {
                StringBuilder sb = new StringBuilder();
                switch (this.ContainingSide)
                {
                    case BinaryOpCoercionContainingSide.LeftSide:
                        sb.Append(this.Parent.Name);
                        sb.Append(this.Name);
                        sb.Append(this.OtherSide.Name);
                        break;
                    case BinaryOpCoercionContainingSide.RightSide:
                        sb.Append(this.OtherSide.Name);
                        sb.Append(this.Name);
                        sb.Append(this.Parent.Name);
                        break;
                    case BinaryOpCoercionContainingSide.Both:
                        sb.Append(this.Parent.Name);
                        sb.Append(this.Name);
                        sb.Append(this.Parent.Name);
                        break;
                    default:
                        return null;
                }
                return sb.ToString();
            }
        }
    }
}
