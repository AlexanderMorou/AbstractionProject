using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// a reference expression that relates
    /// to a parent of members.
    /// </summary>
    public interface IMemberParentReferenceExpression :
        INaryOperandExpression,
        IUnaryOperationPrimaryTerm/* :
        ILinkableExpression*/
    {
        /// <summary>
        /// Obtains a <see cref="IMethodReferenceStub"/> for
        /// a method with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// of the method to reference.</param>
        /// <returns>A <see cref="IMethodReferenceStub"/> instance
        /// which points to a series of methods named '<paramref name="name"/>'.</returns>
        IMethodReferenceStub GetMethod(string name);
        /// <summary>
        /// Obtains a <see cref="IMethodReferenceStub"/> for
        /// a method with the <paramref name="name"/> and
        /// <paramref name="genericParameters"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> which
        /// denotes the name of the method.</param>
        /// <param name="genericParameters">A <see cref="ITypeCollection"/>
        /// which is used as generic parameter replacements.</param>
        /// <returns>A <see cref="IMethodReferenceStub"/> which
        /// refers to a series of methods with the given 
        /// generic parameters.</returns>
        IMethodReferenceStub GetMethod(string name, ITypeCollection genericParameters);
        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// for a method with the <paramref name="name"/> and
        /// <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> which
        /// denotes the name of the method.</param>
        /// <param name="signature">An <see cref="ITypeCollection"/>
        /// used to denote the signature of the method.</param>
        /// <returns>A <see cref="IMethodPointerReferenceExpression"/>
        /// instance which should be a verifiable pointer to the 
        /// method.</returns>
        /// <remarks>For verifiability, <see cref="ILinkableExpression.Link()"/>
        /// the <see cref="IMethodPointerReferenceExpression"/>.</remarks>
        IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature);
        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// for a method with the <paramref name="name"/>, 
        /// <paramref name="signature"/>, and <paramref name="genericParameters"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> which
        /// denotes the name of the method.</param>
        /// <param name="signature">An <see cref="ITypeCollection"/>
        /// used to denote the signature of the method.</param>
        /// <param name="genericParameters">An <see cref="ITypeCollection"/>
        /// which is used as generic parameter replacements.</param>
        /// <returns>A <see cref="IMethodPointerReferenceExpression"/>
        /// instance which should be a verifiable pointer to the 
        /// method.</returns>
        /// <remarks>For verifiability, <see cref="ILinkableExpression.Link()"/>
        /// the <see cref="IMethodPointerReferenceExpression"/>.</remarks>
        IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature, ITypeCollection genericParameters);
        /// <summary>
        /// Gets a <paramref name="name"/>d indexer with the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> indicating
        /// the name of the indexer to retrieve.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/>
        /// used to reference the indexer.</param>
        /// <returns>An <see cref="IIndexerReferenceExpression"/> as
        /// described by <paramref name="name"/> and
        /// <paramref name="parameters"/>.</returns>
        /// <remarks>C&#9839; does not allow indexers of any other
        /// name than 'Item', because its language semantics
        /// do not have named indexers.</remarks>
        IIndexerReferenceExpression GetIndexer(string name, params IExpression[] parameters);
        /// <summary>
        /// Obtains a <see cref="IPropertyReferenceExpression"/>
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// denoting the name of the property to reference.</param>
        /// <returns>A <see cref="IPropertyReferenceExpression"/> related
        /// to the property described by <paramref name="name"/>.</returns>
        IPropertyReferenceExpression GetProperty(string name);
        /// <summary>
        /// Gets a general case indexer with the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">The <see cref="IExpressionCollection"/>
        /// used to reference the indexer.</param>
        /// <returns></returns>
        /// <remarks>If the <see cref="ForwardType"/> is an array type
        /// returns an array indexer where all parameter types
        /// must be a number; otherwise it returns
        /// an indexer with the default name 'Item'.</remarks>
        IIndexerReferenceExpression GetIndexer(params IExpression[] parameters);
        /// <summary>
        /// Returns the <see cref="IFieldReferenceExpression"/>
        /// relative to the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> of the 
        /// name of the field to retrieve.</param>
        /// <returns>A <see cref="IFieldReferenceExpression"/> 
        /// relative to the <paramref name="name"/>d field 
        /// that needs retrieved.</returns>
        IFieldReferenceExpression GetField(string name);
        /// <summary>
        /// Returns the <see cref="IEventReferenceExpression"/> relative to the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> of the name of the event
        /// to reference.</param>
        /// <returns>A <see cref="IEventReferenceExpression"/> relative to the <paramref name="name"/>d 
        /// event to reference.</returns>
        IEventReferenceExpression GetEvent(string name);
    }
}
