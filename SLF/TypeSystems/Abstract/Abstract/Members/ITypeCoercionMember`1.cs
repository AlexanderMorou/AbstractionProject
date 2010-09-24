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
    /// Defines generic properties and methods for working with a type-coercion member.
    /// </summary>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// type coercion member in the current implementation.</typeparam>
    public interface ITypeCoercionMember<TCoercionParent> :
        ICoercionMember<ITypeCoercionMember<TCoercionParent>, TCoercionParent>,
        ITypeCoercionMember
        where TCoercionParent :
            ICoercibleType<ITypeCoercionMember<TCoercionParent>, TCoercionParent>
    {
    }
}
