using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    internal class IndexerReferenceExpression :
        MemberParentReferenceExpressionBase,
        IIndexerReferenceExpression
    {
        private IndexerReferenceType indexerType;
        private MethodReferenceType referenceType;
        private string name;
        public IndexerReferenceExpression(string name, IExpressionCollection parameters, IMemberParentReferenceExpression source)
        {
            this.name = name;
            this.Parameters = new MalleableExpressionCollection(parameters);
            this.Source = source;
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

        public MethodReferenceType ReferenceType
        {
            get
            {
                return this.referenceType;
            }
            set
            {
                if (value == Expressions.MethodReferenceType.VirtualMethodReference &&
                    this.IndexerType == IndexerReferenceType.ArrayIndexer)
                    throw new ArgumentException("Cannot have a virtual method reference to an array indexer", "value");
                else 
                    this.referenceType = value;
            }
        }

        public IMalleableExpressionCollection Parameters { get; private set; }

        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        #region IMemberReferenceExpression Members

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == string.Empty)
                    throw new ArgumentOutOfRangeException("value", "May be null, but not empty.");
                this.name = value;
            }
        }

        #endregion

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.IndexerReference; }
        }

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", this.Source, string.Join(", ", this.Parameters));
        }
    }
}
