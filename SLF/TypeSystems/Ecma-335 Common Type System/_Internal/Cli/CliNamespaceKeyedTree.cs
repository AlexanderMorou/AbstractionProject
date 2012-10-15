using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceKeyedTree :
        ControlledKeyedTree<uint, uint, CliNamespaceKeyedTreeNode>,
        _ICliTypeParent
    {
        public IControlledCollection<ICliMetadataTypeDefinitionTableRow> _Types { get; private set; }

        protected CliNamespaceKeyedTree() { }

        internal CliNamespaceKeyedTree(ICliMetadataTypeDefinitionTableRow[] namespaceTypes)
        {
            this._Types = new ArrayReadOnlyCollection<ICliMetadataTypeDefinitionTableRow>(namespaceTypes);
        }

        public CliNamespaceKeyedTreeNode Add(uint partId, uint namespaceHeapIndex, int subspaceStart, int subspaceLength, ICliMetadataStringsHeaderAndHeap stringsSection)
        {
            var result = new CliNamespaceKeyedTreeNode(namespaceHeapIndex, subspaceStart, subspaceLength, stringsSection);
            base._Add(partId, result);
            return result;
        }
        public CliNamespaceKeyedTreeNode Add(uint partId, uint namespaceHeapIndex, int subspaceStart, int subspaceLength, ICliMetadataTypeDefinitionTableRow[] namespaceTypes, ICliMetadataStringsHeaderAndHeap stringsSection)
        {
            var result = new CliNamespaceKeyedTreeNode(namespaceHeapIndex, subspaceStart, subspaceLength, namespaceTypes, stringsSection);
            base._Add(partId, result);
            return result;
        }

        internal virtual void PushModuleTypes(ICliMetadataTypeDefinitionTableRow[] namespaceTypes)
        {
            if (this._Types == null)
                this._Types = new ArrayReadOnlyCollection<ICliMetadataTypeDefinitionTableRow>(namespaceTypes);
            else
            {
                int len = this._Types.Count;
                var copy = this._Types.ToArray().EnsureMinimalSpaceExists((uint) len, (uint) namespaceTypes.Length, (uint) (len + namespaceTypes.Length));
                namespaceTypes.CopyTo(copy, len);
                Array.Sort<ICliMetadataTypeDefinitionTableRow>(copy, CliAssembly._CompareTo_);
                this._Types = new ArrayReadOnlyCollection<ICliMetadataTypeDefinitionTableRow>(copy);
            }
        }

    }
}
