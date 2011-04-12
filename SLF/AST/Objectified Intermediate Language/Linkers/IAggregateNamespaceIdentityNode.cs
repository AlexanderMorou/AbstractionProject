using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public interface IAggregateNamespaceIdentityNode :
        IAggregateNamespaceParentIdentityNode, 
        IControlledStateDictionary<string, IAggregateIdentityNode>
    {
        /// <summary>
        /// Returns the <see cref="INamespaceDeclaration"/> set associated to the 
        /// aggregated identity.
        /// </summary>
        IEnumerable<INamespaceDeclaration> Namespaces { get; }
        /// <summary>
        /// Returns the <see cref="IAggregateNamespaceParentIdentityNode"/> which
        /// represents the identity which contains the current
        /// <see cref="IAggregateNamespaceIdentityNode"/>.
        /// </summary>
        new IAggregateNamespaceParentIdentityNode Parent { get; }
    }
}
