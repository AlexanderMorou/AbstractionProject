 /* ----------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.  |
 |  Version: 0.5.0.0                                          |
 |------------------------------------------------------------|
 |  To ensure the code works properly,                        |
 |  please do not make any changes to the file.               |
 |------------------------------------------------------------|
 |  The specific language is C♯                               |
 |  Sub-tool Name: C♯ Code Translator                         |
 |  Sub-tool Version: 1.0.0.0                                 |
 \---------------------------------------------------------- */
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.IO;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    /// <summary>
    /// Provides a table which defines information about imported modules.
    /// </summary>
    internal class CliMetadataModuleReferenceTableReader :
        CliMetadataLazyTable<ICliMetadataModuleReferenceTableRow>, 
        ICliMetadataTable, 
        ICliMetadataModuleReferenceTable
    {
        private ICliMetadataRoot metadataRoot;
        private EndianAwareBinaryReader reader;
        private object syncObject;
        private FileStream fStream;
        private uint rowCount;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataModuleReferenceLockedTableRow"/>
        /// is 2 bytes long.
        /// </summary>
        internal const int __COR_MODULEREFERENCE_CALC_SIZE_1__ = 2;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataModuleReferenceLockedTableRow"/>
        /// is 4 bytes long.
        /// </summary>
        internal const int __COR_MODULEREFERENCE_CALC_SIZE_2__ = 4;
        private int __size;
        private byte state;
        /// <summary>
        /// Data member which denotes where in the original stream the <see cref="CliMetadataModuleReferenceTableReader"/>
        /// is.
        /// </summary>
        private long streamOffset;
        private long length;
        private bool fullyRead;
        public override CliMetadataTableKinds Kind
        {
            get
            {
                return CliMetadataTableKinds.ModuleReference;
            }
        }
        public long Length
        {
            get
            {
                return this.length;
            }
        }
        /// <summary>
        /// Initializes the <see cref="CliMetadataModuleReferenceTableReader"/> with the <paramref name="streamOffset"/>,<paramref name="stringsHeapSize"/>
        /// provided.
        /// </summary>
        /// <param name="streamOffset">
        /// The <see cref="Int64"/> value which denotes where in the stream of <see cref="reader"/>
        /// the <see cref="CliMetadataModuleReferenceTableReader"/> is.
        /// </param>
        /// <param name="stringsHeapSize">
        /// The <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.CliMetadataReferenceIndexSize"/>
        /// used to denote the word size of the <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataStringsHeaderAndHeap"/>.
        /// </param>
        internal void Initialize(long streamOffset, CliMetadataReferenceIndexSize stringsHeapSize)
        {
            this.streamOffset = streamOffset;
            if (stringsHeapSize == CliMetadataReferenceIndexSize.DWord)
            {
                this.state = 1;
                this.__size = CliMetadataModuleReferenceTableReader.__COR_MODULEREFERENCE_CALC_SIZE_2__;
            }
            else
            {
                this.state = 0;
                this.__size = CliMetadataModuleReferenceTableReader.__COR_MODULEREFERENCE_CALC_SIZE_1__;
            }
            this.length = this.__size * this.rowCount;
        }
        protected override ICliMetadataModuleReferenceTableRow ReadElementAt(uint index)
        {
            bool gotLock = false;
            System.Threading.Monitor.Enter(this.syncObject, ref gotLock);
            if (index == 0)
                return null;
            this.fStream.Seek(this.streamOffset + (index - 1) * this.__size, SeekOrigin.Begin);
            uint nameIndex;
            switch (this.state)
            {
                case 1:
                    nameIndex = this.reader.ReadUInt32();
                    break;
                default:
                    nameIndex = this.reader.ReadUInt16();
                    break;
            }
            if (gotLock)
                System.Threading.Monitor.Exit(this.syncObject);
            return new CliMetadataModuleReferenceLockedTableRow(index, this.state, this.metadataRoot, nameIndex);
        }
        public override void Read()
        {
            if (this.fullyRead)
                return;
            for (uint index = 1; index <= this.Count; index++)
            {
                bool gotLock = false;
                System.Threading.Monitor.Enter(this.syncObject, ref gotLock);
                if (!base.ItemLoaded(index))
                {
                    this.fStream.Seek(this.streamOffset + (index - 1) * this.__size, SeekOrigin.Begin);
                    uint nameIndex;
                    switch (this.state)
                    {
                        case 1:
                            nameIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            nameIndex = this.reader.ReadUInt16();
                            break;
                    }
                    base.InjectLoadedItem(new CliMetadataModuleReferenceLockedTableRow(index, this.state, this.metadataRoot, nameIndex), index);
                }
                if (gotLock)
                    System.Threading.Monitor.Exit(this.syncObject);
            }
            this.fullyRead = true;
        }
        public CliMetadataModuleReferenceTableReader(ICliMetadataRoot metadataRoot, Tuple<object, FileStream, EndianAwareBinaryReader> readerInfo, uint rowCount)
            : base(metadataRoot, rowCount)
        {
            this.metadataRoot = metadataRoot;
            this.syncObject = readerInfo.Item1;
            this.fStream = readerInfo.Item2;
            this.reader = readerInfo.Item3;
            this.rowCount = rowCount;
        }
    };
};