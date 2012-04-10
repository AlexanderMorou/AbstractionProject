using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public class CliMetadataUserStringsHeaderAndHeap :
        CliMetadataBlobTypeHeaderAndHeap<string>
    {

        public CliMetadataUserStringsHeaderAndHeap(CliMetadataStreamHeader header)
            : base(header)
        {
        }

        protected override bool ItemsEqual(string item1, string item2)
        {
            return item1 == item2;
        }

        protected unsafe override string GetData(byte[] data)
        {
            if (data.Length == 0)
                return string.Empty;
            if ((data.Length & 1) != 1) // (blobCacheData.Length % 2) != 1
                throw new BadImageFormatException("The userstring count was not odd.");
            char[] result = new char[data.Length >> 1];
            fixed (byte* dataPointer = data)
            {
                fixed (char* resultPtr = result)
                {
                    char* dataCharPtr = (char*) dataPointer;
                    char* realResultPtr = resultPtr;
                    int realLen = data.Length >> 1;
                    for (int i = 0; i < realLen; i++, dataCharPtr++, realResultPtr++)
                        *realResultPtr = *dataCharPtr;
                }
            }
            return new string(result);
        }
        public override IEnumerator<Tuple<int, string>> GetEnumerator()
        {
            return (from _ in base._GetEnumerable()
                    orderby _.Item2,
                            _.Item1
                    select _).GetEnumerator();
        }

        protected override int GetHashCode(string value)
        {
            return value.GetHashCode();
        }

        protected override uint GetSizeOf(string value)
        {
            return (uint) ((value.Length << 1) + 1);
        }

    }
}
