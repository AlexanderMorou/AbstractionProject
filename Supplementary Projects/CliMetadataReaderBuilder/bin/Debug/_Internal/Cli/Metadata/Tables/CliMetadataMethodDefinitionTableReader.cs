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
    /// Provides a table which defines information about the image's methods.
    /// </summary>
    internal class CliMetadataMethodDefinitionTableReader :
        CliMetadataLazyTable<ICliMetadataMethodDefinitionTableRow>, 
        ICliMetadataTable, 
        ICliMetadataMethodDefinitionTable
    {
        private ICliMetadataRoot metadataRoot;
        private EndianAwareBinaryReader reader;
        private object syncObject;
        private FileStream fStream;
        private uint rowCount;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataMethodDefinitionLockedTableRow"/>
        /// is 14 bytes long.
        /// </summary>
        internal const int __COR_METHODDEFINITION_CALC_SIZE_1__ = 14;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataMethodDefinitionLockedTableRow"/>
        /// is 16 bytes long.
        /// </summary>
        internal const int __COR_METHODDEFINITION_CALC_SIZE_2__ = 16;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataMethodDefinitionLockedTableRow"/>
        /// is 18 bytes long.
        /// </summary>
        internal const int __COR_METHODDEFINITION_CALC_SIZE_3__ = 18;
        /// <summary>
        /// Size constant used when the total size of the <see cref="CliMetadataMethodDefinitionLockedTableRow"/>
        /// is 20 bytes long.
        /// </summary>
        internal const int __COR_METHODDEFINITION_CALC_SIZE_4__ = 20;
        private int __size;
        private byte state;
        /// <summary>
        /// Data member which denotes where in the original stream the <see cref="CliMetadataMethodDefinitionTableReader"/>
        /// is.
        /// </summary>
        private long streamOffset;
        private long length;
        private bool fullyRead;
        public override CliMetadataTableKinds Kind
        {
            get
            {
                return CliMetadataTableKinds.MethodDefinition;
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
        /// Initializes the <see cref="CliMetadataMethodDefinitionTableReader"/> with the <paramref name="streamOffset"/>,<paramref name="blobHeapSize"/>,
        /// <paramref name="parameterSize"/>, and <paramref name="stringsHeapSize"/> provided.
        /// </summary>
        /// <param name="streamOffset">
        /// The <see cref="Int64"/> value which denotes where in the stream of <see cref="reader"/>
        /// the <see cref="CliMetadataMethodDefinitionTableReader"/> is.
        /// </param>
        /// <param name="blobHeapSize">
        /// The <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.CliMetadataReferenceIndexSize"/>
        /// used to denote the word size of the <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataBlobHeaderAndHeap"/>.
        /// </param>
        /// <param name="parameterSize">
        /// The <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.CliMetadataReferenceIndexSize"/>
        /// used to denote the word size of the <see cref="ICliMetadataParameterTable"/>.
        /// </param>
        /// <param name="stringsHeapSize">
        /// The <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.CliMetadataReferenceIndexSize"/>
        /// used to denote the word size of the <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataStringsHeaderAndHeap"/>.
        /// </param>
        internal void Initialize(long streamOffset, CliMetadataReferenceIndexSize blobHeapSize, CliMetadataReferenceIndexSize parameterSize, CliMetadataReferenceIndexSize stringsHeapSize)
        {
            this.streamOffset = streamOffset;
            if (blobHeapSize == CliMetadataReferenceIndexSize.DWord)
                if (parameterSize == CliMetadataReferenceIndexSize.DWord)
                    if (stringsHeapSize == CliMetadataReferenceIndexSize.DWord)
                    {
                        this.state = 7;
                        this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_4__;
                    }
                    else
                    {
                        this.state = 3;
                        this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_3__;
                    }
                else if (stringsHeapSize == CliMetadataReferenceIndexSize.DWord)
                {
                    this.state = 5;
                    this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_3__;
                }
                else
                {
                    this.state = 1;
                    this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_2__;
                }
            else if (parameterSize == CliMetadataReferenceIndexSize.DWord)
                if (stringsHeapSize == CliMetadataReferenceIndexSize.DWord)
                {
                    this.state = 6;
                    this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_3__;
                }
                else
                {
                    this.state = 2;
                    this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_2__;
                }
            else if (stringsHeapSize == CliMetadataReferenceIndexSize.DWord)
            {
                this.state = 4;
                this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_2__;
            }
            else
            {
                this.state = 0;
                this.__size = CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_1__;
            }
            this.length = this.__size * this.rowCount;
        }
        protected override ICliMetadataMethodDefinitionTableRow ReadElementAt(uint index)
        {
            bool gotLock = false;
            System.Threading.Monitor.Enter(this.syncObject, ref gotLock);
            if (index == 0)
                return null;
            this.fStream.Seek(this.streamOffset + (index - 1) * this.__size, SeekOrigin.Begin);
            uint rva = this.reader.ReadUInt32();
            MethodImplementationDetails implementationDetails = ((MethodImplementationDetails)(this.reader.ReadUInt16()));
            MethodUseDetails usageDetails = ((MethodUseDetails)(this.reader.ReadUInt16()));
            uint nameIndex;
            switch (this.state)
            {
                case 4: case 5: case 6: case 7:
                    nameIndex = this.reader.ReadUInt32();
                    break;
                default:
                    nameIndex = this.reader.ReadUInt16();
                    break;
            }
            uint signatureIndex;
            switch (this.state)
            {
                case 1: case 3: case 5: case 7:
                    signatureIndex = this.reader.ReadUInt32();
                    break;
                default:
                    signatureIndex = this.reader.ReadUInt16();
                    break;
            }
            uint parameterStartIndex;
            switch (this.state)
            {
                case 2: case 3: case 6: case 7:
                    parameterStartIndex = this.reader.ReadUInt32();
                    break;
                default:
                    parameterStartIndex = this.reader.ReadUInt16();
                    break;
            }
            if (gotLock)
                System.Threading.Monitor.Exit(this.syncObject);
            return new CliMetadataMethodDefinitionLockedTableRow(index, this.state, this.metadataRoot, rva, implementationDetails, usageDetails, nameIndex, signatureIndex, parameterStartIndex);
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
                    uint rva = this.reader.ReadUInt32();
                    MethodImplementationDetails implementationDetails = ((MethodImplementationDetails)(this.reader.ReadUInt16()));
                    MethodUseDetails usageDetails = ((MethodUseDetails)(this.reader.ReadUInt16()));
                    uint nameIndex;
                    switch (this.state)
                    {
                        case 4: case 5: case 6: case 7:
                            nameIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            nameIndex = this.reader.ReadUInt16();
                            break;
                    }
                    uint signatureIndex;
                    switch (this.state)
                    {
                        case 1: case 3: case 5: case 7:
                            signatureIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            signatureIndex = this.reader.ReadUInt16();
                            break;
                    }
                    uint parameterStartIndex;
                    switch (this.state)
                    {
                        case 2: case 3: case 6: case 7:
                            parameterStartIndex = this.reader.ReadUInt32();
                            break;
                        default:
                            parameterStartIndex = this.reader.ReadUInt16();
                            break;
                    }
                    base.InjectLoadedItem(new CliMetadataMethodDefinitionLockedTableRow(index, this.state, this.metadataRoot, rva, implementationDetails, usageDetails, nameIndex, signatureIndex, parameterStartIndex), index);
                }
                if (gotLock)
                    System.Threading.Monitor.Exit(this.syncObject);
            }
            this.fullyRead = true;
        }
        public CliMetadataMethodDefinitionTableReader(ICliMetadataRoot metadataRoot, Tuple<object, FileStream, EndianAwareBinaryReader> readerInfo, uint rowCount)
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