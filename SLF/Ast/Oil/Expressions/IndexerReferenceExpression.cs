using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class IndexerSignatureReferenceExpression<TIndexer, TIndexerParent> :
    MemberParentReferenceExpressionBase,
    IIndexerSignatureReferenceExpression<TIndexer, TIndexerParent>
        where TIndexer :
            IIndexerSignatureMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerSignatureParent<TIndexer, TIndexerParent>
    {
        private IndexerReferenceType indexerType;
        public IndexerSignatureReferenceExpression(TIndexer member, IEnumerable<IExpression> parameters, IMemberParentReferenceExpression source, MethodReferenceType referenceType = MethodReferenceType.VirtualMethodReference, IndexerReferenceType indexerType = IndexerReferenceType.InferredIndexer)
            : base()
        {
            this.Source = source;
            this.Member = member;
            this.Parameters = new MalleableExpressionCollection(parameters);
            this.ReferenceType = referenceType;
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.IndexerReference; }
        }

        #region IIndexerSignatureReferenceExpression Members

        public IndexerReferenceType IndexerType
        {
            get
            {
                return this.indexerType;
            }
            set
            {
                this.indexerType = value;
                if (value == IndexerReferenceType.ArrayIndexer &&
                    this.ReferenceType == Expressions.MethodReferenceType.VirtualMethodReference)
                    this.ReferenceType = MethodReferenceType.StandardMethodReference;
            }
        }

        public IMalleableExpressionCollection Parameters { get; private set; }

        #endregion

        #region IIndexerSignatureReferenceExpression<TIndexer,TIndexerParent> Members

        public TIndexer Member { get; private set; }
        #endregion

        #region IBoundMemberReference Members

        public IType MemberType
        {
            get { return this.Member.PropertyType; }
        }

        IMember IBoundMemberReference.Member
        {
            get { return this.Member; ; }
        }

        #endregion

        #region IMemberReferenceExpression Members

        /// <summary>
        /// Returns/sets the name of the member to reference.
        /// </summary>
        public string Name
        {
            get
            {
                return this.Member.Name;
            }
        }

        #endregion

        #region IPropertyReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IndexerSignatureReferenceExpression{TIndexer, TIndexerParent}"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; private set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> which leads up to the
        /// indexer.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class IndexerReferenceExpression<TIndexer, TIndexerParent> :
        MemberParentReferenceExpressionBase,
        IIndexerReferenceExpression<TIndexer, TIndexerParent>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
    {
        private IndexerReferenceType indexerType;
        public IndexerReferenceExpression(TIndexer member, IEnumerable<IExpression> parameters, IMemberParentReferenceExpression source, MethodReferenceType referenceType = MethodReferenceType.VirtualMethodReference, IndexerReferenceType indexerType = IndexerReferenceType.InferredIndexer)
            : base()
        {
            this.Source = source;
            this.Member = member;
            this.Parameters = new MalleableExpressionCollection(parameters);
            this.ReferenceType = referenceType;
            this.IndexerType = indexerType;
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.IndexerReference; }
        }

        #region IIndexerReferenceExpression Members

        public IndexerReferenceType IndexerType
        {
            get
            {
                return this.indexerType;
            }
            private set
            {
                this.indexerType = value;
                if (value == IndexerReferenceType.ArrayIndexer &&
                    this.ReferenceType == MethodReferenceType.VirtualMethodReference)
                    this.ReferenceType = MethodReferenceType.StandardMethodReference;
            }
        }

        public IMalleableExpressionCollection Parameters { get; private set; }

        #endregion

        #region IIndexerReferenceExpression<TIndexer,TIndexerParent> Members

        public TIndexer Member { get; private set; }
        #endregion

        #region IBoundMemberReference Members

        public IType MemberType
        {
            get { return this.Member.PropertyType; }
        }

        IMember IBoundMemberReference.Member
        {
            get { return this.Member; ; }
        }

        #endregion

        #region IMemberReferenceExpression Members

        /// <summary>
        /// Returns/sets the name of the member to reference.
        /// </summary>
        public string Name
        {
            get
            {
                return this.Member.Name;
            }
        }

        #endregion

        #region IPropertyReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IndexerReferenceExpression{TIndexer, TIndexerParent}"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; private set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> which leads up to the
        /// indexer.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

}
