using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CompiledNamespaceInfo
    {
        /// <summary>
        /// Returns the <see cref="UInt32"/> value denoting the 
        /// index within the strings heap of the <see cref="CompiledNamespaceInfo"/>.
        /// </summary>
        public uint NamespaceHeapIndex { get; private set; }

        //Is it a partial namespace betewen two full namespaces?
        public bool IsSubspace { get; private set; }
        //How long, of the longer namespace, is the namespace's substring?
        public int SubspaceLength { get; private set; }

        public int SubspaceStart { get; private set; }

        public CompiledNamespaceInfo(uint namespaceHeapIndex, bool subspace, int subspaceStart, int subspaceLength)
        {
            this.NamespaceHeapIndex = namespaceHeapIndex;
            this.IsSubspace = subspace;
            this.SubspaceLength = subspaceLength;
            this.SubspaceStart = subspaceStart;
        }

    }
}
