using System;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedUnaryOperatorCoercionMembers<TType> :
        LockedGroupedMembersBase<TType, IUnaryOperatorCoercionMember<TType>, MethodInfo>,
        IUnaryOperatorCoercionMemberDictionary<TType>,
        IUnaryOperatorCoercionMemberDictionary
        where TType :
            ICoercibleType<TType>
    {

        public LockedUnaryOperatorCoercionMembers(LockedFullMembersBase master, TType parent, MethodInfo[] sourceData, Func<MethodInfo, IUnaryOperatorCoercionMember<TType>> fetchImpl)
            : base(master, parent, sourceData, fetchImpl, GetName)
        {
        }

        private static string GetName(MethodInfo member)
        {
            return null;
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

        protected override string FetchKey(MethodInfo item)
        {
            return item.GetUniqueIdentifier();
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
