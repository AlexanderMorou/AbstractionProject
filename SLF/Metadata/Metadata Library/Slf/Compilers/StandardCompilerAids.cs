using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public static class StandardCompilerAids
    {
        public static byte[ ] DecompressByteStream(byte[ ] compressedData, int originalLength)
        {
            MemoryStream memoryStream = new MemoryStream();
            //memoryStream.SetLength(Math.Max(originalLength, compressedData.Length));
            memoryStream.Write(compressedData, 0, compressedData.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Decompress, true);
            byte[ ] result = new byte[ originalLength ];
            gzStream.Read(result, 0, originalLength);
            gzStream.Dispose();
            memoryStream.Dispose();
            return result;
        }

        public static byte[ ] CompressByteStream(byte[ ] decompressedData)
        {
            MemoryStream memoryStream = new MemoryStream();
            GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Compress, true);
            gzStream.Write(decompressedData, 0, decompressedData.Length);
            gzStream.Flush();
            gzStream.Dispose();
            memoryStream.Seek(0, SeekOrigin.Begin);
            BinaryReader memoryReader = new BinaryReader(memoryStream);
            byte[ ] result = memoryReader.ReadBytes((int) memoryStream.Length);
            memoryReader.Dispose();
            memoryStream.Dispose();
            return result;
        }

        public unsafe static int[ ] InitializeCompressedInt32Array(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(int);
            byte[ ] compressedData = new byte[ fullByteSize ];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt32Array(fullByteSize, compressedData);
        }

        public unsafe static decimal[ , , , , ] InitializeCompressedDecimalArray(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(decimal);
            if (fullByteSize % sizeof(int) != 0)
                throw new ArgumentException(string.Format("fullByteSize must be divisible by {0}, the size of the {1} primitive type.", sizeof(int), typeof(int)));
            byte[ ] compressedData = new byte[ originalByteSize ];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDecimalArray(length1, length2, length3, length4, length5, compressedData);
        }
        private unsafe static decimal[ ] ConvertByteArrayToDecimalArray(int length, byte[ ] compressedData)
        {
            decimal[ ] result = new decimal[ length ];
            fixed (decimal* resultPtr = result)
                CopyData(length * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        private unsafe static decimal[ , ] ConvertByteArrayToDecimalArray(int dimension1, int dimension2, byte[ ] compressedData)
        {
            decimal[ , ] result = new decimal[ dimension1, dimension2 ];
            fixed (decimal* resultPtr = result)
                CopyData(dimension1 * dimension2 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        private unsafe static decimal[ , , ] ConvertByteArrayToDecimalArray(int dimension1, int dimension2, int dimension3, byte[ ] compressedData)
        {
            decimal[ , , ] result = new decimal[ dimension1, dimension2, dimension3 ];
            fixed (decimal* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        private unsafe static decimal[ , , , ] ConvertByteArrayToDecimalArray(int dimension1, int dimension2, int dimension3, int dimension4, byte[ ] compressedData)
        {
            decimal[ , , , ] result = new decimal[ dimension1, dimension2, dimension3, dimension4 ];
            fixed (decimal* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        private unsafe static decimal[ , , , , ] ConvertByteArrayToDecimalArray(int dimension1, int dimension2, int dimension3, int dimension4, int dimension5, byte[ ] compressedData)
        {
            decimal[ , , , , ] result = new decimal[ dimension1, dimension2, dimension3, dimension4, dimension5 ];
            fixed (decimal* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * dimension5 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        private unsafe static sbyte[ ] ConvertByteArrayToSByteArray(int length, byte[ ] compressedData)
        {
            sbyte[ ] result = new sbyte[ length ];
            fixed (sbyte* resultPtr = result)
                CopyData(length, resultPtr, compressedData);
            return result;
        }
        private unsafe static sbyte[ , ] ConvertByteArrayToSByteArray(int dimension1, int dimension2, byte[ ] compressedData)
        {
            sbyte[ , ] result = new sbyte[ dimension1, dimension2 ];
            fixed (sbyte* resultPtr = result)
                CopyData(dimension1 * dimension2, resultPtr, compressedData);
            return result;
        }
        private unsafe static sbyte[ , , ] ConvertByteArrayToSByteArray(int dimension1, int dimension2, int dimension3, byte[ ] compressedData)
        {
            sbyte[ , , ] result = new sbyte[ dimension1, dimension2, dimension3 ];
            fixed (sbyte* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3, resultPtr, compressedData);
            return result;
        }
        private unsafe static sbyte[ , , , ] ConvertByteArrayToSByteArray(int dimension1, int dimension2, int dimension3, int dimension4, byte[ ] compressedData)
        {
            sbyte[ , , , ] result = new sbyte[ dimension1, dimension2, dimension3, dimension4 ];
            fixed (sbyte* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4, resultPtr, compressedData);
            return result;
        }
        private unsafe static sbyte[ , , , , ] ConvertByteArrayToSByteArray(int dimension1, int dimension2, int dimension3, int dimension4, int dimension5, byte[ ] compressedData)
        {
            sbyte[ , , , , ] result = new sbyte[ dimension1, dimension2, dimension3, dimension4, dimension5 ];
            fixed (sbyte* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * dimension5, resultPtr, compressedData);
            return result;
        }


        private unsafe static short[ ] ConvertByteArrayToInt16Array(int length, byte[ ] compressedData)
        {
            short[ ] result = new short[ length ];
            fixed (short* resultPtr = result)
                CopyData(length * sizeof(short), resultPtr, compressedData);
            return result;
        }
        private unsafe static short[ , ] ConvertByteArrayToInt16Array(int dimension1, int dimension2, byte[ ] compressedData)
        {
            short[ , ] result = new short[ dimension1, dimension2 ];
            fixed (short* resultPtr = result)
                CopyData(dimension1 * dimension2 * sizeof(short), resultPtr, compressedData);
            return result;
        }
        private unsafe static short[ , , ] ConvertByteArrayToInt16Array(int dimension1, int dimension2, int dimension3, byte[ ] compressedData)
        {
            short[ , , ] result = new short[ dimension1, dimension2, dimension3 ];
            fixed (short* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * sizeof(short), resultPtr, compressedData);
            return result;
        }
        private unsafe static short[ , , , ] ConvertByteArrayToInt16Array(int dimension1, int dimension2, int dimension3, int dimension4, byte[ ] compressedData)
        {
            short[ , , , ] result = new short[ dimension1, dimension2, dimension3, dimension4 ];
            fixed (short* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * sizeof(short), resultPtr, compressedData);
            return result;
        }
        private unsafe static short[ , , , , ] ConvertByteArrayToInt16Array(int dimension1, int dimension2, int dimension3, int dimension4, int dimension5, byte[ ] compressedData)
        {
            short[ , , , , ] result = new short[ dimension1, dimension2, dimension3, dimension4, dimension5 ];
            fixed (short* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * dimension5 * sizeof(short), resultPtr, compressedData);
            return result;
        }


        private unsafe static ushort[ ] ConvertByteArrayToUInt16Array(int length, byte[ ] compressedData)
        {
            ushort[ ] result = new ushort[ length ];
            fixed (ushort* resultPtr = result)
                CopyData(length * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        private unsafe static ushort[ , ] ConvertByteArrayToUInt16Array(int dimension1, int dimension2, byte[ ] compressedData)
        {
            ushort[ , ] result = new ushort[ dimension1, dimension2 ];
            fixed (ushort* resultPtr = result)
                CopyData(dimension1 * dimension2 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        private unsafe static ushort[ , , ] ConvertByteArrayToUInt16Array(int dimension1, int dimension2, int dimension3, byte[ ] compressedData)
        {
            ushort[ , , ] result = new ushort[ dimension1, dimension2, dimension3 ];
            fixed (ushort* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        private unsafe static ushort[ , , , ] ConvertByteArrayToUInt16Array(int dimension1, int dimension2, int dimension3, int dimension4, byte[ ] compressedData)
        {
            ushort[ , , , ] result = new ushort[ dimension1, dimension2, dimension3, dimension4 ];
            fixed (ushort* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        private unsafe static ushort[ , , , , ] ConvertByteArrayToUInt16Array(int dimension1, int dimension2, int dimension3, int dimension4, int dimension5, byte[ ] compressedData)
        {
            ushort[ , , , , ] result = new ushort[ dimension1, dimension2, dimension3, dimension4, dimension5 ];
            fixed (ushort* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * dimension5 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }

        private unsafe static int[ ] ConvertByteArrayToInt32Array(int length, byte[ ] compressedData)
        {
            int[ ] result = new int[ length ];
            fixed (int* resultPtr = result)
                CopyData(length * sizeof(int), resultPtr, compressedData);
            return result;
        }
        private unsafe static int[ , ] ConvertByteArrayToInt32Array(int dimension1, int dimension2, byte[ ] compressedData)
        {
            int[ , ] result = new int[ dimension1, dimension2 ];
            fixed (int* resultPtr = result)
                CopyData(dimension1 * dimension2 * sizeof(int), resultPtr, compressedData);
            return result;
        }
        private unsafe static int[ , , ] ConvertByteArrayToInt32Array(int dimension1, int dimension2, int dimension3, byte[ ] compressedData)
        {
            int[ , , ] result = new int[ dimension1, dimension2, dimension3 ];
            fixed (int* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * sizeof(int), resultPtr, compressedData);
            return result;
        }
        private unsafe static int[ , , , ] ConvertByteArrayToInt32Array(int dimension1, int dimension2, int dimension3, int dimension4, byte[ ] compressedData)
        {
            int[ , , , ] result = new int[ dimension1, dimension2, dimension3, dimension4 ];
            fixed (int* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * sizeof(int), resultPtr, compressedData);
            return result;
        }
        private unsafe static int[ , , , , ] ConvertByteArrayToInt32Array(int dimension1, int dimension2, int dimension3, int dimension4, int dimension5, byte[ ] compressedData)
        {
            int[ , , , , ] result = new int[ dimension1, dimension2, dimension3, dimension4, dimension5 ];
            fixed (int* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * dimension5 * sizeof(int), resultPtr, compressedData);
            return result;
        }

        private unsafe static uint[ ] ConvertByteArrayToUInt32Array(int length, byte[ ] compressedData)
        {
            uint[ ] result = new uint[ length ];
            fixed (uint* resultPtr = result)
                CopyData(length*sizeof(uint), resultPtr, compressedData);
            return result;
        }
        private unsafe static uint[ , ] ConvertByteArrayToUInt32Array(int dimension1, int dimension2, byte[ ] compressedData)
        {
            uint[ , ] result = new uint[ dimension1, dimension2 ];
            fixed (uint* resultPtr = result)
                CopyData(dimension1 * dimension2 * sizeof(uint), resultPtr, compressedData);
            return result;
        }
        private unsafe static uint[ , , ] ConvertByteArrayToUInt32Array(int dimension1, int dimension2, int dimension3, byte[ ] compressedData)
        {
            uint[ , , ] result = new uint[ dimension1, dimension2, dimension3 ];
            fixed (uint* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * sizeof(uint), resultPtr, compressedData);
            return result;
        }
        private unsafe static uint[ , , , ] ConvertByteArrayToUInt32Array(int dimension1, int dimension2, int dimension3, int dimension4, byte[ ] compressedData)
        {
            uint[ , , , ] result = new uint[ dimension1, dimension2, dimension3, dimension4 ];
            fixed (uint* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * sizeof(uint), resultPtr, compressedData);
            return result;
        }
        private unsafe static uint[ , , , , ] ConvertByteArrayToUInt32Array(int dimension1, int dimension2, int dimension3, int dimension4, int dimension5, byte[ ] compressedData)
        {
            uint[ , , , , ] result = new uint[ dimension1, dimension2, dimension3, dimension4, dimension5 ];
            fixed (uint* resultPtr = result)
                CopyData(dimension1 * dimension2 * dimension3 * dimension4 * dimension5 * sizeof(uint), resultPtr, compressedData);
            return result;
        }

        private static unsafe void CopyData(int fullByteSize, byte* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            fixed (byte* sourceFPtr = decompressedData)
            {
                byte* sourceDPtr = (byte*) sourceFPtr;
                byte* resultDPtr = dataPointer;
                for (int i = 0; i < fullByteSize; i++)
                    resultDPtr[ i ] = sourceDPtr[ i ];
            }
        }

        private static unsafe void CopyData(int fullByteSize, sbyte* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            fixed (byte* sourceFPtr = decompressedData)
            {
                sbyte* sourceDPtr = (sbyte*) sourceFPtr;
                sbyte* resultDPtr = dataPointer;
                for (int i = 0; i < fullByteSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }

        private static unsafe void CopyData(int fullByteSize, decimal* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(decimal);
            fixed (byte* sourceFPtr = decompressedData)
            {
                decimal* sourceDPtr = (decimal*) sourceFPtr;
                decimal* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }

        private static unsafe void CopyData(int fullByteSize, short* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(short);
            fixed (byte* sourceFPtr = decompressedData)
            {
                short* sourceDPtr = (short*) sourceFPtr;
                short* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }

        private static unsafe void CopyData(int fullByteSize, ushort* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(ushort);
            fixed (byte* sourceFPtr = decompressedData)
            {
                ushort* sourceDPtr = (ushort*) sourceFPtr;
                ushort* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }

        private static unsafe void CopyData(int fullByteSize, int* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(int);
            fixed (byte* sourceFPtr = decompressedData)
            {
                int* sourceDPtr = (int*) sourceFPtr;
                int* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }

        private static unsafe void CopyData(int fullByteSize, uint* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(uint);
            fixed (byte* sourceFPtr = decompressedData)
            {
                uint* sourceDPtr = (uint*) sourceFPtr;
                uint* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }
        private static unsafe void CopyData(int fullByteSize, long* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(long);
            fixed (byte* sourceFPtr = decompressedData)
            {
                long* sourceDPtr = (long*) sourceFPtr;
                long* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }
        private static unsafe void CopyData(int fullByteSize, ulong* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(ulong);
            fixed (byte* sourceFPtr = decompressedData)
            {
                ulong* sourceDPtr = (ulong*) sourceFPtr;
                ulong* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }
        private static unsafe void CopyData(int fullByteSize, float* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(float);
            fixed (byte* sourceFPtr = decompressedData)
            {
                float* sourceDPtr = (float*) sourceFPtr;
                float* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }
        private static unsafe void CopyData(int fullByteSize, double* dataPointer, byte[ ] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            int diminishedSize = fullByteSize / sizeof(double);
            fixed (byte* sourceFPtr = decompressedData)
            {
                double* sourceDPtr = (double*) sourceFPtr;
                double* resultDPtr = dataPointer;
                for (int i = 0; i < diminishedSize; i++, resultDPtr++, sourceDPtr++)
                    *(resultDPtr) = *(sourceDPtr);
            }
        }
    }
}
