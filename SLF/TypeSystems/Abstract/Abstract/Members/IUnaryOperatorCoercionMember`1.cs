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
    /// Defines properties and methods for working with a unary operation coercion member.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// unary operation coercion member in the current implementation.</typeparam>
    public interface IUnaryOperatorCoercionMember<TCoercionParent> :
        ICoercionMember<IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>,
        IUnaryOperatorCoercionMember
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
    }
}
