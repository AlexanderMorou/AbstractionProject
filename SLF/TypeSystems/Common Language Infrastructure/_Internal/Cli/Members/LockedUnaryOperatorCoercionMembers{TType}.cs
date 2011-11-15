using System;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class LockedUnaryOperatorCoercionMembers<TType> :
        LockedGroupedMembersBase<TType, IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TType>, MethodInfo>,
        IUnaryOperatorCoercionMemberDictionary<TType>,
        IUnaryOperatorCoercionMemberDictionary
        where TType :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TType>, TType>
    {

        public LockedUnaryOperatorCoercionMembers(LockedFullMembersBase master, TType parent, MethodInfo[] sourceData, Func<MethodInfo, IUnaryOperatorCoercionMember<TType>> fetchImpl)
            : base(master, parent, sourceData, fetchImpl, null)
        {
        }

        #region IUnaryOperatorCoercionMemberDictionary<TType> Members
        /// <summary>
        /// Returns the <see cref="IUnaryOperatorCoercionMember{TCoercionParent}"/>
        /// which coerces the <paramref name="op"/>erator provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> constant
        /// relative to the <see cref="IUnaryOperatorCoercionMember{TCoercionParent}"/> to 
        /// return.</param>
        /// <returns>A <see cref="IUnaryOperatorCoercionMember{TCoercionParent}"/>
        /// instance relative to <paramref name="op"/>.</returns>
        public IUnaryOperatorCoercionMember<TType> this[CoercibleUnaryOperators op]
        {
            get {
                foreach (var unop in this.Values)
                    if (unop.Operator == op)
                        return unop;
                return null;
            }
        }

        #endregion

        protected override IUnaryOperatorUniqueIdentifier FetchKey(MethodInfo item)
        {
            return item.GetUnaryOperatorUniqueIdentifier();
        }

        #region IUnaryOperatorCoercionMemberDictionary Members

        IUnaryOperatorCoercionMember IUnaryOperatorCoercionMemberDictionary.this[CoercibleUnaryOperators op]
        {
            get { return this[op]; }
        }

        public bool ContainsOverload(CoercibleUnaryOperators op)
        {
            foreach (var unop in this.Values)
                if (unop.Operator == op)
                    return true;
            return false;
        }

        #endregion

    }
}
