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
    internal abstract class UnaryOperatorCoercionMemberBase<TCoercionParent> :
        MemberBase<IUnaryOperatorUniqueIdentifier, TCoercionParent>,
        IUnaryOperatorCoercionMember<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        public UnaryOperatorCoercionMemberBase(TCoercionParent parent)
            : base(parent)
        {
        }

        #region ICoercionMember Members

        ICoercibleType ICoercionMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.OnGetAccessLevel(); }
        }

        #endregion

        #region IUnaryOperatorCoercionMember Members

        public CoercibleUnaryOperators Operator
        {
            get { return this.OnGetOperator(); }
        }

        public abstract IType ResultedType { get; }

        #endregion

        protected abstract AccessLevelModifiers OnGetAccessLevel();

        protected abstract CoercibleUnaryOperators OnGetOperator();

    }
}
