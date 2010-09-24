using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base indexer signature members dictionary.
    /// </summary>
    /// <typeparam name="TIndexer">The type of indexer signature in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexer">The type of indexer signature in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TIndexerParent">The type of indexer signature parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateIndexerParent">The type of indexer signature parent in the intermediate
    /// abstract syntax tree.</typeparam>
    public class IntermediateIndexerSignatureMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IntermediateGroupedMemberDictionary<TIndexerParent, TIntermediateIndexerParent, TIndexer, TIntermediateIndexer>,
        IIntermediateIndexerSignatureMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
        IIntermediateIndexerSignatureMemberDictionary
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
            TIndexer
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
            TIndexerParent
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerSignatureMemberDictionary"/> with the 
        /// <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/>
        /// which contains the <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>.</param>
        public IntermediateIndexerSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateIndexerParent parent)
            : base(master, parent)
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerSignatureMemberDictionary"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="items"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/>
        /// which contains the <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>.</param>
        /// <param name="items">The <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// which the is based upon.</param>
        public IntermediateIndexerSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateIndexerParent parent, IntermediateIndexerSignatureMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> root)
            : base(master, parent, root)
        {
        }

        #region IIndexerSignatureMemberDictionary Members

        IIndexerSignatureParent IIndexerSignatureMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        #endregion

    }
}
