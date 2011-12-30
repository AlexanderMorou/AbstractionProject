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

namespace AllenCopeland.Abstraction.Slf.Ast.Members
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
    public abstract class IntermediateIndexerSignatureMemberDictionary<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        IntermediateGroupedMemberDictionary<TIndexerParent, TIntermediateIndexerParent, IGeneralSignatureMemberUniqueIdentifier, TIndexer, TIntermediateIndexer>,
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
            class,
            IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>,
            TIndexerParent
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/> with the 
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
        /// Creates a new <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="root"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateIndexerParent"/>
        /// which contains the <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>.</param>
        /// <param name="root">The <see cref="IntermediateIndexerSignatureMemberDictionary{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
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


        #region IIntermediateIndexerSignatureMemberDictionary<TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent> Members

        public TIntermediateIndexer Add(IType returnType, TypedNameSeries parameters, bool canGet = true, bool canSet = true)
        {
            return this.Add(new TypedName(null, returnType), parameters, canGet, canSet);
        }

        public TIntermediateIndexer Add(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet = true, bool canSet = true)
        {
            var result = this.GetNew(nameAndReturn, parameters, canGet, canSet);
            base._Add(result.UniqueIdentifier, result);
            return result;
        }

        protected abstract TIntermediateIndexer GetNew(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet, bool canSet);
        #endregion


        #region IIntermediateIndexerSignatureMemberDictionary Members

        IIntermediateIndexerSignatureMember IIntermediateIndexerSignatureMemberDictionary.Add(IType returnType, TypedNameSeries parameters, bool canGet, bool canSet)
        {
            return this.Add(returnType, parameters, canGet, canSet);
        }

        IIntermediateIndexerSignatureMember IIntermediateIndexerSignatureMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet, bool canSet)
        {
            return this.Add(nameAndReturn, parameters, canGet, canSet);
        }

        #endregion

    }
}
