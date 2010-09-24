using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
    public interface IUnaryOperatorCoercionMemberDictionary :
        IGroupedMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IUnaryOperatorCoercionMember"/>
        /// which coerces the <paramref name="op"/>erator provided.
        /// </summary>
        /// <param name="op">The <see cref="CoercibleUnaryOperators"/> constant
        /// relative to the <see cref="IUnaryOperatorCoercionMember"/> to 
        /// return.</param>
        /// <returns>A <see cref="IUnaryOperatorCoercionMember"/>
        /// instance relative to <paramref name="op"/>.</returns>
        IUnaryOperatorCoercionMember this[CoercibleUnaryOperators op] { get; }
        /// <summary>
        /// Returns whether the <see cref="IUnaryOperatorCoercionMemberDictionary"/>
        /// contains the a coercion member with the <paramref name="op"/>
        /// provided.
        /// </summary>
        /// <param name="op">The kind of <see cref="CoercibleUnaryOperators"/>
        /// to search for an overload of.</param>
        /// <returns>true if the <see cref="IUnaryOperatorCoercionMemberDictionary"/>
        /// contains an overload for the <paramref name="op"/> provided; false,
        /// otherwise.</returns>
        bool ContainsOverload(CoercibleUnaryOperators op);
    }
}
