using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation of a <see cref="IConstructorPointerReferenceExpression"/>
    /// which provides properties and methods for working with
    /// the pointer to a constructor as an expression
    /// </summary>
    public class ConstructorPointerReferenceExpression :
        IExpression,
        IConstructorPointerReferenceExpression
    {
        /// <summary>
        /// Creates a new <see cref="ConstructorPointerReferenceExpression"/>
        /// with the <paramref name="reference"/> provided.
        /// </summary>
        /// <param name="reference">The <see cref="IConstructorReferenceStub"/>
        /// to which the <see cref="ConstructorPointerReferenceExpression"/>
        /// is rooted.</param>
        public ConstructorPointerReferenceExpression(IConstructorReferenceStub reference)
        {
            this.Reference = reference;
            this.Signature = new TypeCollection();
        }

        /// <summary>
        /// Creates a new <see cref="ConstructorPointerReferenceExpression"/>
        /// with the <paramref name="reference"/> and 
        /// <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="reference">The <see cref="IConstructorReferenceStub"/>
        /// to which the <see cref="ConstructorPointerReferenceExpression"/>
        /// is rooted.</param>
        /// <param name="signature">The series of types which designate
        /// the individual types of the constructor parameters to bind
        /// to.</param>
        public ConstructorPointerReferenceExpression(IConstructorReferenceStub reference, ITypeCollection signature)
            : this(reference)
        {
            this.Signature.AddRange(signature);
        }
        /// <summary>
        /// Creates a new <see cref="ConstructorPointerReferenceExpression"/>
        /// with the <paramref name="reference"/> and 
        /// <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="reference">The <see cref="IConstructorReferenceStub"/>
        /// to which the <see cref="ConstructorPointerReferenceExpression"/>
        /// is rooted.</param>
        /// <param name="signature">The series of types which designate
        /// the individual types of the constructor parameters to bind
        /// to.</param>
        public ConstructorPointerReferenceExpression(IConstructorReferenceStub reference, params IType[] signature)
            : this(reference)
        {
            this.Signature.AddRange(signature);
        }

        #region IExpression Members

        /// <summary>
        /// Returns the type of expression the
        /// <see cref="ConstructorPointerReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns 
        /// <see cref="ExpressionKind.ConstructorReference"/>.</remarks>
        public ExpressionKind Type
        {
            get { return ExpressionKind.ConstructorReference; }
        }

        /// <summary>
        /// Visits the elements of the 
        /// <see cref="ConstructorPointerReferenceExpression"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/>
        /// to which the <see cref="ConstructorPointerReferenceExpression"/> 
        /// needs to repay the visit to.</param>
        /// <remarks></remarks>
        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion

        #region IConstructorPointerReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="IConstructorReferenceStub"/>
        /// associated to the <see cref="ConstructorPointerReferenceExpression"/>.
        /// </summary>
        /// <remarks>Used to provide initial context data 
        /// for the lookup.</remarks>
        public IConstructorReferenceStub Reference { get; private set; }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of
        /// <see cref="IType"/> instances which relate to 
        /// the types of parameters used to
        /// bind to the constructor.
        /// </summary>
        public ITypeCollection Signature { get; private set; }

        /// <summary>
        /// Obtains a <see cref="IConstructorInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of 
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the 
        /// types of the parameters.</param>
        /// <returns>A new <see cref="IConstructorInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IConstructorInvokeExpression Invoke(IExpressionCollection parameters)
        {
            return new ConstructorInvokeExpression(this, parameters);
        }

        /// <summary>
        /// Obtains a <see cref="IConstructorInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the types 
        /// of the parameters.</param>
        /// <returns>A new <see cref="IConstructorInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IConstructorInvokeExpression Invoke(params IExpression[] parameters)
        {
            return new ConstructorInvokeExpression(this, parameters);
        }

        #endregion

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion
    }
}
