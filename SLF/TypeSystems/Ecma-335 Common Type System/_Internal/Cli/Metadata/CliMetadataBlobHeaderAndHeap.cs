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
using AllenCopeland.Abstraction.Slf.Cli.Metadata;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata
{
    internal sealed partial class CliMetadataBlobHeaderAndHeap :
        CliMetadataStreamHeader,
        IDisposable,
        ICliMetadataBlobHeaderAndHeap
    {
        private MemoryStream blobStream;
        private EndianAwareBinaryReader reader;
        private CliMetadataRoot metadataRoot;

        private Dictionary<uint, SmallBlobEntry> smallEntries = new Dictionary<uint, SmallBlobEntry>();
        private Dictionary<uint, MediumBlobEntry> mediumEntries = new Dictionary<uint, MediumBlobEntry>();
        private Dictionary<uint, LargeBlobEntry> largEntries = new Dictionary<uint, LargeBlobEntry>();

        public CliMetadataBlobHeaderAndHeap(CliMetadataStreamHeader originalHeader, CliMetadataRoot metadataRoot)
            : base(originalHeader)
        {
            this.metadataRoot = metadataRoot;
        }

        private bool ItemsEqual(byte[] item1, byte[] item2)
        {
            if (item1 == null && item2 != null ||
                item2 == null && item1 != null)
                return false;
            if (item1 == null && item2 == null)
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

        public T GetSignature<T>(SignatureKinds signatureKind, uint heapIndex)
            where T :
                ICliMetadataSignature
        {
            if (heapIndex >= this.Size)
                throw new ArgumentOutOfRangeException("heapIndex");

            SmallBlobEntry smallResult;
            MediumBlobEntry mediumResult;
            LargeBlobEntry largeResult;
            if (smallEntries.TryGetValue(heapIndex, out smallResult))
            {
                if (smallResult.Signature == null)
                    LoadSignatureGeneral(signatureKind, heapIndex, smallResult);
                return (T) smallResult.Signature;
            }
            else if (mediumEntries.TryGetValue(heapIndex, out mediumResult))
            {
                if (mediumResult.Signature == null)
                    LoadSignatureGeneral(signatureKind, heapIndex, mediumResult);
                return (T) mediumResult.Signature;
            }
            else if (!largEntries.TryGetValue(heapIndex, out largeResult))
                throw new IndexOutOfRangeException("heapIndex");
            else
            {
                if (largeResult.Signature == null)
                    LoadSignatureGeneral(signatureKind, heapIndex, largeResult);
                return (T) largeResult.Signature;
            }
            throw new ArgumentOutOfRangeException("heapIndex");
        }

        #region ICliMetadataBlobHeaderAndHeap Members


        public int SizeOf(uint heapIndex)
        {
            if (heapIndex >= this.Size)
                throw new ArgumentOutOfRangeException("index");
            SmallBlobEntry smallResult;
            MediumBlobEntry mediumResult;
            LargeBlobEntry largeResult;
            if (smallEntries.TryGetValue(heapIndex, out smallResult))
                return smallResult.Length;
            else if (mediumEntries.TryGetValue(heapIndex, out mediumResult))
            {
                return mediumResult.Length;
            }
            else if (largEntries.TryGetValue(heapIndex, out largeResult))
            {
                return largeResult.Length;
            }
            throw new IndexOutOfRangeException("heapIndex");

        }

        #endregion
        private void LoadSignatureGeneral(SignatureKinds signatureKind, uint heapIndex, _ICliMetadataBlobEntry entry)
        {
            uint offset = heapIndex + entry.LengthByteCount;

            this.reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            switch (signatureKind)
            {
                case SignatureKinds.MethodDefSig:
                    entry.Signature = SignatureParser.ParseMethodDefSig(this.reader, metadataRoot);
                    break;
                case SignatureKinds.MethodRefSig:
                    entry.Signature = SignatureParser.ParseMethodRefSig(this.reader, metadataRoot);
                    break;
                case SignatureKinds.FieldSig:
                    entry.Signature = SignatureParser.ParseFieldSig(this.reader, metadataRoot);
                    break;
                case SignatureKinds.PropertySig:
                    entry.Signature = SignatureParser.ParsePropertySig(this.reader, metadataRoot);
                    break;
                case SignatureKinds.StandaloneSignature:
                    entry.Signature = SignatureParser.ParseStandaloneSig(this.reader, metadataRoot);
                    break;
                case SignatureKinds.CustomModifier:
                    entry.Signature = SignatureParser.ParseCustomModifier(this.reader, metadataRoot);
                    break;
                case SignatureKinds.Param:
                    entry.Signature = SignatureParser.ParseParam(this.reader, metadataRoot);
                    break;
                case SignatureKinds.Type:
                    entry.Signature = SignatureParser.ParseType(this.reader, metadataRoot);
                    break;
                case SignatureKinds.ArrayShape:
                    entry.Signature = SignatureParser.ParseArrayShape(this.reader, metadataRoot);
                    break;
                case SignatureKinds.TypeSpec:
                    entry.Signature = SignatureParser.ParseTypeSpec(this.reader, metadataRoot);
                    break;
                case SignatureKinds.MethodSpec:
                    entry.Signature = SignatureParser.ParseMethodSpec(this.reader, metadataRoot);
                    break;
                case SignatureKinds.MemberRefSig:
                    entry.Signature = SignatureParser.ParseMemberRefSig(this.reader, metadataRoot);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("signatureKind");
            }
        }

        public ICliMetadataBlobEntry GetEntry(uint heapIndex)
        {
            SmallBlobEntry smallResult;
            MediumBlobEntry mediumResult;
            LargeBlobEntry largeResult;
            if (smallEntries.TryGetValue(heapIndex, out smallResult))
                return smallResult;
            else if (mediumEntries.TryGetValue(heapIndex, out mediumResult))
                return mediumResult;
            else if (largEntries.TryGetValue(heapIndex, out largeResult))
                return largeResult;
            throw new ArgumentOutOfRangeException("heapIndex");
        }

        public byte[] this[uint index]
        {
            get
            {
                if (index >= this.Size)
                    throw new ArgumentOutOfRangeException("index");
                SmallBlobEntry smallResult;
                MediumBlobEntry mediumResult;
                LargeBlobEntry largeResult;
                if (smallEntries.TryGetValue(index, out smallResult))
                {
                    if (smallResult.BlobData == null)
                        LoadBlobDataGeneral(index, smallResult);
                    return smallResult.BlobData;
                }
                else if (mediumEntries.TryGetValue(index, out mediumResult))
                {
                    if (mediumResult.BlobData == null)
                        LoadBlobDataGeneral(index, mediumResult);
                    return mediumResult.BlobData;
                }
                else if (largEntries.TryGetValue(index, out largeResult))
                {
                    if (largeResult.BlobData == null)
                        LoadBlobDataGeneral(index, largeResult);
                    return largeResult.BlobData;
                }
                throw new IndexOutOfRangeException("heapIndex");
            }
        }

        private byte[] LoadBlobDataGeneral(uint index, _ICliMetadataBlobEntry result)
        {
            if (result.BlobData == null)
            {
                this.reader.BaseStream.Seek(index + result.LengthByteCount, SeekOrigin.Begin);
                byte[] resultBlob = new byte[result.Length];
                reader.Read(resultBlob, 0, result.Length);
                result.BlobData = resultBlob;
                return resultBlob;
            }
            else
                return result.BlobData;
        }

        internal void Read(EndianAwareBinaryReader reader)
        {
            byte[] blobSectionData = new byte[this.Size];
            reader.Read(blobSectionData, 0, blobSectionData.Length);
            MemoryStream blobStream = new MemoryStream(blobSectionData);
            this.reader = new EndianAwareBinaryReader(blobStream, Endianness.LittleEndian, false);
            this.blobStream = blobStream;
            //this.reader = new EndianAwareBinaryReader(reader.BaseStream, Endianness.LittleEndian, false);
            byte firstNull = (byte) (this.reader.PeekByte() & 0xFF);
            if (firstNull != 0)
                throw new BadImageFormatException(string.Format("The first item of a {0} heap must be null.", this.Name));
            this.smallEntries.Add(0, new SmallBlobEntry(1) { BlobData = new byte[this.reader.ReadByte()] });
            byte currentItemLengthWidth;
            long currentPosition = 1;
            while (currentPosition < this.Size)
            {
                int currentLength = CliMetadataRoot.ReadCompressedUnsignedInt(this.reader, out currentItemLengthWidth);
                if (currentItemLengthWidth == 1)
                    this.smallEntries.Add((uint) currentPosition, new SmallBlobEntry((sbyte) currentLength));
                else if (currentItemLengthWidth == 2)
                    this.mediumEntries.Add((uint) currentPosition, new MediumBlobEntry((short) currentLength));
                else
                    this.largEntries.Add((uint) currentPosition, new LargeBlobEntry(currentLength));
                if (currentPosition + currentItemLengthWidth + currentLength > this.Size)
                    break;
                currentPosition += currentItemLengthWidth + currentLength;
                this.reader.BaseStream.Seek(currentLength, SeekOrigin.Current);

            }
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


        public void Dispose()
        {
            if (this.blobStream != null)
            {
                this.blobStream.Dispose();
                this.blobStream = null;
            }
            if (this.reader != null)
            {
                this.reader.Close();
                this.reader.Dispose();
                this.reader = null;
            }
            if (this.smallEntries != null)
            {
                this.smallEntries.Clear();
                this.smallEntries = null;
            }
            if (this.mediumEntries != null)
            {
                this.mediumEntries.Clear();
                this.mediumEntries = null;
            }
            if (this.largEntries != null)
            {
                this.largEntries.Clear();
                this.largEntries = null;
            }
        }


    }
}
