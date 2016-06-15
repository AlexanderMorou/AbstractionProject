using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliBinaryOperatorMemberDictionary<TCoercionParent> :
        CliCoercionMemberDictionary<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>,
        IBinaryOperatorCoercionMemberDictionary<TCoercionParent>,
        IBinaryOperatorCoercionMemberDictionary
        where TCoercionParent :
            ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        public CliBinaryOperatorMemberDictionary(TCoercionParent parent, CliFullMemberDictionary fullMembers)
            : base(parent, fullMembers, CliMemberType.BinaryOperator)
        {
        }


        public IBinaryOperatorCoercionMember<TCoercionParent> this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
        {
            get {
                foreach (var element in this.Values)
                    if (element.Operator == op && element.ContainingSide == side &&
                        element.OtherSide == otherSide)
                        return element;
                throw new KeyNotFoundException();
            }
        }

        public IBinaryOperatorCoercionMember<TCoercionParent> this[CoercibleBinaryOperators op]
        {
            get {
                foreach (var element in this.Values)
                    if (element.ContainingSide == BinaryOpCoercionContainingSide.Both &&
                        element.Operator == op)
                        return element;
                throw new KeyNotFoundException();

            }
        }

        IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
        {
            get { return this[op, side, otherSide]; }
        }

        IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op]
        {
            get { return this[op]; }
        }
    }
}
