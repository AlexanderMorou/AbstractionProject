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
    /// Provides a table which defines information about the constants within the image.
    /// </summary>
    /// <remarks>
    /// The constants are defined and perhaps used within metadata; however, they are not
    /// referenceable through any IL instruction.  Compilers must fold the constants into
    /// the emitted IL.
    /// </remarks>
    internal class CliMetadataConstantTableReader :
        CliMetadataLazyTable<ICliMetadataConstantTableRow>, 
        ICliMetadataTable, 
        ICliMetadataConstantTable
    {
        private ICliMetadataRoot metadataRoot;
        private EndianAwareBinaryReader reader;
        private object syncObject;
        private FileStream fStream;
        private uint rowCount;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataConstantLockedTableRow"/>
        /// is 6 bytes long.
        /// </summary>
        internal const int __COR_CONSTANT_CALC_SIZE_1__ = 6;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataConstantLockedTableRow"/>
        /// is 8 bytes long.
        /// </summary>
        internal const int __COR_CONSTANT_CALC_SIZE_2__ = 8;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataConstantLockedTableRow"/>
        /// is 10 bytes long.
        /// </summary>
        internal const int __COR_CONSTANT_CALC_SIZE_3__ = 10;
        private int __size;
        private byte state;
        /// <summary>
        /// Data member which denotes where in the original stream the <see cref="CliMetadataConstantTableReader"/>
        /// is.
        /// </summary>
        private long streamOffset;
        private long length;
        private bool fullyRead;
        public override CliMetadataTableKinds Kind
        {
            get
            {
                return CliMetadataTableKinds.Constant;
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
        /// Initializes the <see cref="CliMetadataConstantTableReader"/> with the <paramref name="streamOffset"/>,<paramref name="blobHeapSize"/>,
        /// and <paramref name="hasConstantSize"/> provided.
        /// </summary>
        /// <param name="streamOffset">
        /// The <see cref="Int64"/> value which denotes where in the stream of <see cref="reader"/>
        /// the <see cref="CliMetadataConstantTableReader"/> is.
        /// </param>
        /// <param name="blobHeapSize">
        /// The <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.CliMetadataReferenceIndexSize"/>
        /// used to denote the word size of the <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataBlobHeaderAndHeap"/>.
        /// </param>
        /// <param name="hasConstantSize">
        /// The <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.CliMetadataReferenceIndexSize"/>
        /// used to denote the word size of the indices nwith the <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataHasConstantTag"/>
        /// encoding.
        /// </param>
        internal void Initialize(long streamOffset, CliMetadataReferenceIndexSize blobHeapSize, CliMetadataReferenceIndexSize hasConstantSize)
        {
            this.streamOffset = streamOffset;
            if (blobHeapSize == CliMetadataReferenceIndexSize.DWord)
                if (hasConstantSize == CliMetadataReferenceIndexSize.DWord)
                {
                    this.state = 3;
                    this.__size = CliMetadataConstantTableReader.__COR_CONSTANT_CALC_SIZE_3__;
                }
                else
                {
                    this.state = 1;
                    this.__size = CliMetadataConstantTableReader.__COR_CONSTANT_CALC_SIZE_2__;
                }
            else if (hasConstantSize == CliMetadataReferenceIndexSize.DWord)
            {
                this.state = 2;
                this.__size = CliMetadataConstantTableReader.__COR_CONSTANT_CALC_SIZE_2__;
            }
            else
            {
                this.state = 0;
                this.__size = CliMetadataConstantTableReader.__COR_CONSTANT_CALC_SIZE_1__;
            }
            this.length = this.__size * this.rowCount;
        }
        protected override ICliMetadataConstantTableRow ReadElementAt(uint index)
        {
            bool gotLock = false;
            System.Threading.Monitor.Enter(this.syncObject, ref gotLock);
            if (index == 0)
                return null;
            this.fStream.Seek(this.streamOffset + (index - 1) * this.__size, SeekOrigin.Begin);
            CliMetadataNativeTypes @type = ((CliMetadataNativeTypes)(this.reader.ReadUInt16()));
            uint parentIndex;
            CliMetadataHasConstantTag parentSource;
            switch (this.state)
            {
                case 2: case 3:
                    parentIndex = this.reader.ReadUInt32();
                    break;
                default:
                    parentIndex = this.reader.ReadUInt16();
                    break;
            }
            parentSource = ((CliMetadataHasConstantTag)(parentIndex & 3));
            parentIndex = parentIndex >> 2;
            uint valueIndex;
            switch (this.state)
            {
                case 1: case 3:
                    valueIndex = this.reader.ReadUInt32();
                    break;
                default:
                    valueIndex = this.reader.ReadUInt16();
                    break;
            }
            if (gotLock)
                System.Threading.Monitor.Exit(this.syncObject);
            return new CliMetadataConstantLockedTableRow(this.state, this.metadataRoot, @type, parentSource, parentIndex, valueIndex);
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
                    CliMetadataNativeTypes @type = ((CliMetadataNativeTypes)(this.reader.ReadUInt16()));
                    uint parentIndex;
                    CliMetadataHasConstantTag parentSource;
                    switch (this.state)
                    {
                        case 2: case 3:
                            parentIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            parentIndex = this.reader.ReadUInt16();
                            break;
                    }
                    parentSource = ((CliMetadataHasConstantTag)(parentIndex & 3));
                    parentIndex = parentIndex >> 2;
                    uint valueIndex;
                    switch (this.state)
                    {
                        case 1: case 3:
                            valueIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            valueIndex = this.reader.ReadUInt16();
                            break;
                    }
                    base.InjectLoadedItem(new CliMetadataConstantLockedTableRow(this.state, this.metadataRoot, @type, parentSource, parentIndex, valueIndex), index);
                }
                if (gotLock)
                    System.Threading.Monitor.Exit(this.syncObject);
            }
            this.fullyRead = true;
        }
        public CliMetadataConstantTableReader(ICliMetadataRoot metadataRoot, Tuple<object, FileStream, EndianAwareBinaryReader> readerInfo, uint rowCount)
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
