using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// The kind of reference an indexer reference is.
    /// </summary>
    public enum IndexerReferenceType
    {
        /// <summary>
        /// The indexer is supported by the 
        /// CLI.
        /// </summary>
        ArrayIndexer,
        /// <summary>
        /// The indexer is supported by the
        /// same framework as a property
        /// with parameters.
        /// </summary>
        PropertyIndexer,
        /// <summary>
        /// The indexer is inferred by the compiler.
        /// </summary>
        InferredIndexer,
    }
    public interface IIndexerReferenceExpression<TIndexer, TIndexerParent> :
        IBoundMemberReference,
        IIndexerReferenceExpression
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {
        new TIndexer Member { get; }
    }

    public interface IIndexerSignatureReferenceExpression<TIndexer, TIndexerParent> :
        IBoundMemberReference,
        IIndexerReferenceExpression
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
        new TIndexer Member { get; }
    }

    /// <summary>
    /// Defines properties and methods 
    /// for working with an indexer 
    /// reference.
    /// </summary>
    public interface IIndexerReferenceExpression :
        IPropertyReferenceExpression,
        IAssignTargetExpression
    {
        /// <summary>
        /// Returns the <see cref="IndexerReferenceType"/>
        /// of the <see cref="IIndexerReferenceExpression"/>.
        /// </summary>
        IndexerReferenceType IndexerType { get; }
        /// <summary>
        /// The <see cref="IExpressionCollection"/> used
        /// to refer to the indexer.
        /// </summary>
        IMalleableExpressionCollection Parameters { get; }
    }

    public interface IUnboundIndexerReferenceExpression :
        IIndexerReferenceExpression 
    {
        /// <summary>
        /// Returns/sets the <see cref="IndexerReferenceType"/>
        /// of the <see cref="IUnboundIndexerReferenceExpression"/>.
        /// </summary>
        new IndexerReferenceType IndexerType { get; set; }
    }
}
