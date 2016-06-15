using AllenCopeland.Abstraction.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Platforms.WindowsNT
{
    public class CoffRelocSection :
        CoffSection,
        IEnumerable<CoffRelocBlock>
    {
        private CoffRelocBlock[] relocBlocks;
        public CoffRelocSection()
        {
        }
        public override bool HasCustomRead { get { return true; } }
        public override void CustomRead(EndianAwareBinaryReader reader)
        {
            var offset = 0;
            var relocBlocks = new List<CoffRelocBlock>();
            while (offset < reader.BaseStream.Length)
            {
                var newSection = new CoffRelocBlock();
                newSection.Read(reader);
                relocBlocks.Add(newSection);
                offset += newSection.Length;
            }
            this.relocBlocks = relocBlocks.ToArray();
        }


        public IEnumerator<CoffRelocBlock> GetEnumerator()
        {
            foreach (var block in this.relocBlocks)
                yield return block;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}
