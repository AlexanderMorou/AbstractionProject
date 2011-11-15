using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
    /// <typeparam name="TCoercionIdentifier">The kind of identifier used to
    /// differentiate the <typeparamref name="TIntermediateCoercion"/> from its 
    /// siblings.</typeparam>
    /// <typeparam name="TCoercion">The type of coercion in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateCoercion">The type of coercion in the
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TCoercionParent">The type which owns the <typeparamref name="TCoercion"/>
    /// members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCoercionParent">The intermediate type which owns the
    /// <typeparamref name="TIntermediateCoercion"/> members in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercionMember<TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent> :
        IIntermediateMember<TCoercionIdentifier, TCoercionParent, TIntermediateCoercionParent>,
        IIntermediateCoercionMember,
        ICoercionMember<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TCoercionIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TCoercion :
            ICoercionMember<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TIntermediateCoercion :
            IIntermediateCoercionMember<TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
            TCoercion
        where TCoercionParent :
            ICoercibleType<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TIntermediateCoercionParent :
            IIntermediateCoercibleType<TCoercionIdentifier, TCoercion, TIntermediateCoercion, TCoercionParent, TIntermediateCoercionParent>,
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
