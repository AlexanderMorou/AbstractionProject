using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Utilities.Arrays;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class StrongNameKeyPairHelper
    {
        /// <summary>
        /// Constant used to represent the RSA algorithm's identifier.
        /// </summary>
        private const uint RSASigningAlgorithmId = 0x00002400U;
        /// <summary>
        /// Constant used to represent the SHA1 algorithm's identifier.
        /// </summary>
        private const uint SHA1HashAlgorithmId = 0x00008004U;
        /// <summary>
        /// The default number of bits for creating strong names.
        /// </summary>
        private const uint DefaultKeyBitSize = 0x00000400;
        /// <summary>
        /// The version number used in the strong name key files.
        /// </summary>
        private const byte StrongNameVersion = 0x00000002;
        /// <summary>
        /// The large chunk denominator.
        /// </summary>
        private const uint LargeChunkDenominator = 0x00000008;
        /// <summary>
        /// The small chunk denominator.
        /// </summary>
        internal const uint SmallChunkDenominator = 0x00000010;
        internal const uint ChunkCount = 0x00000009;
        /// <summary>
        /// The private key's uint ID, backwards RSA2 in uint form.
        /// </summary>
        private const uint PrivateKeyId = 0x32415352;
        /// The public key's uint ID, backwards RSA1 in uint form.
        private const uint PublicKeyId = 0x31415352;
        /// <summary>
        /// The general key header used for public and private
        /// keys.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Size = 20, Pack = 1)]
        private struct GeneralKeyHeader
        {
            public enum KeyTypes :
                byte
            {
                PublicKey = 0x00000006,
                PrivateKey = 0x00000007,
            }
            public enum KeyIdentifiers :
                uint
            {
                None = 0x00000000,
                PublicKey = 0x31415352,
                PrivateKey = 0x32415352,
            }
            KeyTypes keyType;
            byte version;
            ushort reserved;
            uint algorithmId;
            KeyIdentifiers keyTypeId;
            uint bitLength;
            uint exponent;

            /// <summary>
            /// Returns the <see cref="KeyTypes"/> value denoting whether the 
            /// key is public or private.
            /// </summary>
            public KeyTypes KeyType { get { return this.keyType; } }
            /// <summary>
            /// Returns the <see cref="Byte"/> value which denotes the 
            /// strong name file type version.
            /// </summary>
            /// <remarks>As of now, there is only one valid value: 0x2.</remarks>
            public byte Version { get { return this.version; } }
            /// <summary>
            /// Returns the cryptographic service provider algorithm identifier.
            /// </summary>
            /// <remarks>As of now, there is only one valid value: 0x2400</remarks>
            public uint AlgorithmId { get { return this.algorithmId; } }
            /// <summary>
            /// Returns the <see cref="KeyIdentifiers"/> value denoting the 
            /// <see cref="UInt32"/> value of <see cref="KeyType"/> in four-byte 
            /// string.
            /// </summary>
            public KeyIdentifiers KeyTypeId { get { return this.keyTypeId; } }
            /// <summary>
            /// Returns the <see cref="UInt32"/> value which determines the number of bits
            /// used to construct the cryptographic key used by the algorithm.
            /// </summary>
            public uint BitLength { get { return this.bitLength; } }
            /// <summary>
            /// Returns the <see cref="UInt32"/> value denoting the public exponent of the
            /// current key pair.
            /// </summary>
            public uint Exponent { get { return this.exponent; } }

            /// <summary>
            /// Creates a new <see cref="GeneralKeyHeader"/> with the
            /// <paramref name="reader"/> from which to read information from.
            /// </summary>
            /// <param name="reader">The <see cref="BinaryReader"/>
            /// from which to source the information from.</param>
            public GeneralKeyHeader(BinaryReader reader)
            {
                this.keyType = (KeyTypes) reader.ReadByte();
                this.version = reader.ReadByte();
                this.reserved = reader.ReadUInt16();
                this.algorithmId = reader.ReadUInt32();
                this.keyTypeId = (KeyIdentifiers) reader.ReadUInt32();
                this.bitLength = reader.ReadUInt32();
                this.exponent = reader.ReadUInt32();
            }

            /// <summary>
            /// Creates a new <see cref="GeneralKeyHeader"/> with the 
            /// <paramref name="keyType"/>, <paramref name="bitLength"/>
            /// and <paramref name="exponent"/> provided.
            /// </summary>
            /// <param name="keyType">The <see cref="KeyTypes"/> value which determines
            /// whether the <see cref="GeneralKeyHeader"/> is for a public or a private
            /// key.</param>
            /// <param name="bitLength">The <see cref="UInt32"/> which determines the number of bits
            /// used to construct the cryptographic key used by the algorithm.</param>
            /// <param name="exponent">The <see cref="UInt32"/> value denoting the public exponent of the
            /// current key pair.</param>
            public GeneralKeyHeader(KeyTypes keyType, uint bitLength, uint exponent)
            {
                switch (keyType)
                {
                    case KeyTypes.PublicKey:
                        this.keyTypeId = KeyIdentifiers.PublicKey;
                        break;
                    case KeyTypes.PrivateKey:
                        this.keyTypeId = KeyIdentifiers.PrivateKey;
                        break;
                    default:
                        this.keyTypeId = KeyIdentifiers.None;
                        break;
                }
                this.keyType = keyType;
                this.bitLength = bitLength;
                this.version = StrongNameVersion;
                this.reserved = 0;
                this.algorithmId = RSASigningAlgorithmId;
                this.exponent = exponent;
            }

            /// <summary>
            /// Writes information from the the <see cref="GeneralKeyHeader"/> 
            /// to the <paramref name="writer"/> provided.
            /// </summary>
            /// <param name="writer">The <see cref="BinaryWriter"/> in which to
            /// store the data of the <see cref="GeneralKeyHeader"/>'s information
            /// sequentially.</param>
            internal void Write(BinaryWriter writer)
            {
                writer.Write((byte) this.keyType);
                writer.Write(this.version);
                writer.Write(this.reserved);
                writer.Write(this.algorithmId);
                writer.Write((uint) this.keyTypeId);
                writer.Write(this.bitLength);
                writer.Write(this.exponent);
            }
        }

        /// <summary>
        /// Provides a structure for the <see cref="StrongNameKeyPair"/>'s public key 
        /// file.
        /// </summary>
        private class PublicKeyData :
            IStrongNamePublicKeyInfo
        {
            uint signingAlgorithm;
            uint hashAlgorithm;
            uint remainingSize;
            GeneralKeyHeader header;
            byte[] modulus;
            public void Read(BinaryReader reader)
            {
                this.signingAlgorithm = reader.ReadUInt32();
                this.hashAlgorithm = reader.ReadUInt32();
                this.remainingSize = reader.ReadUInt32();
                this.header = new GeneralKeyHeader(reader);
                modulus = reader.ReadBytes((int) (this.header.BitLength / LargeChunkDenominator));
                ReverseArray(modulus);
            }
            public void Write(BinaryWriter writer)
            {
                writer.Write(this.signingAlgorithm);
                writer.Write(this.hashAlgorithm);
                writer.Write(this.remainingSize);
                this.header.Write(writer);
                writer.Write(modulus.Reverse().ToArray(), 0, modulus.Length);
            }
            internal PublicKeyData() { }
            public PublicKeyData(RSAParameters parameters, int keySize)
            {
                this.signingAlgorithm = RSASigningAlgorithmId;
                this.hashAlgorithm = SHA1HashAlgorithmId;
                byte[] exponent = parameters.Exponent;
                byte[] exponentReal = new byte[4];
                exponent.CopyTo(exponentReal, 0);
                Array.Reverse(exponentReal);
                this.header = new GeneralKeyHeader(GeneralKeyHeader.KeyTypes.PublicKey, (uint) keySize, ((uint) exponentReal[0] | (uint) exponentReal[1] << 8 | (uint) exponentReal[2] << 16 | (uint) exponentReal[3] << 24));
                this.modulus = parameters.Modulus;
                this.remainingSize = (uint) (Marshal.SizeOf(typeof(GeneralKeyHeader)) + modulus.Length);
            }

            public static bool IsDataProperLength(byte[] data)
            {
                if (data.Length <= 32)
                    return false;
                int a = data[24], b = data[25], c = data[26], d = data[27];
                uint bitLength = (uint) (a | b << 8 | c << 16 | d << 24);
                var modulusLength = bitLength / LargeChunkDenominator;
                if (data.Length < 32 + modulusLength)
                    return false;
                return true;
            }

            public PublicKeyData(RSACryptoServiceProvider provider)
                : this(provider.ExportParameters(false), provider.KeySize)
            {
            }

            #region IStrongNamePublicKeyInfo Members

            public bool PrivateKeyAvailable
            {
                get { return false; }
            }

            public IStrongNamePrivateKeyInfo PrivateKey
            {
                get { throw new NotSupportedException(); }
            }

            public PublicKeyTokenData PublicToken
            {
                get
                {
                    byte[] sourceBytes = null;
                    using (MemoryStream targetStream = new MemoryStream())
                    {
                        BinaryWriter targetWriter = new BinaryWriter(targetStream);
                        this.Write(targetWriter);
                        sourceBytes = targetStream.ToArray();
                    }

                    byte[] resultHash = null;
                    using (SHA1 target = SHA1.Create())
                        resultHash = target.ComputeHash(sourceBytes);
                    int resultLength = resultHash.Length;
                    return new PublicKeyTokenData(resultHash[resultLength - 1], resultHash[resultLength - 2], resultHash[resultLength - 3], resultHash[resultLength - 4], resultHash[resultLength - 5], resultHash[resultLength - 6], resultHash[resultLength - 7], resultHash[resultLength - 8]);
                }
            }

            #endregion

            #region IStrongNameKeyInfo Members

            public StrongNameKeyInfoType InformationType
            {
                get { return StrongNameKeyInfoType.Public; }
            }

            public int KeySize
            {
                get { return (int) this.header.BitLength; }
            }

            public void WriteTo(string filename)
            {
                using (FileStream targetStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                using (BinaryWriter targetWriter = new BinaryWriter(targetStream))
                    this.Write(targetWriter);
            }

            #endregion
        }

        private static void ReverseArray<T>(T[] source)
        {
            for (int i = 0; i < source.Length / 2; i++)
            {
                int otherSide = source.Length - (i + 1);
                var temp = source[otherSide];
                source[otherSide] = source[i];
                source[i] = temp;
            }

        }
        /// <summary>
        /// Provides a structure for the <see cref="StrongNameKeyPair"/>'s private
        /// key file.
        /// </summary>
        private struct PrivateKeyData
        {
            /// <summary>
            /// Data member which contains the general key's
            /// header information.
            /// </summary>
            public GeneralKeyHeader header;
            /// <summary>
            /// Data member which contains the modulus of the
            /// Cryptography instance.
            /// </summary>
            byte[] modulus;
            /// <summary>
            /// Data member which contains the first portion of the
            /// prime aspect of the Cryptography instance.
            /// </summary>
            byte[] prime1;
            /// <summary>
            /// Data member which contains the second portion of the
            /// prime aspect of the Cryptography instance.
            /// </summary>
            byte[] prime2;
            /// <summary>
            /// Data member which contains the first portion of the
            /// exponent aspect of the Cryptography instance.
            /// </summary>
            byte[] exponent1;
            /// <summary>
            /// Data member which contains the second portion of the 
            /// exponent aspect of the Cryptography instance.
            /// </summary>
            byte[] exponent2;

            byte[] coefficient1;
            byte[] coefficient2;
            public void Read(BinaryReader reader)
            {
                this.header = new GeneralKeyHeader(reader);
                modulus = reader.ReadBytes((int) (this.header.BitLength / LargeChunkDenominator));
                prime1 = reader.ReadBytes((int) (this.header.BitLength / SmallChunkDenominator));
                prime2 = reader.ReadBytes((int) (this.header.BitLength / SmallChunkDenominator));
                exponent1 = reader.ReadBytes((int) (this.header.BitLength / SmallChunkDenominator));
                exponent2 = reader.ReadBytes((int) (this.header.BitLength / SmallChunkDenominator));
                coefficient1 = reader.ReadBytes((int) (this.header.BitLength / SmallChunkDenominator));
                coefficient2 = reader.ReadBytes((int) (this.header.BitLength / LargeChunkDenominator));
                Array.Reverse(modulus);
                Array.Reverse(prime1);
                Array.Reverse(prime2);
                Array.Reverse(exponent1);
                Array.Reverse(exponent2);
                Array.Reverse(coefficient1);
                Array.Reverse(coefficient2);
            }

            public PrivateKeyData(RSAParameters parameters, int keySize)
            {
                byte[] exponent = parameters.Exponent;
                byte[] exponentReal = new byte[4];
                exponent.CopyTo(exponentReal, 0);
                Array.Reverse(exponentReal);
                this.header = new GeneralKeyHeader(GeneralKeyHeader.KeyTypes.PrivateKey, (uint) keySize, ((uint) exponentReal[0] | (uint) exponentReal[1] << 8 | (uint) exponentReal[2] << 16 | (uint) exponentReal[3] << 24));
                this.modulus = parameters.Modulus;
                this.prime1 = parameters.P;
                this.prime2 = parameters.Q;
                this.exponent1 = parameters.DP;
                this.exponent2 = parameters.DQ;
                this.coefficient1 = parameters.InverseQ;
                this.coefficient2 = parameters.D;
            }

            public PrivateKeyData(RSACryptoServiceProvider provider)
                : this(provider.ExportParameters(true), provider.KeySize)
            {
            }

            public void Write(BinaryWriter writer)
            {
                this.header.Write(writer);
                writer.Write(modulus.Reverse().ToArray(), 0, modulus.Length);
                writer.Write(prime1.Reverse().ToArray(), 0, prime1.Length);
                writer.Write(prime2.Reverse().ToArray(), 0, prime2.Length);
                writer.Write(exponent1.Reverse().ToArray(), 0, exponent1.Length);
                writer.Write(exponent2.Reverse().ToArray(), 0, exponent2.Length);
                writer.Write(coefficient1.Reverse().ToArray(), 0, coefficient1.Length);
                writer.Write(coefficient2.Reverse().ToArray(), 0, coefficient2.Length);
            }


            public Tuple<int, byte[], bool> GetData()
            {
                RSAParameters parameters = new RSAParameters();
                parameters.Modulus = this.modulus;
                parameters.P = this.prime1;
                parameters.Q = this.prime2;
                parameters.DP = this.exponent1;
                parameters.DQ = this.exponent2;
                parameters.InverseQ = this.coefficient1;
                parameters.D = this.coefficient2;
                byte[] exponent = new byte[4];
                var exponentInt = this.header.Exponent;
                exponent[0] = (byte) ((exponentInt >> 24) & 0xFF);
                exponent[1] = (byte) ((exponentInt >> 16) & 0xFF);
                exponent[2] = (byte) ((exponentInt >> 8) & 0xFF);
                exponent[3] = (byte) (exponentInt & 0xFF);
                parameters.Exponent = exponent;
                return DataFromRSAParameters((int) this.header.BitLength, parameters);
            }
        }
        internal static RSACryptoServiceProvider strongNameEncoder;
        internal static readonly int strongNameBufferSize;
        static StrongNameKeyPairHelper()
        {
            strongNameEncoder = new RSACryptoServiceProvider();
            var keyData = LoadKeyData(Resources.RSAKeyPair);
            strongNameBufferSize = 350;
            strongNameEncoder.ImportParameters(keyData);
        }

        internal static RSAParameters LoadKeyData(byte[] keyData, bool o = false)
        {
            int bitSize = (keyData.Length - sizeof(int)) / (int) ChunkCount * (int) SmallChunkDenominator;
            int smallChunkSize = bitSize / (int) SmallChunkDenominator;
            byte[] pcepmc = new byte[smallChunkSize * 8];
            Array.ConstrainedCopy(keyData, 0, pcepmc, 0, pcepmc.Length);
            var pcep_mcT = ArrayExtensions.SeparateInterleave(pcepmc);
            var pc_epT = ArrayExtensions.SeparateInterleave(pcep_mcT.Item1);
            var pcT = ArrayExtensions.SeparateInterleave(pc_epT.Item1);
            var epT = ArrayExtensions.SeparateInterleave(pc_epT.Item2);
            var mcT = ArrayExtensions.SeparateInterleave(pcep_mcT.Item2);
            if (o)
                mcT = new Tuple<byte[], byte[]>(mcT.Item2.Take(mcT.Item2.Length - 1).ToArray(), mcT.Item1.Take(mcT.Item1.Length - 1).ToArray());
            byte[] c1 = new byte[smallChunkSize];
            Array.ConstrainedCopy(keyData, pcepmc.Length + 4, c1, 0, c1.Length);
            return new RSAParameters() { P = epT.Item2, Q = pcT.Item1, DP = c1, DQ = epT.Item1, InverseQ = pcT.Item2, D = mcT.Item2, Modulus = mcT.Item1, Exponent = new byte[] { keyData[pcepmc.Length], keyData[pcepmc.Length + 1], keyData[pcepmc.Length + 2], keyData[pcepmc.Length + 3] } };
        }

        internal static IStrongNameKeyInfo LoadStrongNameKeyData(string filename, bool @private = true)
        {
            using (FileStream targetStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                if (@private)
                {
                    PrivateKeyData targetData = new PrivateKeyData();
                    using (var targetReader = new BinaryReader(targetStream))
                        targetData.Read(targetReader);
                    var data = targetData.GetData();
                    StrongNamePrivateKeyInfo result = new StrongNamePrivateKeyInfo(data.Item3, data.Item2, data.Item1, (int) targetData.header.BitLength);
                    return result;
                }
                else
                {
                    PublicKeyData targetData = new PublicKeyData();
                    using (var targetReader = new BinaryReader(targetStream))
                        targetData.Read(targetReader);
                    return targetData;
                }
            }
        }

        private class PublicKeyStream :
            IStrongNamePublicKeyInfo
        {
            private byte[] dataStream;

            internal PublicKeyStream(byte[] data)
            {
                this.dataStream = data;
            }

            #region IStrongNamePublicKeyInfo Members

            public bool PrivateKeyAvailable
            {
                get { return false; }
            }

            public IStrongNamePrivateKeyInfo PrivateKey
            {
                get { throw new NotSupportedException(); }
            }

            public PublicKeyTokenData PublicToken
            {
                get
                {
                    return GetPublicKeyToken(this.dataStream);
                }
            }

            #endregion

            #region IStrongNameKeyInfo Members

            public StrongNameKeyInfoType InformationType
            {
                get { return StrongNameKeyInfoType.Public; }
            }

            public int KeySize
            {
                get { return this.dataStream.Length; }
            }

            public void WriteTo(string filename)
            {
                BinaryWriter bw = new BinaryWriter(new FileStream(filename, FileMode.Create, FileAccess.Write));
                bw.Write(this.dataStream, 0, this.dataStream.Length);
                bw.Close();
                bw.Dispose();
                bw.BaseStream.Close();
                bw.BaseStream.Dispose();

            }

            #endregion
        }

        private static PublicKeyTokenData GetPublicKeyToken(byte[] publicKey)
        {
            var targetSha = SHA1.Create();
            var targetHash = targetSha.ComputeHash(publicKey);
            byte[] result = new byte[8];
            for (int i = 0; i < 8; i++)
                result[i] = targetHash[targetHash.Length - 1 - i];
            return new PublicKeyTokenData(result[0], result[1], result[2], result[3], result[4], result[5], result[6], result[7]);
        }

        internal static IStrongNameKeyInfo LoadStrongNameKeyData(byte[] data, bool @private = true)
        {
            if (@private)
            {
                using (MemoryStream targetStream = new MemoryStream(data))
                {
                    PrivateKeyData targetData = new PrivateKeyData();
                    using (var targetReader = new BinaryReader(targetStream))
                        targetData.Read(targetReader);
                    var resultData = targetData.GetData();
                    StrongNamePrivateKeyInfo result = new StrongNamePrivateKeyInfo(resultData.Item3, resultData.Item2, resultData.Item1, (int) targetData.header.BitLength);
                    return result;
                }
            }
            else
            {
                if (PublicKeyData.IsDataProperLength(data))
                {
                    using (MemoryStream targetStream = new MemoryStream(data))
                    {
                        PublicKeyData targetData = new PublicKeyData();
                        using (var targetReader = new BinaryReader(targetStream))
                            targetData.Read(targetReader);
                        return targetData;
                    }
                }
                else
                {
                    return new PublicKeyStream(data);
                }
            }
        }

        internal static Tuple<int, byte[], bool> GetNewStrongNameData(int keySize)
        {
            return DataFromRSAParameters(keySize, new RSACryptoServiceProvider(keySize).ExportParameters(true));
        }

        private static Tuple<int, byte[], bool> DataFromRSAParameters(int keySize, RSAParameters parameters)
        {
            var dataStream = new MemoryStream();
            BinaryWriter keyWriter = new BinaryWriter(dataStream);
            var modulus = parameters.Modulus;
            var prime1 = parameters.P;
            var prime2 = parameters.Q;
            var exponent1 = parameters.DP;
            var exponent2 = parameters.DQ;
            var coefficient1 = parameters.InverseQ;
            var coefficient2 = parameters.D;
            byte[] paramExp = parameters.Exponent;
            byte[] publicExponentArray = new byte[4];
            paramExp.CopyTo(publicExponentArray, 0);
            ReverseArray(publicExponentArray);
            uint publicExponent = ((uint) publicExponentArray[0] | (uint) publicExponentArray[1] << 8 | (uint) publicExponentArray[2] << 16 | (uint) publicExponentArray[3] << 24);
            bool oddStroke = (keySize / 8) % 2 != 0;
            byte[] interleaved =
                ArrayExtensions.Interleave(
                    ArrayExtensions.Interleave(
                        ArrayExtensions.Interleave(
                            prime2,
                            coefficient1),
                        ArrayExtensions.Interleave(
                            exponent2,
                            prime1)),
                    ArrayExtensions.Interleave(
                    oddStroke ? coefficient2.Add((byte) 0) : modulus,
                    oddStroke ? modulus.Add((byte) 0) : coefficient2));

            keyWriter.Write(interleaved, 0, interleaved.Length);
            keyWriter.Write(publicExponent);
            keyWriter.Write(exponent1, 0, exponent1.Length);
            var data = dataStream.ToArray();
            keyWriter.Dispose();
            var result = EncryptRsaData(data);
            return new Tuple<int, byte[], bool>(result.Item1, result.Item2, oddStroke);
        }

        internal static StrongNameKeyPair CreateNewStrongNamePrivateKey(RSAParameters parameters, int keySize)
        {
            PrivateKeyData resultData = new PrivateKeyData(parameters, keySize);
            MemoryStream resultStream = new MemoryStream();
            var resultWriter = new BinaryWriter(resultStream);
            resultData.Write(resultWriter);
            return new StrongNameKeyPair(resultStream.ToArray());
        }

        internal static byte[] CreateNewPrivateKey(RSAParameters parameters, int keySize)
        {
            PrivateKeyData resultData = new PrivateKeyData(parameters, keySize);
            MemoryStream resultStream = new MemoryStream();
            var resultWriter = new BinaryWriter(resultStream);
            resultData.Write(resultWriter);
            return resultStream.ToArray();
        }

        internal static byte[] CreateNewPublicKey(RSAParameters parameters, int keySize)
        {
            PublicKeyData resultData = new PublicKeyData(parameters, keySize);
            MemoryStream resultStream = new MemoryStream();
            var resultWriter = new BinaryWriter(resultStream);
            resultData.Write(resultWriter);
            return resultStream.ToArray();
        }

        internal static byte[] DecryptRsaData(byte[] data, int chunkCount)
        {
            var chunks = data.Chunk(data.Length / chunkCount);
            for (int i = 0; i < chunks.Length; i++)
                chunks[i] = strongNameEncoder.Decrypt(chunks[i], true);
            return ArrayExtensions.MergeArrays(chunks);
        }

        internal static Tuple<int, byte[]> EncryptRsaData(byte[] data)
        {
            int length = data.Length;
            byte[][] chunks = data.Chunk(strongNameBufferSize);
            for (int i = 0, o = 0; i < chunks.Length; i++, o += strongNameBufferSize)
            {
                Array.ConstrainedCopy(data, o, chunks[i], 0, chunks[i].Length);
                chunks[i] = strongNameEncoder.Encrypt(chunks[i], true);
            }
            return new Tuple<int, byte[]>(chunks.Length, ArrayExtensions.MergeArrays(chunks));
        }

    }
}
