﻿using System;
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
    /// Defines properties and methods for working with a unary operation coercion member.
    /// </summary>
    /// <typeparam name="TCoercionParentIdentifier">The type of the identifier that represents
    /// the parent's uniqueness from the other types.</typeparam>
    /// <typeparam name="TCoercionParent">The type of parent that contains the 
    /// unary operation coercion member in the current implementation.</typeparam>
    public interface IUnaryOperatorCoercionMember<TCoercionParent> :
        ICoercionMember<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>,
        IUnaryOperatorCoercionMember
        where TCoercionParent :
            ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
    {
        new TCoercionParent Parent { get; }
    }
}
