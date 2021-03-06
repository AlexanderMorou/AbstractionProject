 /* -----------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.   |
 |  Version: 1.0.0.14276                                       |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v4.0.30319)  |
 |  Sub-tool Name: Abstraction's Old C♯ Code Translator        |
 |  Sub-tool Version: 1.0.0.14276                              |
 \----------------------------------------------------------- */
using System;
using System.IO;
using System.Threading;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    // Module: RootModule
    /// <summary>
    /// Provides a table which defines information about the data size and
    /// packing size of a type.
    /// </summary>
    internal class CliMetadataClassLayoutTableReader :
        CliMetadataLazyTable<ICliMetadataClassLayoutTableRow>,
        ICliMetadataTable,
        ICliMetadataClassLayoutTable
    {
        private ICliMetadataRoot metadataRoot;
        
        private EndianAwareBinaryReader reader;
        
        private object syncObject;
        
        private FileStream fStream;
        
        private uint rowCount;
        
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataClassLayoutLockedTableRow"/>
        /// is 8 bytes long.
        /// </summary>
        internal const int __COR_CLASSLAYOUT_CALC_SIZE_1__ = 8;
        
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataClassLayoutLockedTableRow"/>
        /// is 10 bytes long.
        /// </summary>
        internal const int __COR_CLASSLAYOUT_CALC_SIZE_2__ = 10;
        
        private int __size;
        
        private byte state;
        
        /// <summary>
        /// Data member which denotes where in the original stream the <see cref="CliMetadataClassLayoutTableReader"/>
        /// is.
        /// </summary>
        private long streamOffset;
        
        private long length;
        
        private bool fullyRead;
        public override sealed CliMetadataTableKinds Kind
        {
            get
            {
                return CliMetadataTableKinds.ClassLayout;
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
        /// Initializes the <see cref="CliMetadataClassLayoutTableReader"/> with the <paramref name="streamOffset"/>,<paramref name="typeDefinitionSize"/>
        /// provided.
        /// </summary>
        /// <param name="streamOffset">The <see cref="Int64"/> value which denotes where in the stream of <see cref="reader"/>
        /// the <see cref="CliMetadataClassLayoutTableReader"/> is.</param>
        /// <param name="typeDefinitionSize">The <see cref="CliMetadataReferenceIndexSize"/> used to denote the word size
        /// of the <see cref="ICliMetadataTypeDefinitionTable"/>.</param>
        internal void Initialize(long streamOffset, CliMetadataReferenceIndexSize typeDefinitionSize)
        {
            this.streamOffset = streamOffset;
            if (typeDefinitionSize == CliMetadataReferenceIndexSize.DWord)
            {
                this.state = 1;
                this.__size = __COR_CLASSLAYOUT_CALC_SIZE_2__;
            }
            else
            {
                this.state = 0;
                this.__size = __COR_CLASSLAYOUT_CALC_SIZE_1__;
            }
            this.length = (this.__size * this.rowCount);
        }
        
        protected override sealed ICliMetadataClassLayoutTableRow ReadElementAt(uint index)
        {
            bool gotLock = false;
            Monitor.Enter(this.syncObject, ref gotLock);
            if (index == 0)
                return null;
            this.fStream.Seek((this.streamOffset + ((index - 1) * this.__size)), SeekOrigin.Begin);
            ushort packingSize = this.reader.ReadUInt16();
            uint classSize = this.reader.ReadUInt32();
            uint parentIndex;
            switch (this.state)
            {
                case 1:
                    parentIndex = this.reader.ReadUInt32();
                    break;
                default:
                    parentIndex = this.reader.ReadUInt16();
                    break;
            }
            if (gotLock)
                Monitor.Exit(this.syncObject);
            return new CliMetadataClassLayoutLockedTableRow(this.state, this.metadataRoot, packingSize, 
                classSize, parentIndex);
        }
        
        public override sealed void Read()
        {
            if (this.fullyRead)
                return;
            for(uint index = 1; (index <= this.Count); index++)
            {
                bool gotLock = false;
                Monitor.Enter(this.syncObject, ref gotLock);
                if (!(base.ItemLoaded(index)))
                {
                    this.fStream.Seek((this.streamOffset + ((index - 1) * this.__size)), SeekOrigin.Begin);
                    ushort packingSize = this.reader.ReadUInt16();
                    uint classSize = this.reader.ReadUInt32();
                    uint parentIndex;
                    switch (this.state)
                    {
                        case 1:
                            parentIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            parentIndex = this.reader.ReadUInt16();
                            break;
                    }
                    base.InjectLoadedItem(new CliMetadataClassLayoutLockedTableRow(this.state, this.metadataRoot, packingSize, 
                            classSize, parentIndex), index);
                }
                if (gotLock)
                    Monitor.Exit(this.syncObject);
            }
            this.fullyRead = true;
        }
        public CliMetadataClassLayoutTableReader(ICliMetadataRoot metadataRoot, Tuple<object, FileStream, EndianAwareBinaryReader> readerInfo, uint rowCount)
             : base(metadataRoot, rowCount)
        {
            this.metadataRoot = metadataRoot;
            this.syncObject = readerInfo.Item1;
            this.fStream = readerInfo.Item2;
            this.reader = readerInfo.Item3;
            this.rowCount = rowCount;
        }
    }
}
