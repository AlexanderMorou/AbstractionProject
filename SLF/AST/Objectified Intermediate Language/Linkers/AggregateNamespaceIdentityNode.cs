using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public class AggregateNamespaceIdentityNode :
        AggregateIdentitySetNode<string, IAggregateIdentityNode>,
        IAggregateNamespaceIdentityNode
    {
        private string[] identifiers;
        /// <summary>
        /// The data member for <see cref="Namespaces"/>.
        /// </summary>
        private INamespaceDeclaration[] namespaces;
        /// <summary>
        /// Creates a new <see cref="AggregateNamespaceIdentityNode"/>
        /// with the <paramref name="namespaces"/> provided.
        /// </summary>
        /// <param name="namespaces">The <see cref="INamespaceDeclaration"/> array
        /// which represents the identities that make up the aggregate namespace identity.</param>
        internal AggregateNamespaceIdentityNode(INamespaceDeclaration[] namespaces) 
        {
            this.namespaces = namespaces;
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

    }
}
