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
    /// Provides a table which defines the processor of the target machine of the reference
    /// assembly.
    /// </summary>
    /// <remarks>
    /// This record should not be emitted into any PE image; however if present, it should
    /// be treated as if all its fields were zero. Supported to ensure proper reading of the
    /// metadata.
    /// </remarks>
    internal class CliMetadataAssemblyRefProcessorTableReader :
        CliMetadataLazyTable<ICliMetadataAssemblyRefProcessorTableRow>, 
        ICliMetadataTable, 
        ICliMetadataAssemblyRefProcessorTable
    {
        private ICliMetadataRoot metadataRoot;
        private EndianAwareBinaryReader reader;
        private object syncObject;
        private FileStream fStream;
        private uint rowCount;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataAssemblyRefProcessorLockedTableRow"/>
        /// is 6 bytes long.
        /// </summary>
        internal const int __COR_ASSEMBLYREFPROCESSOR_CALC_SIZE_1__ = 6;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataAssemblyRefProcessorLockedTableRow"/>
        /// is 8 bytes long.
        /// </summary>
        internal const int __COR_ASSEMBLYREFPROCESSOR_CALC_SIZE_2__ = 8;
        private int __size;
        private byte state;
        /// <summary>
        /// Data member which denotes where in the original stream the <see cref="CliMetadataAssemblyRefProcessorTableReader"/>
        /// is.
        /// </summary>
        private long streamOffset;
        private long length;
        private bool fullyRead;
        public override CliMetadataTableKinds Kind
        {
            get
            {
                return CliMetadataTableKinds.AssemblyReferenceProcessor;
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
        /// Initializes the <see cref="CliMetadataAssemblyRefProcessorTableReader"/> with the
        /// <paramref name="streamOffset"/>,<paramref name="assemblyRefSize"/> provided.
        /// </summary>
        /// <param name="streamOffset">
        /// The <see cref="Int64"/> value which denotes where in the stream of <see cref="reader"/>
        /// the <see cref="CliMetadataAssemblyRefProcessorTableReader"/> is.
        /// </param>
        /// <param name="assemblyRefSize">
        /// The <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.CliMetadataReferenceIndexSize"/>
        /// used to denote the word size of the <see cref="ICliMetadataAssemblyRefTable"/>.
        /// </param>
        internal void Initialize(long streamOffset, CliMetadataReferenceIndexSize assemblyRefSize)
        {
            this.streamOffset = streamOffset;
            if (assemblyRefSize == CliMetadataReferenceIndexSize.DWord)
            {
                this.state = 1;
                this.__size = CliMetadataAssemblyRefProcessorTableReader.__COR_ASSEMBLYREFPROCESSOR_CALC_SIZE_2__;
            }
            else
            {
                this.state = 0;
                this.__size = CliMetadataAssemblyRefProcessorTableReader.__COR_ASSEMBLYREFPROCESSOR_CALC_SIZE_1__;
            }
            this.length = this.__size * this.rowCount;
        }
        protected override ICliMetadataAssemblyRefProcessorTableRow ReadElementAt(uint index)
        {
            bool gotLock = false;
            System.Threading.Monitor.Enter(this.syncObject, ref gotLock);
            if (index == 0)
                return null;
            this.fStream.Seek(this.streamOffset + (index - 1) * this.__size, SeekOrigin.Begin);
            uint processor = this.reader.ReadUInt32();
            uint assemblyRefIndex;
            switch (this.state)
            {
                case 1:
                    assemblyRefIndex = this.reader.ReadUInt32();
                    break;
                default:
                    assemblyRefIndex = this.reader.ReadUInt16();
                    break;
            }
            if (gotLock)
                System.Threading.Monitor.Exit(this.syncObject);
            return new CliMetadataAssemblyRefProcessorLockedTableRow(this.state, this.metadataRoot, processor, assemblyRefIndex);
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
                    uint processor = this.reader.ReadUInt32();
                    uint assemblyRefIndex;
                    switch (this.state)
                    {
                        case 1:
                            assemblyRefIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            assemblyRefIndex = this.reader.ReadUInt16();
                            break;
                    }
                    base.InjectLoadedItem(new CliMetadataAssemblyRefProcessorLockedTableRow(this.state, this.metadataRoot, processor, assemblyRefIndex), index);
                }
                if (gotLock)
                    System.Threading.Monitor.Exit(this.syncObject);
            }
            this.fullyRead = true;
        }
        public CliMetadataAssemblyRefProcessorTableReader(ICliMetadataRoot metadataRoot, Tuple<object, FileStream, EndianAwareBinaryReader> readerInfo, uint rowCount)
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