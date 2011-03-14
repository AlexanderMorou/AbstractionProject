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
    /// <typeparam name="TType">The type which owns the <typeparamref name="TCoercion"/>
    /// members in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The intermediate type which owns the
    /// <typeparamref name="TIntermediateCoercion"/> members in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateCoercionMember<TCoercion, TIntermediateCoercion, TType, TIntermediateType> :
        IIntermediateMember<TType, TIntermediateType>,
        IIntermediateCoercionMember,
        ICoercionMember<TCoercion, TType>
        where TCoercion :
            ICoercionMember<TCoercion, TType>
        where TIntermediateCoercion :
            TCoercion,
            IIntermediateCoercionMember<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
        where TType :
            ICoercibleType<TCoercion, TType>
        where TIntermediateType :
            TType,
            IIntermediateCoercibleType<TCoercion, TIntermediateCoercion, TType, TIntermediateType>
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
