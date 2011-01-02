using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract class UnaryOperatorCoercionMemberBase<TCoercionParent> :
        MemberBase<TCoercionParent>,
        IUnaryOperatorCoercionMember<TCoercionParent>
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
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

        protected override string OnGetName()
        {
            string p = null;
            switch (this.Operator)
            {
                case CoercibleUnaryOperators.Plus:
                    p = "+";
                    break;
                case CoercibleUnaryOperators.Negative:
                    p = "-";
                    break;
                case CoercibleUnaryOperators.EvaluatesToFalse:
                    p = "false";
                    break;
                case CoercibleUnaryOperators.EvaluatesToTrue:
                    p = "true";
                    break;
                case CoercibleUnaryOperators.LogicalInvert:
                    p = "!";
                    break;
                case CoercibleUnaryOperators.Complement:
                    p = "~";
                    break;
                case CoercibleUnaryOperators.Increment:
                    p = "++";
                    break;
                case CoercibleUnaryOperators.Decrement:
                    p = "--";
                    break;
                default:
                    return null;
            }

            return string.Format("unary operator {0}", p);
        }
    }
}
