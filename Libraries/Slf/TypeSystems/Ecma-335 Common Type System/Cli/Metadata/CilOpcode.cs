using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata
{
    public struct CilOpcode
    {
        private readonly ushort opcodeValue;
        public static implicit operator ushort(CilOpcode opcode)
        {
            return opcode.opcodeValue;
        }

        public static unsafe implicit operator CilOpcode(ushort value)
        {
            return *(CilOpcode*) value;
        }
    }
}
