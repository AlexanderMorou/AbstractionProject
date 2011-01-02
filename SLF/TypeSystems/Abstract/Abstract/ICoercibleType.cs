using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// coercible type.
    /// </summary>
    public interface ICoercibleType :
        IType
    {
        /// <summary>
        /// Returns the <see cref="IBinaryOperatorCoercionMemberDictionary"/> 
        /// assocaited to the <see cref="ICoercibleType"/>.
        /// </summary>
        IBinaryOperatorCoercionMemberDictionary BinaryOperatorCoercions { get; }
        /// <summary>
        /// Returns the <see cref="ITypeCoercionMemberDictionary"/> 
        /// assocaited to the <see cref="ICoercibleType"/>.
        /// </summary>
        ITypeCoercionMemberDictionary TypeCoercions { get; }
        /// <summary>
        /// Returns the <see cref="IUnaryOperatorCoercionMemberDictionary"/> 
        /// assocaited to the <see cref="ICoercibleType"/>.
        /// </summary>
        IUnaryOperatorCoercionMemberDictionary UnaryOperatorCoercions { get; }
    }
}
