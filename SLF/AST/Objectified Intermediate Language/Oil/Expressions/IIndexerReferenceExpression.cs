using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
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
        /// The indexer is inferred by the 
        /// <see cref="IMemberParentReferenceExpression.ForwardType"/>'s
        /// value.
        /// </summary>
        InferredIndexer,
    }
    /// <summary>
    /// Defines properties and methods 
    /// for working with an indexer 
    /// reference.
    /// </summary>
    public interface IIndexerReferenceExpression :
        IMemberParentReferenceExpression,
        IMemberReferenceExpression,
        /*ILinkableExpression,*/
        IAssignTargetExpression
    {
        /// <summary>
        /// Returns the <see cref="IndexerReferenceType"/>
        /// of the <see cref="IIndexerReferenceExpression"/>.
        /// </summary>
        IndexerReferenceType IndexerType { get; set; }
        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IIndexerReferenceExpression"/>,
        /// get/set methods, is.
        /// </summary>
        /// <remarks>Always <see cref="MethodReferenceType.StandardMethodReference"/>
        /// when <see cref="IndexerType"/> is 
        /// <see cref="IndexerReferenceType.ArrayIndexer"/>.</remarks>
        MethodReferenceType ReferenceType { get; set; }
        /// <summary>
        /// The <see cref="IExpressionCollection"/> used
        /// to refer to the indexer.
        /// </summary>
        IMalleableExpressionCollection Parameters { get; }
        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IIndexerReferenceExpression"/>.
        /// </summary>
        IMemberParentReferenceExpression Source { get; }
    }
}
