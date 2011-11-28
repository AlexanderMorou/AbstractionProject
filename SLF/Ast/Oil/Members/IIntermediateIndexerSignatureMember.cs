using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// indexer signature member.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexer signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer signature parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type of indexer signature parent in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IIntermediateSignatureMember<IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIntermediateIndexer, IIndexerSignatureParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerSignatureParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>, TIndexerParent, TIntermediateIndexerParent>,
        IIntermediateIndexerSignatureMember,
        IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
    {
        /// <summary>
        /// Obtains an indexer reference for the current <see cref="IIntermediateIndexerMember"/>.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads up to the <see cref="IIntermediateIndexerMember"/>.</param>
        /// <param name="parameters">The series of <see cref="IExpression"/> elements
        /// needed to get or set an element of the indexer</param>
        /// <returns>An indexer reference for the current <see cref="IIntermediateIndexerMember"/>.</returns>
        new IIndexerSignatureReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression source, params IExpression[] parameters);
        /// <summary>
        /// Obtains an indexer reference for the current <see cref="IIntermediateIndexerMember"/>.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads up to the <see cref="IIntermediateIndexerMember"/>.</param>
        /// <param name="parameters">The <see cref="IEnumerable{T}"/> of <see cref="IExpression"/> elements
        /// needed to get or set an element of the indexer</param>
        /// <returns>An indexer reference for the current <see cref="IIntermediateIndexerMember"/>.</returns>
        new IIndexerSignatureReferenceExpression<TIndexer, TIndexerParent> GetReference(IMemberParentReferenceExpression source, IEnumerable<IExpression> parameters);
    }

    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// indexer signature member.
    /// </summary>
    public interface IIntermediateIndexerSignatureMember :
        IIntermediateSignatureMember,
        IIntermediatePropertySignatureMember,
        IIndexerSignatureMember
    {
        /// <summary>
        /// Obtains an indexer reference for the current <see cref="IIntermediateIndexerSignatureMember"/>.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads up to the <see cref="IIntermediateIndexerMember"/>.</param>
        /// <param name="parameters">The series of <see cref="IExpression"/> elements
        /// needed to get or set an element of the indexer</param>
        /// <returns>An indexer reference for the current <see cref="IIntermediateIndexerSignatureMember"/>.</returns>
        IIndexerReferenceExpression GetReference(IMemberParentReferenceExpression source, params IExpression[] parameters);
        /// <summary>
        /// Obtains an indexer reference for the current <see cref="IIntermediateIndexerSignatureMember"/>.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// which leads up to the <see cref="IIntermediateIndexerMember"/>.</param>
        /// <param name="parameters">The <see cref="IEnumerable{T}"/> of <see cref="IExpression"/> elements
        /// needed to get or set an element of the indexer</param>
        /// <returns>An indexer reference for the current <see cref="IIntermediateIndexerSignatureMember"/>.</returns>
        IIndexerReferenceExpression GetReference(IMemberParentReferenceExpression source, IEnumerable<IExpression> parameters);
    }
}
