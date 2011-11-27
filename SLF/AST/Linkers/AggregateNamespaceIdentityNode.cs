using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public class AggregateNamespaceIdentityNode :
        AggregateNamespaceParentIdentityNode,
        IAggregateNamespaceIdentityNode
    {
        private IAggregateNamespaceParentIdentityNode parent;
        private INamespaceDeclaration[] namespaces;
        public AggregateNamespaceIdentityNode(IAggregateNamespaceParentIdentityNode parent, INamespaceDeclaration[] namespaces)
            : base(namespaces)
        {
            this.parent = parent;
        }

        #region IAggregateNamespaceIdentityNode Members

        public IEnumerable<INamespaceDeclaration> Namespaces
        {
            get
            {
                foreach (var @namespace in this.namespaces)
                    yield return @namespace;
            }
        }

        #endregion

        public override string Name
        {
            get { return this.Namespaces.First().Name; }
        }

        /// <summary>
        /// Returns the <see cref="AggregateIdentityKind"/> which denotes
        /// the kind of identity represented by the current
        /// <see cref="AggregateNamespaceIdentityNode"/>.
        /// </summary>
        /// <remarks>Returns <see cref="AggregateIdentityKind.Namespace"/>.</remarks>
        public override AggregateIdentityKind Kind
        {
            get { return AggregateIdentityKind.Namespace; }
        }

        public override void Dispose()
        {
            try
            {
                this.namespaces = null;
            }
            finally
            {
                base.Dispose();
            }
        }

        #region IAggregateNamespaceIdentityNode Members

        public new IAggregateNamespaceParentIdentityNode Parent
        {
            get { return this.parent; }
        }

        #endregion

        protected override IAggregateIdentityNode OnGetParent()
        {
            return this.Parent;
        }
    }
}
