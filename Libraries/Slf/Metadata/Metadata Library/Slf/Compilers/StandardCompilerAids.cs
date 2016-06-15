using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public static class StandardCompilerAids
    {

        public static T GetInternalBridge<T>(Type target, Type bridge, Guid bridgeGuid)
        {
            var bridgeAttribute = (from attr in target.GetCustomAttributes(true).Cast<HasStructuralTypeBridgeAttribute>()
                                   where attr.BridgeCreatable &&
                                         attr.BridgeGuidValid &&
                                         attr.BridgeGuid == bridgeGuid
                                   select attr).FirstOrDefault();
            if (bridgeAttribute == null)
                return default(T);
            if (bridge.IsAssignableFrom(bridgeAttribute.Bridge))
                return (T)Activator.CreateInstance(bridgeAttribute.Bridge);
            return default(T);
        }

        public static byte[] DecompressByteStream(this byte[] compressedData, int originalLength = -1)
        {
            MemoryStream memoryStream = new MemoryStream(compressedData);
            //memoryStream.SetLength(Math.Max(originalLength, compressedData.Length));
            GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Decompress, true);
            byte[] resultBuffer = null;
            if (originalLength > -1)
            {
                resultBuffer = new byte[originalLength];
                gzStream.Read(resultBuffer, 0, originalLength);
            }
            else
            {
                const int chunkSize = 4096;
                MemoryStream result = new MemoryStream();
                int bytesRead = 0;
                byte[] buffer = new byte[chunkSize];
                do
                {
                    if (bytesRead > 0)
                        result.Write(buffer, 0, bytesRead);
                    bytesRead = gzStream.Read(buffer, 0, chunkSize);
                } while (bytesRead  > 0);
                resultBuffer = result.ToArray();
                result.Dispose();
            }
            gzStream.Dispose();
            memoryStream.Dispose();
            return resultBuffer;
        }

        public static byte[] CompressByteStream(this byte[] decompressedData)
        {
            MemoryStream memoryStream = new MemoryStream();
            GZipStream gzStream = new GZipStream(memoryStream, CompressionMode.Compress, true);
            gzStream.Write(decompressedData, 0, decompressedData.Length);
            gzStream.Flush();
            gzStream.Dispose();
            memoryStream.Seek(0, SeekOrigin.Begin);
            BinaryReader memoryReader = new BinaryReader(memoryStream);
            byte[] result = memoryReader.ReadBytes((int) memoryStream.Length);
            memoryReader.Dispose();
            memoryStream.Dispose();
            return result;
        }

        public unsafe static decimal[] InitializeCompressedDecimalArray(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(decimal);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDecimalArray(fullByteSize, compressedData);
        }
        public unsafe static decimal[,] InitializeCompressedDecimalArray(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(decimal);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDecimalArray(length1, length2, compressedData);
        }
        public unsafe static decimal[, ,] InitializeCompressedDecimalArray(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(decimal);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDecimalArray(length1, length2, length3, compressedData);
        }
        public unsafe static decimal[, , ,] InitializeCompressedDecimalArray(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(decimal);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDecimalArray(length1, length2, length3, length4, compressedData);
        }
        public unsafe static decimal[, , , ,] InitializeCompressedDecimalArray(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(decimal);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDecimalArray(length1, length2, length3, length4, length5, compressedData);
        }
        public unsafe static decimal[] ConvertByteArrayToDecimalArray(int length, byte[] compressedData)
        {
            decimal[] result = new decimal[length];
            fixed (decimal* resultPtr = result)
                CopyData(length * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        public unsafe static decimal[,] ConvertByteArrayToDecimalArray(int length1, int length2, byte[] compressedData)
        {
            decimal[,] result = new decimal[length1, length2];
            fixed (decimal* resultPtr = result)
                CopyData(length1 * length2 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        public unsafe static decimal[, ,] ConvertByteArrayToDecimalArray(int length1, int length2, int length3, byte[] compressedData)
        {
            decimal[, ,] result = new decimal[length1, length2, length3];
            fixed (decimal* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        public unsafe static decimal[, , ,] ConvertByteArrayToDecimalArray(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            decimal[, , ,] result = new decimal[length1, length2, length3, length4];
            fixed (decimal* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }
        public unsafe static decimal[, , , ,] ConvertByteArrayToDecimalArray(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            decimal[, , , ,] result = new decimal[length1, length2, length3, length4, length5];
            fixed (decimal* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(decimal), resultPtr, compressedData);
            return result;
        }

        public unsafe static sbyte[] InitializeCompressedSByteArray(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(sbyte);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSByteArray(fullByteSize, compressedData);
        }
        public unsafe static sbyte[,] InitializeCompressedSByteArray(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2;
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSByteArray(length1, length2, compressedData);
        }
        public unsafe static sbyte[, ,] InitializeCompressedSByteArray(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3;
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSByteArray(length1, length2, length3, compressedData);
        }
        public unsafe static sbyte[, , ,] InitializeCompressedSByteArray(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4;
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSByteArray(length1, length2, length3, length4, compressedData);
        }
        public unsafe static sbyte[, , , ,] InitializeCompressedSByteArray(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5;
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSByteArray(length1, length2, length3, length4, length5, compressedData);
        }


        public unsafe static short[] InitializeCompressedInt16Array(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(short);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt16Array(fullByteSize, compressedData);
        }
        public unsafe static short[,] InitializeCompressedInt16Array(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(short);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt16Array(length1, length2, compressedData);
        }
        public unsafe static short[, ,] InitializeCompressedInt16Array(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(short);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt16Array(length1, length2, length3, compressedData);
        }
        public unsafe static short[, , ,] InitializeCompressedInt16Array(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(short);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt16Array(length1, length2, length3, length4, compressedData);
        }
        public unsafe static short[, , , ,] InitializeCompressedInt16Array(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(short);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt16Array(length1, length2, length3, length4, length5, compressedData);
        }

        public unsafe static ushort[] InitializeCompressedUInt16Array(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(ushort);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt16Array(fullByteSize, compressedData);
        }
        public unsafe static ushort[,] InitializeCompressedUInt16Array(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(ushort);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt16Array(length1, length2, compressedData);
        }
        public unsafe static ushort[, ,] InitializeCompressedUInt16Array(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(ushort);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt16Array(length1, length2, length3, compressedData);
        }
        public unsafe static ushort[, , ,] InitializeCompressedUInt16Array(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(ushort);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt16Array(length1, length2, length3, length4, compressedData);
        }
        public unsafe static ushort[, , , ,] InitializeCompressedUInt16Array(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(ushort);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt16Array(length1, length2, length3, length4, length5, compressedData);
        }

        public unsafe static int[] InitializeCompressedInt32Array(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(int);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt32Array(fullByteSize, compressedData);
        }
        public unsafe static int[,] InitializeCompressedInt32Array(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(int);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt32Array(length1, length2, compressedData);
        }
        public unsafe static int[, ,] InitializeCompressedInt32Array(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(int);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt32Array(length1, length2, length3, compressedData);
        }
        public unsafe static int[, , ,] InitializeCompressedInt32Array(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(int);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt32Array(length1, length2, length3, length4, compressedData);
        }
        public unsafe static int[, , , ,] InitializeCompressedInt32Array(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(int);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt32Array(length1, length2, length3, length4, length5, compressedData);
        }

        public unsafe static uint[] InitializeCompressedUInt32Array(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(uint);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt32Array(fullByteSize, compressedData);
        }
        public unsafe static uint[,] InitializeCompressedUInt32Array(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(uint);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt32Array(length1, length2, compressedData);
        }
        public unsafe static uint[, ,] InitializeCompressedUInt32Array(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(uint);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt32Array(length1, length2, length3, compressedData);
        }
        public unsafe static uint[, , ,] InitializeCompressedUInt32Array(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(uint);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt32Array(length1, length2, length3, length4, compressedData);
        }
        public unsafe static uint[, , , ,] InitializeCompressedUInt32Array(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(uint);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt32Array(length1, length2, length3, length4, length5, compressedData);
        }
        public unsafe static long[] InitializeCompressedInt64Array(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(long);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt64Array(fullByteSize, compressedData);
        }
        public unsafe static long[,] InitializeCompressedInt64Array(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(long);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt64Array(length1, length2, compressedData);
        }
        public unsafe static long[, ,] InitializeCompressedInt64Array(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(long);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt64Array(length1, length2, length3, compressedData);
        }
        public unsafe static long[, , ,] InitializeCompressedInt64Array(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(long);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt64Array(length1, length2, length3, length4, compressedData);
        }
        public unsafe static long[, , , ,] InitializeCompressedInt64Array(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(long);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToInt64Array(length1, length2, length3, length4, length5, compressedData);
        }

        public unsafe static ulong[] InitializeCompressedUInt64Array(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(ulong);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt64Array(fullByteSize, compressedData);
        }
        public unsafe static ulong[,] InitializeCompressedUInt64Array(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(ulong);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt64Array(length1, length2, compressedData);
        }
        public unsafe static ulong[, ,] InitializeCompressedUInt64Array(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(ulong);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt64Array(length1, length2, length3, compressedData);
        }
        public unsafe static ulong[, , ,] InitializeCompressedUInt64Array(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(ulong);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt64Array(length1, length2, length3, length4, compressedData);
        }
        public unsafe static ulong[, , , ,] InitializeCompressedUInt64Array(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(ulong);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToUInt64Array(length1, length2, length3, length4, length5, compressedData);
        }

        public unsafe static float[] InitializeCompressedSingleArray(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(float);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSingleArray(fullByteSize, compressedData);
        }
        public unsafe static float[,] InitializeCompressedSingleArray(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(float);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSingleArray(length1, length2, compressedData);
        }
        public unsafe static float[, ,] InitializeCompressedSingleArray(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(float);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSingleArray(length1, length2, length3, compressedData);
        }
        public unsafe static float[, , ,] InitializeCompressedSingleArray(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(float);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSingleArray(length1, length2, length3, length4, compressedData);
        }
        public unsafe static float[, , , ,] InitializeCompressedSingleArray(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(float);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToSingleArray(length1, length2, length3, length4, length5, compressedData);
        }
        public unsafe static double[] InitializeCompressedDoubleArray(int length, RuntimeFieldHandle handle)
        {
            int fullByteSize = length * sizeof(double);
            byte[] compressedData = new byte[fullByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDoubleArray(fullByteSize, compressedData);
        }
        public unsafe static double[,] InitializeCompressedDoubleArray(int originalByteSize, int length1, int length2, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * sizeof(double);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDoubleArray(length1, length2, compressedData);
        }
        public unsafe static double[, ,] InitializeCompressedDoubleArray(int originalByteSize, int length1, int length2, int length3, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * sizeof(double);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDoubleArray(length1, length2, length3, compressedData);
        }
        public unsafe static double[, , ,] InitializeCompressedDoubleArray(int originalByteSize, int length1, int length2, int length3, int length4, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * sizeof(double);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDoubleArray(length1, length2, length3, length4, compressedData);
        }
        public unsafe static double[, , , ,] InitializeCompressedDoubleArray(int originalByteSize, int length1, int length2, int length3, int length4, int length5, RuntimeFieldHandle handle)
        {
            int fullByteSize = length1 * length2 * length3 * length4 * length5 * sizeof(double);
            byte[] compressedData = new byte[originalByteSize];
            RuntimeHelpers.InitializeArray(compressedData, handle);
            return ConvertByteArrayToDoubleArray(length1, length2, length3, length4, length5, compressedData);
        }
        public unsafe static double[] ConvertByteArrayToDoubleArray(int length, byte[] compressedData)
        {
            double[] result = new double[length];
            fixed (double* resultPtr = result)
                CopyData(length * sizeof(double), resultPtr, compressedData);
            return result;
        }
        public unsafe static double[,] ConvertByteArrayToDoubleArray(int length1, int length2, byte[] compressedData)
        {
            double[,] result = new double[length1, length2];
            fixed (double* resultPtr = result)
                CopyData(length1 * length2 * sizeof(double), resultPtr, compressedData);
            return result;
        }
        public unsafe static double[, ,] ConvertByteArrayToDoubleArray(int length1, int length2, int length3, byte[] compressedData)
        {
            double[, ,] result = new double[length1, length2, length3];
            fixed (double* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(double), resultPtr, compressedData);
            return result;
        }
        public unsafe static double[, , ,] ConvertByteArrayToDoubleArray(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            double[, , ,] result = new double[length1, length2, length3, length4];
            fixed (double* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(double), resultPtr, compressedData);
            return result;
        }
        public unsafe static double[, , , ,] ConvertByteArrayToDoubleArray(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            double[, , , ,] result = new double[length1, length2, length3, length4, length5];
            fixed (double* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(double), resultPtr, compressedData);
            return result;
        }

        public unsafe static float[] ConvertByteArrayToSingleArray(int length, byte[] compressedData)
        {
            float[] result = new float[length];
            fixed (float* resultPtr = result)
                CopyData(length * sizeof(float), resultPtr, compressedData);
            return result;
        }
        public unsafe static float[,] ConvertByteArrayToSingleArray(int length1, int length2, byte[] compressedData)
        {
            float[,] result = new float[length1, length2];
            fixed (float* resultPtr = result)
                CopyData(length1 * length2 * sizeof(float), resultPtr, compressedData);
            return result;
        }
        public unsafe static float[, ,] ConvertByteArrayToSingleArray(int length1, int length2, int length3, byte[] compressedData)
        {
            float[, ,] result = new float[length1, length2, length3];
            fixed (float* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(float), resultPtr, compressedData);
            return result;
        }
        public unsafe static float[, , ,] ConvertByteArrayToSingleArray(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            float[, , ,] result = new float[length1, length2, length3, length4];
            fixed (float* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(float), resultPtr, compressedData);
            return result;
        }
        public unsafe static float[, , , ,] ConvertByteArrayToSingleArray(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            float[, , , ,] result = new float[length1, length2, length3, length4, length5];
            fixed (float* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(float), resultPtr, compressedData);
            return result;
        }


        public unsafe static ulong[] ConvertByteArrayToUInt64Array(int length, byte[] compressedData)
        {
            ulong[] result = new ulong[length];
            fixed (ulong* resultPtr = result)
                CopyData(length * sizeof(ulong), resultPtr, compressedData);
            return result;
        }
        public unsafe static ulong[,] ConvertByteArrayToUInt64Array(int length1, int length2, byte[] compressedData)
        {
            ulong[,] result = new ulong[length1, length2];
            fixed (ulong* resultPtr = result)
                CopyData(length1 * length2 * sizeof(ulong), resultPtr, compressedData);
            return result;
        }
        public unsafe static ulong[, ,] ConvertByteArrayToUInt64Array(int length1, int length2, int length3, byte[] compressedData)
        {
            ulong[, ,] result = new ulong[length1, length2, length3];
            fixed (ulong* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(ulong), resultPtr, compressedData);
            return result;
        }
        public unsafe static ulong[, , ,] ConvertByteArrayToUInt64Array(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            ulong[, , ,] result = new ulong[length1, length2, length3, length4];
            fixed (ulong* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(ulong), resultPtr, compressedData);
            return result;
        }
        public unsafe static ulong[, , , ,] ConvertByteArrayToUInt64Array(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            ulong[, , , ,] result = new ulong[length1, length2, length3, length4, length5];
            fixed (ulong* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(ulong), resultPtr, compressedData);
            return result;
        }


        public unsafe static long[] ConvertByteArrayToInt64Array(int length, byte[] compressedData)
        {
            long[] result = new long[length];
            fixed (long* resultPtr = result)
                CopyData(length * sizeof(long), resultPtr, compressedData);
            return result;
        }
        public unsafe static long[,] ConvertByteArrayToInt64Array(int length1, int length2, byte[] compressedData)
        {
            long[,] result = new long[length1, length2];
            fixed (long* resultPtr = result)
                CopyData(length1 * length2 * sizeof(long), resultPtr, compressedData);
            return result;
        }
        public unsafe static long[, ,] ConvertByteArrayToInt64Array(int length1, int length2, int length3, byte[] compressedData)
        {
            long[, ,] result = new long[length1, length2, length3];
            fixed (long* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(long), resultPtr, compressedData);
            return result;
        }
        public unsafe static long[, , ,] ConvertByteArrayToInt64Array(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            long[, , ,] result = new long[length1, length2, length3, length4];
            fixed (long* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(long), resultPtr, compressedData);
            return result;
        }
        public unsafe static long[, , , ,] ConvertByteArrayToInt64Array(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            long[, , , ,] result = new long[length1, length2, length3, length4, length5];
            fixed (long* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(long), resultPtr, compressedData);
            return result;
        }


        public unsafe static sbyte[] ConvertByteArrayToSByteArray(int length, byte[] compressedData)
        {
            sbyte[] result = new sbyte[length];
            fixed (sbyte* resultPtr = result)
                CopyData(length, resultPtr, compressedData);
            return result;
        }
        public unsafe static sbyte[,] ConvertByteArrayToSByteArray(int length1, int length2, byte[] compressedData)
        {
            sbyte[,] result = new sbyte[length1, length2];
            fixed (sbyte* resultPtr = result)
                CopyData(length1 * length2, resultPtr, compressedData);
            return result;
        }
        public unsafe static sbyte[, ,] ConvertByteArrayToSByteArray(int length1, int length2, int length3, byte[] compressedData)
        {
            sbyte[, ,] result = new sbyte[length1, length2, length3];
            fixed (sbyte* resultPtr = result)
                CopyData(length1 * length2 * length3, resultPtr, compressedData);
            return result;
        }
        public unsafe static sbyte[, , ,] ConvertByteArrayToSByteArray(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            sbyte[, , ,] result = new sbyte[length1, length2, length3, length4];
            fixed (sbyte* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4, resultPtr, compressedData);
            return result;
        }
        public unsafe static sbyte[, , , ,] ConvertByteArrayToSByteArray(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            sbyte[, , , ,] result = new sbyte[length1, length2, length3, length4, length5];
            fixed (sbyte* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5, resultPtr, compressedData);
            return result;
        }


        public unsafe static short[] ConvertByteArrayToInt16Array(int length, byte[] compressedData)
        {
            short[] result = new short[length];
            fixed (short* resultPtr = result)
                CopyData(length * sizeof(short), resultPtr, compressedData);
            return result;
        }
        public unsafe static short[,] ConvertByteArrayToInt16Array(int length1, int length2, byte[] compressedData)
        {
            short[,] result = new short[length1, length2];
            fixed (short* resultPtr = result)
                CopyData(length1 * length2 * sizeof(short), resultPtr, compressedData);
            return result;
        }
        public unsafe static short[, ,] ConvertByteArrayToInt16Array(int length1, int length2, int length3, byte[] compressedData)
        {
            short[, ,] result = new short[length1, length2, length3];
            fixed (short* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(short), resultPtr, compressedData);
            return result;
        }
        public unsafe static short[, , ,] ConvertByteArrayToInt16Array(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            short[, , ,] result = new short[length1, length2, length3, length4];
            fixed (short* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(short), resultPtr, compressedData);
            return result;
        }
        public unsafe static short[, , , ,] ConvertByteArrayToInt16Array(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            short[, , , ,] result = new short[length1, length2, length3, length4, length5];
            fixed (short* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(short), resultPtr, compressedData);
            return result;
        }


        public unsafe static ushort[] ConvertByteArrayToUInt16Array(int length, byte[] compressedData)
        {
            ushort[] result = new ushort[length];
            fixed (ushort* resultPtr = result)
                CopyData(length * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        public unsafe static ushort[,] ConvertByteArrayToUInt16Array(int length1, int length2, byte[] compressedData)
        {
            ushort[,] result = new ushort[length1, length2];
            fixed (ushort* resultPtr = result)
                CopyData(length1 * length2 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        public unsafe static ushort[, ,] ConvertByteArrayToUInt16Array(int length1, int length2, int length3, byte[] compressedData)
        {
            ushort[, ,] result = new ushort[length1, length2, length3];
            fixed (ushort* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        public unsafe static ushort[, , ,] ConvertByteArrayToUInt16Array(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            ushort[, , ,] result = new ushort[length1, length2, length3, length4];
            fixed (ushort* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }
        public unsafe static ushort[, , , ,] ConvertByteArrayToUInt16Array(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            ushort[, , , ,] result = new ushort[length1, length2, length3, length4, length5];
            fixed (ushort* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(ushort), resultPtr, compressedData);
            return result;
        }

        public unsafe static int[] ConvertByteArrayToInt32Array(int length, byte[] compressedData)
        {
            int[] result = new int[length];
            fixed (int* resultPtr = result)
                CopyData(length * sizeof(int), resultPtr, compressedData);
            return result;
        }
        public unsafe static int[,] ConvertByteArrayToInt32Array(int length1, int length2, byte[] compressedData)
        {
            int[,] result = new int[length1, length2];
            fixed (int* resultPtr = result)
                CopyData(length1 * length2 * sizeof(int), resultPtr, compressedData);
            return result;
        }
        public unsafe static int[, ,] ConvertByteArrayToInt32Array(int length1, int length2, int length3, byte[] compressedData)
        {
            int[, ,] result = new int[length1, length2, length3];
            fixed (int* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(int), resultPtr, compressedData);
            return result;
        }
        public unsafe static int[, , ,] ConvertByteArrayToInt32Array(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            int[, , ,] result = new int[length1, length2, length3, length4];
            fixed (int* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(int), resultPtr, compressedData);
            return result;
        }
        public unsafe static int[, , , ,] ConvertByteArrayToInt32Array(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            int[, , , ,] result = new int[length1, length2, length3, length4, length5];
            fixed (int* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(int), resultPtr, compressedData);
            return result;
        }

        public unsafe static uint[] ConvertByteArrayToUInt32Array(int length, byte[] compressedData)
        {
            uint[] result = new uint[length];
            fixed (uint* resultPtr = result)
                CopyData(length * sizeof(uint), resultPtr, compressedData);
            return result;
        }
        public unsafe static uint[,] ConvertByteArrayToUInt32Array(int length1, int length2, byte[] compressedData)
        {
            uint[,] result = new uint[length1, length2];
            fixed (uint* resultPtr = result)
                CopyData(length1 * length2 * sizeof(uint), resultPtr, compressedData);
            return result;
        }
        public unsafe static uint[, ,] ConvertByteArrayToUInt32Array(int length1, int length2, int length3, byte[] compressedData)
        {
            uint[, ,] result = new uint[length1, length2, length3];
            fixed (uint* resultPtr = result)
                CopyData(length1 * length2 * length3 * sizeof(uint), resultPtr, compressedData);
            return result;
        }
        public unsafe static uint[, , ,] ConvertByteArrayToUInt32Array(int length1, int length2, int length3, int length4, byte[] compressedData)
        {
            uint[, , ,] result = new uint[length1, length2, length3, length4];
            fixed (uint* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * sizeof(uint), resultPtr, compressedData);
            return result;
        }
        public unsafe static uint[, , , ,] ConvertByteArrayToUInt32Array(int length1, int length2, int length3, int length4, int length5, byte[] compressedData)
        {
            const int test = sizeof(int);
            uint[, , , ,] result = new uint[length1, length2, length3, length4, length5];
            fixed (uint* resultPtr = result)
                CopyData(length1 * length2 * length3 * length4 * length5 * sizeof(uint), resultPtr, compressedData);
            return result;
        }

        public static unsafe void CopyData(int fullByteSize, byte* dataPointer, byte[] compressedData)
        {
            var decompressedData = DecompressByteStream(compressedData, fullByteSize);
            fixed (byte* sourceFPtr = decompressedData)
            {
                byte* sourceDPtr = (byte*) sourceFPtr;
                byte* resultDPtr = dataPointer;
                for (int i = 0; i < fullByteSize; i++)
                    resultDPtr[i] = sourceDPtr[i];
            }
        }

        public static unsafe void CopyData(int fullByteSize, sbyte* dataPointer, byte[] compressedData)
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

        public static unsafe void CopyData(int fullByteSize, decimal* dataPointer, byte[] compressedData)
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

        public static unsafe void CopyData(int fullByteSize, short* dataPointer, byte[] compressedData)
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

        public static unsafe void CopyData(int fullByteSize, ushort* dataPointer, byte[] compressedData)
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

        public static unsafe void CopyData(int fullByteSize, int* dataPointer, byte[] compressedData)
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

        public static unsafe void CopyData(int fullByteSize, uint* dataPointer, byte[] compressedData)
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
        public static unsafe void CopyData(int fullByteSize, long* dataPointer, byte[] compressedData)
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
        public static unsafe void CopyData(int fullByteSize, ulong* dataPointer, byte[] compressedData)
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
        public static unsafe void CopyData(int fullByteSize, float* dataPointer, byte[] compressedData)
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
        public static unsafe void CopyData(int fullByteSize, double* dataPointer, byte[] compressedData)
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
