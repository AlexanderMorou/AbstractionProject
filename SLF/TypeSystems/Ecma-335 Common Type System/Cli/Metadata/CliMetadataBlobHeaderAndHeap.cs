using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.IO;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public sealed class CliMetadataBlobHeaderAndHeap :
        CliMetadataStreamHeader,
        IDisposable
    {

        private byte[][] blobCacheData;
        private int[] blobCacheHashCodes;
        private int count;
        private Dictionary<uint, uint> cachedPosToBlobDataIndexTable = new Dictionary<uint, uint>();
        private Dictionary<uint, uint> cachedPosToBlobInfoIndexTable = new Dictionary<uint, uint>();
        private MemoryStream blobStream;
        private EndianAwareBinaryReader reader;
        private uint[] itemOffsets;
        private uint[] itemLengths;
        private uint _count;
        internal Dictionary<uint, ICliMetadataSignature> signatures = new Dictionary<uint, ICliMetadataSignature>();

        public CliMetadataBlobHeaderAndHeap(CliMetadataStreamHeader originalHeader)
            : base(originalHeader)
        {
        }

        private bool ItemsEqual(byte[] item1, byte[] item2)
        {
            if (item1 == null && item2 != null ||
                item2 == null && item1 != null)
                return false;
            if (item1 == null &&
                item2 == null)
                return true;
            if (item1.Length != item2.Length)
                return false;
            for (int i = 0; i < item1.Length; i++)
                if (item1[i] != item2[i])
                    return false;
            return true;
        }

        private byte[] GetData(byte[] data)
        {
            return data;
        }

        internal T GetSignature<T>(SignatureKinds signatureKind, uint heapIndex, CliMetadataRoot metadataRoot)
            where T :
                ICliMetadataSignature
        {
            if (heapIndex >= this.Size)
                throw new ArgumentOutOfRangeException("heapIndex");
            ICliMetadataSignature result;
            if (!signatures.TryGetValue(heapIndex, out result))
            {
                uint offset;
                if (this.cachedPosToBlobInfoIndexTable.ContainsKey(heapIndex))
                    offset = this.itemOffsets[this.cachedPosToBlobInfoIndexTable[heapIndex]];
                if (heapIndex < this.Size)
                {
                    while (!this.cachedPosToBlobDataIndexTable.ContainsKey(heapIndex) &&
                            this.ReadDataSpan())
                        ;
                    uint offsetIndex;
                    if (this.cachedPosToBlobInfoIndexTable.TryGetValue(heapIndex, out offsetIndex))
                        offset = this.itemOffsets[offsetIndex];
                    else
                        throw new ArgumentOutOfRangeException("heapIndex");
                }
                else
                    throw new ArgumentOutOfRangeException("heapIndex");

                this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                switch (signatureKind)
                {
                    case SignatureKinds.MethodDefSig:
                        result = SignatureParser.ParseMethodDefSig(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.MethodRefSig:
                        result = SignatureParser.ParseMethodRefSig(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.FieldSig:
                        result = SignatureParser.ParseFieldSig(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.PropertySig:
                        result = SignatureParser.ParsePropertySig(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.StandaloneSignature:
                        result = SignatureParser.ParseStandaloneSig(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.CustomModifier:
                        result = SignatureParser.ParseCustomModifier(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.Param:
                        result = SignatureParser.ParseParam(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.Type:
                        result = SignatureParser.ParseType(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.ArrayShape:
                        result = SignatureParser.ParseArrayShape(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.TypeSpec:
                        result = SignatureParser.ParseTypeSpec(this.reader, metadataRoot);
                        break;
                    case SignatureKinds.MethodSpec:
                        result = SignatureParser.ParseMethodSpec(this.reader, metadataRoot);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("signatureKind");
                }
                signatures.Add(heapIndex, result);
            }
            return (T)result;
        }

        protected int GetHashCode(byte[] value)
        {
            int result = value.Length;
            for (int i = 0; i < value.Length; i++)
                if ((i & 3) == 0)
                    result ^= value[i] << 24;
                else if (i % 3 == 0)
                    result ^= value[i] << 16;
                else if ((i & 1) == 0)
                    result ^= value[i] << 8;
                else
                    result ^= value[i];
            return result;
        }

        public uint AddDataSpan(uint length, byte compressedByteSize)
        {
            this.itemOffsets = itemOffsets.EnsureSpaceExists((int) this._count, 2);
            this.itemLengths = itemLengths.EnsureSpaceExists((int) this._count, 2);
            uint offset = 0;
            if (this._count > 0)
                offset = this.itemOffsets[this._count];
            cachedPosToBlobInfoIndexTable.Add(offset, this._count);
            itemOffsets[this._count] = offset + compressedByteSize;
            itemLengths[this._count++] = length;
            itemOffsets[this._count] = offset + length + compressedByteSize;
            return (uint) this._count - 1;
        }

        public uint AddData(byte[] value, uint offset)
        {
            this.blobCacheData = this.blobCacheData.EnsureSpaceExists(count, 1);
            this.blobCacheHashCodes = this.blobCacheHashCodes.EnsureSpaceExists(count, 1);
            cachedPosToBlobDataIndexTable.Add(offset, (uint) count);
            this.blobCacheData[count] = value;
            this.blobCacheHashCodes[count] = GetHashCode(value);
            return (uint) count++;
        }

        public byte[] this[uint index]
        {
            get
            {
                if (index >= this.Size)
                    throw new ArgumentOutOfRangeException("index");
                uint offset;
                uint offsetIndex;
                if (this.cachedPosToBlobInfoIndexTable.TryGetValue(index, out offsetIndex))
                    offset = itemOffsets[offsetIndex];
                else if (index < this.Size)
                {
                    while (!this.cachedPosToBlobInfoIndexTable.ContainsKey(index) &&
                            this.ReadDataSpan())
                        ;
                    if (this.cachedPosToBlobInfoIndexTable.TryGetValue(index, out offsetIndex))
                        offset = this.itemOffsets[offsetIndex];
                    else
                        throw new ArgumentOutOfRangeException("index");
                }
                else
                    throw new ArgumentOutOfRangeException("index");
                uint cachedBlobIndex;
                if (cachedPosToBlobDataIndexTable.TryGetValue(index, out cachedBlobIndex))
                    return this.blobCacheData[cachedBlobIndex];
                else
                {
                    byte[] result = new byte[this.itemLengths[offsetIndex]];
                    this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    this.reader.Read(result, 0, result.Length);
                    this.AddData(result, index);
                    return result;
                }
            }
        }

        internal void Read(EndianAwareBinaryReader reader)
        {
            byte[] blobSectionData = new byte[this.Size];
            reader.Read(blobSectionData, 0, blobSectionData.Length);
            MemoryStream blobStream = new MemoryStream(blobSectionData);
            this.reader = new EndianAwareBinaryReader(blobStream, Endianness.LittleEndian, false);
            this.blobStream = blobStream;

            byte firstNull = (byte) (this.reader.PeekChar() & 0xFF);
            if (firstNull != 0)
                throw new BadImageFormatException(string.Format("The first item of a {0} heap must be null.", this.Name));
            this.AddDataSpan(byte.MinValue, 1);
        }

        private bool ReadDataSpan()
        {
            uint offset;
            if (this.itemLengths == null)
                offset = 0;
            else
                offset = this.itemOffsets[this._count];
            this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            int currentLength = 0;
            byte compressedIntBytes = 0;
            currentLength = CliMetadataRoot.ReadCompressedUnsignedInt(reader, out compressedIntBytes);
            if (currentLength + offset + compressedIntBytes >= this.Size)
                return false;
            this.AddDataSpan((uint) currentLength, compressedIntBytes);
            return true;
        }

        private uint _Size { get { return base.Size; } }

        public int IndexSize
        {
            get
            {
                if (this._Size > ushort.MaxValue)
                    return 4;
                return 2;
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (this.blobStream != null)
            {
                this.blobStream.Dispose();
                this.blobStream = null;
            }
            if (this.signatures != null)
            {
                this.signatures.Clear();
                this.signatures = null;
            }
        }

        #endregion
    }
}
