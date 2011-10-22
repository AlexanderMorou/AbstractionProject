using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class UnaryOperatorCoercionMemberBase<TCoercionParentIdentifier, TCoercionParent> :
        MemberBase<IUnaryOperatorUniqueIdentifier, TCoercionParent>,
        IUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, TCoercionParentIdentifier, IUnaryOperatorCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
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
