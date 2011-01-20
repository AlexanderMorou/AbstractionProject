using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
    public class IndexerSignatureReferenceExpression<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
    MemberParentReferenceExpressionBase,
    IIndexerSignatureReferenceExpression<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
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
        private IndexerReferenceType indexerType;
        public IndexerSignatureReferenceExpression(TIntermediateIndexer member, IEnumerable<IExpression> parameters, IMemberParentReferenceExpression source)
            : base()
        {
            this.Source = source;
            this.Member = member;
            this.Parameters = new MalleableExpressionCollection(parameters);
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.IndexerReference; }
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

        #region IIndexerSignatureReferenceExpression<TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent> Members

        public TIntermediateIndexer Member { get; private set; }
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
            set
            {
                this.Member.Name = value;
            }
        }

        #endregion

        #region IPropertyReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IndexerSignatureReferenceExpression{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; set; }

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

    public class IndexerReferenceExpression<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> :
        MemberParentReferenceExpressionBase,
        IIndexerReferenceExpression<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
    {
        private IndexerReferenceType indexerType;
        public IndexerReferenceExpression(TIntermediateIndexer member, IEnumerable<IExpression> parameters, IMemberParentReferenceExpression source)
            : base()
        {
            this.Source = source;
            this.Member = member;
            this.Parameters = new MalleableExpressionCollection(parameters);
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.IndexerReference; }
        }

        #region IIndexerReferenceExpression Members

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

        #region IIndexerReferenceExpression<TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent> Members

        public TIntermediateIndexer Member { get; private set; }
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
            set
            {
                this.Member.Name = value;
            }
        }

        #endregion

        #region IPropertyReferenceExpression Members

        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IndexerReferenceExpression{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>,
        /// get/set methods, is.
        /// </summary>
        public MethodReferenceType ReferenceType { get; set; }

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

    internal class IndexerReferenceExpression :
        PropertyReferenceExpression,
        IIndexerReferenceExpression
    {
        private IndexerReferenceType indexerType;
        private string name;
        public IndexerReferenceExpression(string name, IEnumerable<IExpression> parameters, IMemberParentReferenceExpression source)
            : base(name, source)
        {
            this.name = name;
            this.Parameters = new MalleableExpressionCollection(parameters);
        }

        #region IIndexerReferenceExpression Members

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

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.IndexerReference; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", this.Source, string.Join(", ", this.Parameters));
        }
    }
}
