using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
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
    internal class CompiledBinaryOperatorCoercionMemberBase<TCoercionParent> :
        BinaryOperatorCoercionMemberBase<TCoercionParent>,
        ICompiledBinaryOperatorCoercionMember
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        private IBinaryOperatorUniqueIdentifier uniqueIdentifier;
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
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParentIdentifier, TCoercionParent}"/>.
        /// </summary>
        public MethodInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        /// <summary>
        /// Obtains the <see cref="IType"/> that the 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParentIdentifier, TCoercionParent}"/>
        /// yields upon return.
        /// </summary>
        protected override IType OnGetReturnType()
        {
            if (!obtainedData)
                this.GetData();
            return returnType;
        }

        private ICompiledType Parent { get { return (ICompiledType) base.Parent; } }

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        /// <summary>
        /// Obtains the <see cref="DeclarationBase.Name"/> for the <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that contains 
        /// the name of the 
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</returns>
        protected override sealed string OnGetName()
        {
            switch (this.Operator)
            {
                case CoercibleBinaryOperators.Add:
                    return CliCommon.BinaryOperatorNames.Addition;
                case CoercibleBinaryOperators.Subtract:
                    return CliCommon.BinaryOperatorNames.Subtraction;
                case CoercibleBinaryOperators.Multiply:
                    return CliCommon.BinaryOperatorNames.Multiply;
                case CoercibleBinaryOperators.Divide:
                    return CliCommon.BinaryOperatorNames.Division;
                case CoercibleBinaryOperators.Modulus:
                    return CliCommon.BinaryOperatorNames.Modulus;
                case CoercibleBinaryOperators.BitwiseAnd:
                    return CliCommon.BinaryOperatorNames.BitwiseAnd;
                case CoercibleBinaryOperators.BitwiseOr:
                    return CliCommon.BinaryOperatorNames.BitwiseOr;
                case CoercibleBinaryOperators.ExclusiveOr:
                    return CliCommon.BinaryOperatorNames.ExclusiveOr;
                case CoercibleBinaryOperators.LeftShift:
                    return CliCommon.BinaryOperatorNames.LeftShift;
                case CoercibleBinaryOperators.RightShift:
                    return CliCommon.BinaryOperatorNames.RightShift;
                case CoercibleBinaryOperators.IsEqualTo:
                    return CliCommon.BinaryOperatorNames.Equality;
                case CoercibleBinaryOperators.IsNotEqualTo:
                    return CliCommon.BinaryOperatorNames.Inequality;
                case CoercibleBinaryOperators.LessThan:
                    return CliCommon.BinaryOperatorNames.LessThan;
                case CoercibleBinaryOperators.GreaterThan:
                    return CliCommon.BinaryOperatorNames.GreaterThan;
                case CoercibleBinaryOperators.LessThanOrEqualTo:
                    return CliCommon.BinaryOperatorNames.LessThanOrEqual;
                case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                    return CliCommon.BinaryOperatorNames.GreaterThanOrEqual;
                default:
                    return null;
            }
        }
        /* *
         * Obtains the pertinent information for the binary operation coercion member. 
         * */
        private void GetData()
        {
            Type[] _params = this.MemberInfo.GetParameters().OnAll(p => p.ParameterType).ToArray();
            if (_params.Length != 2)
                throw new InvalidOperationException("object in invalid state, member info not of proper kind.");
            var thisParent = this.Parent;
            Type pType = thisParent.UnderlyingSystemType;
            this.returnType = thisParent.Manager.ObtainTypeReference(this.MemberInfo.ReturnType);
            if (_params[0] == pType)
                if (_params[1] == pType)
                    containingSide = BinaryOpCoercionContainingSide.Both;
                else
                {
                    containingSide = BinaryOpCoercionContainingSide.LeftSide;
                    otherSide = thisParent.Manager.ObtainTypeReference(_params[1]);
                }
            else if (_params[1] == pType)
            {
                containingSide = BinaryOpCoercionContainingSide.RightSide;
                otherSide = thisParent.Manager.ObtainTypeReference(_params[0]);
            }
            else
                throw new InvalidOperationException("object in invalid state, binary operation doesn't work on containing type.");
            string s = MemberInfo.Name;
            //CLI operator overload names.
            switch (s)
            {
                case CliCommon.BinaryOperatorNames.Addition: //                '+'      - op_Addition
                    this._operator = CoercibleBinaryOperators.Add;
                    break;
                case CliCommon.BinaryOperatorNames.BitwiseAnd: //          '&' or 'And' - op_BitwiseAnd
                    this._operator = CoercibleBinaryOperators.BitwiseAnd;
                    break;
                case CliCommon.BinaryOperatorNames.BitwiseOr: //           '|' or "Or"  - op_BitwiseOr
                    this._operator = CoercibleBinaryOperators.BitwiseOr;
                    break;
                case CliCommon.BinaryOperatorNames.Division: //                '/'      - op_Division
                    this._operator = CoercibleBinaryOperators.Divide;
                    break;
                case CliCommon.BinaryOperatorNames.ExclusiveOr: //         '^' or 'XOr' - op_ExclusiveOr
                    this._operator = CoercibleBinaryOperators.ExclusiveOr;
                    break;
                case CliCommon.BinaryOperatorNames.GreaterThan: //             '>'      - op_GreaterThan
                    this._operator = CoercibleBinaryOperators.GreaterThan;
                    break;
                case CliCommon.BinaryOperatorNames.GreaterThanOrEqual: //  ">="         - op_GreaterThanOrEqual
                    this._operator = CoercibleBinaryOperators.GreaterThanOrEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.Equality: //            "==" or '='  - op_Equality
                    this._operator = CoercibleBinaryOperators.IsEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.Inequality: //          "!=" or "<>" - op_Inequality
                    this._operator = CoercibleBinaryOperators.IsNotEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.LeftShift: //               "<<"     - op_LeftShift
                    this._operator = CoercibleBinaryOperators.LeftShift;
                    break;
                case CliCommon.BinaryOperatorNames.LessThan: //                '<'      - op_LessThan
                    this._operator = CoercibleBinaryOperators.LessThan;
                    break;
                case CliCommon.BinaryOperatorNames.LessThanOrEqual: //         "<="     - op_LessThanOrEqual
                    this._operator = CoercibleBinaryOperators.LessThanOrEqualTo;
                    break;
                case CliCommon.BinaryOperatorNames.Modulus: //             '%' or "Mod" - op_Modulus
                    this._operator = CoercibleBinaryOperators.Modulus;
                    break;
                case CliCommon.BinaryOperatorNames.Multiply: //                '*'      - op_Multiply
                    this._operator = CoercibleBinaryOperators.Multiply;
                    break;
                case CliCommon.BinaryOperatorNames.RightShift: //             ">>"      - op_RightShift
                    this._operator = CoercibleBinaryOperators.RightShift;
                    break;
                case CliCommon.BinaryOperatorNames.Subtraction: //             '-'      - op_Subtraction
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
        /// is overridden by the <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParentIdentifier, TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="CoercibleBinaryOperators"/> value
        /// indicating which binary operator is coerced by the 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParentIdentifier, TCoercionParent}"/>.</returns>
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
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParentIdentifier, TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="AccessLevelModifiers"/> value representing
        /// the accessibility of the current 
        /// <see cref="CompiledBinaryOperatorCoercionMemberBase{TCoercionParentIdentifier, TCoercionParent}"/>.</returns>
        protected override AccessLevelModifiers OnGetAccessLevel()
        {
            return this.MemberInfo.GetAccessModifiers();
        }

        public override IBinaryOperatorUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                {
                    if (!this.obtainedData)
                        this.GetData();
                    this.uniqueIdentifier = AstIdentifier.BinaryOperator(this._operator, this.containingSide, this.otherSide);
                }
                return this.uniqueIdentifier;
            }
        }
    }
}
