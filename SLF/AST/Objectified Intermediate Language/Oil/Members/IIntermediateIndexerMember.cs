using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate indexer member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexzer in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type the <typeparamref name="TIndexer"/> instances
    /// belong to in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type the <typeparamref name="TIntermediateIndexer"/> instances
    /// belong to in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IIntermediateSignatureMember<IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIntermediateIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
        IIntermediateIndexerMember,
        IIntermediatePropertyMember,
        IIndexerMember<TIndexer, TIndexerParent>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
    {
        /// <summary>
        /// Obtains an indexer reference for the current <see cref="IIntermediateIndexerMember"/>.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads up to the <see cref="IIntermediateIndexerMember"/>.</param>
        /// <param name="parameters">The series of <see cref="IExpression"/> elements
        /// needed to get or set an element of the indexer</param>
        /// <returns>An indexer reference for the current <see cref="IIntermediateIndexerMember"/>.</returns>
        new IIndexerReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression source, params IExpression[] parameters);
        /// <summary>
        /// Obtains an indexer reference for the current <see cref="IIntermediateIndexerMember"/>.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads up to the <see cref="IIntermediateIndexerMember"/>.</param>
        /// <param name="parameters">The <see cref="IEnumerable{T}"/> of <see cref="IExpression"/> elements
        /// needed to get or set an element of the indexer</param>
        /// <returns>An indexer reference for the current <see cref="IIntermediateIndexerMember"/>.</returns>
        new IIndexerReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression source, IEnumerable<IExpression> parameters);
    }

    /// <summary>
    /// Defines properties and methods for working with an intermediate indexer member.
    /// </summary>
    public interface IIntermediateIndexerMember :
        IIntermediateIndexerSignatureMember,
        IIntermediatePropertyMember,
        IIntermediateExtendedInstanceMember,
        IIndexerMember
    {
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IIntermediateIndexerMember"/>.
        /// </summary>
        new IIntermediatePropertyMethodMember GetMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertyMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IIntermediateIndexerMember"/>.
        /// </summary>
        new IIntermediatePropertySetMethodMember SetMethod { get; }
    }
}
