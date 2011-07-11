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
    /// Defines generic properties and methods for working with a series of
    /// intermediate indexer signature members.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexer signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer signature parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type of indexer signature parent in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateIndexerSignatureMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IIntermediateGroupedMemberDictionary<TIndexerParent, TIntermediateIndexerParent, TIndexer, TIntermediateIndexer>,
        IIndexerSignatureMemberDictionary<TIndexer, TIndexerParent>
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
        /// Adds a new <typeparamref name="TIntermediateIndexer"/> with
        /// the <paramref name="returnType"/> and
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="returnType">The <see cref="IType"/> which
        /// results from the indexer operation.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which denotes the types and names of the parameters of the
        /// indexer to add.</param>
        /// <param name="canGet">Whether the indexer can be read.</param>
        /// <param name="canSet">Whether the indexer can be written.</param>
        /// <returns>A <typeparamref name="TIntermediateIndexer"/> which
        /// represents the indexer added.</returns>
        /// <exception cref="System.ArgumentException">thrown another
        /// member within the <see cref="IIntermediateIndexerMemberDictionary"/>
        /// contains the same signature.</exception>
        TIntermediateIndexer Add(IType returnType, TypedNameSeries parameters, bool canGet = true, bool canSet = true);
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateIndexer"/> with
        /// the <paramref name="nameAndReturn"/> and
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">The <see cref="TypedName"/> 
        /// which specifies the indexer name and return type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which denotes the types and names of the parameters of the
        /// indexer to add.</param>
        /// <param name="canGet">Whether the indexer can be read.</param>
        /// <param name="canSet">Whether the indexer can be written.</param>
        /// <returns>A <typeparamref name="TIntermediateIndexer"/> which
        /// represents the indexer added.</returns>
        /// <exception cref="System.ArgumentException">thrown another
        /// member within the <see cref="IIntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// contains the same signature.</exception>
        TIntermediateIndexer Add(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet = true, bool canSet = true);
    }

    /// <summary>
    /// Defines properties and methods for working with a series of
    /// intermediate indexer signature members.
    /// </summary>
    public interface IIntermediateIndexerSignatureMemberDictionary :
        IIntermediateGroupedMemberDictionary,
        IIndexerSignatureMemberDictionary
    {
        /// <summary>
        /// Adds a new <see cref="IIntermediateIndexerSignatureMember"/> with
        /// the <paramref name="returnType"/> and
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="returnType">The <see cref="IType"/> which
        /// results from the indexer operation.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which denotes the types and names of the parameters of the
        /// indexer to add.</param>
        /// <param name="canGet">Whether the property can be read.</param>
        /// <param name="canSet">Whether the property can be written.</param>
        /// <returns>A <see cref="IIntermediateIndexerSignatureMember"/> which
        /// represents the indexer added.</returns>
        /// <exception cref="System.ArgumentException">thrown another
        /// member within the <see cref="IIntermediateIndexerSignatureMemberDictionary"/>
        /// contains the same signature.</exception>
        IIntermediateIndexerSignatureMember Add(IType returnType, TypedNameSeries parameters, bool canGet = true, bool canSet = true);
        /// <summary>
        /// Adds a new <see cref="IIntermediateIndexerMember"/> with
        /// the <paramref name="nameAndReturn"/> and
        /// <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">The <see cref="TypedName"/> 
        /// which specifies the indexer name and return type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which denotes the types and names of the parameters of the
        /// indexer to add.</param>
        /// <param name="canGet">Whether the property can be read.</param>
        /// <param name="canSet">Whether the property can be written.</param>
        /// <returns>A <see cref="IIntermediateIndexerMember"/> which
        /// represents the indexer added.</returns>
        /// <exception cref="System.ArgumentException">thrown another
        /// member within the <see cref="IIntermediateIndexerMemberDictionary"/>
        /// contains the same signature.</exception>
        IIntermediateIndexerSignatureMember Add(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet = true, bool canSet = true);
    }
}
