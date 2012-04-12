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

        /** 
         * <summary>Returns whether the <see cref="CliNamespaceInfo"/> represents
         * a sub-namespace which contains no types and acts as a bridge to get to
         * the full namespace(s).</summary>
         * **/
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

        /// <summary>
        /// Creates a new <see cref="CliNamespaceInfo"/> instance
        /// with the <paramref name="namespaceHeapIndex"/>, <paramref name="subspaceStart"/>,
        /// <paramref name="subspaceLength"/>, and <paramref name="namespaceTypes"/> provided.
        /// </summary>
        /// <param name="namespaceHeapIndex">The <see cref="UInt32"/> value which denotes
        /// the index within the <see cref="CliMetadataRoot.StringsHeap"/>.</param>
        /// <param name="subspaceStart">The <see cref="Int32"/> value which denotes the offset
        /// within the full namespace name that the current namespace information starts.</param>
        /// <param name="subspaceLength">The <see cref="Int32"/> value denoting how long the
        /// current namespace segment is.</param>
        /// <param name="namespaceTypes">The <see cref="CliMetadataTypeDefinitionTableRow"/> array
        /// which denotes the namespace's defined types.</param>
        /// <remarks>Namespace information with types are fully qualified namespaces.</remarks>
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
