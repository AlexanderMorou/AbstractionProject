using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a type-coercion member.
    /// </summary>
    /// <typeparam name="TCoercionParentIdentifier">The type of the identifier that represents
    /// the parent's uniqueness from the other types.</typeparam>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// type coercion member in the current implementation.</typeparam>
    public interface ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> :
        ICoercionMember<ITypeCoercionUniqueIdentifier, TCoercionParentIdentifier, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>,
        ITypeCoercionMember
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, TCoercionParentIdentifier, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
    {
    }
}
