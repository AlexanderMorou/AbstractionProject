using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base indexer members dictionary.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexzer in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type the <typeparamref name="TIndexer"/> instances
    /// belong to in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type the <typeparamref name="TIntermediateIndexer"/> instances
    /// belong to in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateIndexerMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IntermediateGroupedMemberDictionary<TIndexerParent, TIntermediateIndexerParent, TIndexer, TIntermediateIndexer>,
        IIntermediateIndexerMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
        IIntermediateIndexerMemberDictionary
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
            TIndexer
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
            TIndexerParent
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/> with the 
        /// <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateIndexerMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/>
        /// which contains the <see cref="IntermediateIndexerMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>.</param>
        public IntermediateIndexerMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateIndexerParent parent)
            : base(master, parent)
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="items"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateIndexerMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/>
        /// which contains the <see cref="IntermediateIndexerMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>.</param>
        /// <param name="root">The <see cref="IntermediateIndexerMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// which the is based upon.</param>
        public IntermediateIndexerMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateIndexerParent parent, IntermediateIndexerMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> root)
            : base(master, parent, root)
        {
        }


        #region IIntermediateIndexerMemberDictionary<TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent> Members

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
        /// <returns>A <typeparamref name="TIntermediateIndexer"/> which
        /// represents the indexer added.</returns>
        /// <exception cref="System.ArgumentException">thrown another
        /// member within the <see cref="IIntermediateIndexerMemberDictionary"/>
        /// contains the same signature.</exception>
        public virtual TIntermediateIndexer Add(IType returnType, TypedNameSeries parameters)
        {
            return this.Add(new TypedName(Resources.DefaultIndexerName, returnType), parameters);
        }

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
        /// <returns>A <typeparamref name="TIntermediateIndexer"/> which
        /// represents the indexer added.</returns>
        /// <exception cref="System.ArgumentException">thrown another
        /// member within the <see cref="IIntermediateIndexerMemberDictionary"/>
        /// contains the same signature.</exception>
        public abstract TIntermediateIndexer Add(TypedName nameAndReturn, TypedNameSeries parameters);

        #endregion

        #region IIntermediateIndexerMemberDictionary Members

        IIntermediateIndexerMember IIntermediateIndexerMemberDictionary.Add(IType returnType, TypedNameSeries parameters)
        {
            return this.Add(returnType, parameters);
        }

        IIntermediateIndexerMember IIntermediateIndexerMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            return this.Add(nameAndReturn, parameters);
        }

        #endregion

    }
}
