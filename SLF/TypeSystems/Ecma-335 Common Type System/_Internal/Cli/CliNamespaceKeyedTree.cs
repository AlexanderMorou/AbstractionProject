using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

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

        public CliNamespaceKeyedTreeNode Add(uint partId, uint namespaceHeapIndex, int subspaceStart, int subspaceLength, ICliMetadataStringsHeaderAndHeap stringsSection)
        {
            var result = new CliNamespaceKeyedTreeNode(namespaceHeapIndex, subspaceStart, subspaceLength, stringsSection);
            base._Add(partId, result);
            return result;
        }
        public CliNamespaceKeyedTreeNode Add(uint partId, uint namespaceHeapIndex, int subspaceStart, int subspaceLength, CliMetadataTypeDefinitionTableRow[] namespaceTypes, ICliMetadataStringsHeaderAndHeap stringsSection)
        {
            var result = new CliNamespaceKeyedTreeNode(namespaceHeapIndex, subspaceStart, subspaceLength, namespaceTypes, stringsSection);
            base._Add(partId, result);
            return result;
        }

        internal virtual void PushModuleTypes(CliMetadataTypeDefinitionTableRow[] namespaceTypes)
        {
            if (this.NamespaceTypes == null)
                this.NamespaceTypes = namespaceTypes;
            else
            {
                int len = this.NamespaceTypes.Length;
                var copy = this.NamespaceTypes.EnsureMinimalSpaceExists((uint) len, (uint) namespaceTypes.Length, (uint) (len + namespaceTypes.Length));
                namespaceTypes.CopyTo(copy, len);
                Array.Sort<CliMetadataTypeDefinitionTableRow>(copy, CliAssembly._CompareTo_);
                this.NamespaceTypes = copy;
            }
        }
    }
}
