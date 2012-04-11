using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliNamespaceInfo
    {
        /// <summary>
        /// Returns the <see cref="UInt32"/> value denoting the 
        /// index within the strings heap of the <see cref="CliNamespaceInfo"/>.
        /// </summary>
        public uint NamespaceHeapIndex { get; private set; }

        //Is it a partial namespace betewen two full namespaces?
        public bool IsSubspace { get; private set; }
        //How long, of the longer namespace, is the namespace's substring?
        public int SubspaceLength { get; private set; }

        public int SubspaceStart { get; private set; }

        public CliMetadataTypeDefinitionTableRow[] NamespaceTypes { get; private set; }

        public CliNamespaceInfo(uint namespaceHeapIndex, int subspaceStart, int subspaceLength)
        {
            this.NamespaceHeapIndex = namespaceHeapIndex;
            this.IsSubspace = true;
            this.SubspaceLength = subspaceLength;
            this.SubspaceStart = subspaceStart;
            this.NamespaceTypes = null;
        }
        public CliNamespaceInfo(uint namespaceHeapIndex, int subspaceStart, int subspaceLength, CliMetadataTypeDefinitionTableRow[] namespaceTypes)
        {
            this.NamespaceHeapIndex = namespaceHeapIndex;
            this.IsSubspace = false;
            this.SubspaceLength = subspaceLength;
            this.SubspaceStart = subspaceStart;
            this.NamespaceTypes = namespaceTypes;
        }

    }
}
