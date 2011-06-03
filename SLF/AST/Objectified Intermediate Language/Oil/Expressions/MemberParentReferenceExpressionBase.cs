using System.Collections.Generic;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides an abstract base for <see cref="IMemberParentReferenceExpression"/>
    /// implementations.
    /// </summary>
    public abstract class MemberParentReferenceExpressionBase :
        IMemberParentReferenceExpression
    {

        /// <summary>
        /// Returns the type of expression the <see cref="ExpressionBase"/> is.
        /// </summary>
        public abstract ExpressionKind Type { get; }

        #region IMemberParentReferenceExpression Members

        public IEventReferenceExpression GetEvent(string name)
        {
            return new UnboundEventReferenceExpression(name, this.ObtainRelativeGetMemberTarget());
        }

        /// <summary>
        /// Obtains a <see cref="IMethodReferenceStub"/> for
        /// a method with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// of the method to reference.</param>
        /// <returns>A <see cref="IMethodReferenceStub"/> instance
        /// which points to a series of methods named '<paramref name="name"/>'.</returns>
        public virtual IMethodReferenceStub GetMethod(string name)
        {
            return new UnboundMethodReferenceStub(this.ObtainRelativeGetMemberTarget(), name);
        }

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
        public virtual IMethodReferenceStub GetMethod(string name, ITypeCollection genericParameters)
        {
            return new UnboundMethodReferenceStub(this.ObtainRelativeGetMemberTarget(), name, genericParameters);
        }

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
        public virtual IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature)
        {
            return this.GetMethod(name).GetPointer(signature);
        }

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
        public virtual IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature, ITypeCollection genericParameters)
        {
            return this.GetMethod(name, genericParameters).GetPointer(signature);
        }

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
        public virtual IIndexerReferenceExpression GetIndexer(string name, params IExpression[] parameters)
        {
            return new UnboundIndexerReferenceExpression(name, parameters, this.ObtainRelativeGetMemberTarget());
        }

        /// <summary>
        /// Obtains a <see cref="IPropertyReferenceExpression"/>
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// denoting the name of the property to reference.</param>
        /// <returns>A <see cref="IPropertyReferenceExpression"/> related
        /// to the property described by <paramref name="name"/>.</returns>
        public virtual IPropertyReferenceExpression GetProperty(string name)
        {
            return new UnboundPropertyReferenceExpression(name, this.ObtainRelativeGetMemberTarget());
        }

        /// <summary>
        /// Gets a general case indexer with the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">The <see cref="IExpressionCollection"/>
        /// used to reference the indexer.</param>
        /// <returns></returns>
        /// <remarks>If the <see cref="TypeLookupAid"/> is an array type
        /// returns an array indexer where all parameter types
        /// must be a number; otherwise it returns
        /// an indexer with the default name defined by the language.</remarks>
        public virtual IIndexerReferenceExpression GetIndexer(params IExpression[] parameters)
        {
            return GetIndexer(null, parameters);
        }

        /// <summary>
        /// Returns the <see cref="IFieldReferenceExpression"/>
        /// relative to the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> of the 
        /// name of the field to retrieve.</param>
        /// <returns>A <see cref="IFieldReferenceExpression"/> 
        /// relative to the <paramref name="name"/>d field 
        /// that needs retrieved.</returns>
        public virtual IFieldReferenceExpression GetField(string name)
        {
            return new UnboundFieldReferenceExpression(name, this.ObtainRelativeGetMemberTarget());
        }

        /*
        /// <summary>
        /// Returns the type which is used as a spring
        /// point for obtaining and linking the members.
        /// </summary>
        /// <remarks>Necessary for every 
        /// <see cref="IMemberParentReferenceExpression"/>
        /// to have in order to properly link.</remarks>
        public abstract IType ForwardType { get; }
        */
        #endregion

        /// <summary>
        /// Returns whether the current type associated to the
        /// member parent reference enables static-only 
        /// light lookup.
        /// </summary>
        protected virtual bool IsStaticTarget { get { return false; } }

        protected virtual MemberParentReferenceExpressionBase ObtainRelativeGetMemberTarget()
        {
            return this;
        }

        internal virtual MethodReferenceType MethodReferenceType
        {
            get
            {
                return MethodReferenceType.VirtualMethodReference;
            }
        }

        public abstract void Visit(IExpressionVisitor visitor);

        protected virtual IType TypeLookupAid
        {
            get
            {
                return null;
            }
        }

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion
    }
}
