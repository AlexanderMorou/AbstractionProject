using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate member that coerces
    /// the target's evaluation in an expression.
    /// </summary>
    /// <typeparam name="TCoercion">The type of coercion in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateCoercion">The type of coercion in the
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TCoercionParent">The type which owns the <typeparamref name="TCoercion"/>
    /// members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The intermediate type which owns the
    /// <typeparamref name="TIntermediateCoercion"/> members in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercionMember<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateMember<TCoercionIdentifier, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateCoercionMember,
        ICoercionMember<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TCoercionParent>
        where TCoercionIdentifier :
            IMemberUniqueIdentifier<TCoercionIdentifier>
        where TCoercionParentIdentifier :   
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercion :
            ICoercionMember<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TCoercionParent>
        where TIntermediateCoercion :
            IIntermediateCoercionMember<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
            TCoercion
        where TCoercionParent :
            ICoercibleType<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<TCoercionIdentifier, TCoercionParentIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
            TCoercionParent
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate member that coerces
    /// the target's evaluation in an expression.
    /// </summary>
    public interface IIntermediateCoercionMember :
        IIntermediateMember,
        IIntermediateScopedDeclaration,
        ICoercionMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IIntermediateCoercionMember"/>.
        /// </summary>
        new IIntermediateCoercibleType Parent { get; }
    }
}
