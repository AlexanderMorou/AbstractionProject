using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceKeyedTreeNode :
        CliNamespaceKeyedTree,
        IControlledKeyedTreeNode<uint, uint, CliNamespaceKeyedTreeNode>
    {
        public bool IsSubspace { get; private set; }
        public int SubspaceLength { get; private set; }
        public int SubspaceStart { get; private set; }
        public ICliMetadataStringsHeaderAndHeap StringsSection { get; private set; }

        public CliNamespaceKeyedTreeNode(uint namespaceHeapIndex, int subspaceStart, int subspaceLength, ICliMetadataStringsHeaderAndHeap stringsSection)
            : base()
        {
            this.Value= namespaceHeapIndex;
            this.IsSubspace = true;
            this.SubspaceLength = subspaceLength;
            this.SubspaceStart = subspaceStart;
            this.StringsSection = stringsSection;
        }

        /// <summary>
        /// Creates a new <see cref="CliNamespaceKeyedTreeNode"/> instance
        /// with the <paramref name="namespaceHeapIndex"/>, <paramref name="subspaceStart"/>,
        /// <paramref name="subspaceLength"/>, and <paramref name="namespaceTypes"/> provided.
        /// </summary>
        /// <param name="namespaceHeapIndex">The <see cref="UInt32"/> value which denotes
        /// the index within the <see cref="CliMetadataRoot.StringsHeap"/>.</param>
        /// <param name="subspaceStart">The <see cref="Int32"/> value which denotes the offset
        /// within the full namespace name that the current namespace information starts.</param>
        /// <param name="subspaceLength">The <see cref="Int32"/> value denoting how long the
        /// current namespace segment is.</param>
        /// <param name="namespaceTypes">The <see cref="ICliMetadataTypeDefinitionTableRow"/> array
        /// which denotes the namespace's defined types.</param>
        /// <remarks>Namespace information with types are fully qualified namespaces.</remarks>
        internal CliNamespaceKeyedTreeNode(uint namespaceHeapIndex, int subspaceStart, int subspaceLength, ICliMetadataTypeDefinitionTableRow[] namespaceTypes, ICliMetadataStringsHeaderAndHeap stringsSection)
            : base(namespaceTypes)
        {
            this.Value = namespaceHeapIndex;
            this.IsSubspace = false;
            this.SubspaceLength = subspaceLength;
            this.SubspaceStart = subspaceStart;
            this.StringsSection = stringsSection;
        }


        //#region IKeyedTreeNode<uint,uint,CliNamespaceKeyedTreeNode> Members

        public uint Value { get; private set; }

        //#endregion

        internal override void PushModuleTypes(ICliMetadataTypeDefinitionTableRow[] namespaceTypes)
        {
            this.IsSubspace = false;
            base.PushModuleTypes(namespaceTypes);
        }
    }
}
