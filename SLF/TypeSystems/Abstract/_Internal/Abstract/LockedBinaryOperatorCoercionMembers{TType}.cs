using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedBinaryOperatorCoercionMembers<TCoercionParent> :
        LockedGroupedMembersBase<TCoercionParent, IBinaryOperatorCoercionMember<TCoercionParent>, MethodInfo>,
        IBinaryOperatorCoercionMemberDictionary<TCoercionParent>,
        IBinaryOperatorCoercionMemberDictionary
        where TCoercionParent :
            ICoercibleType<TCoercionParent>
    {
        public LockedBinaryOperatorCoercionMembers(LockedFullMembersBase master, TCoercionParent parent, MethodInfo[] sourceData, Func<MethodInfo, IBinaryOperatorCoercionMember<TCoercionParent>> fetchImpl)
            : base(master, parent, sourceData, fetchImpl)
        {
        }
        public LockedBinaryOperatorCoercionMembers(LockedFullMembersBase master, TCoercionParent parent)
            : base(master, parent)
        {
        }
        #region IBinaryOperatorCoercionMemberDictionary<TCoercionParent> Members

        public IBinaryOperatorCoercionMember<TCoercionParent> this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
        {
            get
            {
                foreach (var binOpC in this.Values)
                    if (binOpC.Operator == op && binOpC.ContainingSide == side && binOpC.OtherSide.Equals(otherSide))
                        return binOpC;
                return null;
            }
        }

        public IBinaryOperatorCoercionMember<TCoercionParent> this[CoercibleBinaryOperators op]
        {
            get
            {
                foreach (var binOpC in this.Values)
                    if (binOpC.ContainingSide == BinaryOpCoercionContainingSide.Both && op == binOpC.Operator)
                    {
                        return binOpC;
                    }
                return null;
            }
        }

        #endregion


        #region IBinaryOperatorCoercionMemberDictionary Members

        IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
        {
            get { return this[op, side, otherSide]; }
        }

        IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op]
        {
            get { return this[op]; }
        }

        #endregion

        protected override string FetchKey(MethodInfo item)
        {
            return item.GetUniqueIdentifier();
        }

    }
}
