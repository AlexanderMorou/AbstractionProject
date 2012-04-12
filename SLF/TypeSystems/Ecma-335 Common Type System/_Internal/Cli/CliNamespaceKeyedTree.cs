using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceKeyedTree :
        ControlledKeyedTree<uint, uint, CliNamespaceKeyedTreeNode>
    {
        public CliMetadataTypeDefinitionTableRow[] NamespaceTypes { get; private set; }

        protected CliNamespaceKeyedTree() { }

        internal CliNamespaceKeyedTree(CliMetadataTypeDefinitionTableRow[] namespaceTypes)
        {
            this.NamespaceTypes = namespaceTypes;
        }

        public CliNamespaceKeyedTreeNode Add(uint partId, uint namespaceHeapIndex, int subspaceStart, int subspaceLength)
        {
            var result = new CliNamespaceKeyedTreeNode(namespaceHeapIndex, subspaceStart, subspaceLength);
            base._Add(partId, result);
            return result;
        }
        public CliNamespaceKeyedTreeNode Add(uint partId, uint namespaceHeapIndex, int subspaceStart, int subspaceLength, CliMetadataTypeDefinitionTableRow[] namespaceTypes)
        {
            var result = new CliNamespaceKeyedTreeNode(namespaceHeapIndex, subspaceStart, subspaceLength, namespaceTypes);
            base._Add(partId, result);
            return result;
        }
    }
}
