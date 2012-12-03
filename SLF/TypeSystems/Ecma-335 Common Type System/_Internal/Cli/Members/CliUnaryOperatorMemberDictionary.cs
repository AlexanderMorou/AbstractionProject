using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliUnaryOperatorMemberDictionary<TCoercionParent> :
        CliCoercionMemberDictionary<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>,
        IUnaryOperatorCoercionMemberDictionary<TCoercionParent>,
        IUnaryOperatorCoercionMemberDictionary
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        public CliUnaryOperatorMemberDictionary(TCoercionParent parent, CliFullMemberDictionary fullMembers)
            : base(parent, fullMembers, CliMemberType.UnaryOperator)
        {
        }

        public IUnaryOperatorCoercionMember<TCoercionParent> this[CoercibleUnaryOperators op]
        {
            get {
                foreach (var member in this.Values)
                    if (member.Operator == op)
                        return member;
                throw new KeyNotFoundException();
            }
        }

        IUnaryOperatorCoercionMember IUnaryOperatorCoercionMemberDictionary.this[CoercibleUnaryOperators op]
        {
            get { return this[op]; }
        }

        public bool ContainsOverload(CoercibleUnaryOperators op)
        {
            foreach (var member in this.Values)
                if (member.Operator == op)
                    return true;
            return false;
        }

    }
}
