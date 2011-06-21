using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    internal class UnboundIndexerReferenceExpression :
        UnboundPropertyReferenceExpression,
        IIndexerReferenceExpression
    {
        private IndexerReferenceType indexerType;
        private string name;
        public UnboundIndexerReferenceExpression(string name, IEnumerable<IExpression> parameters, IMemberParentReferenceExpression source)
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
            get { return ExpressionKind.IndexerReference; }
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
