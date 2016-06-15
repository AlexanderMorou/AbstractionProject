using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// series of unary operator coercion members.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of 
    /// parent that contains the unary operator coercion 
    /// members in the current implementation.</typeparam>
    public interface IUnaryOperatorCoercionMemberDictionary<TCoercionParent> :
        IGroupedMemberDictionary<TCoercionParent, IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>>
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        /// <summary>
        /// Returns the <see cref="IUnaryOperatorCoercionMember{TCoercionParent}"/>
        /// which coerces the <paramref name="op"/>erator provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> constant
        /// relative to the <see cref="IUnaryOperatorCoercionMember{TCoercionParent}"/> to 
        /// return.</param>
        /// <returns>A <see cref="IUnaryOperatorCoercionMember{TCoercionParent}"/>
        /// instance relative to <paramref name="op"/>.</returns>
        IUnaryOperatorCoercionMember<TCoercionParent> this[CoercibleUnaryOperators op] { get; }
    }
}
