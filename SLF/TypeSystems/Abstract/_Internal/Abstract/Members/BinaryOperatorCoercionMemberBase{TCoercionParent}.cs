using System;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract class BinaryOperatorCoercionMemberBase<TCoercionParent> :
        MemberBase<IBinaryOperatorUniqueIdentifier, TCoercionParent>,
        IBinaryOperatorCoercionMember<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {

        /// <summary>
        /// Creates a new <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/> instance
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TCoercionParent"/>
        /// which contains the 
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</param>
        public BinaryOperatorCoercionMemberBase(TCoercionParent parent)
            : base(parent)
        {
        }

        #region IBinaryOperatorCoercionMember Members

        /// <summary>
        /// Returns the <see cref="CoercibleBinaryOperators"/> coerced
        /// by the <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        public CoercibleBinaryOperators Operator
        {
            get { return this.OnGetOperator(); }
        }

        /// <summary>
        /// Returns which side the required self reference
        /// the <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>'s
        /// parent is on.
        /// </summary>
        public BinaryOpCoercionContainingSide ContainingSide
        {
            get { return this.OnGetContainingSide(); }
        }

        /// <summary>
        /// Returns the type of the other side of the expression
        /// used when performing the coercion.
        /// </summary>
        public IType OtherSide
        {
            get { return this.OnGetOtherSide(); }
        }

        public IType ReturnType
        {
            get { return this.OnGetReturnType(); }
        }

        #endregion

        #region ICoercionMember Members

        ICoercibleType ICoercionMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        /// <summary>
        /// Obtains the <see cref="BinaryOpCoercionContainingSide"/> which
        /// denotes which side the type that contains the
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>
        /// is on.
        /// </summary>
        /// <returns>A <see cref="BinaryOpCoercionContainingSide"/> value
        /// indicating which side (if not both) the 
        /// <typeparamref name="TCoercionParent"/> is on.</returns>
        protected abstract BinaryOpCoercionContainingSide OnGetContainingSide();

        /// <summary>
        /// Obtains the <see cref="CoercibleBinaryOperators"/> which
        /// is overridden by the <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="CoercibleBinaryOperators"/> value
        /// indicating which binary operator is coerced by the 
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</returns>
        protected abstract CoercibleBinaryOperators OnGetOperator();

        /// <summary>
        /// Obtains the alternate side's <see cref="IType"/> which
        /// is the <see cref="MemberBase{TParent}.Parent"/> if
        /// <see cref="ContainingSide"/> is 
        /// <see cref="BinaryOpCoercionContainingSide.Both"/>
        /// </summary>
        /// <returns>An <see cref="IType"/> which
        /// relates to the alternate side's <see cref="IType"/> which
        /// is the <see cref="MemberBase{TParent}.Parent"/> if
        /// <see cref="ContainingSide"/> is 
        /// <see cref="BinaryOpCoercionContainingSide.Both"/></returns>
        protected abstract IType OnGetOtherSide();

        /// <summary>
        /// Obtains the access level of the current 
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="AccessLevelModifiers"/> value representing
        /// the accessibility of the current 
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</returns>
        protected abstract AccessLevelModifiers OnGetAccessLevel();

        /// <summary>
        /// Obtains the <see cref="IType"/> that the 
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>
        /// yields upon return.
        /// </summary>
        protected abstract IType OnGetReturnType();

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.OnGetAccessLevel(); }
        }

        #endregion


        /// <summary>
        /// Obtains a <see cref="String"/> value that represents the current
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value that represents the current
        /// <see cref="BinaryOperatorCoercionMemberBase{TCoercionParent}"/>.</returns>
        public override string ToString()
        {
            IType l, r = l = null;
            if (ContainingSide == BinaryOpCoercionContainingSide.LeftSide)
            {
                l = this.Parent;
                r = this.OtherSide;
            }
            else if (ContainingSide == BinaryOpCoercionContainingSide.RightSide)
            {
                r = this.Parent;
                l = this.OtherSide;
            }
            else
            {
                l = r = this.Parent;
            }
            return string.Format("{0} operator {1}({2} l, {3} r)", this.ReturnType.Name, this.OnGetName(), l.Name, r.Name);
        }

    }
}
